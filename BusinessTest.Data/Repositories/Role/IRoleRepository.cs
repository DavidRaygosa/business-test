namespace BusinessTest.Data.Repositories.Role
{
    using BusinessTest.Entity.Role;

    public interface IRoleRepository
    {
        void Delete(Role role);
        void Update(Role Role);
        Role? Get(long Id);
        List<Role>? GetAll();
        void Create(Role Role);
    }
}