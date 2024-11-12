using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WindowsFormsApp3
{
    public partial class LoaiSach : Form
    {
        string connectionString = "Server=127.0.0.1;Database=hoan12345;User Id=root;Password=hoan12345;";

        public LoaiSach()
        {
            InitializeComponent();
            LoadLoaiSach(); // Tải dữ liệu loại sách khi mở form
        }

        // Phương thức tải dữ liệu từ bảng LoaiSach
        private void LoadLoaiSach()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM LoaiSach";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvLoaiSach.DataSource = dataTable;
            }
        }

        // Thêm loại sách mới
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO LoaiSach (TenLoai, MoTa) VALUES (@TenLoai, @MoTa)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TenLoai", txtTenLoai.Text);
                        command.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Thêm loại sách thành công!", "Thông báo");
                    LoadLoaiSach();
                    ClearTextBoxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
            }
        }


        // Sửa loại sách đã chọn
        private void btnSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE LoaiSach SET TenLoai = @TenLoai, MoTa = @MoTa WHERE MaLoai = @MaLoai";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaLoai", txtMaLoai.Text);
                    command.Parameters.AddWithValue("@TenLoai", txtTenLoai.Text);
                    command.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
                    command.ExecuteNonQuery();
                }
                LoadLoaiSach(); // Tải lại dữ liệu
                ClearTextBoxes();
            }
        }

        // Xóa loại sách đã chọn
        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM LoaiSach WHERE MaLoai = @MaLoai";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaLoai", txtMaLoai.Text);
                    command.ExecuteNonQuery();
                }
                LoadLoaiSach(); // Tải lại dữ liệu
                ClearTextBoxes();
            }
        }

        // Hàm xóa nội dung trong các TextBox
        private void ClearTextBoxes()
        {
            txtMaLoai.Clear();
            txtTenLoai.Clear();
            txtMoTa.Clear();
        }
        private void dgvLoaiSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLoaiSach.Rows[e.RowIndex];
                txtMaLoai.Text = row.Cells["MaLoai"].Value.ToString();
                txtTenLoai.Text = row.Cells["TenLoai"].Value.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value.ToString();
            }
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

    }
}
