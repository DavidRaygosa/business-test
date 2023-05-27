namespace BusinessTest.Business.Authentication
{
    using BusinessTest.Business.Authentication.Models;
    using BusinessTest.Business.Client;
    using BusinessTest.Business.Client.Models;
    using BusinessTest.Business.Role;
    using BusinessTest.Business.Roles;
    using BusinessTest.Data.Repositories.Authentication;
    using BusinessTest.Data.Repositories.Client;
    using BusinessTest.Entity.Authentication;
    using BusinessTest.Entity.Client;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;

    public class AuthenticationAppService : IAuthenticationAppService
    {
        private readonly IConfiguration Configuration;
        private readonly IAuthenticationRepository AuthenticationRepository;
        private readonly IClientRepository ClientRepository;
        private readonly IRoleAppService RoleAppService;

        public AuthenticationAppService(
            IConfiguration configuration,
            IAuthenticationRepository authenticationRepository,
            IClientRepository clientRepository,
            IRoleAppService roleAppService
        ) => (Configuration, AuthenticationRepository, ClientRepository, RoleAppService) = (configuration, authenticationRepository, clientRepository, roleAppService);

        public List<string> Validate(string? Authorization)
        {
            //
            if (Authorization is null)
                throw new HttpRequestException("Authorization header should not be empty or null");

            List<string>? Permissions = new();
            long? ClientId;

            // DECODE TOKEN
            try
            {
                string Token = Authorization.Split(" ").Last();
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(Token);
                ClientId = Int64.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == "sub").Value);
                Permissions = JsonConvert.DeserializeObject<List<string>>(Decrypt(jwtSecurityToken.Claims.First(claim => claim.Type == "typ").Value));
            }
            catch (Exception Error)
            {
                throw new Exception($"Error at decoding token. Error: {Error.Message}");
            }

            if(ClientId is null || ClientId == 0)
                throw new Exception("ClientId is null, please contact an administrator");
            if (Permissions is null)
                throw new HttpRequestException("Permissions are null. Please contact an administrator");

            return Permissions;
        }

        public string Login(AuthenticationLoginRequestModelDto Body)
        {
            //
            if(string.IsNullOrEmpty(Body.UserName) || string.IsNullOrEmpty(Body.Password))
                throw new HttpRequestException("Username and/or password should not be empty or null");

            // GET AND VALIDATE IF AUTHENTICATION EXISTS
            Authentication? Authentication = AuthenticationRepository.GetByUserName(Body.UserName);
            if(Authentication is null)
                throw new KeyNotFoundException("Username not found");

            // HANDLE PASSWORD NULL
            if(Authentication.Password is null)
                throw new HttpRequestException("Password is null, please contact an administrator");
            
            // DECRYPT AND COMPARE PASSWORD PROVIDED
            if(Decrypt(Authentication.Password) != Body.Password)
                throw new HttpRequestException("Username or password not match");

            // GET CLIENT DATA USING CLIENT REPOSITORY
            ClientModelDto ClientModel = GetClient(Authentication.ClientId);
            List<string> Permissions = ClientModel.Permissions is not null && ClientModel.Permissions.Count > 0 ? ClientModel.Permissions : new List<string>();

            return GenerateToken(Authentication.ClientId.ToString(), Permissions);
        }

        public void Delete(Client Client)
        {
            Authentication? Authentication = AuthenticationRepository.GetByClient(Client);
            if (Authentication is not null)
                AuthenticationRepository.Delete(Authentication);
        }

        public void Create(Authentication Authentication) => AuthenticationRepository.Create(Authentication);

        public AuthenticationResponseModelDto? GetByUsername(string Username)
        {
            Authentication? Authentication = AuthenticationRepository.GetByUserName(Username);

            if (Authentication is null)
                return null;

            AuthenticationResponseModelDto AuthenticationModel = new()
            {
                Id = Authentication.Id,
                Username = Authentication.Username,
                Password = Authentication.Password,
                Client = Authentication.Client is not null ? CastUserModel(Authentication.Client) : null
            };
            return AuthenticationModel;
        }

        private static ClientModelDto CastUserModel(Client Authentication)
        {
            ClientModelDto ClientModel = new()
            {
                Id = Authentication.Id,
                Name = Authentication.Name,
                Direction = Authentication.Direction
            };
            return ClientModel;
        }

        private string GenerateToken(string? Id, List<string>? PermissionList)
        {
            if (Id is null)
                throw new Exception("Id is null. Please contact an administrator");

            string issuer = Configuration["Jwt:Issuer"];
            string audience = Configuration["Jwt:Audience"];
            JwtSecurityTokenHandler jwtTokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Typ, Encrypt(JsonConvert.SerializeObject(PermissionList))),
                    new Claim(JwtRegisteredClaimNames.Sub, Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Audience = audience,
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            SecurityToken Jwt = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(Jwt);
        }

        private ClientModelDto GetClient(long? Id)
        {
            //
            if (Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            Client? Client = ClientRepository.Get((long)Id);
            if (Client is null)
                throw new KeyNotFoundException("Client not found");

            // CREATE CLIENT MODEL
            ClientModelDto ClientModel = new()
            {
                Id = Client.Id,
                Name = Client.Name,
                Direction = Client.Direction,
                RoleId = Client.RoleId,
                Permissions = new List<string>()
            };

            // GET CLIENT PERMISSIONS BY USER ROLE ID
            if (ClientModel.RoleId is not null && ClientModel.RoleId > 0)
                ClientModel.Permissions = RoleAppService.Get(ClientModel.RoleId).Permissions;

            return ClientModel;
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

        private string Decrypt(string EnecryptedText)
        {
            string Key = Configuration["EncyptionPswd"];
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(EnecryptedText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new(buffer))
                {
                    using (CryptoStream cryptoStream = new((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}