namespace BusinessTest.Business.ClientItem.Models
{
    using BusinessTest.Business.Client.Models;
    using BusinessTest.Business.Item.Models;

    public class ClientItemResponseModelDto
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public long ItemId { get; set; }
        public ClientModelDto? Client { get; set; }
        public ItemResponseModelDto? Item { get; set; }
        public int Quantity { get; set; }
        public DateTime? Date { get; set; }
    }
}