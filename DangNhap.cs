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
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

  
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            // Lấy thông tin đăng nhập từ TextBox
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Kiểm tra thông tin đăng nhập
            if (KiemTraDangNhap(username, password))
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo");
                this.Hide();
                HeThong heThongForm = new HeThong();
                heThongForm.Show();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi");
            }
        }

        // Hàm kiểm tra đăng nhập
        private bool KiemTraDangNhap(string username, string password)
        {
            bool result = false;

            // Chuỗi kết nối tới cơ sở dữ liệu (thay đổi cho phù hợp)
            string connectionString = "Server=127.0.0.1;Database=hoan12345;User Id=root;Password=hoan12345;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Câu lệnh truy vấn kiểm tra tài khoản
                    string query = "SELECT COUNT(*) FROM TaiKhoan WHERE Username = @username AND Password = @password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm các tham số để tránh SQL Injection
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        result = count > 0; // Nếu tìm thấy tài khoản thì đăng nhập thành công
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }

            return result;
        }
    }
}
