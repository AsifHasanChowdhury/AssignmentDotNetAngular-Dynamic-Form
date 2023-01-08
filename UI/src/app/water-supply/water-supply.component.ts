import { Component, OnInit } from '@angular/core';
import {DomSanitizer, SafeHtml} from "@angular/platform-browser";
import {HttpClient} from "@angular/common/http";
import {map} from "rxjs/operators";
import {NgForm} from "@angular/forms";
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';

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
  private keyID:string;
  private value:string;
  private inputItem:any;
  private inputLabel:any;
  private formValidation:boolean;

  constructor(private http: HttpClient,
              private sanitizer:DomSanitizer,
              private router: Router,
              private toast:NgToastService) {}

  ngOnInit() {
    this.fetchWaterForm();
  }

  openSuccess(){
    this
      .toast
      .success
      ({detail:"Submitted",summary:"Your Form has been Submitted", position:"tr", duration:5000})
    }

    pendingSuccess(){
      this
        .toast
        .error
        ({detail:"Warning",summary:"Your Form Response is Invalid", position:"tr", duration:5000})
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
        //console.log("Hello");
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


  for(let i=0;i<document
                .getElementById('data')
                .querySelectorAll('input')
                .length; i++){

      this.formValidation=true;

      this.keyID=document
                .getElementById('data')
                .querySelectorAll('input')
                .item(i).id;

      this.value=document
                .getElementById('data')
                .querySelectorAll('input')
                .item(i).value;

      this.inputItem=document
        .getElementById('data')
        .querySelectorAll('input')
        .item(i);

      this.inputLabel= document
        .getElementById('data')
        .querySelectorAll('label')
        .item(i)

      if (this.value.length==0){

        this.formValidation=false;

        this.inputItem.placeholder=this.inputItem.validationMessage;

        this.inputItem.style.borderColor="red";

       // this.inputLabel.innerText=this.inputLabel.innerText.
      }

      if(this.formValidation==true){

        this.person[this.keyID]=this.value;
        this.person["formType"]="waterTable";
        this.person['Decison']="Pending"
        this.PersonList.push(this.person);

      }


  }
      if(this.value.length>0) {

        this.SendingApIResponse();
        this.router.navigate(['/']);
        this.openSuccess();
      }
      else{
        this.pendingSuccess();
      }
      //console.log(this.PersonList);
  }

}
