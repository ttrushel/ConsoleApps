using ClientConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientConsoleApp.Data
{
    public class ClientContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<TokenValut> TokenValut { get; set; }
        public DbSet<ClientResponseData> ClientResponseData { get; set; }
        public DbSet<ClientRequestData> ClientRequestData { get; set; }


        public ClientContext()
        {
        }
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\\SQLEXPRESS;Database=Demo;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
