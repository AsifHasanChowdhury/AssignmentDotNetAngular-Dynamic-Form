import { Router } from '@angular/router';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { AuthenticatedResponse } from 'app/_interfaces/authenticated-response.model';
import { LoginModel } from 'app/_interfaces/login.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  //@Output() redirect:EventEmitter<any> = new EventEmitter();
  invalidLogin: boolean;
  credentials: LoginModel = {username:'', password:''};
  userRole: string;
  

  constructor(private router: Router, private http: HttpClient) { }

  ngOnInit(): void {
    
  }

  login = ( form: NgForm) => {
    if (form.valid) {
      this.http.post<AuthenticatedResponse>("https://localhost:44379/api/auth/login", this.credentials, {
        headers: new HttpHeaders({ "Content-Type": "application/json"})
      })
      .subscribe({
        next: (response: AuthenticatedResponse) => {
          const token = response.token;

          //parse jwt token
          let jwtData = token.split('.')[1]
          let decodedJwtJsonData = window.atob(jwtData)
          this.userRole= JSON.parse(decodedJwtJsonData)['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
          console.log(this.userRole)
          
          //done with jwt parsing
         // this.redirect.emit(this.userRole);
         localStorage.setItem("role", this.userRole); 
          localStorage.setItem("jwt", token); 
          this.invalidLogin = false; 
          this.router.navigate(["/"]);
        },
        error: (err: HttpErrorResponse) => this.invalidLogin = true
      })
    }
  }
}

