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
using System.Data.SqlClient;
namespace WindowsFormsApp3
{
    public partial class HoaDon : Form
    {
        private string connectionString;

        public HoaDon()
        {
            InitializeComponent();
            LoadHoaDon();
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {

        }
        private void LoadHoaDon()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM HoaDon";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvHoaDon.DataSource = dataTable;
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO HoaDon (NgayLap, MaKhachHang) VALUES (@NgayLap, @MaKhachHang)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NgayLap", DateTime.Parse(txtNgayLap.Text));
                    command.Parameters.AddWithValue("@MaKhachHang", txtMaKhachHang.Text);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Thêm hóa đơn thành công!", "Thông báo");
                LoadHoaDon();
                ClearTextBoxes();
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE HoaDon SET NgayLap = @NgayLap, MaKhachHang = @MaKhachHang WHERE MaHoaDon = @MaHoaDon";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaHoaDon", txtMaHoaDon.Text);
                    command.Parameters.AddWithValue("@NgayLap", DateTime.Parse(txtNgayLap.Text));
                    command.Parameters.AddWithValue("@MaKhachHang", txtMaKhachHang.Text);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Cập nhật hóa đơn thành công!", "Thông báo");
                LoadHoaDon();
                ClearTextBoxes();
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaHoaDon", txtMaHoaDon.Text);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo");
                LoadHoaDon();
                ClearTextBoxes();
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM HoaDon WHERE MaKhachHang = @MaKhachHang OR NgayLap = @NgayLap";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaKhachHang", txtMaKhachHang.Text);
                    command.Parameters.AddWithValue("@NgayLap", DateTime.Parse(txtNgayLap.Text));
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvHoaDon.DataSource = dataTable;
                }
            }
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvHoaDon.Rows[e.RowIndex];
                txtMaHoaDon.Text = row.Cells["MaHoaDon"].Value.ToString();
                txtNgayLap.Text = row.Cells["NgayLap"].Value.ToString();
                txtMaKhachHang.Text = row.Cells["MaKhachHang"].Value.ToString();

                LoadChiTietHoaDon(txtMaHoaDon.Text); // Gọi hàm để tải chi tiết hóa đơn
            }
        }

        private void LoadChiTietHoaDon(string maHoaDon)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM ChiTietHoaDon WHERE MaHoaDon = @MaHoaDon";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvChiTietHoaDon.DataSource = dataTable;
                }
            }
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
            LoadHoaDon();
            dgvChiTietHoaDon.DataSource = null;
        }

        private void ClearTextBoxes()
        {
            txtMaHoaDon.Clear();
            txtNgayLap.Clear();
            txtMaKhachHang.Clear();
        }

    }
}
