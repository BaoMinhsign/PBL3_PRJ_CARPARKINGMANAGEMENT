using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using System.IO;
using System.Globalization;

namespace PBL3_CARPARKINGMANAGEMENT
{
    public partial class GiaoDien : UserControl
    {
        private GiaoDichBLL _bll;
        private List<TransactionDTO> _currentTransactions;

        public GiaoDien()
        {
            InitializeComponent();
            _bll = new GiaoDichBLL();
        }

        private void GiaoDien_Load(object sender, EventArgs e)
        {
            try
            {
                // Set default dates
                dtpFromDate.Value = DateTime.Now.AddDays(-30); // Default to last 30 days
                dtpToDate.Value = DateTime.Now;

                // Hide the no data message initially
                lblNoData.Visible = false;

                // Configure date pickers tooltip text
                var toolTip = new ToolTip();
                toolTip.SetToolTip(dtpFromDate, "Từ ngày");
                toolTip.SetToolTip(dtpToDate, "Đến ngày");

                // Initialize the DataGridView
                ConfigureDataGridView();

                // Load initial data
                LoadTransactions();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo giao diện: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            try
            {
                // Set custom formatting for date columns
                EntryTime.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                ExitTime.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                
                // Set currency format for Amount column
                Amount.DefaultCellStyle.Format = "N0";
                Amount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                // Style the action button column
                Actions.UseColumnTextForButtonValue = true;
                Actions.Text = "";
                Actions.FlatStyle = FlatStyle.Flat;
                
                // Configure CellFormatting event for custom cell styling
                dataGridView1.CellFormatting += DataGridView1_CellFormatting;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Lỗi cấu hình DataGridView: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Custom formatting for specific columns or cells
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                // Format the button column
                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    if (e.Value == null)
                    {
                        e.Value = ".";
                    }
                    
                    // Button column styling will be handled in CellPainting event
                }
                
                // Format the Amount column with thousand separators
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Amount" && e.Value != null)
                {
                    if (decimal.TryParse(e.Value.ToString(), out decimal amount))
                    {
                        e.Value = string.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:N0}", amount);
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        private void LoadTransactions()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                string keyword = txtSearch.Text.Trim();
                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1); // Get until end of day

                _currentTransactions = _bll.SearchTransactions(keyword, fromDate, toDate);
                
                // Show empty state message if no data
                lblNoData.Visible = _currentTransactions == null || _currentTransactions.Count == 0;
                dataGridView1.Visible = !lblNoData.Visible;
                
                if (_currentTransactions != null && _currentTransactions.Count > 0)
                {
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = _currentTransactions;
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi tải dữ liệu: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblNoData.Visible = true;
                dataGridView1.Visible = false;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GiaoDichBLL bll = new GiaoDichBLL();
            dataGridView1.DataSource = bll.SearchTransaction(txtSearch.Text, dtpFromDate.Value, dtpToDate.Value);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                // Reset all filters
                txtSearch.Text = "";
                dtpFromDate.Value = DateTime.Now.AddDays(-30);
                dtpToDate.Value = DateTime.Now;
                
                // Clear the grid and datasource
                _currentTransactions = null;
                dataGridView1.DataSource = null;
                
                // Show empty state
                lblNoData.Visible = true;
                dataGridView1.Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Lỗi khi đặt lại bộ lọc: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentTransactions == null || _currentTransactions.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất báo cáo!", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV Files|*.csv",
                    FileName = $"BaoCaoGiaoDich_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        ExportToCSV(saveFileDialog.FileName, _currentTransactions);
                        MessageBox.Show("Xuất báo cáo thành công!", 
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xuất báo cáo: {ex.Message}", 
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToCSV(string filePath, List<TransactionDTO> data)
        {
            // Simple CSV export implementation
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // Write headers
                sw.WriteLine("Mã GD,Biển số xe,Khách hàng,Loại vé,Thời gian vào,Thời gian ra,Số tiền,Phương thức thanh toán");

                // Write data
                foreach (var item in data)
                {
                    sw.WriteLine($"{item.TransactionID}," +
                                $"\"{item.LicensePlate}\"," +
                                $"\"{item.CustomerName}\"," +
                                $"\"{item.TicketType}\"," +
                                $"\"{item.EntryTime:dd/MM/yyyy HH:mm:ss}\"," +
                                $"\"{(item.ExitTime.HasValue ? item.ExitTime.Value.ToString("dd/MM/yyyy HH:mm:ss") : "")}\"," +
                                $"{item.Amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"))}," +
                                $"\"{item.PaymentMethod}\"");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
