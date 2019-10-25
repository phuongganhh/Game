import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Application } from 'src/app/models/Application';
import { HttpClient } from '@angular/common/http';
import { HOST } from 'src/app/common/Constant';
import { APIService } from '../api.service';
import { Result } from 'src/app/models/Result';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  constructor(private api: APIService) { }

  GetMenu() : Observable<Result<Application[]>>{
    return this.api.Get<Result<Application[]>>("/Application/GetMenu");
  }
}
