using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Win32.Constants
{ 
    //https://docs.microsoft.com/en-us/windows/win32/controls/tvm-endeditlabelnow
    //#define CCM_FIRST               0x2000      // Common control shared messages

    public enum TreeViewMessages : uint
    {
        /// <summary>
        /// Internal Value
        /// </summary>
        TV_FIRST = 0x1100,
        /// <summary>
        /// Creates a dragging bitmap for the specified item in a tree-view control. 
        /// The message also creates an image list for the bitmap and adds the bitmap to the image list. 
        /// An application can display the image when dragging the item by using the image list functions. 
        /// You can send this message explicitly or by using the TreeView_CreateDragImage macro.
        /// <para>Parameters: wParam -Must be zero. lParam -Handle to the item that receives the new dragging bitmap.</para>
        /// <para>Returns the handle to the image list to which the dragging bitmap was added if successful, or NULL otherwise.</para>
        /// </summary>
        /// <remarks>If you create a tree-view control without an associated image list, 
        /// you cannot use the TVM_CREATEDRAGIMAGE message to create the image to display during a drag operation. 
        /// You must implement your own method of creating a drag cursor. 
        /// Your application is responsible for destroying the image list when it is no longer needed.</remarks>
        TVM_CREATEDRAGIMAGE = TV_FIRST + 18,
        /// <summary>
        /// Removes an item and all its children from a tree-view control. 
        /// You can send this message explicitly or by using the TreeView_DeleteItem macro.
        /// <para>Parameters: wParam -Must be zero. lParam -HTREEITEM handle to the item to delete. 
        /// If lParam is set to TVI_ROOT or to NULL, all items are deleted. 
        /// You can also use the TreeView_DeleteAllItems macro to delete all items.</para>
        /// <para>Returns TRUE if successful, or FALSE otherwise.</para>
        /// </summary>
        /// <remarks>It is not safe to delete items in response to a notification such as TVN_SELCHANGING.
        /// Once an item is deleted, its handle is invalid and cannot be used.
        /// The parent window receives a TVN_DELETEITEM notification code when each item is removed.
        /// If the item label is being edited, 
        /// the edit operation is canceled and the parent window receives the TVN_ENDLABELEDIT notification code.
        /// If you delete all items in a tree-view control that has the TVS_NOSCROLL style, 
        /// items subsequently added may not display properly. For more information, see TreeView_DeleteAllItems.
        /// </remarks>
        TVM_DELETEITEM = TV_FIRST + 1,
        /// <summary>
        /// Begins in-place editing of the specified item's text, 
        /// replacing the text of the item with a single-line edit control containing the text. 
        /// This message implicitly selects and focuses the specified item. 
        /// You can send this message explicitly or by using the TreeView_EditLabel macro.
        /// <para>Parameters: wParam -Must be zero. lParam -Handle to the item to edit.</para>
        /// <para>Returns the handle to the edit control used to edit the item text if successful, or NULL otherwise.</para>
        /// </summary>
        /// <remarks>This message sends a TVN_BEGINLABELEDIT notification code to the parent of the tree-view control.
        /// When the user completes or cancels editing, the edit control is destroyed and the handle is no longer valid. 
        /// You can subclass the edit control, but do not destroy it.
        /// The control must have the focus before you send this message to the control. 
        /// Focus can be set using the SetFocus function.</remarks>
        TVM_EDITLABEL_A = TV_FIRST + 14,
        /// <summary>
        /// Unicode wide format. 
        /// Begins in-place editing of the specified item's text, 
        /// replacing the text of the item with a single-line edit control containing the text. 
        /// This message implicitly selects and focuses the specified item. 
        /// You can send this message explicitly or by using the TreeView_EditLabel macro.
        /// <para>Parameters: wParam -Must be zero. lParam -Handle to the item to edit.</para>
        /// <para>Returns the handle to the edit control used to edit the item text if successful, or NULL otherwise.</para>
        /// </summary>
        /// <remarks>This message sends a TVN_BEGINLABELEDIT notification code to the parent of the tree-view control.
        /// When the user completes or cancels editing, the edit control is destroyed and the handle is no longer valid. 
        /// You can subclass the edit control, but do not destroy it.
        /// The control must have the focus before you send this message to the control. 
        /// Focus can be set using the SetFocus function.</remarks>
        TVM_EDITLABEL_W = TV_FIRST + 65,

        TVM_ENDEDITLABELNOW = TV_FIRST + 22,
        TVM_ENSUREVISIBLE = TV_FIRST + 20,
        TVM_EXPAND = TV_FIRST + 2,
        TVM_GETBKCOLOR = TV_FIRST + 31,
        TVM_GETCOUNT = TV_FIRST + 5,
        TVM_GETEDITCONTROL = TV_FIRST + 15,
        TVM_GETEXTENDEDSTYLE = TV_FIRST + 45,
        TVM_GETIMAGELIST = TV_FIRST + 8,
        TVM_GETINDENT = TV_FIRST + 6,
        TVM_GETINSERTMARKCOLOR = TV_FIRST + 38,
        TVM_GETISEARCHSTRING_A = TV_FIRST + 23,
        /// <summary>
        /// Unicode wide format
        /// </summary>
        TVM_GETISEARCHSTRING_W = TV_FIRST + 64,
        TVM_GETITEM_A = TV_FIRST + 12,
        TVM_GETITEM_W = TV_FIRST + 62,
        TVM_GETITEMHEIGHT = TV_FIRST + 28,
        TVM_GETITEMPARTRECT = TV_FIRST + 72,
        TVM_GETITEMRECT = TV_FIRST + 4,
        TVM_GETITEMSTATE = TV_FIRST + 39,
        TVM_GETLINECOLOR = TV_FIRST + 41,
        TVM_GETNEXTITEM = TV_FIRST + 10,
        TVM_GETSCROLLTIME = TV_FIRST + 34,
        TVM_GETSELECTEDCOUNT = TV_FIRST + 70,
        TVM_GETTEXTCOLOR = TV_FIRST + 32,
        TVM_GETTOOLTIPS = TV_FIRST + 25,
        TVM_GETUNICODEFORMAT = 0x2000 + 6, //= CCM_GETUNICODEFORMAT = CCM_FIRST + 6
        TVM_GETVISIBLECOUNT = TV_FIRST + 16,
        TVM_HITTEST = TV_FIRST + 17,
        TVM_INSERTITEM_A = TV_FIRST + 0,
        TVM_INSERTITEM_W = TV_FIRST + 50,
        TVM_MAPACCIDTOHTREEITEM = TV_FIRST + 42,
        TVM_MAPHTREEITEMTOACCID = TV_FIRST + 43,
        TVM_SELECTITEM = TV_FIRST + 11,
        TVM_SETAUTOSCROLLINFO = TV_FIRST + 59,
        TVM_SETBKCOLOR = TV_FIRST + 29,
        TVM_SETBORDER = TV_FIRST + 35,
        TVM_SETEXTENDEDSTYLE = TV_FIRST + 44,
        TVM_SETHOT = TV_FIRST + 58,
        TVM_SETIMAGELIST = TV_FIRST + 9,
        TVM_SETINDENT = TV_FIRST + 7,
        TVM_SETINSERTMARK = TV_FIRST + 26,
        TVM_SETINSERTMARKCOLOR = TV_FIRST + 37,
        TVM_SETITEM_A = TV_FIRST + 13,
        TVM_SETITEM_W = TV_FIRST + 63,
        TVM_SETITEMHEIGHT = TV_FIRST + 27,
        TVM_SETLINECOLOR = TV_FIRST + 40,
        TVM_SETSCROLLTIME = TV_FIRST + 33,
        TVM_SETTEXTCOLOR = TV_FIRST + 30,
        TVM_SETTOOLTIPS = TV_FIRST + 24,
        TVM_SETUNICODEFORMAT = 0x2000 + 5, // = CCM_SETUNICODEFORMAT = CCM_FIRST + 5
        TVM_SHOWINFOTIP = TV_FIRST + 71,
        TVM_SORTCHILDREN = TV_FIRST + 19,
        TVM_SORTCHILDRENCB = TV_FIRST + 21,
    }
}
