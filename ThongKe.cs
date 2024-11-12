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
    public partial class ThongKe : Form
    {
        private string connectionString;

        public ThongKe()
        {
            InitializeComponent();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dtpThangNam.Value;
            int selectedMonth = selectedDate.Month;
            int selectedYear = selectedDate.Year;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT s.TenSach, SUM(cthd.SoLuong) AS SoLuongBan, SUM(cthd.DonGia * cthd.SoLuong) AS DoanhThu
                         FROM ChiTietHoaDon cthd
                         JOIN HoaDon hd ON cthd.MaHoaDon = hd.MaHoaDon
                         JOIN Sach s ON cthd.MaSach = s.MaSach
                         WHERE MONTH(hd.NgayLap) = @Thang AND YEAR(hd.NgayLap) = @Nam
                         GROUP BY s.TenSach";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Thang", selectedMonth);
                    command.Parameters.AddWithValue("@Nam", selectedYear);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvThongKe.DataSource = dataTable;

                    // Tính tổng doanh thu
                    decimal totalRevenue = 0;
                    foreach (DataRow row in dataTable.Rows)
                    {
                        totalRevenue += Convert.ToDecimal(row["DoanhThu"]);
                    }
                    txtTongDoanhThu.Text = totalRevenue.ToString("C");
                }
            }
        }

    }
}
