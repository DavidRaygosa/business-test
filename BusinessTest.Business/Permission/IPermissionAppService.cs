namespace BusinessTest.Business.Permission
{
    using BusinessTest.Business.Permission.Models;

    public interface IPermissionAppService
    {
        void Delete(long? Id);
        void Update(long? Id, PermissionRequestModelDto Body);
        PermissionResponseModelDto Get(long? Id);
        List<PermissionResponseModelDto> GetAll();
        void Create(PermissionRequestModelDto Body);
    }
}