using Belatrix.WebApi.Repository.Postgresql;
using GenFu;

namespace Belatrix.WebApi.Tests.Builder.Data
{
    public partial class BelatrixDbContextBuilder
    {
        public BelatrixDbContextBuilder AddTenRandomProducts()
        {
            AddRandomProducts(_context, 10);
            return this;
        }

        public BelatrixDbContextBuilder AddRandomProduct()
        {
            AddRandomProducts(_context, 1);
            return this;
        }
        private void AddRandomProducts(BelatrixDbContext context, int numberOfProductsToAdd)
        {
            var ProductList = A.ListOf<Models.Product>(numberOfProductsToAdd);
            context.Product.AddRange(ProductList);
            context.SaveChanges();
        }

        public BelatrixDbContextBuilder AddSpecificProduct(Models.Product product)
        {
            AddProduct(product);
            return this;
        }

        private void AddProduct(Models.Product product)
        {
            _context.Product.Add(product);
            _context.SaveChanges();
        }
    }
}
