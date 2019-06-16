using Belatrix.WebApi.Repository.Postgresql;
using GenFu;

namespace Belatrix.WebApi.Tests.Builder.Data
{
    public partial class BelatrixDbContextBuilder
    {
        public BelatrixDbContextBuilder AddTenRandomOrderItems()
        {
            AddRandomOrderItems(_context, 10);
            return this;
        }

        public BelatrixDbContextBuilder AddRandomOrderItem()
        {
            AddRandomOrderItems(_context, 1);
            return this;
        }
        private void AddRandomOrderItems(BelatrixDbContext context, int numberOfOrderItemsToAdd)
        {
            var OrderItemList = A.ListOf<Models.OrderItem>(numberOfOrderItemsToAdd);
            context.OrderItem.AddRange(OrderItemList);
            context.SaveChanges();
        }

        public BelatrixDbContextBuilder AddSpecificOrderItem(Models.OrderItem orderItem)
        {
            AddOrderItem(orderItem);
            return this;
        }

        private void AddOrderItem(Models.OrderItem orderItem)
        {
            _context.OrderItem.Add(orderItem);
            _context.SaveChanges();
        }
    }
}
