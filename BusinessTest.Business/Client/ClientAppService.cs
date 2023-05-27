namespace BusinessTest.Business.Client
{
    using BusinessTest.Business.Authentication;
    using BusinessTest.Business.Client.Models;
    using BusinessTest.Data.Repositories.Client;
    using BusinessTest.Entity.Client;
    using BusinessTest.Entity.Authentication;
    using BusinessTest.Data.Repositories;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.Extensions.Configuration;
    using BusinessTest.Business.Roles;
    using BusinessTest.Entity.Role;

    public class ClientAppService : BaseAppService, IClientAppService
    {
        private readonly IConfiguration Configuration;
        private readonly IClientRepository ClientRepository;
        private readonly IAuthenticationAppService AuthenticationAppService;
        private readonly IRoleAppService RoleAppService;

        public ClientAppService(
            IUnitOfWork unitOfWork,
            IConfiguration configuration,
            IClientRepository clientRepository,
            IAuthenticationAppService authenticationAppService,
            IRoleAppService roleAppService
        ) : base(unitOfWork) => (Configuration, ClientRepository, AuthenticationAppService, RoleAppService) = (configuration, clientRepository, authenticationAppService, roleAppService);

        public void Delete(long? Id)
        {
            //
            if (Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            // VALIDATE IF USER EXISTS
            Client? Client = ClientRepository.Get((long)Id);
            if (Client is null)
                throw new KeyNotFoundException("Client not found");

            // DELETE CLIENT
            ClientRepository.Delete(Client);

            // DELETE AUTHENTICATION
            AuthenticationAppService.Delete(Client);

            // SAVE CHANGES
            UnitOfWork.SaveChanges();
        }

        public void Update(long? Id, UpdateClientModelDto Body)
        {
            //
            if(Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            // VALIDATE IF USER EXISTS
            Client? Client = ClientRepository.Get((long)Id);
            if (Client is null)
                throw new KeyNotFoundException("Client not found");

            // UPDATE IF BODY FIELD IS NOT NULL
            Client.Name = Body.Name is not null ? Body.Name : Client.Name;
            Client.Lastname = Body.Lastname is not null ? Body.Lastname : Client.Lastname;
            Client.Direction = Body.Direction is not null ? Body.Direction : Client.Direction;

            if(Body.RoleId is not null && Body.RoleId > 0)
            {
                Role? Role = RoleAppService.GetEntity(Body.RoleId);
                if (Role is null)
                    throw new KeyNotFoundException("Role not found");

                Client.Role = Role;
            }

            // SAVE CHANGES
            ClientRepository.Update(Client);
            UnitOfWork.SaveChanges();
        }

        public void Create(CreateClientModelDto Body)
        {
            //
            if(string.IsNullOrEmpty(Body.Username) || string.IsNullOrEmpty(Body.Password))
                throw new HttpRequestException("Username and/or password should not be empty");
            if (string.IsNullOrEmpty(Body.Name) || string.IsNullOrEmpty(Body.Lastname))
                throw new HttpRequestException("Name and/or lastname should not be empty");

            // VALIDATE IF USERNAME IS ALREADY REGISTERED
            if(AuthenticationAppService.GetByUsername(Body.Username) is not null)
                throw new HttpRequestException("Username already registered");

            // CREATE CLIENT
            Client Client = new()
            {
                Name = Body.Name,
                Lastname = Body.Lastname,
                Direction = Body.Direction is not null ? Body.Direction : null
            };
            ClientRepository.Create(Client);

            // ASSING DEFAULT ROLE
            Role? Role = RoleAppService.GetEntity(Int64.Parse(Configuration["DefaultRoleId"]));
            if (Role is null)
                throw new KeyNotFoundException("Role not found. Please contact an administrator");

            Client.Role = Role;

            // CREATE AUTHENTICATION
            Authentication Authentication = new()
            {
                Username = Body.Username,
                Password = Encrypt(Body.Password),
                Client = Client
            };
            AuthenticationAppService.Create(Authentication);

            // SET AUTHENTICATION TO CLIENT
            Client.Authentication = Authentication;

            // SAVE CHANGES
            UnitOfWork.SaveChanges();
        }

        public ClientModelDto GetOne(long? Id)
        {
            //
            if(Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            Client? Client = ClientRepository.Get((long)Id);
            if(Client is null)
                throw new KeyNotFoundException("Client not found");

            // CREATE CLIENT MODEL
            ClientModelDto ClientModel = new()
            {
                Id = Client.Id,
                Name = Client.Name,
                Lastname = Client.Lastname,
                Direction = Client.Direction,
                RoleId = Client.RoleId,
                Permissions = new List<string>()
            };

            // GET CLIENT PERMISSIONS BY USER ROLE ID
            if(ClientModel.RoleId is not null && ClientModel.RoleId > 0)
                ClientModel.Permissions = RoleAppService.Get(ClientModel.RoleId).Permissions;

            return ClientModel;
        }

        public Client? GetEntity(long? Id)
        {
            //
            if (Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            return ClientRepository.Get((long)Id);
        }

        public List<ClientModelDto> GetAll()
        {
            List<ClientModelDto> Clients = new();
            ClientRepository.GetAll()?.ForEach(Client =>
            {
                // CREATE CLIENT MODEL
                ClientModelDto ClientModel = new()
                {
                    Id = Client.Id,
                    Name = Client.Name,
                    Lastname = Client.Lastname,
                    Direction = Client.Direction,
                    RoleId = Client.RoleId,
                    Permissions = new List<string>()
                };

                // GET CLIENT PERMISSIONS BY USER ROLE ID
                if (ClientModel.RoleId is not null && ClientModel.RoleId > 0)
                    ClientModel.Permissions = RoleAppService.Get(ClientModel.RoleId).Permissions;

                // ADD CLIENT MODEL TO CLIENT LIST
                Clients.Add(ClientModel);
            });
            return Clients;
        }

        private string Encrypt(string Text)
        {
            string Key = Configuration["EncyptionPswd"];
            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new())
                {
                    using (CryptoStream cryptoStream = new((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new((Stream)cryptoStream))
                        {
                            streamWriter.Write(Text);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }
    }
}