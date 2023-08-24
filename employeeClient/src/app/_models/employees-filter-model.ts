export class EmployeesFilterModel {
    departmentName: string;
    employeeFullName: string;
    dateOfBirth: string;
    dateOfEmployment: string;
    salarySearchStr: string;
    sortColumnName: string;
    ascDirection: boolean;

    constructor() {
        this.departmentName = '';
        this.employeeFullName = '';
        this.salarySearchStr ='';
        this.dateOfBirth ='';
        this.dateOfEmployment ='';
        this.sortColumnName = 'FullName';
        this.ascDirection = true;
    }
}
