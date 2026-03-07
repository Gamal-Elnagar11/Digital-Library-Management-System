using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Identity;

namespace BookShopping_MVC.Repositories
{
    public class UserOrderRepository : IUserOrderRepository 
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserOrderRepository(ApplicationDbContext db,
                                  UserManager<IdentityUser> userManager,
                                  IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Order>> UserOrders(bool getAll = false)
        {
            var orders = _db.orders
                                .Include(a => a.orderStatus)
                                .Include(a => a.orderDetail)
                                .ThenInclude(a => a.Book)
                                .ThenInclude(a => a.Genre)
                                .AsQueryable();
            if(!getAll)
            {                                   // here condation on user the user send false and this condation will execute  (return data for user only not admin)
                var userId = GetUserId();      // here get userId from server or session or identity  not database 
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged-in");

                orders = orders.Where( a => a.UserId == userId);
                return await orders.ToListAsync();
            }

            return await orders.ToListAsync();
              // here return all data from DB this point only user access this point
        }





        public async Task TogglePaymentStatus(int orderId)
        {
            var order = await _db.orders.FindAsync(orderId);
            if(order == null)
            {
                throw new InvalidOperationException($"order with id: {orderId} does not found");
            }
            order.IsPaid = !order.IsPaid;
            await _db.SaveChangesAsync();
        }




        public async Task<Order?> GetOrderById(int id)
        {
            return await _db.orders.FindAsync(id);
        }

        public async Task<IEnumerable<OrderStatus>> GetOrdersStatuses()
        {
            return await _db.ordersStatus.ToListAsync();
        }


        public async Task ChangeOrderStatus(UpdateOrderStatusModel data)
        {
            var order = await _db.orders.FindAsync(data.OrderId);
            if (order == null)
                throw new InvalidOperationException($"order with id: {data.OrderId} does not found");
            order.OrderStatusId = data.OrderStatusId;
            await _db.SaveChangesAsync();
        }








        // this method Display user Orders 
        /*  public async Task<IEnumerable<Order>> UserOrders()
          {
              var userId = GetUserId();

              if (string.IsNullOrWhiteSpace(userId))
                  throw new Exception("User is not logged id");

              var orders = await _db.orders
                                      .Include(b => b.orderStatus)
                                      .Include(a => a.orderDetail)
                                      .ThenInclude(a => a.Book)
                                      .ThenInclude(a => a.Gener)
                                      .Where(a => a.UserId == userId)
                                      .ToListAsync();

              return orders;  // return object has list by all the above 
          }
           */


        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
            // here return id for user from DB to recorded you operation by your id 
        }

    }

}
