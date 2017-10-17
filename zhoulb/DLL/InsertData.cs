using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using zhoulb.SqlLite;

namespace zhoulb.DLL
{
    public static class InsertData
    {
        public static bool InsertIntoData(string sql)
        {
            var returnValue = false;
            var returndtDataTable = new DataTable();
            DbHelper.OpenConnection(sql, "Insert", out returnValue, out returndtDataTable);
            return returnValue;
        }
    }
}
