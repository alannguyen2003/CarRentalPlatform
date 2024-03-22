using DataTransferLayer.DataTransfer.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Packaging.Core;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.AdminPage.CarManagement;

public class Detail : PageModel
{
    private readonly ICarRepository _carRepository;

    public Detail(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }
    
    [BindProperty]
    public CarResponse? CarResponse { get; set; }
    
    public IActionResult OnGet(int id)
    {
        
        return Page();
    }

    public void ResetFormData(int id)
    {
        CarResponse = null;
    }
}