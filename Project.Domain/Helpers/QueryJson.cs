using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Helpers
{
    public class QueryJson
    {
        private readonly DbContext db;
        private string query = "SELECT (SELECT @TOP@ @SELECTED@ FROM @ENTITY@ @CONDITIONS@ @ORDER@ FOR JSON PATH, INCLUDE_NULL_VALUES)";
        private SqlParameter[] parameters = new SqlParameter[] { };

        public QueryJson(DbContext db, string entityName)
        {
            this.db = db;

            query = query.Replace("@ENTITY@", entityName);
        }

        /// <summary>
        /// Id = 1 AND Name = 'Zenic'
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public QueryJson Where(string conditions)
        {
            if (string.IsNullOrEmpty(conditions))
            {
                return this;
            }

            query = query.Replace("@CONDITIONS@", "WHERE " + conditions);
            return this;
        }

        public QueryJson Select(params string[] columns)
        {
            if (columns?.Length > 0)
            {
                string selector = string.Join(",", columns);
                query = query.Replace("@SELECTED@", selector);
            }

            return this;
        }

        public QueryJson WithParameters(params SqlParameter[] parameters)
        {
            this.parameters = parameters;
            return this;
        }

        /// <summary>
        /// Id ASC, Name DESC
        /// </summary>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public QueryJson OrderBy(params string[] orderBy)
        {
            query = query.Replace("@ORDER@", "ORDER BY " + string.Join(",", orderBy));
            return this;
        }

        public QueryJson Limit(int limit)
        {
            query = query.Replace("@TOP@", $"TOP({limit})");
            return this;
        }

        public IEnumerable<TResult> GetResults<TResult>()
        {
            CleanQuery();

            string queryResult = db.Database.SqlQuery<string>(query, parameters).FirstOrDefault();

            if (queryResult == null)
            {
                return new List<TResult>();
            }

            return JsonConvert.DeserializeObject<IEnumerable<TResult>>(queryResult);
        }

        private void CleanQuery()
        {
            query = query.Replace("@TOP@", "")
                .Replace("@SELECTED@", "*")
                .Replace("@CONDITIONS@", "")
                .Replace("@ORDER@", "");
        }
    }

    public class QueryJson<TEntity> : QueryJson
    {
        public QueryJson(DbContext db) :
            base(db, ((TableAttribute)typeof(TEntity).GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault()).Name)
        {
        }
    }
}
