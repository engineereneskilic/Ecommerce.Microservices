using System.Text.Json;
using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<ResponseDto<BasketDto>> GetBasket(string userid)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(userid);

            if (string.IsNullOrEmpty(existBasket))
            {
                return ResponseDto<BasketDto>.Fail(new List<string> { "basket not found" },404);
            }

            return ResponseDto<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket),200);

        }

        public async Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId,JsonSerializer.Serialize(basketDto));

            return status ? ResponseDto<bool>.Success(true,204) : ResponseDto<bool>.Fail(new List<string> { "Basket could not update or save" },500);
        }

        public async Task<ResponseDto<bool>> Delete(string userid)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(userid);
            return status ? ResponseDto<bool>.Success(true,204) : ResponseDto<bool>.Fail(new List<string> { "basket not found" }, 404);

        }
    }
}
