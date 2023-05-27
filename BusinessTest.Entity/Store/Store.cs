namespace BusinessTest.Entity.Store
{
    using BusinessTest.Entity.ItemStore;

    public class Store
    {
        public long Id { get; set; }
        public string? Branch { get; set; }
        public string? Direction { get; set; }
        public ICollection<ItemStore>? ItemStore { get; set; }
    }
}