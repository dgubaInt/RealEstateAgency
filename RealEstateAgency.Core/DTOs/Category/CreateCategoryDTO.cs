namespace RealEstateAgency.Core.DTOs.Category
{
    public class CreateCategoryDTO
    {
        public string CategoryName { get; set; }
        public Guid ParentCategoryId { get; set; }
        public int Position { get; set; }
    }
}
