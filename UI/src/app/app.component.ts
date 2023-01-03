import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import {DomSanitizer, SafeHtml}  from "@angular/platform-browser";
import {NgForm} from "@angular/forms";
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
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

    



  constructor(private http: HttpClient,private sanitizer:DomSanitizer,private jwtHelper: JwtHelperService,private router:Router) {}

  ngOnInit() {
   // this.fetchPosts();
  }


   onCreatePost(postData: { title: string; content: string }) {
     // Send Http request

    }

  isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("jwt");

    if (token && !this.jwtHelper.isTokenExpired(token)){
      return true;
    }

    return false;
  }
  logOut = () => {
    localStorage.removeItem("jwt");
    this.router.navigate(['/login']);
  }
}
