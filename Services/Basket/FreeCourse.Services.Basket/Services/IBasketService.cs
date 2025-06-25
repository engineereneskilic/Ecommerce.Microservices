using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<ResponseDto<BasketDto>> GetBasket(string userid);

        Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basketDto);

        Task<ResponseDto<bool>> Delete(string userid);
    }
}
