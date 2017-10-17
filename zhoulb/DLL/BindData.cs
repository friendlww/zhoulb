using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using zhoulb.SqlLite;

namespace zhoulb.DLL
{
    public static class BindData
    {
        public static void BingDataGridView(DataGridView dgView,string sql)
        {
            var returnValue = false;
            var returndtDataTable = new DataTable();
            DbHelper.OpenConnection(sql, "Select", out returnValue, out returndtDataTable);
            dgView.DataSource = returndtDataTable;
        }

        public static DataTable ReturnDataTable(string sql)
        {
            var returnValue = false;
            var returndtDataTable = new DataTable();
            DbHelper.OpenConnection(sql, "Select", out returnValue, out returndtDataTable);
            return returndtDataTable;
        }
    }
}
