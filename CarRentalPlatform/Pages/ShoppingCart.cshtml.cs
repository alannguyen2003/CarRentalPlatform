using BuildObject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CarRentalPlatform.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CartItem> CartItems
        {
            get
            {
                var sessionData = _httpContextAccessor.HttpContext.Session.GetString("CartItems");
                return sessionData != null ? JsonSerializer.Deserialize<List<CartItem>>(sessionData) : new List<CartItem>();
            }
            set
            {
                _httpContextAccessor.HttpContext.Session.SetString("CartItems", JsonSerializer.Serialize(value));
            }
        }

        public class CartItem
        {
            public CarEntity Car { get; set; }
            public string StartDate { get; set; }
            public string AccountName { get; set; }
            public string DriverLicense { get; set; }
        }

        public void OnGet()
        {
        }
    }
}
