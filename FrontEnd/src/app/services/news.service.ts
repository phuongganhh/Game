import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { News } from '../models/News';
import { NewsData, NewsDetail } from '../common/data';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

  constructor() { }
  GetNews(group_id: string) : Observable<News[]> {
    return of(NewsData);
  }
  GetDetail(id: string) : Observable<News>{
    return of(NewsDetail);
  }
}
