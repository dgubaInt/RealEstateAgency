using Microsoft.Data.SqlClient;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;

namespace RealEstateAgency.Service.Helpers
{
    public class SortHelper : ISortHelper
    {
        public string SortOption { get; set; } = "";
        public SortOrder SortOrder { get; set; } = SortOrder.Unspecified;

        private List<SortableColumn> _sortableColumns = new List<SortableColumn>();

        public void AddColumn(string colName, bool isDefault = false)
        {
            SortableColumn? column = _sortableColumns.Where(c => c.Name.ToLower() == colName.ToLower()).SingleOrDefault();

            if (column is null)
            {
                _sortableColumns.Add(new SortableColumn { Name = colName });
            }

            if (isDefault || _sortableColumns.Count() == 1)
            {
                SortOption = colName;
                SortOrder = SortOrder.Ascending;
            }
        }

        public SortableColumn GetColumn(string colName)
        {
            SortableColumn? column = _sortableColumns.Where(c => c.Name.ToLower() == colName.ToLower()).SingleOrDefault();

            if (column is null)
            {
                column = new SortableColumn { Name = colName };
                _sortableColumns.Add(column);
            }

            return column;
        }
        public void SetSortOptions(string sortExpression)
        {
            if (sortExpression == "")
            {
                sortExpression = SortOption.ToLower();
            }

            foreach (var sortableColumn in _sortableColumns)
            {
                sortableColumn.SortIcon = "";
                sortableColumn.SortExpression = sortableColumn.Name.ToLower();

                if (sortExpression == sortableColumn.Name.ToLower())
                {
                    SortOption = sortableColumn.Name;
                    SortOrder = SortOrder.Ascending;
                    sortableColumn.SortExpression = sortableColumn.Name + "_desc";
                    sortableColumn.SortIcon = "fas fa-arrow-down";
                }
                else if (sortExpression == sortableColumn.Name.ToLower() + "_desc")
                {
                    SortOption = sortableColumn.Name; ;
                    SortOrder = SortOrder.Descending;
                    sortableColumn.SortExpression = sortableColumn.Name; ;
                    sortableColumn.SortIcon = "fas fa-arrow-up";
                }
            }
        }
    }
}
