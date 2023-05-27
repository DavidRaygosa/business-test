namespace BusinessTest.Entity.ItemStore
{
    using BusinessTest.Entity.Item;
    using BusinessTest.Entity.Store;

    public class ItemStore
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long StoreId { get; set; }
        public Item? Item { get; set; }
        public Store? Store { get; set; }
        public DateTime? Date { get; set; }
        public ItemStore()
        {
            Date = DateTime.UtcNow;
        }
    }
}