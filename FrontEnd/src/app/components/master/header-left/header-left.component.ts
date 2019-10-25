import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/User';
import { UserService } from 'src/app/services/User/user.service';
import { _user } from 'src/app/common/common';

@Component({
  selector: 'app-header-left',
  templateUrl: './header-left.component.html',
  styleUrls: ['./header-left.component.scss']
})
export class HeaderLeftComponent implements OnInit {

  type: number = 1; //1 signin
  user: User = _user;
  username: string;
  password: string;
  email: string;
  constructor(private _service: UserService) { }

  ngOnInit() {
    if(this.user == null){
      this.type = 1;
      this._service.GetUser().subscribe(x=> {
        if(x.code === 200){
          this.user = x.data;
          this.type = -1;
        }
      });
    }
    else{
      this.type = -1;
    }
  }

  SignOut(){
    this.user = null;
    this.type = 1;
    this.username = '';
    this.password = '';
    //todo: clear cookie
  }
  SignIn(){
    this._service.SignIn(this.username,this.password).subscribe(x=>{
      if(x.code === 200){
        this.type = -1;
        this.user = x.data;
        localStorage.setItem('token',x.data.token);
      }
      else{
        alert(x.message);
      }
    });
  }

  SignUp(){
    console.log(this);
  }

  Forget(){
    console.log(this);
  }
}
