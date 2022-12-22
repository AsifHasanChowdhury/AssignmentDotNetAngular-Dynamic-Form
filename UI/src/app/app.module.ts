import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
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



const  appRoutes: Routes=[
  {path: 'water-supply',component:WaterSupplyComponent},
  {path: 'birth-certificate' , component:BirthCertificateComponent},
  {path: 'house-Permit' , component:HousePermitComponent},
  {path: 'Show-Application', component:ShowApplicationsListComponent},
  
]
@NgModule({
  declarations: [
    AppComponent,
    CockpitComponent,
    ServerElementComponent,
    WaterSupplyComponent,
    BirthCertificateComponent,
    HousePermitComponent,
    ShowApplicationsListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
