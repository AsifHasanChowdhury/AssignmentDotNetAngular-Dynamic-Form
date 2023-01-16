import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { JwtModule } from "@auth0/angular-jwt";
import {HttpClientModule, HttpHeaders, HttpParams} from '@angular/common/http';
import {RouterModule, Routes} from '@angular/router'; //Added by Asif
import { AppComponent } from './app.component';
import { CockpitComponent } from './cockpit/cockpit.component';
import { ServerElementComponent } from './server-element/server-element.component';
import { WaterSupplyComponent } from './water-supply/water-supply.component';
import { BirthCertificateComponent } from './birth-certificate/birth-certificate.component';
import { HousePermitComponent } from './house-permit/house-permit.component';
import { ShowApplicationsListComponent } from './show-applications-list/show-applications-list.component';
import * as Http from "http";
import { LoginComponent } from './login/login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { CustomersComponent } from './customers/customers.component';
import { NgToastModule } from 'ng-angular-popup';



const  appRoutes: Routes=[
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'customers', component: CustomersComponent, canActivate: [AuthGuard] },
  {path: 'water-supply',component:WaterSupplyComponent},
  {path: 'birth-certificate' , component:BirthCertificateComponent},
  {path: 'house-Permit' , component:HousePermitComponent},
  {path: 'Show-Application', component:ShowApplicationsListComponent},
  
]
export function tokenGetter() { 
  return localStorage.getItem("jwt"); 
}
@NgModule({
  declarations: [
    AppComponent,
    CockpitComponent,
    ServerElementComponent,
    WaterSupplyComponent,
    BirthCertificateComponent,
    HousePermitComponent,
    ShowApplicationsListComponent,
    LoginComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:44379"],
        disallowedRoutes: []
      }
    }),
    HttpClientModule,
    RouterModule.forRoot(appRoutes,{useHash:true}),
    NgToastModule
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
