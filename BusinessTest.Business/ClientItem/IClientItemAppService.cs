namespace BusinessTest.Business.ClientItem
{
    using BusinessTest.Business.ClientItem.Models;

    public interface IClientItemAppService
    {
        void Delete(long? Id);
        void Update(long? Id, ClientItemRequestModelDto Body);
        ClientItemResponseModelDto Get(long? Id);
        List<ClientItemResponseModelDto> GetAll(long? ItemId, long? ClientId);
        void Create(string? Authorization, ClientItemRequestModelDto Body);
    }
}