
namespace BookShopping_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderDetail>()
         .HasOne(od => od.Order) // الـ OrderDetail له طلب واحد
         .WithMany(o => o.orderDetail) // الطلب له تفاصيل كتير
         .HasForeignKey(od => od.OrderId)
         .OnDelete(DeleteBehavior.Restrict); // السطر ده هو الحل: بيمنع المسح التلقائي المتسلسل
        }
            
        


        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> ordersDetails { get; set; }
        public DbSet<OrderStatus> ordersStatus { get; set; }
        public DbSet<Stock> Stock { get; set; }

    }
}
