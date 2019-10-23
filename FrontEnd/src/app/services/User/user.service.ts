import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Result } from 'src/app/models/Result';
import { User } from 'src/app/models/User';
import { SignInData } from 'src/app/common/data';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor() { }
  GetUser(): Observable<Result<User>>{
    return of(SignInData);
  }
  SignIn(username: string,password:string) : Observable<Result<User>>{
    return of(SignInData);
  }
}
