namespace BusinessTest.Data.Repositories.Role
{
    using BusinessTest.Data.BusinessDbContext;
    using BusinessTest.Entity.Role;

    public class RoleRepository : IRoleRepository
    {
        private readonly BusinessTestDbContext Context;
        public RoleRepository(BusinessTestDbContext context) => (Context) = (context);

        public void Delete(Role role) => Context.Role?.Remove(role);
        public void Update(Role Role) => Context.Role?.Update(Role);
        public Role? Get(long Id) => Context.Role?.Where(x => x.Id == Id).FirstOrDefault();
        public List<Role>? GetAll() => Context.Role?.ToList();
        public void Create(Role Role) => Context.Role?.Add(Role);
    }
}