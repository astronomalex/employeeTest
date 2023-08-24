namespace EmployeeTestApi.Data.Constants
{
    public static class StoredProcedures
    {
        public static readonly string GetEmployees = "[dbo].[GetEmployees]";
        public static readonly string GetEmployee = "[dbo].[GetEmployee]";
        public static readonly string UpdateEmployee = "[dbo].[UpdateEmployee]";
        public static readonly string GetDepartments = "[dbo].[GetDepartments]";
        public static readonly string CreateEmployee = "[dbo].[AddNewEmployee]";
        public static readonly string DeleteEmployee = "[dbo].[DeleteEmployee]";
    }
}