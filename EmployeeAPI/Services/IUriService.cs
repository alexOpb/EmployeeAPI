using System;
using EmployeeAPI.Filter;

namespace EmployeeAPI.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}