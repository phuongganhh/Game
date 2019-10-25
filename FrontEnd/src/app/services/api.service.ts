import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HOST } from '../common/Constant';
import { ToParam } from '../common/hash';

@Injectable({
  providedIn: 'root'
})
export class APIService {
  constructor(private http: HttpClient) {}

  Get<T>(url:string,data: any = {}) : Observable<T>{
    data['token'] = localStorage.getItem('token');
    return this.http.get<T>(`${HOST}${url}?${ToParam(data)}`);
  }

  Post<T>(url:string,data:any) : Observable<T>{
    var headers = new HttpHeaders({'Content-Type' : 'application/x-www-form-urlencoded'});
    return this.http.post<T>(HOST + url,ToParam(data),{headers : headers});
  }
}
