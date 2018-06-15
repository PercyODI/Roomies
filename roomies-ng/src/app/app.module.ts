import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';

import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { BillComponent } from './bill/bill.component';
import { PeopleListComponent } from './people-list/people-list.component';
import { PersonComponent } from './person/person.component';
import { BillListComponent } from './bill-list/bill-list.component';

@NgModule({
  declarations: [
    AppComponent,
    BillComponent,
    PeopleListComponent,
    PersonComponent,
    BillListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
