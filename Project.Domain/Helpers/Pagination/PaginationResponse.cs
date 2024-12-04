using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Helpers.Pagination
{
    public class PaginationResponse<TItem>
    {
        public PaginationResponse()
        {
        }

        public PaginationResponse(IEnumerable<TItem> items, int currentPage, int limitRow, int resultRow)
        {
            this.items = items;
            this.currentPage = currentPage;
            this.limitRow = limitRow;
            this.resultRow = resultRow;
        }

        public IEnumerable<TItem> items { get; set; }

        public int currentPage { get; set; }

        public int limitRow { get; set; }

        public int resultRow { get; set; }
    }
}
