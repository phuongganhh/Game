import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MasterLayoutComponent } from './layouts/master-layout/master-layout.component';
import { NewsPageComponent } from './pages/master/news-page/news-page.component';
import { NewsComponent } from './components/master/news/news.component';
import { MasterPageComponent } from './pages/master/master-page/master-page.component';
import { NewsDetailComponent } from './pages/master/news-detail/news-detail.component';


const routes: Routes = [
  {
    path: '',
    component: MasterLayoutComponent,
    children:[
      {
        path: '',
        component: MasterPageComponent,
        children:[
          {
            path: '',
            component: NewsPageComponent
          },
          {
            path: 'detail',
            component: NewsDetailComponent
          }
        ]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
