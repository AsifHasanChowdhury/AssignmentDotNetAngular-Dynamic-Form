import { Component, Input, OnInit } from '@angular/core';
import {DomSanitizer, SafeHtml} from "@angular/platform-browser";
import {HttpClient} from "@angular/common/http";
import {map} from "rxjs/operators";
import { NgForm } from '@angular/forms';
import { TitleStrategy } from '@angular/router';
import { LoginComponent } from 'app/login/login/login.component';

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
  role:string;
  roleBoolean:boolean=false;
  

  constructor(private http: HttpClient,private sanitizer:DomSanitizer) {}

  ngOnInit() {
    this.fetchAllResult();
    this.role=localStorage.getItem("role");
    //console.log(this.role);
    //console.log(this.userRole)
    if(this.role=="AdminUser"){
      this.roleBoolean=true;
    }
  }
  toggle(individualUser){
    this.display= !this.display;
    this.individualUser=individualUser;
    
    
  }

  hideForm(){
    this.display=false;
  }

  private fetchAllResult(){

    this.http
      .post('https://localhost:44379/Form/ShowAllRequests',{"id":"1"})
      .pipe(
        map(responseData => {

          this.alphas=responseData as string
          //this.safeWaterForm=this.sanitizer.bypassSecurityTrustHtml(this.alphas);
          //console.log(this.alphas)
          for(let item of this.alphas){

            for(let i=0;i<Object.keys(item).length;i++){
              const key = Object.keys(item)[i]
              //console.log(key)
              
                this.UIKeys.push(key);
              
              
              this.UIKeys=this.UIKeys.sort();
            }

            break;
          }
          //console.log(this.UIKeys);

        })
      )
      .subscribe(posts => {
        // ...
        //console.log("Hello");
      });
  }


  private sendToAPI(){

    this.http
      .post(
        'https://localhost:44379/Form/UpdateFormInfo',
        this.person)
      .subscribe(responseData => {
       // console.log(responseData);
      });
    //console.log(form['value']);
  }

  private onUpdate(form:NgForm){
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
//console.log(this.person)
    this.display=false;
this.sendToAPI();
window.location.reload();

  }


  private deleteData(Data:any){
    console.log(Data['Oid']);

    this.http
      .post(
        'https://localhost:44379/Form/DeleteRequest',
        Data)
      .subscribe(responseData => {
        // console.log(responseData);
      });
      window.location.reload();
      //YOOO

  }
  private onApproved(decisionValue){
    decisionValue['Decison']='APPROVED';
    this.person=decisionValue;
    
    this.sendToAPI();
//window.location.reload();
  }
  private onRejected(decisionValue){
    decisionValue['Decison']='REJECTED';
    
    this.person=decisionValue;
    
    this.sendToAPI();
  }

}
