import { Component, OnInit } from '@angular/core';
import { ApplicationService } from 'src/app/services/Application/application.service';
import { Application } from 'src/app/models/Application';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  menuLeft: Application[];
  menuRight: Application[];
  constructor(private _service: ApplicationService) { }

  ngOnInit() {
    this._service.GetMenu().subscribe(x=>{
      if(x.code == 200){
          this.menuLeft = [];
          this.menuRight = [];
        for(var i = 0; i< x.data.length;i++){
          if(i % 2 == 0){
            this.menuLeft.push(x.data[i]);
          }
          else{
            this.menuRight.push(x.data[i]);
          }
        }
        console.log(this);
      }
      else{
        alert(x.message);
      }
    })
  }

}
