import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Result } from 'src/app/models/Result';
import { User } from 'src/app/models/User';
import { SignInData } from 'src/app/common/data';
import { APIService } from '../api.service';
import { MD5, ToParam } from 'src/app/common/hash';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HOST } from 'src/app/common/Constant';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private api: APIService,private http:HttpClient) { }
  GetUser(): Observable<Result<User>>{
    return of(SignInData);
  }
  SignIn(username: string,password:string) : Observable<Result<User>>{
    // return this.http.post<Result<User>>(HOST + '/Auth/SignIn',ToParam({username,password: MD5(password)}),
    // {
    //   headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    // });
    return this.api.Post('/Auth/SignIn',{username,password: MD5(password)});
  }
}
