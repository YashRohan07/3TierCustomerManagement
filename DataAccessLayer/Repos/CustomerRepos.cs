using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repos
{
    public class CustomerRepos
    {
        private readonly Context _context;

        public CustomerRepos(Context context)
        {
            _context = context;
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public Customer GetCustomerById(int id)
        {
            return _context.Customers.Find(id);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = _context.Customers.Find(customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.Address = customer.Address;
                _context.SaveChanges();
            }
        }

        public bool DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Customer> SearchCustomers(string name, string phone, int? id)
        {
            var query = _context.Customers.AsQueryable();

            if (id.HasValue)
            {
                query = query.Where(c => c.Id == id.Value);
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(c => c.Phone.Contains(phone));
            }

            return query.ToList();
        }
    }
}
