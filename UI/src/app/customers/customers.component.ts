import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {
  customers: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get("https://localhost:44379/api/customers/Getinfo")
    .subscribe({
      next: (result: any) => this.customers = result,
      error: (err: HttpErrorResponse) => console.log(err)

    })
    
  }

}

