using BlazorEcommerce.Shared;
using Blazored.LocalStorage;

namespace BlazorEcommerce.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorages;

        public CartService(ILocalStorageService localStorages)
        {
            _localStorages = localStorages;
        }

        public event Action OnChange;

        public async Task AddToCart(CartItem cartItem)
        {
            List<CartItem>? cart = await GetCart();
            cart.Add(cartItem);

            await _localStorages.SetItemAsync("cart", cart);
            OnChange.Invoke();
        }

        private async Task<List<CartItem>> GetCart()
        {
            var cart = await _localStorages.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            return cart;
        }

        public Task<List<CartItem>> GetCartItems()
        {
            var cart = GetCart();

            return cart;
        }
    }
}
