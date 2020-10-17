using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.CodeFirst;

namespace Azmongir.Classes
{
   public class MyDbContext : DbContext
    {
        public MyDbContext() : base("default") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //var sqliteconnection = new SqliteCreateDatabaseIfNotExists<MyDbContext>(modelBuilder);
            var sqliteconnection = new MyDbContextInitializ(modelBuilder);
            Database.SetInitializer(sqliteconnection);
        }

       
    }

    public class MyDbContextInitializ : SqliteDropCreateDatabaseWhenModelChanges<MyDbContext>
    {
        public MyDbContextInitializ(DbModelBuilder ModelBuilder) : base(ModelBuilder) { }

        protected override void Seed(MyDbContext context)
        {
            // base.Seed(context);   //if Create or change database add data into database
        }

    }
}
