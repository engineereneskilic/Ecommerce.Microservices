using System.Data;
using Dapper;
using FreeCourse.Shared.Dtos;
using Npgsql;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }


        public async Task<ResponseDto<List<Models.Discount>>> GetAll()
        {
            var disocunts = await _dbConnection.QueryAsync<Models.Discount>("Select * From discount");

            return ResponseDto<List<Models.Discount>>.Success(disocunts.ToList(),200);

        }

        public async Task<ResponseDto<Models.Discount>> GetById(int id)
        {
            var disocunt = (await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount WHERE id=@Id",new { Id = id })).SingleOrDefault();

            if (disocunt == null)
            {
                return ResponseDto<Models.Discount>.Fail("Discount not found",404);
            }

            return ResponseDto<Models.Discount>.Success(disocunt,200);
        }

        public async Task<ResponseDto<NoContent>> Save(Models.Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount(userid,rate,code) VALUES(@UserId,@Rate,@Code)",discount); 
            
            if(saveStatus > 0)
            {
                return ResponseDto<NoContent>.Success(204);
            }

            return ResponseDto<NoContent>.Fail("an error occured while adding",500);// kullanıcı kaynaklanan hata
        }

        public async Task<ResponseDto<NoContent>> Update(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("Update discount set userid=@UserId, code=@Code, rate=@Rate WHERE Id=@id", new { Id = discount.Id,UserId=discount.UserId,Code=discount.Code,Rate=discount.Rate});

            if(status > 0)
            {
                return ResponseDto<NoContent>.Success(204);
            }

            return ResponseDto<NoContent>.Fail("Discount not found",404);
        }

        public async Task<ResponseDto<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = await _dbConnection.QueryAsync<Models.Discount>("Select * From discount WHERE userid=@UserId and code=@Code", new { UserId = userId, Code = code });

            var hasDiscount = discount.FirstOrDefault();

            if (hasDiscount != null)
            {
                return ResponseDto<Models.Discount>.Fail("Discount not found",404);
            }

            return ResponseDto<Models.Discount>.Success(hasDiscount,200);
        }


        public async Task<ResponseDto<NoContent>> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("Delete From discount WHERE id=@Id",new { Id = id });

            return status > 0 ? ResponseDto<NoContent>.Success(204) : ResponseDto<NoContent>.Fail("Discount not found",404);

        }

    
    }
}
