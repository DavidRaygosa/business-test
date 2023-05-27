namespace BusinessTest.Data.Repositories.Permission
{
    using BusinessTest.Data.BusinessDbContext;
    using BusinessTest.Entity.Permission;

    public class PermissionRepository : IPermissionRepository
    {
        private readonly BusinessTestDbContext Context;
        public PermissionRepository(BusinessTestDbContext context) => (Context) = (context);

        public void Delete(Permission Permission) => Context.Permission?.Remove(Permission);
        public void Update(Permission Permission) => Context.Permission?.Update(Permission);
        public Permission? Get(long Id) => Context.Permission?.Where(x => x.Id == Id).FirstOrDefault();
        public List<Permission>? GetAll() => Context.Permission?.ToList();
        public void Create(Permission Permission) => Context.Permission?.Add(Permission);
    }
}