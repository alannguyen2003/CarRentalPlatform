using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PaymentWebhook.Settings;
using Repository.Repository.Abstract;
using Stripe;
using Stripe.Checkout;

namespace PaymentWebhook.Controllers;

[Route("api/webhook")]
public class WebHookController : ControllerBase
{
    private static string ClientDomain = "https://localhost:7129/";
    private readonly ILogger<WebHookController> _logger;
    private readonly IOptions<StripeSettings> _stripeOptions;
    private readonly IAccountRepository _accountRepository;
    private readonly IPaymentRepository _paymentRepository;

    public WebHookController(IOptions<StripeSettings> stripeOptions, ILogger<WebHookController> logger,
                            IAccountRepository accountRepository, IPaymentRepository paymentRepository)
    {
        _logger = logger;
        _stripeOptions = stripeOptions;
        _accountRepository = accountRepository;
        _paymentRepository = paymentRepository;
    }
    
    [HttpPost("webhook")]
    public async Task<IActionResult> WebHook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _stripeOptions.Value.WHKey
            );

            // Handle the event
            if (stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                //Do stuff
                if (paymentIntent != null)
                {
                    _logger.LogInformation(paymentIntent.Id);
                }
            }
            else if(stripeEvent.Type == Events.CheckoutSessionAsyncPaymentFailed)
            {
                var checkoutSession = stripeEvent.Data.Object as Session;
                if (checkoutSession != null && !string.IsNullOrEmpty(checkoutSession.CustomerEmail))
                {
                    _logger.LogInformation("Add credit failed");
                }
            }
            else if(stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var checkoutSession = stripeEvent.Data.Object as Session;
                var accountDto = await _accountRepository.GetAccountWithEmail(checkoutSession.CustomerEmail);
                var account = await _accountRepository.GetAccountById(accountDto.Id);
                account.WalletBalance += (int) checkoutSession.LineItems.Data[0].AmountTotal;
                await _accountRepository.UpdateAccount(account);
                _logger.LogInformation("Add credit success" + checkoutSession.LineItems.Data[0].AmountTotal);
            }
            else
            {
                _logger.LogInformation($"Unhandled Events: {stripeEvent.Type}");
            }
            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest(e.StripeError.Message);
        }
    }
}