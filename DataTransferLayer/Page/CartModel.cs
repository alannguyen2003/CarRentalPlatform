using DataTransferLayer.DataTransfer;

namespace DataTransferLayer.Page;

public class CartModel
{
    public AccountDto Account { get; set; }

    public CarDto? Car { get; set; } = new CarDto();

}