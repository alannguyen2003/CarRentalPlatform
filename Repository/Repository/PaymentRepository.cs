using Repository.Repository.Abstract;
using Stripe.Checkout;

namespace Repository.Repository;

public class PaymentRepository : IPaymentRepository
{
    private static string ClientDomain = "https://localhost:7129/";
    
    public async Task<Session> GetSession(string email, int totalAmount)
    {
        
        var options = new SessionCreateOptions
        {
            SuccessUrl = ClientDomain + $"success?money=" + totalAmount,
            CancelUrl = ClientDomain + "checkout-fail",
            LineItems = new List<SessionLineItemOptions>(),
            Mode = "payment",
            PaymentIntentData = new SessionPaymentIntentDataOptions(),
            CustomerEmail = email
        };
        var sessionLineItem = new SessionLineItemOptions()
        {
            PriceData = new SessionLineItemPriceDataOptions()
            {
                UnitAmount = totalAmount,
                Currency = "vnd",
                ProductData = new SessionLineItemPriceDataProductDataOptions()
                {
                    Name = "Package: Nạp tiền",
                    Description = "Nạp tiền vào hệ thống",
                }
            },
            Quantity = 1
        };
        options.LineItems.Add(sessionLineItem);
        var service = new SessionService();
        Session session = await service.CreateAsync(options);
        return session;
    }
}