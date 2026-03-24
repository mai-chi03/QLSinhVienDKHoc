using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLSinhvienDangkyHoc
{
    public partial class FormThemThuCong : Form
    {
        string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLyHocPhan;Integrated Security=True";
        FormQLHocPhan frmCha;

        // Constructor nhận Form Cha
        public FormThemThuCong(FormQLHocPhan f)
        {
            InitializeComponent();
            frmCha = f;
        }

        // Constructor mặc định (Phòng trường hợp gọi sai cách)
        public FormThemThuCong()
        {
            InitializeComponent();
        }

        private void FormThemThuCong_Load(object sender, EventArgs e)
        {
            txtMaLopMonHoc.Text = "";
            txtMaLopMonHoc.ForeColor = Color.Black; // Chữ 'a' khi gõ màu đen

            // Cấu hình nhãn "Mã lớp môn học" màu đen chuẩn
            label9.Text = "Mã lớp môn học";
            label9.ForeColor = Color.Black;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            // Vị trí ban đầu: Placeholder trong ô
            label9.Left = txtMaLopMonHoc.Left + 5;
            label9.Top = txtMaLopMonHoc.Top + 5;

            label9.BringToFront();
            label9.Click += (s, ev) => { txtMaLopMonHoc.Focus(); };

            // Đăng ký sự kiện (nếu trong Designer chưa có)
            txtMaLopMonHoc.TextChanged += TxtMaLopMonHoc_TextChanged;
            txtMaLopMonHoc.Enter += TxtMaLopMonHoc_Enter;
            txtMaLopMonHoc.Leave += TxtMaLopMonHoc_Leave;
        }

        private void TxtMaLopMonHoc_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaLopMonHoc.Text))
            {
                // Khi có chữ: Bay lên và thẳng hàng với Sĩ Số
                label9.Top = txtMaLopMonHoc.Top - 20;
                label9.Left = txtMaLopMonHoc.Left;
                label9.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                label9.ForeColor = Color.Black;
            }
        }

        private void TxtMaLopMonHoc_Enter(object sender, EventArgs e)
        {
            label9.Top = txtMaLopMonHoc.Top - 20;
            label9.Left = txtMaLopMonHoc.Left;
            label9.ForeColor = Color.Black;
        }

        private void TxtMaLopMonHoc_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLopMonHoc.Text))
            {
                label9.Top = txtMaLopMonHoc.Top + 5;
                label9.Left = txtMaLopMonHoc.Left + 5;
                label9.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                label9.ForeColor = Color.Black;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string maLop = txtMaLopMonHoc.Text.Trim();
            string tenMonHoc = cboMonHoc.Text;
            string thu = cboThu.Text;
            string tiet = txtTiet.Text.Trim();
            string gioiHan = txtSiSo.Text.Trim();
            string soTinChi = "";
            string daDangKy = "0";

            // Validate dữ liệu
            if (string.IsNullOrEmpty(maLop)) { MessageBox.Show("Nhập mã lớp!"); return; }
            if (tenMonHoc == "") { MessageBox.Show("Chọn môn học!"); return; }

            switch (tenMonHoc)
            {
                case "Trí tuệ nhân tạo": soTinChi = "3"; break;
                case "Lập trình C#": soTinChi = "3"; break;
                case "Cơ sở dữ liệu": soTinChi = "3"; break;
                case "Mạng máy tính": soTinChi = "2"; break;
                default: soTinChi = "3"; break;
            }

            // --- FIX LỖI NULL TẠI ĐÂY ---
            if (frmCha != null)
            {
                frmCha.ThemDongVaoDGV(maLop, tenMonHoc, soTinChi, thu, tiet, gioiHan, daDangKy);
                MessageBox.Show("Thêm lớp học thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi hệ thống: Không tìm thấy Form chính để cập nhật!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}