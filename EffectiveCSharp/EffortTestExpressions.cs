using Effort;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace EffectiveCSharp
{
    class EffortTestExpressions
    {
        private static dbContext _context;
        private static ICustomerRepository _repository;
        static void Main(string[] args)
        {

            SetupSQLCE();
            _repository = new CustomerRepository(_context);
            const string title = "Enlighment";
            var customer = new Customer(title);
            _repository.Insert(customer);
            _repository.Insert(customer);
            _repository.Insert(customer);
            
            var filteredCustomer = _context.Customers.StartsWith(c => c.Title, "E");
            
            var generatedSQL = filteredCustomer.ToString();
            Debug.WriteLine(filteredCustomer);
            //var sql = ((System.Data.Entity.Infrastructure.DbQuery <<> f__AnonymousType3 < string,string,string,short,string>>)filteredCustomer).ToString();
            //var generatedSQL= ((System.Data.Objects.ObjectQuery)filteredCustomer).ToTraceString();
            Console.WriteLine($"No of filtered Customer: {filteredCustomer.ToList().Count}");
            Console.WriteLine($"No of filtered Customer: {generatedSQL}");
            Console.ReadKey();

        }

        public static void SetupEffortDB()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new dbContext(connection);
        }
        
        public static void SetupSQLCE()
        {
            // file path of the database to create
            var filePath = @"sample.sdf";

            // delete it if it already exists
            if (File.Exists(filePath))
                File.Delete(filePath);

            // create the SQL CE connection string - this just points to the file path
            string connectionString = "Datasource = " + filePath;

            // NEED TO SET THIS TO MAKE DATABASE CREATION WORK WITH SQL CE!!!
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            using (var context = new dbContext(connectionString))
            {
                 
                // this will create the database with the schema from the Entity Model
                context.Database.Create();
            }

            // initialise our DbContext class with the SQL CE connection string, 
            // ready for our tests to use it.
            _context = new dbContext(connectionString);
        }

        public class dbContext : DbContext
        {
            public dbContext() { }
            public dbContext(string connectionString) : base(connectionString)
            {

            }

            public DbSet<Customer> Customers { get; set; }

            public dbContext(DbConnection connection) : base(connection, true)
            { }

        }
        public class CustomerRepository : ICustomerRepository
        {
            private readonly dbContext _context;

            public CustomerRepository(dbContext context)
            {
                _context = context;
            }

            public Customer Get(int id)
            {
                return _context.Customers.SingleOrDefault(x => x.Id == id);
            }

            public void Insert(Customer customer)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
        }

        public interface ICustomerRepository
        {
            Customer Get(int id);
            void Insert(Customer customer);
        }
    }


}
