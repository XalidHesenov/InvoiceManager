using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

/// <summary>
/// Enum for sorting
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortEnum
{
    /// <summary>
    /// Sort by ascending
    /// </summary>
    ASC,

    /// <summary>
    /// Sort by descending 
    /// </summary>
    DESC,
}
namespace InvoiceManager.DTOs
{
    /// <summary>
    /// Filters for customers
    /// </summary>
    public class CustomerFilters
    {
        /// <summary>
        /// Search by name
        /// </summary>
        [FromQuery(Name = "search")]
        public string? Search { get; set; }

        /// <summary>
        /// Sort by adress
        /// </summary>
        [FromQuery(Name = "sort")]
        public SortEnum Enum { get; set; }
    }
}
