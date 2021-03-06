import { Component, OnDestroy, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(
    public translate: TranslateService
  ) {
    translate.addLangs(['en','de','pt-BR', 'zh']);
    translate.setDefaultLang('en');
  }


  ngOnInit() {

  }
}
