using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CustomerSearchService
{
    private readonly DatabaseContext _databaseContext;

    public CustomerSearchService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public List<Customer> SearchByCountry(string country)
    {
        return SearchCustomers(customer =>
            customer.Country.Contains(country, StringComparison.OrdinalIgnoreCase));
    }

    public List<Customer> SearchByCompanyName(string companyName)
    {
        return SearchCustomers(customer =>
            customer.CompanyName.Contains(companyName, StringComparison.OrdinalIgnoreCase));
    }

    public List<Customer> SearchByContactName(string contactName)
    {
        return SearchCustomers(customer =>
            customer.ContactName.Contains(contactName, StringComparison.OrdinalIgnoreCase));
    }

    public string ExportCustomersToCsv(List<Customer> customers)
    {
        StringBuilder csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("CustomerID,CompanyName,ContactName,Country");

        foreach (Customer customer in customers)
        {
            csvBuilder.AppendLine(
                $"{customer.CustomerID},{customer.CompanyName},{customer.ContactName},{customer.Country}"
            );
        }

        return csvBuilder.ToString();
    }

    private List<Customer> SearchCustomers(Func<Customer, bool> predicate)
    {
        return _databaseContext.Customers
                               .Where(predicate)
                               .OrderBy(customer => customer.CustomerID)
                               .ToList();
    }
}
