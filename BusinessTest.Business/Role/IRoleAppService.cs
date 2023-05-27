namespace BusinessTest.Business.Roles
{
    using BusinessTest.Business.Role.Models;
    using BusinessTest.Entity.Role;

    public interface IRoleAppService
    {
        void Delete(long? Id);
        void Update(long? Id, RoleUpdateRequestModelDto Body);
        RoleResponseModelDto Get(long? Id);
        Role? GetEntity(long? Id);
        List<RoleResponseModelDto> GetAll();
        void Create(RoleRequestModelDto Body);
    }
}