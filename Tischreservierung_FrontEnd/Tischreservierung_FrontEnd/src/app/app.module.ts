import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { RestaurantRegistrationComponent } from './restaurant-registration/restaurant-registration.component';
import {  } from '@angular/material';

@NgModule({
  declarations: [
    AppComponent,
    RestaurantRegistrationComponent,
  ],
  imports: [
    BrowserModule,
  ],
  exports: [
    BrowserModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
