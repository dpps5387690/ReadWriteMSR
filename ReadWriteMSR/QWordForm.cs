using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadWriteMSR
{
    public partial class QWordForm : Form
    {
        public static UInt64 OutValue = 0;
        UInt64 OldValue = 0;
        public QWordForm()
        {
            InitializeComponent();
        }

        private void dgv_init(DataGridView dgv, TextBox tb, int bitshift)
        {
            // 列头隐藏 
            dgv.ColumnHeadersVisible = false;
            // 行头隐藏 
            dgv.RowHeadersVisible = false;
            dgv.Font = new System.Drawing.Font("Consolas", 9F);
            dgv.ScrollBars = ScrollBars.None;
            dgv.BackgroundColor = SystemColors.ButtonHighlight;
            dgv.BorderStyle = BorderStyle.Fixed3D;


            for (int i = 0; i < 8; i++)
            {
                DataGridViewTextBoxColumn cloumn = new DataGridViewTextBoxColumn();
                int width = dgv.Width;
                cloumn.Name = i.ToString();
                cloumn.Width = width / 8;
                cloumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns.Add(cloumn);
            }
            int height = dgv.Height;

            dgv.Rows.Add();
            for (int i = 0; i < 8; i++)
            {
                dgv.Rows[0].Cells[i].Value = (bitshift + 7 - i);
                dgv.Rows[0].Cells[i].Style.BackColor = Color.DarkGray;
            }
            dgv.Rows[0].Height = height / 2;
            dgv.Rows.Add();
            dgv.Rows[1].Height = height / 2;

            Debug.WriteLine(OutValue.ToString("X"));

            tb.Text = String.Format("{0:X02}", (Byte)(OutValue >> bitshift));
            Debug.WriteLine(tb.Text);

            Byte bytevalue = Convert.ToByte(tb.Text.ToString(), 16);
            for (int i = 0; i < 8; i++)
                dgv.Rows[1].Cells[7 - i].Value = String.Format("{0:X01}", (bytevalue >> i) & 0x1);

            dgv.ClearSelection();

        }

        private void QWordForm_Load(object sender, EventArgs e)
        {
            //dgv_init(dgv_qword0, tb_qword0, 0);
            //dgv_init(dgv_qword1, tb_qword1, 8);
            //dgv_init(dgv_qword2, tb_qword2, 16);
            //dgv_init(dgv_qword3, tb_qword3, 24);
            //dgv_init(dgv_qword4, tb_qword4, 32);
            //dgv_init(dgv_qword5, tb_qword5, 40);
            //dgv_init(dgv_qword6, tb_qword6, 48);
            //dgv_init(dgv_qword7, tb_qword7, 56);
            OldValue = OutValue;
            for (int Row = 0, index = 0 ;Row< tableLayoutPanel1.RowCount; Row++)
            {
                for (int Col = 0; Col < tableLayoutPanel1.ColumnCount && (Row == 0 || Row == 2); Col++, index++)
                {
                    Control nowtb = tableLayoutPanel1.GetControlFromPosition(Col, Row);
                    DataGridView dgv = nowtb as DataGridView;
                    nowtb = tableLayoutPanel1.GetControlFromPosition(Col, Row + 1);
                    TextBox tb = nowtb as TextBox;
                    Debug.WriteLine(index);
                    dgv_init(dgv, tb, 64 - (index + 1) * 8);
                }
            }
        }
        private void WriteToOutValue(TextBox tb)
        {
            int shiftbit = Convert.ToInt32(tb.Tag);
            OutValue &= ~((UInt64)0xFF << (shiftbit * 8));
            OutValue |= Convert.ToUInt64(tb.Text, 16) << (shiftbit * 8);
        }
        private void dgv_QWord_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.RowIndex > 0 && e.ColumnIndex > -1)
            {
                //Debug.WriteLine(e.RowIndex + " " + e.ColumnIndex);
                switch (Convert.ToUInt16(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                {
                    case 0:
                        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                        break;
                    case 1:
                        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                        break;
                    default:
                        break;
                }

                Byte byte_data = 0x00;
                for (int i = 0; i < 8; i++)
                {
                    byte_data |= (Byte)(Convert.ToByte(dgv.Rows[1].Cells[7 - i].Value) << i);
                }

                int dgv_Row = tableLayoutPanel1.GetCellPosition(dgv).Row;
                int dgv_Col = tableLayoutPanel1.GetCellPosition(dgv).Column;

                TextBox tb = tableLayoutPanel1.GetControlFromPosition(dgv_Col, dgv_Row + 1) as TextBox;

                tb.Text = String.Format("{0:X02}", byte_data);
                //WriteToOutValue(tb);
            }
        }

        private string oldString = "00"; //全域變數，儲存先前的textBox1.Text
        private void tb_QWord_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;      

            //避免因為重設相同的值給textBox1.Text時而造成事件發生
            if (tb.Text.Equals(oldString)) return;
            //如果只輸入負號先不處理
            if (tb.Text.Equals("-")) return;
            //存輸入的值
            UInt32 lTemp = 0;
            //暫存游標的位置
            int selectStartTemp = tb.SelectionStart;
            //暫存轉換前textBox1的字串長度
            int oriStringLength = tb.Text.Length;
            //先將之前轉換過的字串還原成數值並存入lTemp
            //若無法正常轉換則有兩種狀況要處理
            if (!System.Text.RegularExpressions.Regex.IsMatch(tb.Text, @"^[0-9A-Fa-f]+$"))
            {
                //textBox1為空字串時無法轉換，則保留空字串值
                if (tb.Text.Equals("")) oldString = "";
                //textBox為非數字字串時則還原先前的值
                else
                {
                    tb.Text = oldString;
                    tb.Select(selectStartTemp - 1, 1);
                }
                return;
            }
            //轉換成金錢格式，注意這裡是使用N0，使用沒有小數點的金錢格式
            tb.Text = string.Format("{0:X02}", Convert.ToByte(tb.Text, 16));
            //若轉換後的字串長度比轉換前大，表示多了逗號，需要修正游標位置
            //if (textBox1.Text.Length > oriStringLength)
            //  selectStartTemp += textBox1.Text.Length - oriStringLength;
            oldString = tb.Text;
            //指定游標到正確位置
            tb.SelectionStart = selectStartTemp;
            tb.Select(selectStartTemp, 1);

            int tb_Row = tableLayoutPanel1.GetCellPosition(tb).Row;
            int tb_Col = tableLayoutPanel1.GetCellPosition(tb).Column;

            DataGridView dgv = tableLayoutPanel1.GetControlFromPosition(tb_Col, tb_Row - 1) as DataGridView;

            if (System.Text.RegularExpressions.Regex.IsMatch(tb.Text, @"^[0-9A-Fa-f]+$"))//判斷是否為16進制
            {
                Byte buffer = Convert.ToByte(tb.Text.ToString(), 16);
                for (int i = 0; i < 8; i++)
                    dgv.Rows[1].Cells[7 - i].Value = String.Format("{0:X01}", (buffer >> i) & 0x1);
            }
            else
                tb.Text = "FF";

            WriteToOutValue(tb);
        }

        private void tb_QWord_MouseDown(object sender, MouseEventArgs e)
        {
            TextBox tb = sender as TextBox;
            int selectStartTemp = tb.SelectionStart;
            tb.Select(selectStartTemp, 1);
        }

        private void tb_QWord_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;
            int selectStartTemp = tb.SelectionStart;
            tb.Select(selectStartTemp, 1);
        }

        private void bt_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;

            switch (Convert.ToUInt16(bt.Tag))
            {
                case 0://confirm
                    DialogResult = DialogResult.OK;
                    this.Close();
                    break;
                case 1://cancel
                    OutValue = OldValue;
                    DialogResult = DialogResult.Cancel;
                    this.Close();
                    break;
                default:
                    break;
            }

        }
    }
}
