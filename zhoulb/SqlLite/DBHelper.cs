using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;


namespace zhoulb.SqlLite
{
    public static class DbHelper
    {
        public static void CreateDb()
        {
            SQLiteConnection.CreateFile(@"DB\sqlite.db");
        }

        /// <summary>
        /// 创建和打开数据库连接
        /// </summary>
        public static void OpenConnection(string sql,string operateType,out bool returnValue,out DataTable returndtDataTable)
        {
            returnValue = false;
            returndtDataTable= new DataTable();
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\DB\\sqlite.db;Pooling=true;"))
            {
                connection.Open();
                switch (operateType)
                {
                    case "Insert":
                    case "Update":
                    case "Delete":
                        returnValue = OpearData(sql, connection);
                        break;
                    case "Select":
                        returndtDataTable = SelectData(sql, connection);
                        break;
                    

                }
                connection.Close();
                SQLiteConnection.ClearAllPools();  //清除连接池之后，数据库文件才能使用
            }
        }

        /// <summary>
        /// 插入、更新、删除数据记录
        /// </summary>
        /// <returns></returns>
        public static bool OpearData(string sql, SQLiteConnection connection)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
            {
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static DataTable SelectData(string sql, SQLiteConnection connection)
        {
            using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}



        





