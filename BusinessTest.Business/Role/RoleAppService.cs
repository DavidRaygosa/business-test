namespace BusinessTest.Business.Role
{
    using BusinessTest.Business.Role.Models;
    using BusinessTest.Business.Roles;
    using BusinessTest.Data.Repositories;
    using BusinessTest.Data.Repositories.Role;
    using BusinessTest.Entity.Role;
    using Newtonsoft.Json;

    public class RoleAppService : BaseAppService, IRoleAppService
    {
        private readonly IRoleRepository RoleRepository;

        public RoleAppService(
            IUnitOfWork unitOfWork,
            IRoleRepository roleRepository
        ): base(unitOfWork) => (RoleRepository) = (roleRepository);

        public void Delete(long? Id)
        {
            //
            if (Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            Role? Role = RoleRepository.Get((long)Id);
            if (Role is null)
                throw new KeyNotFoundException("Role not found");

            // SAVE CHANGES
            RoleRepository.Delete(Role);
            UnitOfWork.SaveChanges();
        }

        public void Update(long? Id, RoleUpdateRequestModelDto Body)
        {
            //
            if (Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            Role? Role = RoleRepository.Get((long)Id);
            if (Role is null)
                throw new KeyNotFoundException("Role not found");

            // SET CHANGES
            Role.Name = Body.Name is not null ? Body.Name : Role.Name;
            Role.Permissions = Body.Permissions is not null && Body.Permissions.Count > 0 ? JsonConvert.SerializeObject(Body.Permissions) : Role.Permissions;

            // SAVE CHANGES
            RoleRepository.Update(Role);
            UnitOfWork.SaveChanges();
        }

        public RoleResponseModelDto Get(long? Id)
        {
            //
            if(Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            Role? Role = RoleRepository.Get((long)Id);
            if(Role is null)
                throw new KeyNotFoundException("Role not found");

            RoleResponseModelDto RoleModel = new()
            {
                Id = Role.Id,
                Name = Role.Name,
                Permissions = Role.Permissions is not null ? JsonConvert.DeserializeObject<List<string>>(Role.Permissions) : new List<string>()
            };
            return RoleModel;
        }

        public Role? GetEntity(long? Id)
        {
            //
            if (Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            return RoleRepository.Get((long)Id);
        }

        public List<RoleResponseModelDto> GetAll()
        {
            List<RoleResponseModelDto> Roles = new();
            RoleRepository.GetAll()?.ForEach(Role => Roles.Add(new()
            {
                Id = Role.Id,
                Name = Role.Name,
                Permissions = Role.Permissions is not null ? JsonConvert.DeserializeObject<List<string>>(Role.Permissions) : new List<string>()
            }));
            return Roles;
        }

        public void Create(RoleRequestModelDto Body)
        {
            //
            if(string.IsNullOrEmpty(Body.Name))
                throw new HttpRequestException("Name should not be empty or null");

            // CREATE ROLE
            Role Role = new()
            {
                Name = Body.Name,
                Permissions = JsonConvert.SerializeObject(new List<string>())
            };
            RoleRepository.Create(Role);

            // SAVE CHANCES
            UnitOfWork.SaveChanges();
        }
    }
}