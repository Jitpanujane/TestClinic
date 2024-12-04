using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Helpers.Pagination.Installer
{
    public static class Starter
    {
        public static void go(DbContext db)
        {
            //Check have s_Pagination
            var check_have = db.Database.SqlQuery<CountStoreProcedureResultModel>(Scripts.query_sp_pagination).FirstOrDefault();
            if (check_have.count_store == 0)
            {
                //Install s_Pagination
                int create = db.Database.ExecuteSqlCommand(Scripts.create_sp_pagination);
                if (create == 0)
                {
                    throw new Exception("Filed for create store EXEC_s_Pagination.sql");
                }
            }

            //Check have s_PaginationJSON
            check_have = db.Database.SqlQuery<CountStoreProcedureResultModel>(Scripts.query_sp_pagination_json).FirstOrDefault();
            if (check_have.count_store == 0)
            {
                //Install s_PaginationJSON
                int create = db.Database.ExecuteSqlCommand(Scripts.create_sp_pagination_json);
                if (create == 0)
                {
                    throw new Exception("Filed for create store EXEC_s_PaginationJSON.sql");
                }
            }
        }
    }
}
