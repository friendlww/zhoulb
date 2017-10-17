using System;
using System.Windows.Forms;
using zhoulb.DLL;

namespace zhoulb
{
    public partial class Client : Form
    {
        private static string _opearType = string.Empty;
        public Client()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        public Client(string opreaType)
        {
            _opearType = opreaType;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            //添加商品界面选择客户
            if (opreaType == "关联客户")
            {
                button1.Visible = true;
                button1.DialogResult = DialogResult.OK;
            }
        }

        private void 添加客户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientInfo clientinfo = new ClientInfo("添加",0);
            clientinfo.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //修改客户
            if (e.ColumnIndex == dgvClientInfo.Columns[0].Index)
            {
                ClientInfo clientinfo = new ClientInfo("修改", int.Parse(dgvClientInfo.Rows[e.RowIndex].Cells["ClientID"].Value.ToString()));
                clientinfo.ShowDialog();
            }
            //删除客户
            if (e.ColumnIndex == dgvClientInfo.Columns[1].Index)
            {
                //弹出确认删除框，确认删除后，调用删除语句
                if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var sql = DeleteSQL.DeleteClientWithId(int.Parse(dgvClientInfo.Rows[e.RowIndex].Cells["ClientID"].Value.ToString()));
                    MessageBox.Show(DeleteData.DeleteInfo(sql) ? "删除客户成功" : "删除客户失败");
                }
            }
        }

        private void btnSearchClient_Click(object sender, EventArgs e)
        {
            var selectsql = SelectSQL.SelectClient(txtClientName.Text, txtClientPhone.Text);
            DLL.BindData.BingDataGridView(dgvClientInfo, selectsql);
            for (int i = 0; i < dgvClientInfo.Rows.Count; i++)
            {
                dgvClientInfo.Rows[i].Cells[0].Value = "修改客户";
                dgvClientInfo.Rows[i].Cells[1].Value = "删除客户";
            }
            if (_opearType == "关联客户")
            {
                dgvClientInfo.Columns[0].Visible = false;
                dgvClientInfo.Columns[1].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductInfo f1 = (ProductInfo)this.Owner;
            //然后这边随便找个form1窗体里的控件，label,textbox什么都行，假设form1里面有个label1
            TextBox la = f1.Controls["txtReturnValue"] as TextBox;

            if (dgvClientInfo.CurrentRow != null)
            {
                int index = dgvClientInfo.CurrentRow.Index; //取得选中行的索引  
                la.Tag =
                    dgvClientInfo.Rows[index].Cells[2].Value.ToString() + "|" +
                    dgvClientInfo.Rows[index].Cells[3].Value.ToString() + "|" +
                    int.Parse(dgvClientInfo.Rows[index].Cells[4].Value.ToString());
                //tag是object类型，所以字符串，数组，数字，datatable什么都可以传
            }
        }
    }
}
