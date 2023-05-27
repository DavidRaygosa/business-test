namespace BusinessTest.Data.Repositories
{
    using BusinessTest.Data.BusinessDbContext;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly BusinessTestDbContext Context;
        public UnitOfWork(BusinessTestDbContext context) => Context = context;

        public int SaveChanges() => Context.SaveChanges();
    }
}