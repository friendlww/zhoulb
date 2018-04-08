using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using zhoulb.DLL;

namespace zhoulb
{
    public partial class ProductInfo : Form
    {
        private static string _opearType = string.Empty;
        private static int _productId = 0;
        private static int _clientId = 0;
        private static string _strguid = string.Empty;
        private static string _pictureId = string.Empty;

        public ProductInfo()
        {
            InitializeComponent();
        }

        public ProductInfo(string opreaType,  int productId)
        {
            _opearType = opreaType;
            _productId = productId;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductName.Text))
            {
                MessageBox.Show("请输入商品名称");
            }
            if (string.IsNullOrEmpty(txtInPrice.Text))
            {
                MessageBox.Show("请输入进货价格");
            }
            if (string.IsNullOrEmpty(txtProductCode.Text))
            {
                MessageBox.Show("请输入商品编号");
            }
            //插入
            if (_opearType == "添加")
            {
                var sql = InsertSQL.InsertProduct(txtProductName.Text, txtInPrice.Text, txtOutPrice.Text,
                    txtProductCode.Text,txtProductNum.Text ,txtRemark.Text, _clientId, _pictureId);
                MessageBox.Show(InsertData.InsertIntoData(sql) ? "添加商品成功" : "添加商品失败");
            }
            //更新
            if (_opearType == "修改")
            {
                var updatesql = UpdateSQL.UpdateProduct(txtProductName.Text, txtInPrice.Text, txtOutPrice.Text,
                    txtProductCode.Text, txtProductNum.Text, txtRemark.Text, _clientId, _pictureId, _productId);
                MessageBox.Show(UpdateData.UpdateInfo(updatesql) ? "更新商品成功" : "更新商品失败");
            }
            Close();
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            //创建一个对话框对象
            OpenFileDialog ofd = new OpenFileDialog();
            //为对话框设置标题
            ofd.Title = "请选择上传的图片";
            //设置筛选的图片格式
            ofd.Filter = "图片格式|*.jpg";
            //设置是否允许多选
            ofd.Multiselect = false;
            //如果你点了“确定”按钮
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //获得文件的完整路径（包括名字后后缀）
                string filePath = ofd.FileName;
                //将文件路径显示在文本框中
                //txtImgUrl.Text = filePath;
                //找到文件名比如“1.jpg”前面的那个“\”的位置
                int position = filePath.LastIndexOf("\\");
                //从完整路径中截取出来文件名“1.jpg”
                string fileName = filePath.Substring(position + 1);
                //读取选择的文件，返回一个流


                //生成图片GUID
                _strguid = Guid.NewGuid().ToString();
                var dPath = @"./Images/" + _strguid+"/";
                //目录不存在，创建目录
                if (!Directory.Exists(dPath))
                {
                    Directory.CreateDirectory(dPath);
                }

                using (Stream stream = ofd.OpenFile())
                {
                    //创建一个流，用来写入得到的文件流（注意：创建一个名为“Images”的文件夹，如果是用相对路径，必须在这个程序的Degug目录下创建
                    //如果是绝对路径，放在那里都行，我用的是相对路径）
                    //using (FileStream fs = new FileStream(@"./Images/" + fileName, FileMode.CreateNew))
                    using (FileStream fs = new FileStream(dPath + fileName, FileMode.CreateNew))
                    {
                        //将得到的文件流复制到写入流中
                        stream.CopyTo(fs);
                        //将写入流中的数据写入到文件中
                        fs.Flush();
                    }
                    //PictrueBox 显示该图片，此时这个图片已经被复制了一份在Images文件夹下，就相当于上传
                    pictureBox1.SizeMode=PictureBoxSizeMode.StretchImage;
                    pictureBox1.ImageLocation = dPath + fileName;
                }
                //插入图片数据入库
                var sql = InsertSQL.InsertPicture(_strguid, fileName, dPath + fileName);
                if (InsertData.InsertIntoData(sql))
                {
                    MessageBox.Show("上传图片成功");
                    _pictureId = _strguid;
                }
                else
                {
                    MessageBox.Show("上传图片失败");
                }
            }
        }

        private void btnBindClient_Click(object sender, EventArgs e)
        {
            Client client = new Client("关联客户");
            if (client.ShowDialog(this) == DialogResult.OK)
            {
                var clientInfo = txtReturnValue.Tag.ToString().Split('|');
                lblClientName.Text = clientInfo[0].ToString();
                lblClientPhone.Text = clientInfo[1].ToString();
                _clientId = int.Parse(clientInfo[2].ToString());
            }
        }

        private void ProductInfo_Load(object sender, EventArgs e)
        {
            if (_opearType == "修改")
            {
                var selectsql = SelectSQL.SelectProductWithId(_productId);
                var dt = BindData.ReturnDataTable(selectsql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //绑定页面数据
                    txtProductName.Text = dt.Rows[0]["ProductName"].ToString();
                    txtProductCode.Text = dt.Rows[0]["ProductCode"].ToString();
                    txtInPrice.Text = dt.Rows[0]["InPrice"].ToString();
                    txtOutPrice.Text = dt.Rows[0]["OutPrice"].ToString();
                    txtProductNum.Text= dt.Rows[0]["ProductNum"].ToString();
                    txtRemark.Text = dt.Rows[0]["Remark"].ToString();
                    _clientId = int.Parse(dt.Rows[0]["ClientInfo"].ToString());
                    _pictureId = dt.Rows[0]["PictureInfo"].ToString();

                    //绑定客户数据
                    selectsql = SelectSQL.SelectClientWithId(_clientId);
                    dt = BindData.ReturnDataTable(selectsql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lblClientName.Text = dt.Rows[0]["ClientName"].ToString();
                        lblClientPhone.Text = dt.Rows[0]["ClientPhone"].ToString();
                    }

                    //绑定图片数据
                    selectsql = SelectSQL.SelectPictureWithId(_pictureId);
                    dt = BindData.ReturnDataTable(selectsql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.ImageLocation = dt.Rows[0]["PicturePath"].ToString();
                    }
                }
                button1.Text = "修改商品";
            }
        }
    }
}
