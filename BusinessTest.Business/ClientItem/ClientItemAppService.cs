namespace BusinessTest.Business.ClientItem
{
    using BusinessTest.Business.ClientItem.Models;
    using BusinessTest.Business.Item;
    using BusinessTest.Entity.Item;
    using BusinessTest.Data.Repositories.ClientItem;
    using BusinessTest.Entity.Client;
    using System.IdentityModel.Tokens.Jwt;
    using BusinessTest.Business.Client;
    using BusinessTest.Entity.ClientItem;
    using BusinessTest.Data.Repositories;

    public class ClientItemAppService : BaseAppService, IClientItemAppService
    {
        private readonly IItemAppService ItemAppService;
        private readonly IClientAppService ClientAppService;
        private readonly IClientItemRepository ClientItemRepository;

        public ClientItemAppService(
            IUnitOfWork unitOfWork,
            IItemAppService itemAppService,
            IClientAppService clientAppService,
            IClientItemRepository clientItemRepository
        ) : base(unitOfWork) => (ItemAppService, ClientAppService, ClientItemRepository) = (itemAppService, clientAppService, clientItemRepository);

        public void Delete(long? Id)
        {
            //
            if (Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            ClientItem? ClientItem = ClientItemRepository.Get((long)Id);
            if (ClientItem is null)
                throw new KeyNotFoundException("ClientItem not found");

            // SAVE CHANGES
            ClientItemRepository.Delete(ClientItem);
            UnitOfWork.SaveChanges();
        }

        public void Update(long? Id, ClientItemRequestModelDto Body)
        {
            //
            if (Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");
            if (Body.ItemId is null || Body.ItemId == 0)
                throw new HttpRequestException("ItemId should not be empty or null");

            ClientItem? ClientItem = ClientItemRepository.Get((long)Id);
            if (ClientItem is null)
                throw new KeyNotFoundException("ClientItem not found");

            // SET CHANGES
            ClientItem.ItemId = Body.ItemId is not null && Body.ItemId > 0 ? (long)Body.ItemId : ClientItem.ItemId;

            // SAVE CHANGES
            ClientItemRepository.Update(ClientItem);
            UnitOfWork.SaveChanges();
        }

        public ClientItemResponseModelDto Get(long? Id)
        {
            //
            if(Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            ClientItem? ClientItem = ClientItemRepository.Get((long)Id);
            if(ClientItem is null)
                throw new KeyNotFoundException("ClientItem not found");

            ClientItemResponseModelDto ClientItemModel = new()
            {
                Id = ClientItem.Id,
                ClientId = ClientItem.ClientId,
                ItemId = ClientItem.ItemId,
                Item = ClientItem.ItemId > 0 ? ItemAppService.Get(ClientItem.ItemId) : null,
                Client = ClientItem.ClientId > 0 ? ClientAppService.GetOne(ClientItem.ClientId) : null,
                Quantity = ClientItem.Quantity,
                Date = ClientItem.Date
            };
            return ClientItemModel;
        }

        public List<ClientItemResponseModelDto> GetAll(long? ItemId, long? ClientId)
        {
            List<ClientItemResponseModelDto> ClientItems = new();
            ClientItemRepository.GetAll(ItemId, ClientId)?.ForEach(ClientItem => ClientItems.Add(new()
            {
                Id = ClientItem.Id,
                ClientId = ClientItem.ClientId,
                ItemId = ClientItem.ItemId,
                Item = ClientItem.ItemId > 0 ? ItemAppService.Get(ClientItem.ItemId) : null,
                Client = ClientItem.ClientId > 0 ? ClientAppService.GetOne(ClientItem.ClientId) : null,
                Quantity = ClientItem.Quantity,
                Date = ClientItem.Date
            }));
            return ClientItems;
        }

        public void Create(string? Authorization, ClientItemRequestModelDto Body)
        {
            //
            if(string.IsNullOrEmpty(Authorization))
                throw new HttpRequestException("Authorization should not be empty or null");
            if(Body.ItemId is null || Body.ItemId == 0)
                throw new HttpRequestException("ItemId should not be empty or null");
            if(Body.Quantity is null || Body.Quantity == 0)
                throw new HttpRequestException("Quantity should not be empty or null");

            // VALIDATE IF ITEM EXISTS
            Item? Item = ItemAppService.GetEntity(Body.ItemId);
            if(Item is null)
                throw new KeyNotFoundException("Item not found");

            // CHECK IF ITEM HAS SUFFICIENT STOCK
            if(Body.Quantity > Item.Stock)
                throw new HttpRequestException("Insufficient stock");

            // VALIDATE IF CLIENT EXISTS
            long? ClientId = GetClientIdFromAuthorization(Authorization);
            Client? Client = ClientAppService.GetEntity(ClientId);
            if (Client is null)
                throw new KeyNotFoundException("Client not found");

            // CREATE MODEL
            ClientItem ClientItem = new()
            {
                Item = Item,
                Client = Client,
                Quantity = (int)Body.Quantity
            };

            // CREATE CLIENT ITEM
            ClientItemRepository.Create(ClientItem);

            // SAVE ITEM CHANGES
            Item.Stock -= (long)Body.Quantity;
            ItemAppService.UpdateEntity(Item);

            // SAVE CHANGES
            UnitOfWork.SaveChanges();
        }

        private long? GetClientIdFromAuthorization(string? Authorization)
        {
            //
            if (Authorization is null)
                throw new HttpRequestException("Authorization header should not be empty or null");

            long? ClientId;

            // DECODE TOKEN
            try
            {
                string Token = Authorization.Split(" ").Last();
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(Token);
                ClientId = Int64.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == "sub").Value);
            }
            catch (Exception Error)
            {
                throw new Exception($"Error at decoding token. Error: {Error.Message}");
            }

            if (ClientId is null || ClientId == 0)
                throw new Exception("ClientId is null, please contact an administrator");

            return ClientId;
        }
    }
}