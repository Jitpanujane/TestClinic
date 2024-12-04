using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Helpers.Pagination
{
    public class QueryResultModel
    {
        public int Id { get; set; }

        public string Item { get; set; }

        public int Count_row { get; set; }
    }
}
