using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;
using CarRentalPlatform.Configuration;
using BuildObject.Entities;
using DataTransferLayer.DataTransfer;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using BusinessObject.Entities;

namespace CarRentalPlatform.Pages.CustomerPage.Profile
{
    public class IndexModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

		[BindProperty]
		public bool IsLogin { get; set; }
		[BindProperty]
        public AccountEntity UserAccount { get; set; }

        [TempData]
        public string Message { get; set; }

        public IndexModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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
                return Redirect("/Login");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            bool isChangeDetected = false;
            var currentAccount = await _accountRepository.GetAccountById(UserAccount.Id);

            // Validate FirstName
            if (string.IsNullOrEmpty(UserAccount.FirstName) || UserAccount.FirstName.Length < 3 || !Regex.IsMatch(UserAccount.FirstName, @"^[A-Z][a-z]+(?: [A-Z][a-z]+)*$"))
            {
                ModelState.AddModelError("UserAccount.FirstName", "FirstName must be at least 3 characters, start with a capital letter, and contain no special characters or numbers.");

            }
            else if (!UserAccount.FirstName.Equals(currentAccount.FirstName))
            {
                currentAccount.FirstName = UserAccount.FirstName;
                isChangeDetected = true;
            }

            // Validate LastName
            if (string.IsNullOrEmpty(UserAccount.LastName) || UserAccount.LastName.Length < 3 || !Regex.IsMatch(UserAccount.LastName, @"^[A-Z][a-z]+(?: [A-Z][a-z]+)*$"))
            {
                ModelState.AddModelError("UserAccount.LastName", "FirstName must be at least 3 characters, start with a capital letter, and contain no special characters or numbers.");
            }
            else if (!UserAccount.LastName.Equals(currentAccount.LastName))
            {
                currentAccount.LastName = UserAccount.LastName;
                isChangeDetected = true;
            }

            // Validate PhoneNumber
            if (string.IsNullOrEmpty(UserAccount.PhoneNumber) || !Regex.IsMatch(UserAccount.PhoneNumber, @"^\d{10}$"))
            {
                ModelState.AddModelError("UserAccount.PhoneNumber", "Phone number must be 10 digits.");
            }
            else if (!UserAccount.PhoneNumber.Equals(currentAccount.PhoneNumber))
            {
                currentAccount.PhoneNumber = UserAccount.PhoneNumber;
                isChangeDetected = true;
            }

            if (UserAccount.Gender != currentAccount.Gender)
            {
                currentAccount.Gender = UserAccount.Gender;
                isChangeDetected = true;
            }

            // Validate Password
            if (!string.IsNullOrEmpty(UserAccount.Password) && UserAccount.Password.Contains(" "))
            {
                ModelState.AddModelError("UserAccount.Password", "Password cannot contain spaces.");
                
            }
            else if (!UserAccount.Password.Equals(currentAccount.Password))
            {
                currentAccount.Password = UserAccount.Password;
                isChangeDetected = true;
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!isChangeDetected)
            {
                Message = "No changes were detected in the form.";
                return Page();
            }
            // Update Data
            await _accountRepository.UpdateAccount(currentAccount);
            Message = "Profile updated successfully.";
            return RedirectToPage(new { Updated = true });
        }
    }
}
