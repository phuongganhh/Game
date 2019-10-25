import { Component, OnInit, ElementRef } from '@angular/core';

@Component({
  selector: 'app-master-page',
  templateUrl: './master-page.component.html',
  styleUrls: ['./master-page.component.scss']
})
export class MasterPageComponent implements OnInit {
  
  constructor(private elementRef:ElementRef) { }

  ngOnInit() {
    console.log(this.elementRef.nativeElement.offsetHeight);
  }

}
