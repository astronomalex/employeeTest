export class EmployeeEditModel {
  employeeId: number;
  departmentId: number;
  lastName: string;
  firstName: string;
  fathersName: string;
  dateOfBirth: string;
  dateOfEmployment: string;
  salary: number;

  constructor() {
    this.salary = 0;
    this.firstName = '';
    this.lastName = '';
    this.fathersName = '';
    this.departmentId = 1;
    this.dateOfEmployment = '';
    this.dateOfBirth = '';
  }
}
