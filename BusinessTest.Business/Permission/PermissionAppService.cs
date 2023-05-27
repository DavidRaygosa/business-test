namespace BusinessTest.Business.Permission
{
    using BusinessTest.Business.Permission.Models;
    using BusinessTest.Data.Repositories;
    using BusinessTest.Data.Repositories.Permission;
    using BusinessTest.Entity.Permission;

    public class PermissionAppService : BaseAppService, IPermissionAppService
    {
        private readonly IPermissionRepository PermissionRepository;

        public PermissionAppService(
            IUnitOfWork unitOfWork,
            IPermissionRepository permissionRepository
        ) : base(unitOfWork) => (PermissionRepository) = (permissionRepository);

        public void Delete(long? Id)
        {
            //
            if (Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            Permission? Permission = PermissionRepository.Get((long)Id);
            if (Permission is null)
                throw new KeyNotFoundException("Permission not found");

            // SAVE CHANGES
            PermissionRepository.Delete(Permission);
            UnitOfWork.SaveChanges();
        }

        public void Update(long? Id, PermissionRequestModelDto Body)
        {
            //
            if (Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            Permission? Permission = PermissionRepository.Get((long)Id);
            if (Permission is null)
                throw new KeyNotFoundException("Permission not found");

            // SET CHANGES
            Permission.Name = Body.Name is not null ? Body.Name : Permission.Name;

            // SAVE CHANGES
            PermissionRepository.Update(Permission);
            UnitOfWork.SaveChanges();
        }

        public PermissionResponseModelDto? Get(long? Id)
        {
            //
            if(Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            Permission? Permission = PermissionRepository.Get((long)Id);
            if(Permission is null)
                throw new KeyNotFoundException("Permission not found");

            PermissionResponseModelDto PermissionModel = new()
            {
                Name = Permission.Name,
                Id = Permission.Id
            };

            return PermissionModel;
        }

        public List<PermissionResponseModelDto> GetAll()
        {
            List<PermissionResponseModelDto> Permissions = new();
            PermissionRepository.GetAll()?.ForEach(Permission => Permissions.Add(new()
            {
                Name = Permission.Name,
                Id = Permission.Id
            }));
            return Permissions;
        }

        public void Create(PermissionRequestModelDto Body)
        {
            //
            if(string.IsNullOrEmpty(Body.Name))
                throw new HttpRequestException("Name should not be empty or null");

            Permission Permission = new()
            {
                Name = Body.Name
            };

            // SAVE CHANGES
            PermissionRepository.Create(Permission);
            UnitOfWork.SaveChanges();
        }
    }
}