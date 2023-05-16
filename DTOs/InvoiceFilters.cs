using Microsoft.AspNetCore.Mvc;

namespace InvoiceManager.DTOs
{
    /// <summary>
    /// Filters for invoice
    /// </summary>
    public class InvoiceFilters
    {
        /// <summary>
        /// Search by comment
        /// </summary>
        [FromQuery(Name = "search")]
        public string? Search { get; set; }

        /// <summary>
        /// Sort by total sum
        /// </summary>
        [FromQuery(Name = "sort")]
        public SortEnum Enum { get; set; }
    }
}
