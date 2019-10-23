import { Component, OnInit } from '@angular/core';
import { NewsService } from 'src/app/services/news.service';
import { News } from 'src/app/models/News';

@Component({
  selector: 'app-news-detail',
  templateUrl: './news-detail.component.html',
  styleUrls: ['./news-detail.component.scss']
})
export class NewsDetailComponent implements OnInit {
  newsDetail: News;
  constructor(private service: NewsService) { }

  ngOnInit() {
    this.service.GetDetail('1').subscribe(n => this.newsDetail = n);
  }

}
