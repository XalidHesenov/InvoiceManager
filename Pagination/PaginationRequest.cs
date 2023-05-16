﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.Pagination;

/// <summary>
/// Class for request to pagination
/// </summary>
public class PaginationRequest
{
    /// <summary>
    /// Page Number
    /// </summary>
    /// <example>1</example>
    [Range(1, int.MaxValue)]
    [FromQuery(Name ="page")]
    [Required]
    public int Page { get; set; } = 1;


    /// <summary>
    /// Items per page
    /// </summary>
    /// <example>10</example>
    [Range(1, int.MaxValue)]
    [FromQuery(Name = "pageSize")]
    [Required]
    public int PageSize { get; set; } = 10;
}
