using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSinhvienDangkyHoc
{
    public partial class FormQLHocPhan : Form
    {
        string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLyHocPhan;Integrated Security=True";
        // Panel + TextBox chi tiết
        Panel pnlChiTiet;
        TextBox txtMaLop, txtTenMon, txtSoTinChi, txtThu, txtTiet, txtGioiHan, txtDaDangKy;
        public FormQLHocPhan()
        {
            InitializeComponent();   // ✔ luôn phải gọi trước

            TaoPanelChiTiet();       // ✔ gọi 1 lần duy nhất

            dgvHocPhan.CellClick += dgvHocPhan_CellClick;
        }

        private void FormQLHocPhan_Load(object sender, EventArgs e)
        {

            panel3.Visible = true;      // Hiện panel "Vui lòng"
            pnlChiTiet.Visible = false; // Ẩn panel chi tiết
        }
        public void ThemDongVaoDGV(string maLop, string tenMonHoc, string soTinChi,
                           string thu, string tiet, string gioiHan, string daDangKy)
        {
            dgvHocPhan.Rows.Add(false, maLop, tenMonHoc, soTinChi, thu, tiet, gioiHan, daDangKy);
        }

        private void btnTaoHocKy_Click(object sender, EventArgs e)
        {
            pnlTaoHocKy.Visible = !pnlTaoHocKy.Visible;
        }


        private void btnThemThuCong_Click(object sender, EventArgs e)
        {
            FormThemThuCong f = new FormThemThuCong(this);
            f.ShowDialog();
        }

        private void dgvHocPhan_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvHocPhan.IsCurrentCellDirty)
            {
                dgvHocPhan.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvHocPhan_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            KiemTraNutXoa();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void KiemTraNutXoa()
        {
            bool coChon = false;

            foreach (DataGridViewRow row in dgvHocPhan.Rows)
            {
                if (row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                    coChon = true;
                    break;
                }
            }

            if (coChon)
            {
                btnXoaCacLop.Enabled = true;
                btnXoaCacLop.BackColor = Color.Red;
                btnXoaCacLop.ForeColor = Color.White;
            }
            else
            {
                btnXoaCacLop.Enabled = false;
                btnXoaCacLop.BackColor = Color.LightGray;
                btnXoaCacLop.ForeColor = Color.Black;
            }
        }

        private void btnXoaCacLop_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show(
       "Bạn có chắc chắn muốn xóa các lớp đã chọn không?",
       "Xác nhận xóa",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Question);

            if (kq == DialogResult.No)
                return;

            for (int i = dgvHocPhan.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow row = dgvHocPhan.Rows[i];

                if (row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                    dgvHocPhan.Rows.RemoveAt(i);
                }
            }

            KiemTraNutXoa();
        }
        private void TestLoad()
        {
            SqlConnection conn = new SqlConnection(connStr);
            string query = "SELECT * FROM HocKy";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
                MessageBox.Show("Đã kết nối và có dữ liệu!");
            else
                MessageBox.Show("Kết nối OK nhưng chưa có dữ liệu!");
        }

        private void bntTest_Click(object sender, EventArgs e)
        {
            TestLoad();
        }

        private void dgvHocPhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvHocPhan.Rows[e.RowIndex];

            // Hiện dữ liệu
            txtMaLop.Text = row.Cells[1].Value?.ToString();
            txtTenMon.Text = row.Cells[2].Value?.ToString();
            txtSoTinChi.Text = row.Cells[3].Value?.ToString();
            txtThu.Text = row.Cells[4].Value?.ToString();
            txtTiet.Text = row.Cells[5].Value?.ToString();
            txtGioiHan.Text = row.Cells[6].Value?.ToString();
            txtDaDangKy.Text = row.Cells[7].Value?.ToString();

            // 🔥 FIX CHÍNH
            panel3.Visible = false;     // Ẩn panel "Vui lòng"
            pnlChiTiet.Visible = true;  // Hiện panel chi tiết
        }
        private void TaoPanelChiTiet()
        {
            pnlChiTiet = new Panel();
            pnlChiTiet.Width = 300;
            pnlChiTiet.Dock = DockStyle.Right;
            pnlChiTiet.BorderStyle = BorderStyle.FixedSingle;
            pnlChiTiet.BackColor = Color.WhiteSmoke; // Thêm màu nền cho dễ nhìn

            this.Controls.Add(pnlChiTiet);
            this.Controls.Add(pnlChiTiet);
            pnlChiTiet.BringToFront();   // 🔥 THÊM DÒNG NÀY
            int currentTop = 20; // Dùng biến tạm để quản lý khoảng cách dọc

            // Hàm tạo Label nhanh (Sử dụng tên đầy đủ để tránh lỗi)
            System.Windows.Forms.Label TaoLabelNhanh(string text, int y)
            {
                System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
                lbl.Text = text;
                lbl.Left = 10;
                lbl.Top = y;
                lbl.Width = 100;
                lbl.Font = new Font("Arial", 9, FontStyle.Bold);
                return lbl;
            }

            // Hàm tạo TextBox nhanh
            TextBox TaoTextBoxNhanh(int y)
            {
                TextBox txt = new TextBox();
                txt.Left = 120;
                txt.Top = y;
                txt.Width = 150;
                txt.ReadOnly = true;
                txt.BackColor = Color.White;
                return txt;
            }

            // --- Thêm các thành phần vào Panel ---

            // 1. Mã lớp
            pnlChiTiet.Controls.Add(TaoLabelNhanh("Mã lớp:", currentTop));
            txtMaLop = TaoTextBoxNhanh(currentTop);
            pnlChiTiet.Controls.Add(txtMaLop);

            // 2. Tên môn
            currentTop += 40;
            pnlChiTiet.Controls.Add(TaoLabelNhanh("Tên môn:", currentTop));
            txtTenMon = TaoTextBoxNhanh(currentTop);
            pnlChiTiet.Controls.Add(txtTenMon);

            // 3. Số tín chỉ
            currentTop += 40;
            pnlChiTiet.Controls.Add(TaoLabelNhanh("Số tín chỉ:", currentTop));
            txtSoTinChi = TaoTextBoxNhanh(currentTop);
            pnlChiTiet.Controls.Add(txtSoTinChi);

            // 4. Thứ
            currentTop += 40;
            pnlChiTiet.Controls.Add(TaoLabelNhanh("Thứ:", currentTop));
            txtThu = TaoTextBoxNhanh(currentTop);
            pnlChiTiet.Controls.Add(txtThu);

            // 5. Tiết
            currentTop += 40;
            pnlChiTiet.Controls.Add(TaoLabelNhanh("Tiết:", currentTop));
            txtTiet = TaoTextBoxNhanh(currentTop);
            pnlChiTiet.Controls.Add(txtTiet);

            // 6. Giới hạn
            currentTop += 40;
            pnlChiTiet.Controls.Add(TaoLabelNhanh("Giới hạn:", currentTop));
            txtGioiHan = TaoTextBoxNhanh(currentTop);
            pnlChiTiet.Controls.Add(txtGioiHan);

            // 7. Đã đăng ký
            currentTop += 40;
            pnlChiTiet.Controls.Add(TaoLabelNhanh("Đã đăng ký:", currentTop));
            txtDaDangKy = TaoTextBoxNhanh(currentTop);
            pnlChiTiet.Controls.Add(txtDaDangKy);
        }
    }
}
