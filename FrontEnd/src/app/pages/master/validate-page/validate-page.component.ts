import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/services/User/user.service';
import { Noti } from 'src/app/common/common';

@Component({
  selector: 'app-validate-page',
  templateUrl: './validate-page.component.html',
  styleUrls: ['./validate-page.component.scss']
})
export class ValidatePageComponent implements OnInit {

  constructor(private route: ActivatedRoute,private router: Router,private _service: UserService) { }

  ngOnInit() {
    this.route.params.subscribe(x=>{
      this._service.Validate(x['token']).subscribe(result => {
        if(result.code === 200){
          Noti(result.message);
        }
        this.router.navigate(['/']);
      });
    })
  }

}
