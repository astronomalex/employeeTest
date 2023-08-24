import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AboutComponent} from "./about/about.component";
import {EmployeesComponent} from "./employees/employees.component";

const routes: Routes = [
  {path: 'about', component: AboutComponent},
  {path: 'employees', component: EmployeesComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
