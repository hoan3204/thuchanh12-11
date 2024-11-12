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
    public partial class Sach : Form
    {
        private string connectionString;

        public Sach()
        {
            InitializeComponent();
            LoadLoaiSach();
            LoadSach();
        }

        private void Sach_Load(object sender, EventArgs e)
        {

        }
        private void LoadLoaiSach()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM LoaiSach";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                cmbLoaiSach.DataSource = dataTable;
                cmbLoaiSach.DisplayMember = "TenLoai";
                cmbLoaiSach.ValueMember = "MaLoai";
            }
        }
        private void LoadSach()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Sach";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvSach.DataSource = dataTable;
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Sach (TenSach, TacGia, GiaBan, SoLuong, MoTa, MaLoai) VALUES (@TenSach, @TacGia, @GiaBan, @SoLuong, @MoTa, @MaLoai)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenSach", txtTenSach.Text);
                    command.Parameters.AddWithValue("@TacGia", txtTacGia.Text);
                    command.Parameters.AddWithValue("@GiaBan", decimal.Parse(txtGiaBan.Text));
                    command.Parameters.AddWithValue("@SoLuong", int.Parse(txtSoLuong.Text));
                    command.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
                    command.Parameters.AddWithValue("@MaLoai", cmbLoaiSach.SelectedValue);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Thêm sách thành công!", "Thông báo");
                LoadSach(); // Tải lại dữ liệu
                ClearTextBoxes(); // Xóa nội dung các TextBox
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Sach SET TenSach = @TenSach, TacGia = @TacGia, GiaBan = @GiaBan, SoLuong = @SoLuong, MoTa = @MoTa, MaLoai = @MaLoai WHERE MaSach = @MaSach";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaSach", txtMaSach.Text);
                    command.Parameters.AddWithValue("@TenSach", txtTenSach.Text);
                    command.Parameters.AddWithValue("@TacGia", txtTacGia.Text);
                    command.Parameters.AddWithValue("@GiaBan", decimal.Parse(txtGiaBan.Text));
                    command.Parameters.AddWithValue("@SoLuong", int.Parse(txtSoLuong.Text));
                    command.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
                    command.Parameters.AddWithValue("@MaLoai", cmbLoaiSach.SelectedValue);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Cập nhật sách thành công!", "Thông báo");
                LoadSach();
                ClearTextBoxes();
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Sach WHERE MaSach = @MaSach";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaSach", txtMaSach.Text);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Xóa sách thành công!", "Thông báo");
                LoadSach();
                ClearTextBoxes();
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Sach WHERE MaLoai = @MaLoai";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaLoai", cmbLoaiSach.SelectedValue);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvSach.DataSource = dataTable;
                }
            }
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
            LoadSach();
        }

        private void ClearTextBoxes()
        {
            txtMaSach.Clear();
            txtTenSach.Clear();
            txtTacGia.Clear();
            txtGiaBan.Clear();
            txtSoLuong.Clear();
            txtMoTa.Clear();
            cmbLoaiSach.SelectedIndex = -1;
        }
        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSach.Rows[e.RowIndex];
                txtMaSach.Text = row.Cells["MaSach"].Value.ToString();
                txtTenSach.Text = row.Cells["TenSach"].Value.ToString();
                txtTacGia.Text = row.Cells["TacGia"].Value.ToString();
                txtGiaBan.Text = row.Cells["GiaBan"].Value.ToString();
                txtSoLuong.Text = row.Cells["SoLuong"].Value.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value.ToString();
                cmbLoaiSach.SelectedValue = row.Cells["MaLoai"].Value;
            }
        }


    }
}
