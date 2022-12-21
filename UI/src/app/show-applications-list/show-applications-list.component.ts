import { Component, OnInit } from '@angular/core';
import {DomSanitizer, SafeHtml} from "@angular/platform-browser";
import {HttpClient} from "@angular/common/http";
import {map} from "rxjs/operators";

@Component({
  selector: 'app-show-applications-list',
  templateUrl: './show-applications-list.component.html',
  styleUrls: ['./show-applications-list.component.css']
})
export class ShowApplicationsListComponent implements OnInit {

  object: {[key: string]: string} = {"UserName": 'foo', "Email": 'bar'};

  alphas:any;
  safeWaterForm: SafeHtml | undefined;
  person={};

  constructor(private http: HttpClient,private sanitizer:DomSanitizer) {}

  ngOnInit() {
    this.fetchWaterForm();
  }

  private fetchWaterForm(){

    this.http
      .post('https://localhost:44379/Form/ShowAllRequests',{"id":"1"})
      .pipe(
        map(responseData => {

          this.alphas=responseData as string
          //this.safeWaterForm=this.sanitizer.bypassSecurityTrustHtml(this.alphas);
          console.log(this.alphas)

        })
      )
      .subscribe(posts => {
        // ...
        console.log("Hello");
      });
  }

}
