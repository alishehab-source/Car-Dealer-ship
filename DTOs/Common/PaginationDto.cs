using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.common
{
    public class PaginationDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "رقم الصفحة يجب أن يكون أكبر من صفر")]
        public int Page { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "حجم الصفحة يجب أن يكون بين 1 و 100")]
        public int PageSize { get; set; } = 10;

        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = false;
    }

}
