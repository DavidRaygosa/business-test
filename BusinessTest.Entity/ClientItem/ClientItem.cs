namespace BusinessTest.Entity.ClientItem
{
    using BusinessTest.Entity.Client;
    using BusinessTest.Entity.Item;

    public class ClientItem
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public Client? Client { get; set; }
        public Item? Item { get; set; }
        public DateTime? Date { get; set; }
        public ClientItem()
        {
            Date = DateTime.UtcNow;
        }
    }
}