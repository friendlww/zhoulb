using System.Windows.Forms;
using zhoulb.DLL;

namespace zhoulb
{
    public partial class ClientInfo : Form
    {
        private static string _opearType = string.Empty;
        private static int _clientId = 0;
        public ClientInfo()
        {
            InitializeComponent();
        }

        public ClientInfo(string opreaType, int clientId)
        {
            _opearType = opreaType;
            _clientId = clientId;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(txtClientName.Text))
            {
                MessageBox.Show("请输入客户姓名");
            }
            if (string.IsNullOrEmpty(txtClientPhone.Text))
            {
                MessageBox.Show("请输入客户手机");
            }
            //插入
            if (_opearType == "添加")
            {
                var sql = InsertSQL.InsertClient(txtClientName.Text, txtClientPhone.Text);
                MessageBox.Show(InsertData.InsertIntoData(sql) ? "添加客户成功" : "添加客户失败");
            }
            //更新
            if (_opearType == "修改")
            {
                var updatesql = UpdateSQL.UpdateClient(txtClientName.Text, txtClientPhone.Text, _clientId);
                MessageBox.Show(UpdateData.UpdateInfo(updatesql) ? "更新客户成功" : "更新客户失败");
            }
            Close();
        }

        private void ClientInfo_Load(object sender, System.EventArgs e)
        {
            if (_opearType == "修改")
            {
                var selectsql = SelectSQL.SelectClientWithId(_clientId);
                var dt = DLL.BindData.ReturnDataTable(selectsql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtClientName.Text = dt.Rows[0]["ClientName"].ToString();
                    txtClientPhone.Text = dt.Rows[0]["ClientPhone"].ToString();
                }
                button1.Text = "修改";
            }
        }
    }
}
