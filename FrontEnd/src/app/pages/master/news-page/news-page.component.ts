import { Component, OnInit } from '@angular/core';
import { News } from 'src/app/models/News';
import { NewsService } from 'src/app/services/news.service';
import { Authen } from 'src/app/common/common';
import { Paging } from 'src/app/models/Result';

@Component({
  selector: 'app-news-page',
  templateUrl: './news-page.component.html',
  styleUrls: ['./news-page.component.scss']
})
export class NewsPageComponent implements OnInit {

  news: News[];
  paging: Paging;
  constructor(private service: NewsService) { }

  ngOnInit() {
    this.service.GetNews(1).subscribe(n => {
      if(n.code === 200){
        this.news = n.data;
        this.paging = n.paging;
      }
      else{
        Authen(n);
      }
    });
  }

}
