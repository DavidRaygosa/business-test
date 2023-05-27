namespace BusinessTest.Data.Repositories.Item
{
    using BusinessTest.Data.BusinessDbContext;
    using BusinessTest.Entity.Item;

    public class ItemRepository : IItemRepository
    {
        private readonly BusinessTestDbContext Context;
        public ItemRepository(BusinessTestDbContext context) => (Context) = (context);

        public void Delete(Item Item) => Context.Item?.Remove(Item);
        public List<Item>? GetAll() => Context.Item?.ToList();
        public void Update(Item Item) => Context.Item?.Update(Item);
        public Item? Get(long Id) => Context.Item?.Where(x => x.Id == Id).FirstOrDefault();
        public void Create(Item Item) => Context.Item?.Add(Item);
    }
}