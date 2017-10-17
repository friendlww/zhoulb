using System;
using System.Windows.Forms;
using zhoulb.DLL;

namespace zhoulb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void 客户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            client.ShowDialog();
        }

        private void 商品ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns[0].Index)
            {
                Picture picture = new Picture(dataGridView1.Rows[e.RowIndex].Cells["PictureID"].Value.ToString());
                picture.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectsql = SelectSQL.SelectStatisticInfo(txtClientName.Text, txtClientPhone.Text, txtProductName.Text,
                txtProductCode.Text, txtRemak.Text);
            DLL.BindData.BingDataGridView(dataGridView1, selectsql);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = "图片";
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1.BeginPrint += printDocument1_BeginPrint;
            printDocument1.PrintPage += printDocument1_PrintPage;

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog
            {
                WindowState = FormWindowState.Maximized,
                Document = printDocument1
            };
            printPreviewDialog.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Print.PrintPage(dataGridView1, e);
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Print.BeginPrint(dataGridView1, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Open the print dialog
            PrintDialog printDialog = new PrintDialog
            {
                Document = printDocument1,
                UseEXDialog = true
            };
            //Get the document
            if (DialogResult.OK == printDialog.ShowDialog())
            {
                printDocument1.Print();
            }
        }
    }

}
