using InvoiceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManager.Data
{
    /// <summary>
    /// Class for control the database
    /// </summary>
    public class InvoiceContext : DbContext
    {
        /// <summary>
        /// Constructor for create class
        /// </summary>
        /// <param name="options">For assign details of database</param>
        public InvoiceContext(DbContextOptions<InvoiceContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        /// <summary>
        /// Method for configure database
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=InvoiceManagement;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
        /// <summary>
        /// Table for customers
        /// </summary>
        public DbSet<Customer> Customers { get; set; } = null!;

        /// <summary>
        /// Table for invoices
        /// </summary>
        public DbSet<Invoice> Invoices { get; set; } = null!;

        /// <summary>
        /// Table for rows of invoices
        /// </summary>
        public DbSet<InvoiceRow> InvoiceRows { get; set; } = null!;

        /// <summary>
        /// Table for users
        /// </summary>
        public DbSet<User> Users { get; set; } = null!;
    }
}
