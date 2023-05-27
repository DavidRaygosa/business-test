namespace BusinessTest.Business.Item.Models
{
    public class ItemRequestModelDto
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? ImageBase64 { get; set; }
        public long? Stock { get; set; }
    }
}