using Belatrix.WebApi.Repository.Postgresql;
using GenFu;

namespace Belatrix.WebApi.Tests.Builder.Data
{
    public partial class BelatrixDbContextBuilder
    {
        public BelatrixDbContextBuilder AddTenRandomSuppliers()
        {
            AddRandomSuppliers(_context, 10);
            return this;
        }

        public BelatrixDbContextBuilder AddRandomSupplier()
        {
            AddRandomSuppliers(_context, 1);
            return this;
        }
        private void AddRandomSuppliers(BelatrixDbContext context, int numberOfSuppliersToAdd)
        {
            var SupplierList = A.ListOf<Models.Supplier>(numberOfSuppliersToAdd);
            context.Supplier.AddRange(SupplierList);
            context.SaveChanges();
        }

        public BelatrixDbContextBuilder AddSpecificSupplier(Models.Supplier supplier)
        {
            AddSupplier(supplier);
            return this;
        }

        private void AddSupplier(Models.Supplier supplier)
        {
            _context.Supplier.Add(supplier);
            _context.SaveChanges();
        }
    }
}
