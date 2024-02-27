﻿using CarRentalPlatform.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRentalPlatform.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    
    [BindProperty]
    public bool IsLogin { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
    }
}