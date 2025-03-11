using System.Collections.Generic;

namespace Application.DTOs
{
    public class ServiceCategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class CreateServiceCategoryDto
    {
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class UpdateServiceCategoryDto
    {
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
    }
}
