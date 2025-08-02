using AutoMapper;
using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;
using DataAccessLayer.Repos;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
{
    public class CustomerService
    {
        private readonly CustomerRepos customerRepos;
        private readonly IMapper mapper;

        public CustomerService()
        {
            var context = new Context();
            customerRepos = new CustomerRepos(context);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Customer, CustomerDTO>().ReverseMap());
            mapper = config.CreateMapper();
        }

        public void AddCustomer(CustomerDTO customerDto)
        {
            var customer = mapper.Map<Customer>(customerDto);
            customerRepos.AddCustomer(customer);
        }

        public CustomerDTO GetCustomerById(int id)
        {
            return mapper.Map<CustomerDTO>(customerRepos.GetCustomerById(id));
        }

        public List<CustomerDTO> GetAllCustomers()
        {
            var customers = customerRepos.GetAllCustomers();
            return mapper.Map<List<CustomerDTO>>(customers);
        }

        public void UpdateCustomer(CustomerDTO customerDto)
        {
            var customer = mapper.Map<Customer>(customerDto);
            customerRepos.UpdateCustomer(customer);
        }

        public bool DeleteCustomer(int id)
        {
            return customerRepos.DeleteCustomer(id);
        }

        public List<CustomerDTO> SearchCustomers(string name, string phone, int? id)
        {
            var customers = customerRepos.SearchCustomers(name, phone, id);
            return mapper.Map<List<CustomerDTO>>(customers);
        }
    }
}

