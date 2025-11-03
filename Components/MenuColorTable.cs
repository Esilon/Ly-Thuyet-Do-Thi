namespace DoThi.Components
{
    public class MenuColorTable : ProfessionalColorTable
    {
        // Fields
        private readonly Color _backColor;
        private readonly Color _leftColumnColor;
        private readonly Color _borderColor;
        private readonly Color _menuItemBorderColor;
        private readonly Color _menuItemSelectedColor;

        // Constructor
        public MenuColorTable(bool isMainMenu, Color primaryColor)
        {
            _menuItemBorderColor = primaryColor;
            _menuItemSelectedColor = primaryColor;

            if (isMainMenu)
            {
                _backColor = Color.FromArgb(37, 39, 60);
                _leftColumnColor = Color.FromArgb(32, 33, 51);
                _borderColor = Color.FromArgb(32, 33, 51);
            }
            else
            {
                _backColor = Color.White;
                _leftColumnColor = Color.LightGray;
                _borderColor = Color.LightGray;
            }
        }

        // Overrides
        public override Color ToolStripDropDownBackground => _backColor;
        public override Color MenuBorder => _borderColor;
        public override Color MenuItemBorder => _menuItemBorderColor;
        public override Color MenuItemSelected => _menuItemSelectedColor;
        public override Color ImageMarginGradientBegin => _leftColumnColor;
        public override Color ImageMarginGradientMiddle => _leftColumnColor;
        public override Color ImageMarginGradientEnd => _leftColumnColor;
    }
}
