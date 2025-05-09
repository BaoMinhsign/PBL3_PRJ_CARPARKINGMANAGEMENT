using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Windows.Forms.DataVisualization.Charting;
using BLL;

namespace PBL3_CARPARKINGMANAGEMENT
{
    public partial class ThongKeUC : UserControl
    {
        private readonly ThongKeBLL thongKeBLL;
        private enum ViewMode { Month, Quarter, Year }
        private ViewMode currentViewMode = ViewMode.Month;
        private Timer refreshTimer; // Add timer for automatic refresh

        public ThongKeUC()
        {
            InitializeComponent();
            thongKeBLL = new ThongKeBLL();
            
            // Initialize refresh timer
            refreshTimer = new Timer();
            refreshTimer.Interval = 30000; // Refresh every 30 seconds
            refreshTimer.Tick += RefreshTimer_Tick;
        }

        private void ThongKeUC_Load(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị thông tin cơ bản
                labelHeader.Text = "THỐNG KÊ";
                
                // Tải dữ liệu dashboard
                LoadDashboardData();
                
                // Tải dữ liệu biểu đồ
                LoadRevenueChart();
                LoadVehicleTypeChart();
                
                // Load parking status data
                LoadParkingStatusData();
                
                // Thiết lập giá trị mặc định cho combobox chế độ xem
                cboViewMode.SelectedIndex = 0;
                
                // Start the refresh timer
                refreshTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // Add method to handle timer tick event
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            RefreshAllStatistics();
        }
        
        // Public method to refresh all statistics data
        public void RefreshAllStatistics()
        {
            LoadDashboardData();
            LoadParkingStatusData();
            LoadRevenueChart();
            LoadVehicleTypeChart();
        }
        
        // This method will be called when the control is removed from view
        protected override void OnHandleDestroyed(EventArgs e)
        {
            // Stop the timer when the control is destroyed to prevent memory leaks
            refreshTimer.Stop();
            base.OnHandleDestroyed(e);
        }
        
