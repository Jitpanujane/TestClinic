using Newtonsoft.Json;
using Project.Domain.Helpers.Pagination.Installer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Project.Domain.Helpers.Pagination
{
    public class PaginationProvider : IDisposable
    {
        private DbContext context;
        private int page = 1;
        private int pagelimit = 15;
        private string filter = "";
        private string name = "";
        private string sortBy = "";
        private string sortType = "";

        public PaginationProvider(DbContext context)
        {
            this.context = context;
            Starter.go(this.context);
        }

        private bool HasSort => !string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(sortType);

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
                context = null;
            }
        }

        public PaginationResponse<TResponse> QueryPagination<TResponse>(string TableOrView, SQLWhere where = null, params SQLSort[] sort)
        {
            GetParamsRequest();

            string get_where = where == null ? "" : where.GetWhere;
            string get_column = name;
            string get_value = filter.Replace("'", "''");
            string get_sort = HasSort ? GetSort(new SQLSort(sortBy, sortType)) : GetSort(sort);

            var result = context.Database.SqlQuery<QueryResultModel>("EXEC [dbo].[s_PaginationJSON] @TableOrView,@Where,@FilterColumn,@FilterValue,@Page,@Limit_page,@Sortby",
                   new SqlParameter("@TableOrView", TableOrView),
                   new SqlParameter("@Where", get_where),
                   new SqlParameter("@FilterColumn", get_column),
                   new SqlParameter("@FilterValue", get_value),
                   new SqlParameter("@Page", page),
                   new SqlParameter("@Limit_page", pagelimit),
                   new SqlParameter("@Sortby", get_sort)
               ).FirstOrDefault();

            var response = new PaginationResponse<TResponse>(new TResponse[] { }, page, pagelimit, result.Count_row);

            if (result.Item != null)
            {
                response.items = JsonConvert.DeserializeObject<IEnumerable<TResponse>>(result.Item);
            }

            return response;
        }

        public PaginationResponse<TResponse> QueryPagination<TEntity, TResponse>(SQLWhere where = null, params SQLSort[] sort)
        {
            var entity = ((TableAttribute)typeof(TEntity).GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault());
            return QueryPagination<TResponse>(entity.Name, where, sort);
        }

        public PaginationResponse<TEntity> QueryPagination<TEntity>(SQLWhere where = null, params SQLSort[] sort)
        {
            return QueryPagination<TEntity, TEntity>(where, sort);
        }

        private void GetParamsRequest()
        {
            HttpRequest Request = HttpContext.Current.Request;
            page = string.IsNullOrEmpty(Request["page"]) ? 1 : int.Parse(Request["page"]);
            pagelimit = string.IsNullOrEmpty(Request["limit"]) ? 15 : int.Parse(Request["limit"]);
            filter = string.IsNullOrEmpty(Request["filter"]) ? "" : Request["filter"];
            name = string.IsNullOrEmpty(Request["name"]) ? "" : Request["name"];
            sortBy = string.IsNullOrEmpty(Request["sortBy"]) ? "" : Request["sortBy"];
            sortType = string.IsNullOrEmpty(Request["sortType"]) ? "" : Request["sortType"];
        }

        private string GetSort(params SQLSort[] sort)
        {
            string res = "";
            int i = 0;
            foreach (var sorts in sort)
            {
                if (i == 0)
                    res += sorts.GetSort;
                else
                    res += " , " + sorts.GetSort;
                i++;
            }
            return res;
        }
    }
}
