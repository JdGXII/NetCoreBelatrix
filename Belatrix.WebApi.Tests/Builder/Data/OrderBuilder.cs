using Belatrix.WebApi.Repository.Postgresql;
using GenFu;

namespace Belatrix.WebApi.Tests.Builder.Data
{
    public partial class BelatrixDbContextBuilder
    {
        public BelatrixDbContextBuilder AddTenRandomOrders()
        {
            AddRandomOrders(_context, 10);
            return this;
        }

        public BelatrixDbContextBuilder AddRandomOrder()
        {
            AddRandomOrders(_context, 1);
            return this;
        }
        private void AddRandomOrders(BelatrixDbContext context, int numberOfOrdersToAdd)
        {
            var OrderList = A.ListOf<Models.Order>(numberOfOrdersToAdd);
            context.Order.AddRange(OrderList);
            context.SaveChanges();
        }

        public BelatrixDbContextBuilder AddSpecificOrder(Models.Order order)
        {
            AddOrder(order);
            return this;
        }

        private void AddOrder(Models.Order order)
        {
            _context.Order.Add(order);
            _context.SaveChanges();
        }
    }
}
