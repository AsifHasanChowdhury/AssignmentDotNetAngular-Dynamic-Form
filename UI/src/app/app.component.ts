import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import {DomSanitizer, SafeHtml}  from "@angular/platform-browser";
import {NgForm} from "@angular/forms";
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  loadedPosts = [];
  alphas:any;
  htmlview: SafeHtml | undefined;
  genders = ['Male', 'Female'];


  constructor(private http: HttpClient,private sanitizer:DomSanitizer) {}

  ngOnInit() {
   // this.fetchPosts();
  }


   onCreatePost(postData: { title: string; content: string }) {
     // Send Http request

    }

  // onFetchPosts() {
  //   // Send Http request
  //   this.fetchPosts();
  // }


  /*
  private fetchPosts() {
    this.http
      .get('https://localhost:44396/api/ProductAPI/SendFormModule')
      .pipe(
        map(responseData => {

          this.alphas=responseData as string
          this.htmlview=this.sanitizer.bypassSecurityTrustHtml(this.alphas);
          //console.log(this.alphas)

        })
      )
      .subscribe(posts => {
        // ...
        console.log("Hello");
      });
  }

  onSubmit(form:HTMLFormElement){

    this.http
      .post(
        'https://localhost:44396/api/ProductAPI/GetFormResponse',
        form['value']
      )
      .subscribe(responseData => {
        console.log(responseData);
      });
    console.log(form['value']);
  }


   */
}
