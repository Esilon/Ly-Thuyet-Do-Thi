namespace Đồ_Thị.Compoments
{
    public class MenuColorTable : ProfessionalColorTable
    {
        //Fields
        private readonly Color backColor;
        private readonly Color leftColumnColor;
        private readonly Color borderColor;
        private readonly Color menuItemBorderColor;
        private readonly Color menuItemSelectedColor;

        //Constructor
        public MenuColorTable(bool isMainMenu, Color primaryColor)
        {
            if (isMainMenu)
            {
                backColor = Color.FromArgb(37, 39, 60);
                leftColumnColor = Color.FromArgb(32, 33, 51);
                borderColor = Color.FromArgb(32, 33, 51);
                menuItemBorderColor = primaryColor;
                menuItemSelectedColor = primaryColor;
            }
            else
            {
                backColor = Color.White;
                leftColumnColor = Color.LightGray;
                borderColor = Color.LightGray;
                menuItemBorderColor = primaryColor;
                menuItemSelectedColor = primaryColor;
            }
        }

        //Overrides
        public override Color ToolStripDropDownBackground => backColor;
        public override Color MenuBorder => borderColor;
        public override Color MenuItemBorder => menuItemBorderColor;
        public override Color MenuItemSelected => menuItemSelectedColor;
        public override Color ImageMarginGradientBegin => leftColumnColor;
        public override Color ImageMarginGradientMiddle => leftColumnColor;
        public override Color ImageMarginGradientEnd => leftColumnColor;
    }
}
