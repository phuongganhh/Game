import { Component, OnInit } from '@angular/core';
import { NewsService } from 'src/app/services/news.service';
import { News } from 'src/app/models/News';
import { Router, ActivatedRoute } from '@angular/router';
import { Authen } from 'src/app/common/common';

@Component({
  selector: 'app-news-detail',
  templateUrl: './news-detail.component.html',
  styleUrls: ['./news-detail.component.scss']
})
export class NewsDetailComponent implements OnInit {
  newsDetail: News;
  constructor(private service: NewsService,private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(p=>{
        this.service.GetDetail(p['id']).subscribe(n => {
            if(n.code === 200){
                this.newsDetail = n.data;
            }
            else{
                Authen(n);
            }
        });
    });
  }

}
