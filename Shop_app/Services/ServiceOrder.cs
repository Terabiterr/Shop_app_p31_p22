using Shop_app.Models;

namespace Shop_app.Services
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(string userId, List<CartItem> cartItems);
    }
    public class ServiceOrder : IOrderService
    {
        private readonly ShopContext _shopContext;
        public ServiceOrder(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public Task<Order> PlaceOrderAsync(string userId, List<CartItem> cartItems)
        {
            throw new NotImplementedException();
        }
    }
}
