using System.ComponentModel;

namespace DoThi.Components
{
    public class RJDropdownMenu : ContextMenuStrip
    {
        // Fields
        private bool _isMainMenu;
        private int _menuItemHeight = 25;
        private Color _menuItemTextColor = Color.Empty;
        private Color _primaryColor = Color.Empty;
        private Bitmap _menuItemHeaderSize;

        // Properties
        [Browsable(true)]
        public bool IsMainMenu
        {
            get => _isMainMenu;
            set => _isMainMenu = value;
        }

        [Browsable(true)]
        public int MenuItemHeight
        {
            get => _menuItemHeight;
            set => _menuItemHeight = value;
        }

        [Browsable(true)]
        public Color MenuItemTextColor
        {
            get => _menuItemTextColor;
            set => _menuItemTextColor = value;
        }

        [Browsable(true)]
        public Color PrimaryColor
        {
            get => _primaryColor;
            set => _primaryColor = value;
        }

        // Constructor
        public RJDropdownMenu(IContainer container) : base(container)
        {
        }

        // Private methods
        private void LoadMenuItemAppearance()
        {
            _menuItemHeaderSize = _isMainMenu ? new Bitmap(25, 45) : new Bitmap(20, _menuItemHeight);

            foreach (ToolStripMenuItem menuItemL1 in Items)
            {
                menuItemL1.ImageScaling = ToolStripItemImageScaling.None;
                if (menuItemL1.Image == null) menuItemL1.Image = _menuItemHeaderSize;

                foreach (ToolStripMenuItem menuItemL2 in menuItemL1.DropDownItems)
                {
                    menuItemL2.ImageScaling = ToolStripItemImageScaling.None;
                    if (menuItemL2.Image == null) menuItemL2.Image = _menuItemHeaderSize;

                    // You can add more levels if needed
                }
            }
        }

        // Overrides
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!DesignMode)
            {
                Renderer = new MenuRenderer(_isMainMenu, _primaryColor, _menuItemTextColor);
                LoadMenuItemAppearance();
            }
        }
    }
}
