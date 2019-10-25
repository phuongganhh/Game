import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { News } from '../models/News';
import { NewsData, NewsDetail } from '../common/data';
import { APIService } from './api.service';
import { Result } from '../models/Result';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

  constructor(private api: APIService) { }
  GetNews(group_id: number) : Observable<Result<News[]>> {
    return this.api.Get('/News/GetNews',{news_group_id : group_id});
  }
  GetDetail(id: string) : Observable<News>{
    return of(NewsDetail);
  }
}
