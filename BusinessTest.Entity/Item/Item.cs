namespace BusinessTest.Entity.Item
{
    using System.ComponentModel.DataAnnotations.Schema;
    using BusinessTest.Entity.ItemStore;
    using BusinessTest.Entity.ClientItem;

    public class Item
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public long Stock { get; set; }
        public ICollection<ItemStore>? ItemStore { get; set; }
        public ICollection<ClientItem>? ClientItem { get; set; }
    }
}