import { Component, OnInit } from '@angular/core';
import { News } from 'src/app/models/News';
import { NewsService } from 'src/app/services/news.service';
import { Authen } from 'src/app/common/common';
import { Paging } from 'src/app/models/Result';
import { NewsGroup } from 'src/app/models/NewsGroup';

@Component({
  selector: 'app-news-page',
  templateUrl: './news-page.component.html',
  styleUrls: ['./news-page.component.scss']
})
export class NewsPageComponent implements OnInit {

  news: News[];
  newsGroup: NewsGroup[];
  paging: Paging;
  position = 0;
  constructor(private service: NewsService) { }

  GetNews(id: number) {
    this.service.GetNews(id).subscribe(n => {
      if(n.code === 200){
        this.news = n.data;
        this.paging = n.paging;
      }
      else{
        Authen(n);
      }
    });
  }
  ngOnInit() {
    this.service.GetNewsGroup().subscribe(g=>{
      if(g.code === 200){
        this.newsGroup = g.data;
        console.log(this.newsGroup);
      }
      else{
        Authen(g);
      }
    });
    
  }

  hover(item){
    this.newsGroup.forEach((x,i)=>{
      x.active = x.id == item.id;
      if(x.id == item.id){
        this.position = i;
      }
    });
  }
  newsGroupClick(item: NewsGroup){
    this.newsGroup.forEach(i => {
      i.active = i.id == item.id;
    });
    this.GetNews(item.id);
  }
}
