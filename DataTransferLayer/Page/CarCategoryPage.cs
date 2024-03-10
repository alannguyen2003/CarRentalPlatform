using DataTransferLayer.DataTransfer;

namespace DataTransferLayer.Page;

public class CarCategoryPage
{
    public int CurrentPage { get; set; }
    public List<CarDto> Cars { get; set; } = new List<CarDto>();
}