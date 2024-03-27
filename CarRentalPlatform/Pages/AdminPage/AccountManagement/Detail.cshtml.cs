﻿using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using DataTransferLayer.DataTransfer.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.AdminPage.AccountManagement;

public class Detail : PageModel
{
    private readonly IAccountRepository _accountRepository;

    public Detail(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    [BindProperty] 
    public AccountResponse? AccountResponse { get; set; }
    [BindProperty]
    public string Message { get; set; }
    [BindProperty]
    public bool IsLogin { get; set; }

    [BindProperty]
    public AccountDto AccountDto { get; set; }

    public IActionResult OnGet(int id)
    {
        IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
        AccountDto = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
        if (IsLogin == false)
        {
            return RedirectToPage("/login");
        }
        else if (AccountDto.Role != 1)
        {
            return RedirectToPage("/index");
        }
        ResetFormData(id);
        return Page();
    }

    public IActionResult OnPost()
    {
        return Redirect("./edit?id=" + AccountResponse.Id);
    }

    public void ResetFormData(int id)
    {
        AccountResponse = _accountRepository.GetAccountForRequest(id).Result;
    }
}