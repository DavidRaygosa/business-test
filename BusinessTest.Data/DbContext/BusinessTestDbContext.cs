namespace BusinessTest.Data.BusinessDbContext
{
    using BusinessTest.Entity.Item;
    using BusinessTest.Entity.ItemStore;
    using BusinessTest.Entity.Client;
    using BusinessTest.Entity.ClientItem;
    using BusinessTest.Entity.Store;
    using Microsoft.EntityFrameworkCore;
    using BusinessTest.Entity.Authentication;
    using BusinessTest.Entity.Role;
    using BusinessTest.Entity.Permission;

    public class BusinessTestDbContext : DbContext
    {
        public BusinessTestDbContext(DbContextOptions<BusinessTestDbContext> options) : base(options) { }
        public DbSet<Client>? Client { get; set; }
        public DbSet<Item>? Item { get; set; }
        public DbSet<Store>? Store { get; set; }
        public DbSet<ItemStore>? ItemStore { get; set; }
        public DbSet<ClientItem>? ClientItem { get; set; }
        public DbSet<Authentication>? Authentication { get; set; }
        public DbSet<Role>? Role { get; set; }
        public DbSet<Permission>? Permission { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasMany<ItemStore>(s => s.ItemStore)
                .WithOne(i => i.Item)
                .HasForeignKey(i => i.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Item>()
                .HasMany<ClientItem>(c => c.ClientItem)
                .WithOne(i => i.Item)
                .HasForeignKey(i => i.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Store>()
                .HasMany<ItemStore>(i => i.ItemStore)
                .WithOne(i => i.Store)
                .HasForeignKey(i => i.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Client>()
                .HasMany<ClientItem>(c => c.ClientItem)
                .WithOne(c => c.Client)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Client>()
                .HasOne<Authentication>(a => a.Authentication)
                .WithOne(c => c.Client)
                .HasForeignKey<Authentication>(a => a.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role>()
                .HasMany<Client>(c => c.Clients)
                .WithOne(r => r.Role)
                .HasForeignKey(r => r.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}