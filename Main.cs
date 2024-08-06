using FontAwesome.Sharp;
using Đồ_Thị.uc;
using System.Runtime.InteropServices;
namespace Đồ_Thị
{
    public partial class Main : Form
    {
        private readonly int borderSize = 2;
        private IconButton? currentbtn;
        private readonly Panel leftBorderBtn;
        public Main()
        {
            InitializeComponent();
            Padding = new Padding(borderSize);
            BackColor = Color.Sienna;
            leftBorderBtn = new Panel
            {
                Size = new Size(7, 46)
            };
            panelSideBarMenu.Controls.Add(leftBorderBtn);

        }
        //Drag Form
        [LibraryImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static partial void ReleaseCapture();
        [LibraryImport("user32.DLL", EntryPoint = "SendMessage")]
        private static partial void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }
        private void labelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }
        private void labelMenuTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }
        // No Explore BAR (btw what the fuck is this shitty code)
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084; // Win32, Thông báo Đầu vào Chuột: Xác định phần nào của cửa sổ tương ứng với một điểm, cho phép thay đổi kích thước của form.
            const int resizeAreaSize = 10;

            // Thay đổi kích thước FORM
            const int HTCLIENT = 1; // Đại diện cho khu vực client của cửa sổ
            const int HTLEFT = 10;  // Viền trái của cửa sổ, cho phép thay đổi kích thước theo chiều ngang sang trái
            const int HTRIGHT = 11; // Viền phải của cửa sổ, cho phép thay đổi kích thước theo chiều ngang sang phải
            const int HTTOP = 12;   // Viền phía trên của cửa sổ, cho phép thay đổi kích thước theo chiều dọc lên
            const int HTTOPLEFT = 13; // Góc trên bên trái của viền cửa sổ, cho phép thay đổi kích thước theo đường chéo sang trái
            const int HTTOPRIGHT = 14; // Góc trên bên phải của viền cửa sổ, cho phép thay đổi kích thước theo đường chéo sang phải
            const int HTBOTTOM = 15; // Viền phía dưới của cửa sổ, cho phép thay đổi kích thước theo chiều dọc xuống
            const int HTBOTTOMLEFT = 16; // Góc dưới bên trái của viền cửa sổ, cho phép thay đổi kích thước theo đường chéo sang trái
            const int HTBOTTOMRIGHT = 17; // Góc dưới bên phải của viền cửa sổ, cho phép thay đổi kích thước theo đường chéo sang phải
            if (m.Msg == WM_NCHITTEST)
            { // Nếu cửa sổ m là WM_NCHITTEST
                base.WndProc(ref m);
                if (WindowState == FormWindowState.Normal) // Thay đổi kích thước form nếu nó ở trạng thái bình thường
                {
                    if ((int)m.Result == HTCLIENT) // Nếu kết quả của m (chuột) nằm trong khu vực client của cửa sổ
                    {
                        Point screenPoint = new(m.LParam.ToInt32()); // Lấy tọa độ điểm trên màn hình (tọa độ X và Y của chuột)
                        Point clientPoint = PointToClient(screenPoint); // Chuyển đổi vị trí của điểm trên màn hình thành tọa độ client
                        if (clientPoint.Y <= resizeAreaSize) // Nếu chuột ở phía trên của form (trong khu vực thay đổi kích thước - tọa độ X)
                        {
                            if (clientPoint.X <= resizeAreaSize) // Nếu chuột ở tọa độ X=0 hoặc nhỏ hơn kích thước thay đổi (X=10)
                                m.Result = HTTOPLEFT; // Thay đổi kích thước theo đường chéo sang trái
                            else if (clientPoint.X < (Size.Width - resizeAreaSize)) // Nếu chuột nằm giữa trái và phải của form
                                m.Result = HTTOP; // Thay đổi kích thước theo chiều dọc lên
                            else // Thay đổi kích thước theo đường chéo sang phải
                                m.Result = HTTOPRIGHT;
                        }
                        else if (clientPoint.Y <= (Size.Height - resizeAreaSize)) // Nếu chuột nằm bên trong form ở tọa độ Y (loại bỏ kích thước thay đổi)
                        {
                            if (clientPoint.X <= resizeAreaSize) // Thay đổi kích thước theo chiều ngang sang trái
                                m.Result = HTLEFT;
                            else if (clientPoint.X > (Width - resizeAreaSize)) // Thay đổi kích thước theo chiều ngang sang phải
                                m.Result = HTRIGHT;
                        }
                        else
                        {
                            m.Result = clientPoint.X <= resizeAreaSize
                                ? HTBOTTOMLEFT
                                : clientPoint.X < (Size.Width - resizeAreaSize) ? HTBOTTOM : (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }
            const int WM_NCCALCSIZE = 0x0083;
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }
            base.WndProc(ref m);
        }

        //Event Method (Gọi khi ứng dụng phóng to hoặc điều chỉnh size)
        private void Form1_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }
        // Chỉnh sao cho nút tắt chương trình, phóng to , Minimize khỏi lệch Form
        private void AdjustForm()
        {

            switch (WindowState)
            {
                case FormWindowState.Maximized:
                    Padding = new Padding(8, 8, 8, 8);
                    btnFullScreen.IconChar = IconChar.Compress;
                    break;
                case FormWindowState.Normal:
                    if (Padding.Top != borderSize)
                    {
                        Padding = new Padding(borderSize);
                        btnFullScreen.IconChar = IconChar.Expand;
                    }
                    break;

            }
        }
        private void FullScreenMode()
        {
            if (WindowState == FormWindowState.Normal)
            { btnFullScreen.IconChar = FontAwesome.Sharp.IconChar.Compress; WindowState = FormWindowState.Maximized; }
            else
            { WindowState = FormWindowState.Normal; btnFullScreen.IconChar = FontAwesome.Sharp.IconChar.Expand; }
        }
        private void btnMinimized_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            FullScreenMode();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnButtonMenu_Click(object sender, EventArgs e)
        {
            btnSidebarMenu_Transition.Start();
        }
        // Sidebar 
        bool sidebarcheck = true;
        private void btnButtonMenu_Transition_Tick(object sender, EventArgs e)
        {

            if (sidebarcheck)
            {
                panelSideBarMenu.Width -= 10;
                btnButtonMenu.Dock = DockStyle.Top;
                foreach (Button sidemenubtn in panelSideBarMenu.Controls.OfType<Button>())
                {
                    sidemenubtn.Text = "";
                }
                if (panelSideBarMenu.Width <= 60)
                {
                    sidebarcheck = false;
                    labelMenuTitle.Visible = false;
                    btnSidebarMenu_Transition.Stop();
                    foreach (Button sidemenubtn in panelSideBarMenu.Controls.OfType<Button>())
                    {
                        sidemenubtn.ImageAlign = ContentAlignment.MiddleCenter;
                        sidemenubtn.Padding = new Padding(0);
                    }
                }

            }
            else
            {
                panelSideBarMenu.Width += 10;
                btnButtonMenu.Dock = DockStyle.Fill;
                labelMenuTitle.Visible = true;
                foreach (Button sidemenubtn in panelSideBarMenu.Controls.OfType<Button>())
                {
                    sidemenubtn.ImageAlign = ContentAlignment.MiddleLeft;
                    sidemenubtn.Padding = new Padding(10, 0, 0, 0);
                }
                if (panelSideBarMenu.Width >= 206)
                {
                    foreach (Button sidemenubtn in panelSideBarMenu.Controls.OfType<Button>())
                        sidemenubtn.Text = sidemenubtn.Tag != null ? sidemenubtn.Tag.ToString() : "";
                    sidebarcheck = true;
                    btnSidebarMenu_Transition.Stop();
                }
            }
        }
        // KeyDown F11 Key FullScreen
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F11)
            {
                FullScreenMode();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private struct ColorRGB
        {
            public static Color color1 = Color.SaddleBrown;

        }
        // Custom BTN
        private void ActivateBTN(object sender, Color color)
        {
            if (sender != null)
            {
                DisableBTN();
                // Button
                currentbtn = (IconButton)sender;
                currentbtn.BackColor = Color.FromArgb(255, 141, 98);
                currentbtn.ForeColor = color;
                currentbtn.TextAlign = ContentAlignment.MiddleCenter;
                currentbtn.IconColor = color;
                currentbtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentbtn.ImageAlign = ContentAlignment.MiddleRight;
                // Left BorderBTN
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentbtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
            }
        }
        private void DisableBTN()
        {
            if (currentbtn != null)
            {
                currentbtn.BackColor = Color.LightSalmon;
                currentbtn.ForeColor = Color.White;
                currentbtn.TextAlign = ContentAlignment.MiddleCenter;
                currentbtn.IconColor = Color.Black;
                currentbtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentbtn.ImageAlign = ContentAlignment.MiddleLeft;

            }
        }

        private void btnSideBar1_Click(object sender, EventArgs e)
        {
            ActivateBTN(sender, ColorRGB.color1);
            foreach (Control c in panelMain.Controls)
            {
                c.Dispose();
            }
            MatrixShow matrix = new();
            panelMain.Controls.Add(matrix);
            matrix.Dock = DockStyle.Fill;

        }

        private void btnSideBar2_Click(object sender, EventArgs e)
        {
            ActivateBTN(sender, ColorRGB.color1);
            foreach (Control c in panelMain.Controls)
            {
                c.Dispose();
            }
            Test test = new();
            panelMain.Controls.Add(test);
            test.Dock = DockStyle.Fill;
        }

        private void btnSideBar3_Click(object sender, EventArgs e)
        {
            ActivateBTN(sender, ColorRGB.color1);
            foreach (Control c in panelMain.Controls)
            {
                c.Dispose();
            }
            MatrixBlock matrix = new();
            panelMain.Controls.Add(matrix);
            matrix.Dock = DockStyle.Fill;
        }

        private void btnSideBar4_Click(object sender, EventArgs e)
        {
            ActivateBTN(sender, ColorRGB.color1);
            foreach (Control c in panelMain.Controls)
            {
                c.Dispose();
            }
        }

    }
}