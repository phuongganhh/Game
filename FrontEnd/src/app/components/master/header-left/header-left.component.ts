import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/User';
import { UserService } from 'src/app/services/User/user.service';
import {  Authen, Noti } from 'src/app/common/common';
import { Router } from '@angular/router';
import { MD5 } from 'src/app/common/hash';

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
    this.username = '';
    this.password = '';
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
    if(this.username.length < 4 || this.username.length > 12){
      Noti("Tên tài khoản phải từ 4 đến 12 ký tự!");
      return;
    }
    if(this.password.length < 4 || this.password.length > 12){
      Noti("Mật khẩu quá ngắn");
      return;
    }
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
    if(this.username.length < 4 || this.username.length > 12){
      Noti("Tên tài khoản phải từ 4 đến 12 ký tự!");
      return;
    }
    if(this.password.length < 4 || this.password.length > 12){
      Noti("Mật khẩu quá ngắn");
      return;
    }

    this._service.SignUp({
      username : this.username,
      password : MD5(this.password),
      email : this.email
    }).subscribe(result => {
      Noti(result.message);
      if(result.code === 200){
        this.type = 1;
        this.username = '';
        this.password = '';
      }
    });
  }

  Forget(){
    console.log(this);
  }
}
