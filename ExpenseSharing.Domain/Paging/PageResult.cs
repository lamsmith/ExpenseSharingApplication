﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Domain.Paging
{
    public record PaginatedList<T> where T : notnull
    {
        public IEnumerable<T> Items { get; init; } = Enumerable.Empty<T>();
        public long TotalItems { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
        public long TotalPages => (long)Math.Ceiling(TotalItems / (double)PageSize);
    }
}
