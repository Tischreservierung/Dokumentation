﻿using Tischreservierung.Models.Person;

namespace Tischreservierung.Data.Person
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer?> GetCustomerByEMail(string mail);

        void SetCustomer(Customer customer);
        void DeleteCustomer(Customer customer);

        Task Save();
    }
}
