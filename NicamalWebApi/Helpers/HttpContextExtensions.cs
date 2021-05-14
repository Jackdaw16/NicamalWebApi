﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace NicamalWebApi.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task AddPaginationParams<T>(this HttpContext httpContext, IQueryable<T> queryable,
            int countRegistryPerPage)
        {
            double count = await queryable.CountAsync();
            double countPage = Math.Ceiling(count / countRegistryPerPage);
            httpContext.Response.Headers.Add("PageCount", countPage.ToString());
        }
    }
}