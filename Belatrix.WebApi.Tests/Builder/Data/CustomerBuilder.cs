using Belatrix.WebApi.Repository.Postgresql;
using GenFu;

namespace Belatrix.WebApi.Tests.Builder.Data
{
    public partial class BelatrixDbContextBuilder
    {
        public BelatrixDbContextBuilder AddTenRandomCustomers()
        {
            AddRandomCustomers(_context, 10);
            return this;
        }

        public BelatrixDbContextBuilder AddRandomCustomer()
        {
            AddRandomCustomers(_context, 1);
            return this;
        }
        private void AddRandomCustomers(BelatrixDbContext context, int numberOfCustomersToAdd)
        {
            var customerList = A.ListOf<Models.Customer>(numberOfCustomersToAdd);
            context.Customer.AddRange(customerList);
            context.SaveChanges();
        }

        public BelatrixDbContextBuilder AddSpecificCustomer(Models.Customer customer)
        {
            AddCustomer(customer);
            return this;
        }

        private void AddCustomer(Models.Customer customer)
        {
            _context.Customer.Add(customer);
            _context.SaveChanges();
        }
    }
}
