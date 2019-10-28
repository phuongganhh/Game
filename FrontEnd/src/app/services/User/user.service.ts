import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Result, IResult } from 'src/app/models/Result';
import { User } from 'src/app/models/User';
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
    return this.api.Get('/user/get');
  }
  SignIn(username: string,password:string) : Observable<Result<User>>{
    return this.api.Post('/Auth/SignIn',{username,password: MD5(password)});
  }

  SignUp(data: any) : Observable<IResult>{
    return this.api.Post('/Auth/SignUp',data);
  }

  Validate(token: string) : Observable<IResult>{
    return this.api.Post('/Auth/Validate',{token});
  }
}
