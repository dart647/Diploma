using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.ViewModels
{
    public class ReportPages
    {
        public ReportModel ReportModel { get; private set; }
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public ReportPages(ReportModel report, int count, int pageNumber, int pageSize)
        {
            ReportModel = report;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
