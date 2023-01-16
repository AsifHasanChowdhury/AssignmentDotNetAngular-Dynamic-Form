import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TransletLocalHostService {

  constructor() { }
  public localhostIP:string='https://192.168.10.62:44379/';

}
