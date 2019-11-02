namespace Win32.Constants
{
    public static class ShellIconFlags
    {
        /// <summary>
        /// get icon
        /// </summary>
        public const uint SHGFI_ICON = 0x000000100;
        /// <summary>
        /// get display name
        /// </summary>
        public const uint SHGFI_DISPLAYNAME = 0x000000200;
        /// <summary>
        /// get type name
        /// </summary>
        public const uint SHGFI_TYPENAME = 0x000000400;
        /// <summary>
        /// get attributes
        /// </summary>
        public const uint SHGFI_ATTRIBUTES = 0x000000800;
        /// <summary>
        /// get icon location
        /// </summary>
        public const uint SHGFI_ICONLOCATION = 0x000001000;
        /// <summary>
        /// return exe type
        /// </summary>
        public const uint SHGFI_EXETYPE = 0x000002000;
        /// <summary>
        /// get system icon index
        /// </summary>
        public const uint SHGFI_SYSICONINDEX = 0x000004000;
        /// <summary>
        /// put a link overlay on icon
        /// </summary>
        public const uint SHGFI_LINKOVERLAY = 0x000008000;
        /// <summary>
        /// show icon in selected state
        /// </summary>
        public const uint SHGFI_SELECTED = 0x000010000;
        /// <summary>
        /// get only specified attributes
        /// </summary>
        public const uint SHGFI_ATTR_SPECIFIED = 0x000020000;
        /// <summary>
        /// get large icon
        /// </summary>
        public const uint SHGFI_LARGEICON = 0x000000000;
        /// <summary>
        /// get small icon
        /// </summary>
        public const uint SHGFI_SMALLICON = 0x000000001;
        /// <summary>
        /// get open icon
        /// </summary>
        public const uint SHGFI_OPENICON = 0x000000002;
        /// <summary>
        /// get shell size icon
        /// </summary>
        public const uint SHGFI_SHELLICONSIZE = 0x000000004;
        /// <summary>
        /// pszPath is a pidl
        /// </summary>
        public const uint SHGFI_PIDL = 0x000000008;
        /// <summary>
        /// use passed dwFileAttribute
        /// </summary>
        public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
        /// <summary>
        /// apply the appropriate overlays
        /// </summary>
        public const uint SHGFI_ADDOVERLAYS = 0x000000020;
        /// <summary>
        /// Get the index of the overlay in the upper 8 bits of the iIcon
        /// </summary>
        public const uint SHGFI_OVERLAYINDEX = 0x000000040;  
    }
}
