using System.Diagnostics;
using BusinessObject.Entities;
using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;
using Stripe.Checkout;

namespace CarRentalPlatform.Pages.CustomerPage.Profile;

public class RechargeMoney : PageModel
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPaymentRepository _paymentRepository;
    
    [BindProperty]
    public bool IsLogin { get; set; }
    [BindProperty]
    public AccountEntity UserAccount { get; set; }

    [TempData]
    public string Message { get; set; }
    [BindProperty]
    public int Money { get; set; }

    public RechargeMoney(IAccountRepository accountRepository, IPaymentRepository paymentRepository)
    {
        _accountRepository = accountRepository;
        _paymentRepository = paymentRepository;
    }
    
    public async Task<IActionResult> OnGetAsync()
    {
        IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
        AccountDto accountDto = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
        if (accountDto != null)
        {
            UserAccount = await _accountRepository.GetAccountById(accountDto.Id);
            if (UserAccount == null)
            {
                return Redirect("/Error");
            }
        }
        else
        {
            return Redirect("/login");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostRechargeMoneyAsync()
    {
        ModelState.Clear();
        if (Money <= 20000 || Money >= 1000000000)
        {
            ModelState.AddModelError(string.Empty, "Your money to charge must be bigger than 20,000đ and smaller than 1,000,000,000đ!");
        }
        if (Money % 1000 != 0)
        {
            ModelState.AddModelError(string.Empty, "Your money must be multiple of 1,000đ!");
        }
        if (!ModelState.IsValid || ModelState.Values.Count() != 0)
        {
            return Page();
        }
        AccountDto accountDto = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
        Session session = await _paymentRepository.GetSession(accountDto.Email, Money);
        if (session != null)
        {
            return Redirect(session.Url);
        }
        return Page();
    }
}