using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Model;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<ResponseDto<List<Category>>> GetAllAsync();

        Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto category);

        Task<ResponseDto<CategoryDto>> GetByIdAsync(string id);
    }
}
