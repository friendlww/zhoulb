using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using zhoulb.DLL;

namespace zhoulb
{
    public partial class Picture : Form
    {
        public Picture(string pictureId)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            ShowPicture(pictureId);
        }

        private void ShowPicture(string pictureId)
        {
            if (string.IsNullOrEmpty(pictureId) == false)
            {
                var selectsql = SelectSQL.SelectPictureWithId(pictureId);
                var dt = DLL.BindData.ReturnDataTable(selectsql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    BackgroundImage = Image.FromFile(dt.Rows[0]["PicturePath"].ToString());
                }
            }
        }
    }
}
