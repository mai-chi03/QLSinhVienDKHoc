using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLSinhvienDangkyHoc
{
    public partial class FormQLHocPhan : Form
    {
        string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLyHocPhan;Integrated Security=True";
        Panel pnlChiTiet;

        // Các nhãn hiển thị thông tin chi tiết
        Label lblTenMon, lblMaMon, lblMaLop, lblGiaoVien, lblHeDaoTao, lblNgayBD, lblNgayKT, lblLichHoc;

        public FormQLHocPhan()
        {
            InitializeComponent();
            TaoPanelChiTiet();
        }

        private void TaoPanelChiTiet()
        {
            // 1. Khởi tạo Panel nền trắng
            pnlChiTiet = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20) // Tạo khoảng đệm nội bộ cho panel
            };
            panel3.Controls.Add(pnlChiTiet);
            pnlChiTiet.BringToFront();

            // 2. Tiêu đề "Thông tin chi tiết"
            Label lblMainTitle = new Label
            {
                Text = "Thông tin chi tiết",
                Location = new Point(25, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64)
            };
            pnlChiTiet.Controls.Add(lblMainTitle);

            int top = 75;
            int labelX = 30;
            int valueX = 145;

            // Hàm tạo tiêu đề mục (Bên trái, Bold)
            Label TaoTieuDe(string t, int y) => new Label
            {
                Text = t,
                Left = labelX,
                Top = y,
                AutoSize = true,
                ForeColor = Color.FromArgb(44, 62, 80),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            // Hàm tạo nội dung (Bên phải, Chữ đen, Căn lề phải)
            Label TaoGiaTri(int y) => new Label
            {
                Text = "...",
                Left = valueX,
                Top = y,
                Width = 180,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                TextAlign = ContentAlignment.TopRight
            };

            // 3. Danh sách thông tin hiển thị
            pnlChiTiet.Controls.Add(TaoTieuDe("Môn học:", top)); lblTenMon = TaoGiaTri(top); pnlChiTiet.Controls.Add(lblTenMon); top += 35;
            pnlChiTiet.Controls.Add(TaoTieuDe("Mã môn học:", top)); lblMaMon = TaoGiaTri(top); pnlChiTiet.Controls.Add(lblMaMon); top += 35;
            pnlChiTiet.Controls.Add(TaoTieuDe("Mã lớp học:", top)); lblMaLop = TaoGiaTri(top); pnlChiTiet.Controls.Add(lblMaLop); top += 35;
            pnlChiTiet.Controls.Add(TaoTieuDe("Giáo viên:", top)); lblGiaoVien = TaoGiaTri(top); lblGiaoVien.Text = "Chưa có"; pnlChiTiet.Controls.Add(lblGiaoVien); top += 35;
            pnlChiTiet.Controls.Add(TaoTieuDe("Hệ đào tạo:", top)); lblHeDaoTao = TaoGiaTri(top); lblHeDaoTao.Text = "Cử nhân tài năng"; pnlChiTiet.Controls.Add(lblHeDaoTao); top += 35;
            pnlChiTiet.Controls.Add(TaoTieuDe("Ngày bắt đầu:", top)); lblNgayBD = TaoGiaTri(top); lblNgayBD.Text = "01/01/2026"; pnlChiTiet.Controls.Add(lblNgayBD); top += 35;
            pnlChiTiet.Controls.Add(TaoTieuDe("Ngày kết thúc:", top)); lblNgayKT = TaoGiaTri(top); lblNgayKT.Text = "31/12/2026"; pnlChiTiet.Controls.Add(lblNgayKT); top += 35;
            pnlChiTiet.Controls.Add(TaoTieuDe("Lịch học:", top)); lblLichHoc = TaoGiaTri(top); pnlChiTiet.Controls.Add(lblLichHoc);

            // 4. Thiết lập các nút bấm KHÔNG DÍNH VIỀN
            int btnW = 100;
            int btnH = 35;
            int spacing = 15;
            int bottomMargin = 30; // Khoảng cách an toàn so với đáy panel

            // Căn giữa 2 nút dựa trên chiều rộng panel
            int startX = (panel3.Width - (btnW * 2 + spacing)) / 2;
            int startY = panel3.Height - btnH - bottomMargin; // Tự động đẩy lên cách viền dưới

            Button btnXoa_UI = new Button
            {
                Text = "Xóa",
                Size = new Size(btnW, btnH),
                Location = new Point(startX, startY),
                BackColor = Color.FromArgb(189, 189, 189),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            Button btnSua_UI = new Button
            {
                Text = "Sửa",
                Size = new Size(btnW, btnH),
                Location = new Point(startX + btnW + spacing, startY),
                BackColor = Color.FromArgb(63, 81, 181),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            // Gán sự kiện cho nút bấm
            btnSua_UI.Click += (s, ev) => moFormSua();
            btnXoa_UI.Click += (s, ev) => {
                if (dgvHocPhan.CurrentRow != null && MessageBox.Show("Xóa lớp học này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    dgvHocPhan.Rows.Remove(dgvHocPhan.CurrentRow);
            };

            pnlChiTiet.Controls.Add(btnXoa_UI);
            pnlChiTiet.Controls.Add(btnSua_UI);
        }

        // --- XỬ LÝ SỰ KIỆN ---

        private void btnThemThuCong_Click(object sender, EventArgs e)
        {
            FormThemThuCong f = new FormThemThuCong(this); // Đã kích hoạt lệnh mở form
            f.ShowDialog();
        }

        private void moFormSua()
        {
            if (dgvHocPhan.CurrentRow != null)
            {
                var r = dgvHocPhan.CurrentRow;
                FormSuaHocPhan fSua = new FormSuaHocPhan(
                    r.Cells[2].Value.ToString(), r.Cells[3].Value.ToString(),
                    r.Cells[4].Value.ToString(), r.Cells[5].Value.ToString(),
                    r.Cells[6].Value.ToString(), r.Cells[7].Value.ToString());

                if (fSua.ShowDialog() == DialogResult.OK)
                {
                    r.Cells[2].Value = fSua.TenMon;
                    r.Cells[3].Value = fSua.SoTinChi;
                    r.Cells[4].Value = fSua.Thu;
                    r.Cells[5].Value = fSua.Tiet;
                    r.Cells[6].Value = fSua.GioiHan;
                    CapNhatPanelChiTiet(r);
                }
            }
        }

        private void CapNhatPanelChiTiet(DataGridViewRow r)
        {
            lblTenMon.Text = r.Cells[2].Value?.ToString();
            lblMaLop.Text = r.Cells[1].Value?.ToString();
            lblMaMon.Text = r.Cells[1].Value?.ToString().Split('.')[0];
            lblLichHoc.Text = $"{r.Cells[4].Value}, Tiết {r.Cells[5].Value}";
        }

        private void dgvHocPhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            pnlChiTiet.Visible = true;
            CapNhatPanelChiTiet(dgvHocPhan.Rows[e.RowIndex]);
        }

        // --- HÀM HỖ TRỢ VÀ FIX LỖI DESIGNER ---
        private void FormQLHocPhan_Load(object sender, EventArgs e) { if (pnlChiTiet != null) pnlChiTiet.Visible = false; }
        private void btnXoaCacLop_Click(object sender, EventArgs e) { }
        private void btnTaoHocKy_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void dgvHocPhan_CellValueChanged(object sender, DataGridViewCellEventArgs e) { }
        private void dgvHocPhan_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvHocPhan.IsCurrentCellDirty) dgvHocPhan.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        public void ThemDongVaoDGV(string ma, string ten, string tin, string thu, string tiet, string gh, string dk)
        {
            dgvHocPhan.Rows.Add(false, ma, ten, tin, thu, tiet, gh, dk); //
        }
    }
}