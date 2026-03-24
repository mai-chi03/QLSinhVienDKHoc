using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLSinhvienDangkyHoc
{
    public partial class FormSuaHocPhan : Form
    {
        // Các biến lưu trữ dữ liệu để Form chính có thể truy cập sau khi sửa
        public string TenMon, SoTinChi, Thu, Tiet, GioiHan, DaDangKy;

        // Các ô nhập liệu
        TextBox txtTenMon, txtSoTinChi, txtThu, txtTiet, txtGioiHan, txtDaDangKy;

        // Constructor nhận dữ liệu từ Form cha
        public FormSuaHocPhan(string tenMon, string soTinChi, string thu, string tiet, string gioiHan, string daDangKy)
        {
            InitializeComponent(); // Giữ lại để tránh lỗi Designer

            this.TenMon = tenMon;
            this.SoTinChi = soTinChi;
            this.Thu = thu;
            this.Tiet = tiet;
            this.GioiHan = gioiHan;
            this.DaDangKy = daDangKy;

            SetupFormUI();
        }

        // Fix lỗi CS1061 nếu Designer lỡ gọi sự kiện Load
        private void FormSuaHocPhan_Load(object sender, EventArgs e) { }

        private void SetupFormUI()
        {
            this.Text = "Cập nhật thông tin học phần";
            this.Size = new Size(400, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            int top = 30;
            int labelLeft = 30;
            int inputLeft = 160;

            // Hàm hỗ trợ tạo nhanh Label và TextBox
            void AddRow(string labelText, ref TextBox targetTxt, string value, int y)
            {
                Label lbl = new Label { Text = labelText, Left = labelLeft, Top = y + 3, AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Regular) };
                targetTxt = new TextBox { Left = inputLeft, Top = y, Width = 180, Text = value, Font = new Font("Segoe UI", 10), BorderStyle = BorderStyle.FixedSingle };
                this.Controls.Add(lbl);
                this.Controls.Add(targetTxt);
            }

            AddRow("Tên môn học:", ref txtTenMon, TenMon, top); top += 45;
            AddRow("Số tín chỉ:", ref txtSoTinChi, SoTinChi, top); top += 45;
            AddRow("Thứ:", ref txtThu, Thu, top); top += 45;
            AddRow("Tiết học:", ref txtTiet, Tiet, top); top += 45;
            AddRow("Giới hạn SV:", ref txtGioiHan, GioiHan, top); top += 45;
            AddRow("Đã đăng ký:", ref txtDaDangKy, DaDangKy, top); top += 55;

            // Nút Lưu
            Button btnLuu = new Button
            {
                Text = "Lưu thay đổi",
                Left = 60,
                Top = top,
                Width = 120,
                Height = 40,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnLuu.Click += (s, e) => {
                // Gán lại giá trị mới vào các biến public
                TenMon = txtTenMon.Text;
                SoTinChi = txtSoTinChi.Text;
                Thu = txtThu.Text;
                Tiet = txtTiet.Text;
                GioiHan = txtGioiHan.Text;
                DaDangKy = txtDaDangKy.Text;

                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            // Nút Hủy
            Button btnHuy = new Button
            {
                Text = "Hủy bỏ",
                Left = 200,
                Top = top,
                Width = 120,
                Height = 40,
                BackColor = Color.FromArgb(189, 195, 199),
                FlatStyle = FlatStyle.Flat
            };
            btnHuy.Click += (s, e) => this.Close();

            this.Controls.Add(btnLuu);
            this.Controls.Add(btnHuy);
        }
    }
}