namespace BusinessTest.Data.Repositories.Permission
{
    using BusinessTest.Entity.Permission;

    public interface IPermissionRepository
    {
        void Delete(Permission Permission);
        void Update(Permission Permission);
        Permission? Get(long Id);
        List<Permission>? GetAll();
        void Create(Permission Permission);
    }
}