        private void LoadDashboardData()
        {
            try
            {
                // Cập nhật thông tin thống kê
                lblCustomerCount.Text = thongKeBLL.GetTotalCustomers().ToString();
                lblVehicleCount.Text = thongKeBLL.GetTotalVehicles().ToString();
                lblParkedCount.Text = thongKeBLL.GetVehiclesCurrentlyParked().ToString();
                lblAvailableSpaces.Text = thongKeBLL.GetAvailableParkingSpaces().ToString();
                lblTransactionCount.Text = thongKeBLL.GetTotalTransactions().ToString();
                lblTotalRevenue.Text = string.Format("{0:N0} VNĐ", thongKeBLL.GetTotalRevenue());
                
                // Cập nhật thông tin trạng thái bãi đỗ
                labelVehiclesParking.Text = $"Tổng số phương tiện: {thongKeBLL.GetTotalVehicles()}";
                labelVehiclesInside.Text = $"Xe đang đỗ: {thongKeBLL.GetVehiclesCurrentlyParked()}";
                labelEmptySpaces.Text = $"Số chỗ còn trống: {thongKeBLL.GetAvailableParkingSpaces()}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRevenueChart()
        {
            try
            {
                // Xóa dữ liệu cũ
                chartRevenue.Series.Clear();
                
                // Tạo series mới
                Series revenueSeries = new Series("DoanhThu");
                revenueSeries.ChartType = SeriesChartType.Column;
                revenueSeries.Color = Color.FromArgb(108, 93, 211);
                revenueSeries.IsValueShownAsLabel = true;
                chartRevenue.Series.Add(revenueSeries);
                
                // Lấy dữ liệu doanh thu theo tháng
                var revenueData = thongKeBLL.GetRevenueByMonth();
                
                // Thêm dữ liệu vào biểu đồ
                foreach (var item in revenueData)
                {
                    int pointIndex = revenueSeries.Points.AddXY(item.MonthName, item.Total);
                    revenueSeries.Points[pointIndex].Label = string.Format("{0:N0}", item.Total);
                }
                
                // Tạo dữ liệu giả nếu không có dữ liệu thực
                if (revenueData == null || revenueData.Count == 0)
                {
                    // Thêm dữ liệu mẫu để biểu đồ không trống
                    string[] months = { "01/2025", "02/2025", "03/2025", "04/2025", "05/2025" };
                    decimal[] values = { 15000000, 18000000, 12000000, 20000000, 25000000 };
                    
                    for (int i = 0; i < months.Length; i++)
                    {
                        int pointIndex = revenueSeries.Points.AddXY(months[i], values[i]);
                        revenueSeries.Points[pointIndex].Label = string.Format("{0:N0}", values[i]);
                    }
                }
                
                // Cấu hình biểu đồ
                if (chartRevenue.ChartAreas.Count > 0)
                {
                    chartRevenue.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                    chartRevenue.ChartAreas[0].AxisX.Interval = 1;
                    chartRevenue.ChartAreas[0].AxisY.Title = "VNĐ";
                    chartRevenue.ChartAreas[0].AxisX.Title = "Tháng";
                    chartRevenue.ChartAreas[0].AxisY.LabelStyle.Format = "{0:N0}";
                }
                
                // Thiết lập tiêu đề
                if (chartRevenue.Titles.Count > 0)
                {
                    chartRevenue.Titles[0].Text = "BIỂU ĐỒ DOANH THU";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải biểu đồ doanh thu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Vẫn hiển thị biểu đồ trống nếu có lỗi
                chartRevenue.Series.Clear();
                Series errorSeries = new Series("Error");
                errorSeries.ChartType = SeriesChartType.Column;
                errorSeries.Color = Color.FromArgb(108, 93, 211);
                chartRevenue.Series.Add(errorSeries);
            }
        }
        
        private void LoadVehicleTypeChart()
        {
            try
            {
                // Xóa dữ liệu cũ
                chartVehicleTypes.Series.Clear();
                
                // Tạo series mới
                Series vehicleTypeSeries = new Series("LoaiXe");
                vehicleTypeSeries.ChartType = SeriesChartType.Pie;
                vehicleTypeSeries.IsValueShownAsLabel = true;
                chartVehicleTypes.Series.Add(vehicleTypeSeries);
                
                // Lấy dữ liệu phân bố loại xe
                var vehicleTypes = thongKeBLL.GetVehicleTypeDistribution();
                
                // Định nghĩa các màu cố định cho từng loại xe
                Dictionary<string, Color> vehicleColorMap = new Dictionary<string, Color>(StringComparer.OrdinalIgnoreCase)
                {
                    { "xe máy", Color.FromArgb(153, 102, 255) },    // Tím nhạt
                    { "ô tô", Color.FromArgb(65, 105, 225) },       // Xanh dương
                    { "xe đạp", Color.FromArgb(34, 139, 34) }       // Xanh lá
                };
                
                // Thêm dữ liệu vào biểu đồ
                foreach (var type in vehicleTypes)
                {
                    int pointIndex = vehicleTypeSeries.Points.AddXY(type.Type, type.Count);
                    vehicleTypeSeries.Points[pointIndex].Label = $"{type.Type}: {type.Count}";
                    
                    // Thiết lập màu cho từng loại xe từ map màu cố định
                    if (vehicleColorMap.ContainsKey(type.Type.ToLower()))
                    {
                        vehicleTypeSeries.Points[pointIndex].Color = vehicleColorMap[type.Type.ToLower()];
                    }
                    else
                    {
                        // Màu mặc định cho các loại khác
                        Random rand = new Random();
                        vehicleTypeSeries.Points[pointIndex].Color = Color.FromArgb(
                            rand.Next(100, 255),
                            rand.Next(100, 255),
                            rand.Next(100, 255)
                        );
                    }
                }
                
                // Tạo dữ liệu giả nếu không có dữ liệu thực
                if (vehicleTypes == null || vehicleTypes.Count == 0)
                {
                    string[] types = { "Xe máy", "Ô tô", "Xe đạp" };
                    int[] counts = { 65, 25, 10 };
                    
                    for (int i = 0; i < types.Length; i++)
                    {
                        int pointIndex = vehicleTypeSeries.Points.AddXY(types[i], counts[i]);
                        vehicleTypeSeries.Points[pointIndex].Label = $"{types[i]}: {counts[i]}";
                        
                        // Sử dụng cùng một bảng màu cố định
                        if (vehicleColorMap.ContainsKey(types[i].ToLower()))
                        {
                            vehicleTypeSeries.Points[pointIndex].Color = vehicleColorMap[types[i].ToLower()];
                        }
                    }
                }
                
                // Thiết lập tiêu đề
                if (chartVehicleTypes.Titles.Count > 0)
                {
                    chartVehicleTypes.Titles[0].Text = "PHÂN BỐ LOẠI PHƯƠNG TIỆN";
                }
                
                // Cấu hình thêm
                vehicleTypeSeries.IsValueShownAsLabel = true;
                vehicleTypeSeries.Label = "#PERCENT{P1}";
                vehicleTypeSeries.LegendText = "#VALX";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải biểu đồ phân bố loại xe: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Vẫn hiển thị biểu đồ trống nếu có lỗi
                chartVehicleTypes.Series.Clear();
                Series errorSeries = new Series("Error");
                errorSeries.ChartType = SeriesChartType.Pie;
                chartVehicleTypes.Series.Add(errorSeries);
            }
        }
        
        private void LoadParkingStatusData()
        {
            try
            {
                // Kiểm tra dgvParkingStatus đã được khởi tạo chưa
                if (dgvParkingStatus == null)
                {
                    MessageBox.Show("DataGridView chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Vô hiệu hóa tạm thời các sự kiện của DataGridView để tránh các vấn đề với cập nhật UI
                dgvParkingStatus.SuspendLayout();

                // Lấy dữ liệu trạng thái bãi đỗ xe
                var parkingStatus = thongKeBLL?.GetParkingLotStatus();
                
                // Kiểm tra thongKeBLL có null không
                if (thongKeBLL == null)
                {
                    MessageBox.Show("Đối tượng ThongKeBLL chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgvParkingStatus.ResumeLayout();
                    return;
                }
                
                // Tạo dữ liệu mẫu nếu không có dữ liệu thực
                if (parkingStatus == null || parkingStatus.Count == 0)
                {
                    DataTable sampleData = CreateSampleParkingDataTable();
                    dgvParkingStatus.DataSource = sampleData;
                }
                else
                {
                    // Thiết lập nguồn dữ liệu cho DataGridView
                    dgvParkingStatus.DataSource = parkingStatus;
                }
                
                // Đảm bảo rằng DataSource đã được gán và các cột đã được tạo
                // Sử dụng BeginInvoke để đảm bảo cấu hình cột được thực hiện sau khi DataSource đã được xử lý hoàn toàn
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    try
                    {
                        if (dgvParkingStatus.Columns.Count > 0)
                        {
                            // Ẩn cột ID nếu tồn tại
                            if (dgvParkingStatus.Columns.Contains("ParkingLotID"))
                                dgvParkingStatus.Columns["ParkingLotID"].Visible = false;
                            
                            // Thiết lập header và độ rộng các cột với kiểm tra an toàn
                            ConfigureColumn("Name", "Tên bãi", 100);
                            ConfigureColumn("Capacity", "Sức chứa", 70);
                            ConfigureColumn("UsedSpaces", "Đang sử dụng", 80);
                            ConfigureColumn("AvailableSpaces", "Còn trống", 70);
                            
                            // Cấu hình cột tỷ lệ với định dạng phần trăm
                            if (dgvParkingStatus.Columns.Contains("OccupancyRate"))
                            {
                                var column = dgvParkingStatus.Columns["OccupancyRate"];
                                if (column != null)
                                {
                                    column.HeaderText = "Tỷ lệ lấp đầy";
                                    //column.Width = 80;
                                    column.DefaultCellStyle.Format = "0.00\\%";
                                }
                            }

                            // Thiết lập màu cho các hàng
                            StyleDataGridView();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Ghi log lỗi (nếu cần) nhưng không hiển thị MessageBox trong BeginInvoke
                        System.Diagnostics.Debug.WriteLine($"Lỗi khi cấu hình cột: {ex.Message}\nStack trace: {ex.StackTrace}");
                    }
                }));
                
                // Khôi phục xử lý sự kiện của DataGridView
                dgvParkingStatus.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu trạng thái bãi đỗ: {ex.Message}\nStack trace: {ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvParkingStatus.ResumeLayout(); // Đảm bảo luôn khôi phục layout
            }
        }

        // Phương thức mới để cấu hình an toàn một cột trong DataGridView
        private void ConfigureColumn(string columnName, string headerText, int width)
        {
            try
            {
                if (dgvParkingStatus?.Columns != null && dgvParkingStatus.Columns.Contains(columnName))
                {
                    var column = dgvParkingStatus.Columns[columnName];
                    if (column != null)
                    {
                        // Đảm bảo các giá trị thiết lập là hợp lệ
                        if (!string.IsNullOrEmpty(headerText))
                            column.HeaderText = headerText;
                        
                        if (width > 0 && column.GetType().GetProperty("Width") != null)
                        {
                            // Kiểm tra xem thuộc tính Width có thể gán được giá trị không
                            try
                            {
                            }
                            catch (NullReferenceException)
                            {
                                // Nếu gặp NullReferenceException khi thiết lập Width, thử cách khác
                                //System.Diagnostics.Debug.WriteLine($"Không thể thiết lập Width cho cột {columnName}");
                                
                                // Sử dụng phương thức khác để thiết lập Width (nếu có)
                                if (column is DataGridViewColumn dgvColumn)
                                {
                                    dgvColumn.MinimumWidth = width;
                                    dgvColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi trong ConfigureColumn({columnName}, {headerText}, {width}): {ex.Message}");
                // Không hiển thị MessageBox để tránh làm gián đoạn UI
            }
        }

        // Tách phương thức tạo bảng dữ liệu mẫu
        private DataTable CreateSampleParkingDataTable()
        {
            DataTable sampleData = new DataTable();
            sampleData.Columns.Add("Name", typeof(string));
            sampleData.Columns.Add("Capacity", typeof(int));
            sampleData.Columns.Add("UsedSpaces", typeof(int));
            sampleData.Columns.Add("AvailableSpaces", typeof(int));
            sampleData.Columns.Add("OccupancyRate", typeof(decimal));
            
            // Thêm một số dữ liệu mẫu
            sampleData.Rows.Add("Bãi A", 100, 75, 25, 75.00m);
            sampleData.Rows.Add("Bãi B", 50, 30, 20, 60.00m);
            sampleData.Rows.Add("Bãi C", 80, 45, 35, 56.25m);
            
            return sampleData;
        }

        // Phương thức khởi tạo DataGridView mới - gọi từ InitializeComponent hoặc sau khi khởi tạo control
        public void InitializeDataGridView()
        {
            if (dgvParkingStatus != null)
            {
                // Thiết lập các thuộc tính cơ bản cho DataGridView
                dgvParkingStatus.AutoGenerateColumns = true;
                dgvParkingStatus.AllowUserToAddRows = false;
                dgvParkingStatus.AllowUserToDeleteRows = false;
                dgvParkingStatus.ReadOnly = true;
                dgvParkingStatus.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvParkingStatus.MultiSelect = false;
                dgvParkingStatus.BackgroundColor = Color.FromArgb(45, 48, 57);
                dgvParkingStatus.BorderStyle = BorderStyle.None;
                dgvParkingStatus.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvParkingStatus.RowHeadersVisible = false;
                
                // Đăng ký sự kiện DataBindingComplete
                dgvParkingStatus.DataBindingComplete += DgvParkingStatus_DataBindingComplete;
            }
        }

        // Xử lý sự kiện khi binding dữ liệu hoàn tất
        private void DgvParkingStatus_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                // Đảm bảo rằng chỉ thực hiện một lần khi dữ liệu được binding lần đầu
                if (e.ListChangedType != ListChangedType.ItemDeleted)
                {
                    var dgv = sender as DataGridView;
                    if (dgv != null && dgv.Columns.Count > 0)
                    {
                        // Ẩn cột ID nếu tồn tại
                        if (dgv.Columns.Contains("ParkingLotID"))
                            dgv.Columns["ParkingLotID"].Visible = false;
                        
                        // Thiết lập header và độ rộng các cột với kiểm tra an toàn (không dùng ConfigureColumn ở đây để tránh đệ quy)
                        if (dgv.Columns.Contains("Name") && dgv.Columns["Name"] != null)
                        {
                            dgv.Columns["Name"].HeaderText = "Tên bãi";
                            dgv.Columns["Name"].Width = 100;
                        }
                        
                        if (dgv.Columns.Contains("Capacity") && dgv.Columns["Capacity"] != null)
                        {
                            dgv.Columns["Capacity"].HeaderText = "Sức chứa";
                            dgv.Columns["Capacity"].Width = 70;
                        }
                        
                        if (dgv.Columns.Contains("UsedSpaces") && dgv.Columns["UsedSpaces"] != null)
                        {
                            dgv.Columns["UsedSpaces"].HeaderText = "Đang sử dụng";
                            dgv.Columns["UsedSpaces"].Width = 80;
                        }
                        
                        if (dgv.Columns.Contains("AvailableSpaces") && dgv.Columns["AvailableSpaces"] != null)
                        {
                            dgv.Columns["AvailableSpaces"].HeaderText = "Còn trống";
                            dgv.Columns["AvailableSpaces"].Width = 70;
                        }
                        
                        // Cấu hình cột tỷ lệ với định dạng phần trăm
                        if (dgv.Columns.Contains("OccupancyRate") && dgv.Columns["OccupancyRate"] != null)
                        {
                            var column = dgv.Columns["OccupancyRate"];
                            column.HeaderText = "Tỷ lệ lấp đầy";
                            column.Width = 80;
                            column.DefaultCellStyle.Format = "0.00\\%";
                        }
                        
                        // Thiết lập style cho DataGridView
                        StyleDataGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi trong sự kiện DataBindingComplete: {ex.Message}");
            }
        }

        // Phương thức mới để thiết lập style cho DataGridView
        private void StyleDataGridView()
        {
            if (dgvParkingStatus != null)
            {
                dgvParkingStatus.RowsDefaultCellStyle.BackColor = Color.FromArgb(45, 48, 57);
                dgvParkingStatus.RowsDefaultCellStyle.ForeColor = Color.White;
                dgvParkingStatus.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(72, 62, 140);
                dgvParkingStatus.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvParkingStatus.EnableHeadersVisualStyles = false;
                
                // Không cho phép sắp xếp cột
                foreach (DataGridViewColumn col in dgvParkingStatus.Columns)
                {
                    if (col != null)
                    {
                        col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
        }

        private void BtnViewParkingStatus_Click(object sender, EventArgs e)
        {
            try
            {
                // Tải dữ liệu cho bảng trạng thái bãi đỗ
                LoadParkingStatusData();
                
                // Hiển thị panel chi tiết
                panelParkingDetails.Visible = true;
                panelParkingDetails.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị thông tin bãi đỗ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCloseParkingDetails_Click(object sender, EventArgs e)
        {
            panelParkingDetails.Visible = false;
        }
        
        #region Xử lý sự kiện biểu đồ doanh thu chi tiết
        
        // Xử lý sự kiện khi click vào biểu đồ doanh thu
        private void ChartRevenue_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị panel chi tiết doanh thu
                LoadRevenueDetailedChart();
                panelRevenueDetails.Visible = true;
                panelRevenueDetails.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị chi tiết doanh thu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // Xử lý khi thay đổi chế độ xem (tháng, quý, năm)
        private void CboViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cập nhật chế độ xem hiện tại
            switch (cboViewMode.SelectedIndex)
            {
                case 0:
                    currentViewMode = ViewMode.Month;
                    break;
                case 1:
                    currentViewMode = ViewMode.Quarter;
                    break;
                case 2:
                    currentViewMode = ViewMode.Year;
                    break;
                default:
                    currentViewMode = ViewMode.Month;
                    break;
            }
            
            // Tải lại biểu đồ với chế độ xem mới
            LoadRevenueDetailedChart();
        }
        
        // Xử lý sự kiện đóng panel chi tiết doanh thu
        private void BtnCloseRevenueDetails_Click(object sender, EventArgs e)
        {
            panelRevenueDetails.Visible = false;
        }
        
        // Tải biểu đồ doanh thu chi tiết
        private void LoadRevenueDetailedChart()
        {
            try
            {
                // Xóa dữ liệu cũ
                chartRevenueDetailed.Series.Clear();
                
                // Tạo series mới
                Series revenueSeries = new Series("DoanhThu");
                revenueSeries.ChartType = SeriesChartType.Column;
                revenueSeries.Color = Color.FromArgb(108, 93, 211);
                revenueSeries.IsValueShownAsLabel = true;
                chartRevenueDetailed.Series.Add(revenueSeries);
                
                // Lấy dữ liệu doanh thu theo chế độ xem hiện tại
                var revenueData = GetRevenueDataByViewMode();
                
                // Thêm dữ liệu vào biểu đồ
                foreach (var item in revenueData)
                {
                    int pointIndex = revenueSeries.Points.AddXY(item.Key, item.Value);
                    revenueSeries.Points[pointIndex].Label = string.Format("{0:N0}", item.Value);
                    
                    // Thêm gradient effect cho các cột
                    revenueSeries.Points[pointIndex].BackGradientStyle = GradientStyle.TopBottom;
                    revenueSeries.Points[pointIndex].BackSecondaryColor = Color.FromArgb(94, 148, 255);
                }
                
                // Tạo dữ liệu giả nếu không có dữ liệu thực
                if (revenueData.Count == 0)
                {
                    GenerateSampleRevenueData(revenueSeries);
                }
                
                // Cấu hình biểu đồ
                ConfigureRevenueDetailedChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải biểu đồ doanh thu chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // Cấu hình biểu đồ doanh thu chi tiết
        private void ConfigureRevenueDetailedChart()
        {
            if (chartRevenueDetailed.ChartAreas.Count > 0)
            {
                // Cấu hình trục X
                chartRevenueDetailed.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chartRevenueDetailed.ChartAreas[0].AxisX.Interval = 1;
                chartRevenueDetailed.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Times New Roman", 10, FontStyle.Regular);
                chartRevenueDetailed.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                
                // Cấu hình trục Y
                chartRevenueDetailed.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
                chartRevenueDetailed.ChartAreas[0].AxisY.TitleFont = new Font("Times New Roman", 10, FontStyle.Bold);
                chartRevenueDetailed.ChartAreas[0].AxisY.LabelStyle.Format = "{0:N0}";
                chartRevenueDetailed.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Times New Roman", 10, FontStyle.Regular);
                
                // Cấu hình trục X dựa trên chế độ xem hiện tại
                switch (currentViewMode)
                {
                    case ViewMode.Month:
                        chartRevenueDetailed.ChartAreas[0].AxisX.Title = "Tháng";
                        break;
                    case ViewMode.Quarter:
                        chartRevenueDetailed.ChartAreas[0].AxisX.Title = "Quý";
                        break;
                    case ViewMode.Year:
                        chartRevenueDetailed.ChartAreas[0].AxisX.Title = "Năm";
                        break;
                }
                
                // Thiết lập font cho tiêu đề trục
                chartRevenueDetailed.ChartAreas[0].AxisX.TitleFont = new Font("Times New Roman", 10, FontStyle.Bold);
                
                // Thêm hiệu ứng 3D
                chartRevenueDetailed.ChartAreas[0].Area3DStyle.Enable3D = false;
                chartRevenueDetailed.ChartAreas[0].Area3DStyle.Inclination = 15;
                
                // Thiết lập các tùy chọn khác
                chartRevenueDetailed.ChartAreas[0].BackColor = Color.FromArgb(35, 38, 47);
                chartRevenueDetailed.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
                chartRevenueDetailed.ChartAreas[0].BackSecondaryColor = Color.FromArgb(45, 48, 57);
            }
            
            // Thiết lập tiêu đề dựa trên chế độ xem hiện tại
            if (chartRevenueDetailed.Titles.Count > 0)
            {
                switch (currentViewMode)
                {
                    case ViewMode.Month:
                        chartRevenueDetailed.Titles[0].Text = "CHI TIẾT DOANH THU THEO THÁNG";
                        break;
                    case ViewMode.Quarter:
                        chartRevenueDetailed.Titles[0].Text = "CHI TIẾT DOANH THU THEO QUÝ";
                        break;
                    case ViewMode.Year:
                        chartRevenueDetailed.Titles[0].Text = "CHI TIẾT DOANH THU THEO NĂM";
                        break;
                }
            }
        }
        
        // Lấy dữ liệu doanh thu theo chế độ xem
        private Dictionary<string, decimal> GetRevenueDataByViewMode()
        {
            var result = new Dictionary<string, decimal>();
            
            try
            {
                // Lấy dữ liệu từ BLL
                var revenueData = thongKeBLL.GetRevenueByMonth();
                
                if (revenueData != null && revenueData.Count > 0)
                {
                    switch (currentViewMode)
                    {
                        case ViewMode.Month:
                            // Theo tháng - hiển thị 12 tháng gần nhất
                            result = revenueData
                                .OrderByDescending(r => r.Year)
                                .ThenByDescending(r => r.Month)
                                .Take(12)
                                .OrderBy(r => r.Year)
                                .ThenBy(r => r.Month)
                                .ToDictionary(r => r.MonthName, r => r.Total);
                            break;
                            
                        case ViewMode.Quarter:
                            // Theo quý - gộp theo quý
                            var quarterData = revenueData
                                .GroupBy(r => new { r.Year, Quarter = (r.Month - 1) / 3 + 1 })
                                .Select(g => new {
                                    Label = $"Q{g.Key.Quarter}/{g.Key.Year}",
                                    Total = g.Sum(r => r.Total),
                                    Year = g.Key.Year,
                                    Quarter = g.Key.Quarter
                                })
                                .OrderBy(x => x.Year)
                                .ThenBy(x => x.Quarter)
                                .Take(8);
                                
                            foreach (var quarter in quarterData)
                            {
                                result.Add(quarter.Label, quarter.Total);
                            }
                            break;
                            
                        case ViewMode.Year:
                            // Theo năm
                            var yearData = revenueData
                                .GroupBy(r => r.Year)
                                .Select(g => new {
                                    Year = g.Key,
                                    Total = g.Sum(r => r.Total)
                                })
                                .OrderBy(x => x.Year);
                                
                            foreach (var year in yearData)
                            {
                                result.Add(year.Year.ToString(), year.Total);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy dữ liệu doanh thu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return result;
        }
        
        // Tạo dữ liệu mẫu cho biểu đồ
        private void GenerateSampleRevenueData(Series series)
        {
            try
            {
                switch (currentViewMode)
                {
                    case ViewMode.Month:
                        // Dữ liệu mẫu theo tháng
                        string[] months = { "01/2025", "02/2025", "03/2025", "04/2025", "05/2025", 
                                           "06/2025", "07/2025", "08/2025", "09/2025", "10/2025", 
                                           "11/2025", "12/2025" };
                        decimal[] monthValues = { 15000000, 18000000, 12000000, 20000000, 25000000,
                                                22000000, 16000000, 18500000, 21000000, 19500000,
                                                23500000, 27000000 };
                        
                        for (int i = 0; i < months.Length; i++)
                        {
                            int pointIndex = series.Points.AddXY(months[i], monthValues[i]);
                            series.Points[pointIndex].Label = string.Format("{0:N0}", monthValues[i]);
                            series.Points[pointIndex].BackGradientStyle = GradientStyle.TopBottom;
                            series.Points[pointIndex].BackSecondaryColor = Color.FromArgb(94, 148, 255);
                        }
                        break;
                        
                    case ViewMode.Quarter:
                        // Dữ liệu mẫu theo quý
                        string[] quarters = { "Q1/2024", "Q2/2024", "Q3/2024", "Q4/2024", "Q1/2025", "Q2/2025", "Q3/2025", "Q4/2025" };
                        decimal[] quarterValues = { 45000000, 55000000, 48000000, 60000000, 52000000, 65000000, 57000000, 72000000 };
                        
                        for (int i = 0; i < quarters.Length; i++)
                        {
                            int pointIndex = series.Points.AddXY(quarters[i], quarterValues[i]);
                            series.Points[pointIndex].Label = string.Format("{0:N0}", quarterValues[i]);
                            series.Points[pointIndex].BackGradientStyle = GradientStyle.TopBottom;
                            series.Points[pointIndex].BackSecondaryColor = Color.FromArgb(94, 148, 255);
                        }
                        break;
                        
                    case ViewMode.Year:
                        // Dữ liệu mẫu theo năm
                        string[] years = { "2019", "2020", "2021", "2022", "2023", "2024", "2025" };
                        decimal[] yearValues = { 180000000, 210000000, 195000000, 240000000, 270000000, 320000000, 180000000 };
                        
                        for (int i = 0; i < years.Length; i++)
                        {
                            int pointIndex = series.Points.AddXY(years[i], yearValues[i]);
                            series.Points[pointIndex].Label = string.Format("{0:N0}", yearValues[i]);
                            series.Points[pointIndex].BackGradientStyle = GradientStyle.TopBottom;
                            series.Points[pointIndex].BackSecondaryColor = Color.FromArgb(94, 148, 255);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo dữ liệu mẫu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void BtnRefreshStats_Click(object sender, EventArgs e)
        {
            RefreshAllStatistics();
        }
        
        #endregion
    }
}