using Microsoft.Data.SqlClient;
using RealEstateAgency.Core.Entities;

namespace RealEstateAgency.Core.Interfaces
{
    public interface ISortHelper
    {
        public string SortOption { get; set; }
        public SortOrder SortOrder { get; set; }

        void AddColumn(string colName, bool isDefault = false);
        SortableColumn GetColumn(string colName);
        void SetSortOptions(string sortExpression);
    }
}
