import { Component, OnInit } from '@angular/core';
import {DomSanitizer, SafeHtml} from "@angular/platform-browser";
import {HttpClient} from "@angular/common/http";
import {map} from "rxjs/operators";
import { NgForm } from '@angular/forms';

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
  index:1;
  UIKeys=[];
  display:boolean=false;
  individualUser:any;

  constructor(private http: HttpClient,private sanitizer:DomSanitizer) {}

  ngOnInit() {
    this.fetchWaterForm();
  }
  toggle(individualUser){
    this.display= !this.display;
    this.individualUser=individualUser;
    console.log(this.individualUser);
  }
  
  private fetchWaterForm(){

    this.http
      .post('https://localhost:44379/Form/ShowAllRequests',{"id":"1"})
      .pipe(
        map(responseData => {

          this.alphas=responseData as string
          //this.safeWaterForm=this.sanitizer.bypassSecurityTrustHtml(this.alphas);
          console.log(this.alphas)
          for(let item of this.alphas){
         
            for(let i=0;i<Object.keys(item).length;i++){
              const key = Object.keys(item)[i]
              //console.log(key)
              this.UIKeys.push(key);
            }
            
            break;
          }
          console.log(this.UIKeys);
          
        })
      )
      .subscribe(posts => {
        // ...
        //console.log("Hello");
      });
  }
  sendToAPI(){

    this.http
      .post(
        'https://localhost:44379/Form/GetFormResponse',
        this.person)
      .subscribe(responseData => {
       // console.log(responseData);
      });
    //console.log(form['value']);
  }
  onUpdate(form:NgForm){
    for(var i=0;i<document
      .getElementById('Parent')
      .querySelectorAll('input')
      .length; i++){

    var keyID=document
      .getElementById('Parent')
      .querySelectorAll('input')
      .item(i).id;

    var value=document
      .getElementById('Parent')
      .querySelectorAll('input')
      .item(i).value;

    

    this.person[keyID]=value;  
}
console.log(this.person)
this.sendToAPI();
    
  }

}
