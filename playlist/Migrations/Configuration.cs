namespace TestTwo_20151.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TestTwo_20151.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TestTwo_20151.Models.DataContext";
        }

        protected override void Seed(TestTwo_20151.Models.DataContext context)
        {
            //  This method will be called after migrating to the latest version.
            /*
            if (context.Courses.Count() == 0)
            {
                Course CI = new Course();

                CI.Code = "INT422";
                CI.Name = "Web development using ASP.NET";
                CI.Description = "This course will teach how to make web by using ASP.NET with C#";
                context.Courses.Add(CI);

                Course CD = new Course();
                CD.Code = "DBS345";
                CD.Name = "Database";
                CD.Description = "This course will teach about database";
                context.Courses.Add(CD);



                context.SaveChanges();
            }

            ***/




            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
