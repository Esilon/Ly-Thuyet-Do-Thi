using System.ComponentModel;

namespace Đồ_Thị.Compoments
{
    public class RJDropdownMenu : ContextMenuStrip
    {
        //Fields
        private bool isMainMenu;
        private int menuItemHeight = 25;
        private Color menuItemTextColor = Color.Empty;
        private Color primaryColor = Color.Empty;

        private Bitmap menuItemHeaderSize;

        //Constructor
        public RJDropdownMenu(IContainer container)
            : base(container)
        {

        }

        //Properties
        //Optionally, hide the properties in the toolbox to avoid the problem of displaying and/or 
        //saving control property changes in the designer at design time in Visual Studio.
        //If the problem I mention does not occur you can expose the properties and manipulate them from the toolbox.
        [Browsable(true)]
        public bool IsMainMenu
        {
            get => isMainMenu;
            set => isMainMenu = value;
        }

        [Browsable(true)]
        public int MenuItemHeight
        {
            get => menuItemHeight;
            set => menuItemHeight = value;
        }

        [Browsable(true)]
        public Color MenuItemTextColor
        {
            get => menuItemTextColor;
            set => menuItemTextColor = value;
        }

        [Browsable(true)]
        public Color PrimaryColor
        {
            get => primaryColor;
            set => primaryColor = value;
        }

        //Private methods
        private void LoadMenuItemHeight()
        {
            menuItemHeaderSize = isMainMenu ? new Bitmap(25, 45) : new Bitmap(20, menuItemHeight);

            foreach (ToolStripMenuItem menuItemL1 in Items)
            {
                menuItemL1.ImageScaling = ToolStripItemImageScaling.None;
                menuItemL1.Image ??= menuItemHeaderSize;

                foreach (ToolStripMenuItem menuItemL2 in menuItemL1.DropDownItems)
                {
                    menuItemL2.ImageScaling = ToolStripItemImageScaling.None;
                    menuItemL2.Image ??= menuItemHeaderSize;

                    foreach (ToolStripMenuItem menuItemL3 in menuItemL2.DropDownItems)
                    {
                        menuItemL3.ImageScaling = ToolStripItemImageScaling.None;
                        menuItemL3.Image ??= menuItemHeaderSize;

                        foreach (ToolStripMenuItem menuItemL4 in menuItemL3.DropDownItems)
                        {
                            menuItemL4.ImageScaling = ToolStripItemImageScaling.None;
                            menuItemL4.Image ??= menuItemHeaderSize;
                            ///Level 5++
                        }
                    }
                }
            }
        }

        //Overrides
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (DesignMode == false)
            {
                Renderer = new MenuRenderer(isMainMenu, primaryColor, menuItemTextColor);
                LoadMenuItemHeight();
            }
        }
    }
}
