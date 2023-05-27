namespace BusinessTest.Business.Item.Models
{
    public class ItemResponseModelDto
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public long Stock { get; set; }
    }
}