class Employee
{
    private int id;
    private string name;
    private string department;
    private bool isActive;
 
    public int GetId() => id;
    public string GetName() => name;
    public string GetDepartment() => department;
    public bool GetActiveStatus() => isActive;
   
    public void UpdateActiveStatus(bool status)
    {
        isActive = status;
    }
}
 
class EmployeeDataStore
{
    public void Store(Employee emp)
    {
        // Handle database persistence
    }
}
 
class XmlEmployeeReportGenerator
{
    public void Generate(Employee emp)
    {
        string xmlContent = BuildXml(emp);
        Output(xmlContent);
    }
 
    private string BuildXml(Employee emp)
    {
        // Create XML format
        return $"<employee><id>{emp.GetId()}</id><name>{emp.GetName()}</name></employee>";
    }
 
    private void Output(string content)
    {
        // Send to printer/console
    }
}
 
class CsvEmployeeReportGenerator
{
    public void Generate(Employee emp)
    {
        string csvContent = BuildCsv(emp);
        Output(csvContent);
    }
 
    private string BuildCsv(Employee emp)
    {
        // Create CSV format
        return $"{emp.GetId()},{emp.GetName()},{emp.GetDepartment()}";
    }
 
    private void Output(string content)
    {
        // Send to printer/console
    }
}
 
class EmployeeOffboardingHandler
{
    public void ProcessTermination(Employee emp)
    {
        MarkAsInactive(emp);
    }
 
    private void MarkAsInactive(Employee emp)
    {
        emp.UpdateActiveStatus(false);
    }
}
