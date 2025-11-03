using System.Runtime.InteropServices;
using DoThi.uc;
using FontAwesome.Sharp;

namespace DoThi
{
    public partial class Main : Form
    {
        private readonly int _borderSize = 2;
        private IconButton? _currentButton;
        private readonly Panel _leftBorderButton;
        private bool _isSidebarExpanded = true;

        // Constants for window messages and resizing
        private const int WmNchittest = 0x84;
        private const int WmNccalcsize = 0x83;
        private const int Htclient = 1;
        private const int Htleft = 10;
        private const int Htright = 11;
        private const int Httop = 12;
        private const int Httopleft = 13;
        private const int Httopright = 14;
        private const int Htbottom = 15;
        private const int Htbottomleft = 16;
        private const int Htbottomright = 17;
        private const int ResizeAreaSize = 10;

        public Main()
        {
            InitializeComponent();
            Padding = new Padding(_borderSize);
            BackColor = Color.Sienna;
            _leftBorderButton = new Panel
            {
                Size = new Size(7, 46)
            };
            panelSideBarMenu.Controls.Add(_leftBorderButton);
        }

        // Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

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

        // Overridden methods
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmNchittest)
            {
                base.WndProc(ref m);
                if (WindowState == FormWindowState.Normal)
                {
                    HandleResize(ref m);
                }
                return;
            }

            if (m.Msg == WmNccalcsize && m.WParam.ToInt32() == 1)
            {
                return;
            }

            base.WndProc(ref m);
        }

        private void HandleResize(ref Message m)
        {
            if ((int)m.Result != Htclient) return;

            var screenPoint = new Point(m.LParam.ToInt32());
            var clientPoint = PointToClient(screenPoint);

            if (clientPoint.Y <= ResizeAreaSize)
            {
                if (clientPoint.X <= ResizeAreaSize)
                    m.Result = (IntPtr)Httopleft;
                else if (clientPoint.X < (Size.Width - ResizeAreaSize))
                    m.Result = (IntPtr)Httop;
                else
                    m.Result = (IntPtr)Httopright;
            }
            else if (clientPoint.Y <= (Size.Height - ResizeAreaSize))
            {
                if (clientPoint.X <= ResizeAreaSize)
                    m.Result = (IntPtr)Htleft;
                else if (clientPoint.X > (Width - ResizeAreaSize))
                    m.Result = (IntPtr)Htright;
            }
            else
            {
                if (clientPoint.X <= ResizeAreaSize)
                    m.Result = (IntPtr)Htbottomleft;
                else if (clientPoint.X < (Size.Width - ResizeAreaSize))
                    m.Result = (IntPtr)Htbottom;
                else
                    m.Result = (IntPtr)Htbottomright;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }

        private void AdjustForm()
        {
            switch (WindowState)
            {
                case FormWindowState.Maximized:
                    Padding = new Padding(8, 8, 8, 8);
                    btnFullScreen.IconChar = IconChar.Compress;
                    break;
                case FormWindowState.Normal:
                    if (Padding.Top != _borderSize)
                    {
                        Padding = new Padding(_borderSize);
                        btnFullScreen.IconChar = IconChar.Expand;
                    }
                    break;
            }
        }

        private void ToggleFullScreen()
        {
            WindowState = WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
            btnFullScreen.IconChar = WindowState == FormWindowState.Maximized ? IconChar.Compress : IconChar.Expand;
        }

        private void btnMinimized_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            ToggleFullScreen();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnButtonMenu_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if (_isSidebarExpanded)
            {
                CollapseSidebar();
            }
            else
            {
                ExpandSidebar();
            }
        }

        private void CollapseSidebar()
        {
            panelSideBarMenu.Width -= 10;
            if (panelSideBarMenu.Width > 60) return;

            _isSidebarExpanded = false;
            labelMenuTitle.Visible = false;
            sidebarTransition.Stop();

            foreach (var button in panelSideBarMenu.Controls.OfType<Button>())
            {
                button.Text = "";
                button.ImageAlign = ContentAlignment.MiddleCenter;
                button.Padding = new Padding(0);
            }
        }

        private void ExpandSidebar()
        {
            panelSideBarMenu.Width += 10;
            if (panelSideBarMenu.Width < 206) return;

            _isSidebarExpanded = true;
            labelMenuTitle.Visible = true;
            sidebarTransition.Stop();

            foreach (var button in panelSideBarMenu.Controls.OfType<Button>())
            {
                button.Text = button.Tag?.ToString() ?? "";
                button.ImageAlign = ContentAlignment.MiddleLeft;
                button.Padding = new Padding(10, 0, 0, 0);
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData != Keys.F11) return base.ProcessCmdKey(ref msg, keyData);
            ToggleFullScreen();
            return true;
        }

        private struct RgbColors
        {
            public static readonly Color Color1 = Color.SaddleBrown;
        }

        private void ActivateButton(object sender, Color color)
        {
            if (sender == null) return;

            DisableButton();
            _currentButton = (IconButton)sender;
            _currentButton.BackColor = Color.FromArgb(255, 141, 98);
            _currentButton.ForeColor = color;
            _currentButton.TextAlign = ContentAlignment.MiddleCenter;
            _currentButton.IconColor = color;
            _currentButton.TextImageRelation = TextImageRelation.TextBeforeImage;
            _currentButton.ImageAlign = ContentAlignment.MiddleRight;

            _leftBorderButton.BackColor = color;
            _leftBorderButton.Location = new Point(0, _currentButton.Location.Y);
            _leftBorderButton.Visible = true;
            _leftBorderButton.BringToFront();
        }

        private void DisableButton()
        {
            if (_currentButton == null) return;

            _currentButton.BackColor = Color.LightSalmon;
            _currentButton.ForeColor = Color.White;
            _currentButton.TextAlign = ContentAlignment.MiddleCenter;
            _currentButton.IconColor = Color.Black;
            _currentButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _currentButton.ImageAlign = ContentAlignment.MiddleLeft;
        }

        private void OpenUserControl(UserControl userControl)
        {
            foreach (Control control in panelMain.Controls)
            {
                control.Dispose();
            }
            panelMain.Controls.Add(userControl);
            userControl.Dock = DockStyle.Fill;
        }

        private void btnSideBar1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RgbColors.Color1);
            OpenUserControl(new MatrixShow());
        }

        private void btnSideBar2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RgbColors.Color1);
            OpenUserControl(new Prim_BTTT());
        }

        private void btnSideBar3_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RgbColors.Color1);
            OpenUserControl(new MatrixBlock());
        }

        private void btnSideBar4_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RgbColors.Color1);
            foreach (Control c in panelMain.Controls)
            {
                c.Dispose();
            }
        }
    }
}
