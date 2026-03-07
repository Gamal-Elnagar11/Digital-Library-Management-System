namespace BookShopping_MVC.Repositories
{
    public interface IUserOrderRepository
    {
        Task<IEnumerable<Order>> UserOrders(bool getAll = false);
        Task TogglePaymentStatus(int orderId);
        Task<Order?> GetOrderById(int id);
        Task ChangeOrderStatus(UpdateOrderStatusModel data);
        Task<IEnumerable<OrderStatus>> GetOrdersStatuses();
    }
}