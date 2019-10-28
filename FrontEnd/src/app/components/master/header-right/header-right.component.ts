import { Component, OnInit } from '@angular/core';
import { SystemService } from 'src/app/services/System/system.service';
import { Banner } from 'src/app/models/Banner';
import { Authen } from 'src/app/common/common';

@Component({
  selector: 'app-header-right',
  templateUrl: './header-right.component.html',
  styleUrls: ['./header-right.component.scss']
})
export class HeaderRightComponent implements OnInit {
  banners: Banner[];
  constructor(private _service: SystemService) { }

  ngOnInit() {
    this._service.GetBanner().subscribe(x=>{
      if(x.code === 200){
        this.banners = x.data;
      }
      else{
        Authen(x);
      }
    })
  }

}
