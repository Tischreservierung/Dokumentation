import { Component, OnInit} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {Restaurant } from '../Modules/restaurant.module';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    Authorization: 'my-auth-token'
  })
};

@Component({
  selector: 'app-restaurant-registration',
  templateUrl: './restaurant-registration.component.html',
  styleUrls: ['./restaurant-registration.component.css']
})
export class RestaurantRegistrationComponent implements OnInit {
  baseApiUrl : string = environment.baseApiUrl;
  restaurant: string = "";
  constructor(private http: HttpClient) { }

  onKey(event : any){
    this.restaurant = event.target.value;
  }

  request (){
    this.http.post<Restaurant>(this.baseApiUrl+'/api/Restaurants',{name: this.restaurant},httpOptions);
  }

  ngOnInit(): void {
  }
}