using Stripe.Checkout;

namespace Repository.Repository.Abstract;

public interface IPaymentRepository
{
    public Task<Session> GetSession(string email, int totalAmount);
}