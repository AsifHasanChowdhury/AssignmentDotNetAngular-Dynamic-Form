import { Component, OnInit } from '@angular/core';
import {DomSanitizer, SafeHtml} from "@angular/platform-browser";
import {HttpClient} from "@angular/common/http";
import {map} from "rxjs/operators";
import {NgForm} from "@angular/forms";
import { Router } from '@angular/router';

@Component({
  selector: 'app-water-supply',
  templateUrl: './water-supply.component.html',
  styleUrls: ['./water-supply.component.css']
})
//templateUrl: './water-supply.component.html',

export class WaterSupplyComponent implements OnInit {

  PersonList:any = [];
  alphas:any;
  safeWaterForm: SafeHtml | undefined;
  person={};

  constructor(private http: HttpClient,private sanitizer:DomSanitizer, private router: Router) {}

  ngOnInit() {
    this.fetchWaterForm();
  }

  private fetchWaterForm(){

    this.http
      .post('https://localhost:44379/Form/SendFormModule',{"id":"1"})
      .pipe(
        map(responseData => {

          this.alphas=responseData as string
          this.safeWaterForm=this.sanitizer.bypassSecurityTrustHtml(this.alphas);
          //console.log(this.alphas)

        })
      )
      .subscribe(posts => {
        // ...
        console.log("Hello");
      });
  }


  SendingApIResponse(){
  //console.log(form['value']);

    this.http
      .post(
        'https://localhost:44379/Form/GetFormResponse',
        this.person)
      .subscribe(responseData => {
       // console.log(responseData);
      });
   // console.log(form['value']);
  }


  cookBacon(form:NgForm){

  for(var i=0;i<document
                .getElementById('data')
                .querySelectorAll('input')
                .length-1; i++){

      var keyID=document
                .getElementById('data')
                .querySelectorAll('input')
                .item(i).id;

      var value=document
                .getElementById('data')
                .querySelectorAll('input')
                .item(i).value;



      this.person[keyID]=value;
      this.person["formType"]="waterTable";
      this.person['Decison']="pending"
      this.PersonList.push(this.person);

  }
      this.SendingApIResponse();
      this.router.navigate(['/']);
      //console.log(this.PersonList);
  }

}
