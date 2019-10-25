import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { News } from '../models/News';
import { APIService } from './api.service';
import { Result } from '../models/Result';
import { NewsGroup } from '../models/NewsGroup';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

  constructor(private api: APIService) { }
  GetNews(group_id: number) : Observable<Result<News[]>> {
    return this.api.Get('/News/GetNews',{news_group_id : group_id});
  }

  GetNewsGroup() : Observable<Result<NewsGroup[]>>{
      return this.api.Get('/NewsGroup/Get');
  }

  GetDetail(id: number) : Observable<Result<News>>{
    return this.api.Get('/News/GetDetail',{id});
  }
}
