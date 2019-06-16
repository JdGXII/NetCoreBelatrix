using Belatrix.WebApi.Repository.Postgresql;
using Microsoft.EntityFrameworkCore;

namespace Belatrix.WebApi.Tests.Builder.Data
{
    public partial class BelatrixDbContextBuilder
    {
        private BelatrixDbContext _context;
        public BelatrixDbContextBuilder ConfigureInMemory()
        {
            var options = new DbContextOptionsBuilder<BelatrixDbContext>()
                          .UseInMemoryDatabase("test_base")
                          .Options;
                
            _context = new BelatrixDbContext(options);
            return this;
        }

        public BelatrixDbContext Build()
        {
            return _context;
        }
    }
}
