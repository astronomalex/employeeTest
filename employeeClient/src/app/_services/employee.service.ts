import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {EmployeeItem} from "../_models/employee-item";
import {EmployeeEditModel} from "../_models/employee-edit-model";
import {DepartmentModel} from "../_models/department-model";
import {EmployeesFilterModel} from "../_models/employees-filter-model";

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  baseUrl = environment.apiUrl + 'employee/';

  constructor(private http: HttpClient) {
  }

  getList(filters: EmployeesFilterModel) {
    let params = new HttpParams();
    params = params.append("departmentName", filters.departmentName);
    params = params.append("employeeFullName", filters.employeeFullName);
    params = params.append("dateOfBirth", filters.dateOfBirth);
    params = params.append("dateOfEmployment", filters.dateOfEmployment);
    params = params.append("salarySearchStr", filters.salarySearchStr);
    params = params.append("sortColumnName", filters.sortColumnName);
    params = params.append("ascDirection", filters.ascDirection);
    return this.http.get<EmployeeItem[]>(this.baseUrl + 'getEmployees', {params: params});
  }

  getEditModel(id: number) {
    let params = new HttpParams();
    params = params.append("id", id);
    return this.http.get<EmployeeEditModel>(this.baseUrl + 'getEditModel', {params: params})
  }

  getDepartments() {
    return this.http.get<DepartmentModel[]>(this.baseUrl + 'getDepartments');
  }

  updateEmployee(employee: EmployeeEditModel) {
    return this.http.post(this.baseUrl + 'updateEmployee', employee);
  }

  createEmployee(employee: EmployeeEditModel) {
    return this.http.post(this.baseUrl + 'createEmployee', employee);
  }

  deleteEmployee(id: number) {
    return this.http.post(this.baseUrl + 'deleteEmployee', id);
  }
}
