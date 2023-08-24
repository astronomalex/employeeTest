import {LOCALE_ID, NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AboutComponent } from './about/about.component';
import { EmployeesComponent } from './employees/employees.component';
import {HttpClientModule} from "@angular/common/http";
import {ToastrModule} from "ngx-toastr";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import '@angular/common/locales/global/ru';
import { EmployeesEditComponent } from './employees-edit/employees-edit.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    AboutComponent,
    EmployeesComponent,
    EmployeesEditComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        ToastrModule.forRoot({
            positionClass: 'toast-bottom-right'
        }),
        BrowserAnimationsModule,
        ReactiveFormsModule,
        FormsModule,
    ],
  providers: [
    { provide: LOCALE_ID, useValue: "ru-RU" }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
