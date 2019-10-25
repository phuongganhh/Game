import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/User';
import { UserService } from 'src/app/services/User/user.service';
import {  Authen } from 'src/app/common/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header-left',
  templateUrl: './header-left.component.html',
  styleUrls: ['./header-left.component.scss']
})
export class HeaderLeftComponent implements OnInit {

  type: number = 1; //1 signin
  user: User;
  username: string;
  password: string;
  email: string;
  constructor(private _service: UserService,private route: Router) { }
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
    localStorage.removeItem('token');
    this.route.navigate(['/']);
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
        Authen(x);
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
