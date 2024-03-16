using DataTransferLayer.DataTransfer.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.AdminPage.CarManagement;

public class Index : PageModel
{
    private readonly ICarRepository _carRepository;

    public Index(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    [BindProperty] 
    public List<CarResponse> ListCars { get; set; }

    public IActionResult OnGet()
    {
        ListCars = _carRepository.GetAllCarResponses().Result;
        return null;
    }
}