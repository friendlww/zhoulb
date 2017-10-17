using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zhoulb.SqlLite;

namespace zhoulb.DLL
{
    public static class DeleteData
    {
        public static bool DeleteInfo(string sql)
        {
            var returnValue = false;
            var returndtDataTable = new DataTable();
            DbHelper.OpenConnection(sql, "Delete", out returnValue, out returndtDataTable);
            return returnValue;
        }
    }
}
