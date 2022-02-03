using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Win32.Constants
{
        //https://docs.microsoft.com/en-us/windows/win32/controls/tree-view-control-window-extended-styles
    public enum TreeViewExtendedStyles
    {
        /// <summary>
        /// Windows Vista. Remove the horizontal scroll bar and auto-scroll depending on mouse position.
        /// </summary>
        TVS_EX_AUTOHSCROLL = 0x0020,
        /// <summary>
        /// Windows Vista. Include dimmed checkbox state if the control has the TVS_CHECKBOXES style.
        /// </summary>
        TVS_EX_DIMMEDCHECKBOXES = 0x0200,
        /// <summary>
        /// Windows Vista. Specifies how the background is erased or filled.
        /// </summary>
        TVS_EX_DOUBLEBUFFER = 0x0004,
        /// <summary>
        /// Windows Vista. Retrieves calendar grid information.
        /// </summary>
        TVS_EX_DRAWIMAGEASYNC = 0x0400,
        /// <summary>
        /// Windows Vista. Include exclusion checkbox state if the control has the TVS_CHECKBOXES style.
        /// </summary>
        TVS_EX_EXCLUSIONCHECKBOXES = 0x0100,
        /// <summary>
        /// Windows Vista. Fade expando buttons in or out when the mouse moves away or into a state of hovering over the control.
        /// </summary>
        TVS_EX_FADEINOUTEXPANDOS = 0x0040,
        /// <summary>
        /// Not supported. Do not use.
        /// </summary>
        TVS_EX_MULTISELECT = 0x0002,
        /// <summary>
        /// Windows Vista. Do not indent the tree view for the expando buttons.
        /// </summary>
        TVS_EX_NOINDENTSTATE = 0x0008,
        /// <summary>
        /// Windows Vista. Intended for internal use; not recommended for use in applications. Do not collapse the previously selected tree-view item unless it has the same parent as the new selection. This style must be used with the TVS_SINGLEEXPAND style.
        /// <para>[!Note]
        /// This style may not be supported in future versions of Comctl32.dll. Also, this style is not defined in commctrl.h. 
        /// Add the following definition to the source files of your application to use this style: 
        /// #define TVS_EX_NOSINGLECOLLAPSE 0x0001
        /// </para></summary>
        TVS_EX_NOSINGLECOLLAPSE = 0x0001,
        /// <summary>
        /// Windows Vista. Include partial checkbox state if the control has the TVS_CHECKBOXES style.
        /// </summary>
        TVS_EX_PARTIALCHECKBOXES = 0x0080,
        /// <summary>
        /// Windows Vista. Allow rich tooltips in the tree view (custom drawn with icon and text).
        /// </summary>
        TVS_EX_RICHTOOLTIP = 0x0010
    }
}
