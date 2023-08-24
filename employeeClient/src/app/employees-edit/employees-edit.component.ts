import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {filter, Subject, switchMap, takeUntil, tap} from "rxjs";
import {EmployeeEditModel} from "../_models/employee-edit-model";
import {EmployeeService} from "../_services/employee.service";
import {ToastrService} from "ngx-toastr";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {DepartmentModel} from "../_models/department-model";

@Component({
  selector: 'app-employees-edit',
  templateUrl: './employees-edit.component.html',
  styleUrls: ['./employees-edit.component.css']
})
export class EmployeesEditComponent implements OnInit, OnDestroy {
  @Input() employeeId$: Subject<number>;
  @Input() departments: DepartmentModel[];
  @Output() close = new EventEmitter<any>();
  unsubscribe$ = new Subject();
  editModel: EmployeeEditModel;
  editEmployeeId: number;
  employeeEditForm: FormGroup;
  dateOfBirthStr: string;
  title: string;
  constructor(
    private employeeService: EmployeeService,
    private toastr: ToastrService) {
  }

  ngOnInit(): void {

    this.employeeId$.pipe(
      takeUntil(this.unsubscribe$),
      tap(id => {
        this.editEmployeeId = id;
        if (id == 0) {
          this.title = 'Добавление нового сотрудника'
          this.initForm();
        }
      }),
      filter(id => id > 0),
      switchMap(id =>
        this.employeeService.getEditModel(id)
      )).subscribe(model => {

        this.editModel = model;
        this.title = 'Изменение данных сотрудника'
        this.initForm();
    }, error => {
        this.toastr.error(error.message)
    })
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next(true);
    this.unsubscribe$.complete();
  }

  initForm() {
    if (!this.editModel || this.editEmployeeId == 0) {
      this.editModel = new EmployeeEditModel();

    }
    this.employeeEditForm = new FormGroup({
      lastName: new FormControl(this.editModel.lastName, [Validators.required]),
      firstName: new FormControl(this.editModel.firstName, [Validators.required]),
      fathersName: new FormControl(this.editModel.fathersName, [Validators.required]),
      departmentId: new FormControl(this.editModel.departmentId, [Validators.required]),
      dateOfBirth: new FormControl(this.editModel.dateOfBirth.split('T')[0], [Validators.required]),
      dateOfEmployment: new FormControl(this.editModel.dateOfEmployment.split('T')[0], [Validators.required]),
      salary: new FormControl(this.editModel.salary, [Validators.required, Validators.min(0)])
    })
  }


  saveSomeThing() {

    const model = new EmployeeEditModel();
    model.employeeId = this.editModel.employeeId;
    model.departmentId = Number.parseInt(this.employeeEditForm.value["departmentId"]);
    model.firstName = this.employeeEditForm.value["firstName"];
    model.lastName = this.employeeEditForm.value["lastName"];
    model.fathersName = this.employeeEditForm.value["fathersName"];
    model.dateOfBirth = this.employeeEditForm.value["dateOfBirth"];
    model.dateOfEmployment = this.employeeEditForm.value["dateOfEmployment"];
    model.salary = this.employeeEditForm.value["salary"];


    if (!model.employeeId) {
      this.employeeService.createEmployee(model).subscribe(() => {
        this.toastr.success("Сохранено");
        this.close.emit();
      });
    } else {
      this.employeeService.updateEmployee(model).subscribe(() => {
        this.toastr.success("Сохранено");
        this.close.emit();
      });
    }
  }
}
