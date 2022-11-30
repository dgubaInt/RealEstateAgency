namespace RealEstateAgency.Core.DTOs.Category
{
    public class CategoryDTO
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public int Position { get; set; }
    }
}
