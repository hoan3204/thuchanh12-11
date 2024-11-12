using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class HeThong : Form
    {
        public HeThong()
        {
            InitializeComponent();
        }

        private void DangXuatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Đăng Xuất", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                DangNhap dangNhapForm = new DangNhap();
                dangNhapForm.Show();
            }
        }

        // Xử lý sự kiện Thoát
        private void ThoatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Thoát", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // Mở form Quản Lý Sách
        private void QuanLySachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sach sachForm = new Sach();
            sachForm.Show();
        }

        // Mở form Quản Lý Loại Sách
        private void QuanLyLoaiSachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoaiSach loaiSachForm = new LoaiSach();
            loaiSachForm.Show();
        }

        // Mở form Quản Lý Khách Hàng
        private void QuanLyKhachHangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KhachHang khachHangForm = new KhachHang();
            khachHangForm.Show();
        }

        // Mở form Quản Lý Hóa Đơn
        private void QuanLyHoaDonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HoaDon hoaDonForm = new HoaDon();
            hoaDonForm.Show();
        }

        // Mở form Thống Kê
        private void ThongKeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongKe thongKeForm = new ThongKe();
            thongKeForm.Show();
        }
       
    }
}
