import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MasterLayoutComponent } from './layouts/master-layout/master-layout.component';
import { MenuComponent } from './components/master/menu/menu.component';
import { HeaderLeftComponent } from './components/master/header-left/header-left.component';
import { HeaderRightComponent } from './components/master/header-right/header-right.component';
import { BoxRightComponent } from './components/master/box-right/box-right.component';
import { FooterComponent } from './components/master/footer/footer.component';
import { NewsPageComponent } from './pages/master/news-page/news-page.component';
import { NewsComponent } from './components/master/news/news.component';
import { MasterPageComponent } from './pages/master/master-page/master-page.component';
import { NewsDetailComponent } from './pages/master/news-detail/news-detail.component';
import { FormsModule } from '@angular/forms';
import { StoreModule } from '@ngrx/store';
import { reducers, metaReducers } from './reducers';
import { HttpClientModule } from '@angular/common/http';
import { ValidatePageComponent } from './pages/master/validate-page/validate-page.component';
import { UserPageComponent } from './pages/master/user-page/user-page.component';
@NgModule({
  declarations: [
    AppComponent,
    MasterLayoutComponent,
    MenuComponent,
    HeaderLeftComponent,
    HeaderRightComponent,
    BoxRightComponent,
    FooterComponent,
    NewsComponent,
    NewsPageComponent,
    MasterPageComponent,
    NewsDetailComponent,
    ValidatePageComponent,
    UserPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    StoreModule.forRoot(reducers, {
      metaReducers,
      runtimeChecks: {
        strictStateImmutability: true,
        strictActionImmutability: true
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
