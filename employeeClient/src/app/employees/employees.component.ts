import {Component, OnDestroy, OnInit} from '@angular/core';
import {ToastrService} from "ngx-toastr";
import {EmployeeItem} from "../_models/employee-item";
import {EmployeeService} from "../_services/employee.service";
import {debounceTime, filter, Subject, takeUntil} from "rxjs";
import {EmployeesFilterModel} from "../_models/employees-filter-model";
import {DepartmentModel} from "../_models/department-model";

declare let window: any;

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit, OnDestroy {
  formModal: any;
  items: EmployeeItem[] = [];
  selectedEmployeeId: Subject<number> = new Subject<number>();
  filterChangedSubject: Subject<EmployeesFilterModel> = new Subject<EmployeesFilterModel>();
  unsubscribe$ = new Subject();
  filters: EmployeesFilterModel;
  oldFilters: EmployeesFilterModel;
  departments: DepartmentModel[];

  constructor(
    private toastr: ToastrService,
    private employeeService: EmployeeService) {
  }

  ngOnInit(): void {
    this.loadDepartments();
    this.filters = new EmployeesFilterModel();
    this.oldFilters = Object.assign({}, this.oldFilters);
    this.formModal = new window.bootstrap.Modal(
      document.getElementById('employeeEdit')
    );

    this.filterChangedSubject.pipe(
      takeUntil(this.unsubscribe$),
      filter(() => !this.compareFilters()),
      debounceTime(1000)
    ).subscribe(() => {
      this.loadItems();
    })

    this.loadItems();
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next(true);
    this.unsubscribe$.complete();
  }

  loadItems() {
    this.employeeService.getList(this.filters)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(result => {
        this.oldFilters = Object.assign({}, this.oldFilters);
        this.items = result;
      }, (error) => {
        this.toastr.error(error.message);
      });
  }

  editEmployee(id: number) {
    this.selectedEmployeeId.next(id);
    this.formModal.show();
  }

  deleteEmployee(id: number) {
    if (confirm("Подтвердите удаление")) {
      this.employeeService.deleteEmployee(id).subscribe(() => {
        this.toastr.success('Удалено');
        this.loadItems();
      })
    }
  }

  public onEditClose() {
    this.formModal.hide();
    this.loadItems();
  }

  loadDepartments() {
    this.employeeService.getDepartments().subscribe(deps => {
      this.departments = deps;
    }, error => {
      this.toastr.error(error.message);
    });
  }

  compareFilters() {
    return this.filters.employeeFullName == this.oldFilters.employeeFullName &&
      this.filters.departmentName == this.oldFilters.departmentName &&
      this.filters.employeeFullName == this.oldFilters.employeeFullName &&
      this.filters.dateOfEmployment == this.oldFilters.dateOfEmployment &&
      this.filters.dateOfBirth == this.oldFilters.dateOfBirth &&
      this.filters.salarySearchStr == this.oldFilters.salarySearchStr &&
      this.filters.sortColumnName == this.oldFilters.sortColumnName &&
      this.filters.ascDirection == this.oldFilters.ascDirection
  }

  onFiltersChanged() {
    this.filterChangedSubject.next(this.filters);


    this.loadItems();
  }

  changeSortColumn(colName: string) {
    if (this.filters.sortColumnName == colName) {
      this.filters.ascDirection = !this.filters.ascDirection;
    } else {
      this.filters.sortColumnName = colName;
    }
    this.onFiltersChanged();
  }
}
