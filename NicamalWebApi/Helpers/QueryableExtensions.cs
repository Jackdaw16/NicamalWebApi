﻿using System.Linq;
 using NicamalWebApi.Models;

namespace NicamalWebApi.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, Pagination pagination)
        {
            return queryable.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize);
        }
    }
}