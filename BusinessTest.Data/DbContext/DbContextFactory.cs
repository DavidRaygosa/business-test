namespace BusinessTest.Data.DbContext
{
    using Microsoft.EntityFrameworkCore.Design;
    using BusinessTest.Data.BusinessDbContext;
    using Microsoft.EntityFrameworkCore;

    public class DbContextFactory : IDesignTimeDbContextFactory<BusinessTestDbContext>
    {
        public BusinessTestDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<BusinessTestDbContext> OptionsBuilder = new();
            // DEVELOPMENT
            OptionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=BusinessTest; User Id=BusinessTestUser; Password=Password55!; Trusted_Connection=True;");
            // PRODUCTION
            //OptionsBuilder.UseSqlServer("Server=businessTestDavidRay.mssql.somee.com; Database=businessTestDavidRay; User Id=DavidRay_SQLLogin_1; Password=zo9yavkuns;");
            return new BusinessTestDbContext(OptionsBuilder.Options);
        }
    }
}