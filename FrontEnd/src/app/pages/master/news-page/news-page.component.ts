import { Component, OnInit } from '@angular/core';
import { News } from 'src/app/models/News';
import { NewsService } from 'src/app/services/news.service';

@Component({
  selector: 'app-news-page',
  templateUrl: './news-page.component.html',
  styleUrls: ['./news-page.component.scss']
})
export class NewsPageComponent implements OnInit {

  news: News[];
  constructor(private service: NewsService) { }

  ngOnInit() {
    this.service.GetNews('1').subscribe(n => this.news = n);
  }

}
