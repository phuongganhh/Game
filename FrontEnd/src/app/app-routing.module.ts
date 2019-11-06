import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MasterLayoutComponent } from './layouts/master-layout/master-layout.component';
import { NewsPageComponent } from './pages/master/news-page/news-page.component';
import { NewsComponent } from './components/master/news/news.component';
import { MasterPageComponent } from './pages/master/master-page/master-page.component';
import { NewsDetailComponent } from './pages/master/news-detail/news-detail.component';
import { ValidatePageComponent } from './pages/master/validate-page/validate-page.component';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { UserPageComponent } from './pages/master/user-page/user-page.component';


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
            path: 'detail/:title/:id',
            component: NewsDetailComponent
          }
        ]
      },
      {
          path: 'user',
          component: UserPageComponent
      }
    ],
    
  },
  {
    path : 'validate/:token',
    component: ValidatePageComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  providers: [{provide: LocationStrategy, useClass: HashLocationStrategy}],
  exports: [RouterModule]
})
export class AppRoutingModule { }
