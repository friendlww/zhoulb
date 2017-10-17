using System;
using System.Windows.Forms;
using zhoulb.DLL;

namespace zhoulb
{
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            var selectsql = SelectSQL.SelectProduct(txtProductName.Text, txtProductCode.Text, txtRemark.Text);
            BindData.BingDataGridView(dgvProductInfo, selectsql);
            for (int i = 0; i < dgvProductInfo.Rows.Count; i++)
            {
                dgvProductInfo.Rows[i].Cells[0].Value = "修改商品";
                dgvProductInfo.Rows[i].Cells[1].Value = "删除商品";
            }
        }

        private void dgvProductInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //修改商品
            if (e.ColumnIndex == dgvProductInfo.Columns[0].Index)
            {
                ProductInfo productInfo = new ProductInfo("修改", int.Parse(dgvProductInfo.Rows[e.RowIndex].Cells["ProductID"].Value.ToString()));
                productInfo.ShowDialog();
            }
            //删除商品
            if (e.ColumnIndex == dgvProductInfo.Columns[1].Index)
            {
                //弹出确认删除框，确认删除后，调用删除语句
                if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var sql = DeleteSQL.DeleteProductWithId(int.Parse(dgvProductInfo.Rows[e.RowIndex].Cells["ProductID"].Value.ToString()));
                    MessageBox.Show(DeleteData.DeleteInfo(sql) ? "删除商品成功" : "删除商品失败");
                }
            }
        }

        private void 添加商品ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductInfo productInfo = new ProductInfo("添加",0);
            productInfo.ShowDialog();
        }
    }
}
