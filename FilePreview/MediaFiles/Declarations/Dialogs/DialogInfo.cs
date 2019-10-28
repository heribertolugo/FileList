//    nVLC
//    
//    Author:  Roman Ginzburg
//
//    nVLC is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    nVLC is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//     
// ========================================================================

using Declarations.Enums;

namespace Declarations.Dialogs
{
    /// <summary>
    /// 
    /// </summary>
    public class DialogInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LoginDialogInfo : DialogInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string DefaultUsername { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DoStore { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class QuestionDialogInfo : DialogInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public DialogQuestionType DialogQuestionType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CancelButtonText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Action1Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Action2Text { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ProgressDialogInfo : DialogInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Indeterminate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Position { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CancelText { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ProgressUpdateInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public float Position { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserCredential
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserSelection
    {
        /// <summary>
        /// 
        /// </summary>
        public int Selection { get; set; }
    }
}
