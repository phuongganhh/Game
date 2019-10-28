import { Injectable } from '@angular/core';
import { APIService } from '../api.service';
import { Observable } from 'rxjs';
import { Result } from 'src/app/models/Result';
import { Banner } from 'src/app/models/Banner';

@Injectable({
  providedIn: 'root'
})
export class SystemService {

  constructor(private api: APIService) { }

  GetBanner() : Observable<Result<Banner[]>>{
    return this.api.Get('/Banner/Get');
  }
}
