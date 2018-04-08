using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zhoulb
{
    public static class Print
    {
        private static StringFormat _strFormat; //Used to format the grid rows.
        private static ArrayList _arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        private static ArrayList _arrColumnWidths = new ArrayList();//Used to save column widths
        private static int _iCellHeight = 0; //Used to get/set the datagridview cell height
        private static int _iTotalWidth = 0; //
        private static int _iRow = 0;//Used as counter
        private static bool _bFirstPage = false; //Used to check whether we are printing first page
        private static bool _bNewPage = false;// Used to check whether we are printing a new page
        private static int _iHeaderHeight = 0; //Used for the header height

        public static void PrintPage(DataGridView dgView, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height

                if (_bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dgView.Columns)
                    {
                        if (GridCol.Name == "PictureID" || GridCol.Name == "Column6") continue;
                        iTmpWidth = (int) (Math.Floor((double) ((double) GridCol.Width/
                                                                (double) _iTotalWidth*(double) _iTotalWidth*
                                                                ((double) e.MarginBounds.Width/(double) _iTotalWidth))));

                        _iHeaderHeight = (int) (e.Graphics.MeasureString(GridCol.HeaderText,
                            GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        // Save width and height of headres
                        _arrColumnLefts.Add(iLeftMargin);
                        _arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }

                //Loop till all the grid rows not get printed
                while (_iRow <= dgView.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dgView.Rows[_iRow];
                    //Set the cell height
                    _iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allo more rows to print

                    if (iTopMargin + _iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        _bNewPage = true;
                        _bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (_bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString("商品明细打印", new Font(dgView.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("商品明细打印", new Font(dgView.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate, new Font(dgView.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new Font(dgView.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("商品明细打印", new Font(new Font(dgView.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dgView.Columns)
                            {
                                if (GridCol.Name == "PictureID" || GridCol.Name == "Column6") continue;
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)_arrColumnLefts[iCount], iTopMargin,
                                    (int)_arrColumnWidths[iCount], _iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)_arrColumnLefts[iCount], iTopMargin,
                                    (int)_arrColumnWidths[iCount], _iHeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font,
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)_arrColumnLefts[iCount], iTopMargin,
                                    (int)_arrColumnWidths[iCount], _iHeaderHeight), _strFormat);
                                iCount++;
                            }
                            _bNewPage = false;
                            iTopMargin += _iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        //foreach (DataGridViewCell Cel in GridRow.Cells)
                        for (int i = 1; i < GridRow.Cells.Count-1; i++)
                        {
                            if (GridRow.Cells[i].Value != null)
                            {
                                e.Graphics.DrawString(GridRow.Cells[i].Value.ToString(), GridRow.Cells[i].InheritedStyle.Font,
                                            new SolidBrush(GridRow.Cells[i].InheritedStyle.ForeColor),
                                            new RectangleF((int)_arrColumnLefts[iCount], (float)iTopMargin,
                                            (int)_arrColumnWidths[iCount], (float)_iCellHeight), _strFormat);
                            }
                            //Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)_arrColumnLefts[iCount],
                                    iTopMargin, (int)_arrColumnWidths[iCount], _iCellHeight));

                            iCount++;
                        }
                    }
                    _iRow++;
                    iTopMargin += _iCellHeight;
                }

                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void BeginPrint(DataGridView dgView, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                _strFormat = new StringFormat();
                _strFormat.Alignment = StringAlignment.Near;
                _strFormat.LineAlignment = StringAlignment.Center;
                _strFormat.Trimming = StringTrimming.EllipsisCharacter;

                _arrColumnLefts.Clear();
                _arrColumnWidths.Clear();
                _iCellHeight = 0;
                _iRow = 0;
                _bFirstPage = true;
                _bNewPage = true;

                // Calculating Total Widths
                _iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dgView.Columns)
                {
                    _iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
