namespace BusinessTest.Business.ItemStore.Models
{
    using BusinessTest.Business.Item.Models;
    using BusinessTest.Business.Store.Models;

    public class ItemStoreResponseModelDto
    {
        public long Id { get; set; }
        public ItemResponseModelDto? Item { get; set; }
        public StoreResponseModelDto? Store { get; set; }
        public DateTime? Date { get; set; }
    }
}