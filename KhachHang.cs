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

namespace WindowsFormsApp3
{
    public partial class KhachHang : Form
    {
        private string connectionString;

        public KhachHang()
        {
            InitializeComponent();
            LoadKhachHang();
        }


        private void KhachHang_Load(object sender, EventArgs e)
        {

        }
        private void LoadKhachHang()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM KhachHang";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvKhachHang.DataSource = dataTable;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO KhachHang (TenKhachHang, DiaChi, SoDienThoai, Email) VALUES (@TenKhachHang, @DiaChi, @SoDienThoai, @Email)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenKhachHang", txtTenKhachHang.Text);
                    command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    command.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);
                    command.Parameters.AddWithValue("@Email", txtEmail.Text);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo");
                LoadKhachHang();
                ClearTextBoxes();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE KhachHang SET TenKhachHang = @TenKhachHang, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai, Email = @Email WHERE MaKhachHang = @MaKhachHang";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaKhachHang", txtMaKhachHang.Text);
                    command.Parameters.AddWithValue("@TenKhachHang", txtTenKhachHang.Text);
                    command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    command.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);
                    command.Parameters.AddWithValue("@Email", txtEmail.Text);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo");
                LoadKhachHang();
                ClearTextBoxes();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM KhachHang WHERE MaKhachHang = @MaKhachHang";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaKhachHang", txtMaKhachHang.Text);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Xóa khách hàng thành công!", "Thông báo");
                LoadKhachHang();
                ClearTextBoxes();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM KhachHang WHERE TenKhachHang LIKE @TenKhachHang OR SoDienThoai LIKE @SoDienThoai";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenKhachHang", "%" + txtTenKhachHang.Text + "%");
                    command.Parameters.AddWithValue("@SoDienThoai", "%" + txtSoDienThoai.Text + "%");
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvKhachHang.DataSource = dataTable;
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
            LoadKhachHang();
        }

        private void ClearTextBoxes()
        {
            txtMaKhachHang.Clear();
            txtTenKhachHang.Clear();
            txtDiaChi.Clear();
            txtSoDienThoai.Clear();
            txtEmail.Clear();
        }
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                txtMaKhachHang.Text = row.Cells["MaKhachHang"].Value.ToString();
                txtTenKhachHang.Text = row.Cells["TenKhachHang"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
            }
        }

    }
}
