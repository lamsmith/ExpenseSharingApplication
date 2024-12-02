using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Domain.Paging
{
    public record PageRequest
    {
        [FromQuery]
        public bool UsePaging { get; init; } = true;

        [FromQuery]
        public int PageSize { get; init; } = 20;

        [FromQuery]
        public int Page { get; init; } = 1;

        [FromQuery]
        public string? SortBy { get; init; }

        [FromQuery]
        public string? Keyword { get; init; }

        [FromQuery]
        public bool IsAscending { get; init; }
    }
}
