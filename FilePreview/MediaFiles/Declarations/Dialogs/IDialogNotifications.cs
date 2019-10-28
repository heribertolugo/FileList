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

using System;

namespace Declarations.Dialogs
{
    /// <summary>
    /// Provides dialog display notifications
    /// </summary>
    public interface IDialogNotifications : IDisposable
    {
        /// <summary>
        /// Called when a displayed dialog needs to be cancelled
        /// </summary>
        event Action DisplayCancel;

        /// <summary>
        /// Called when an error message needs to be displayed
        /// </summary>
        event Action<DialogInfo> DisplayError;

        /// <summary>
        /// Called when a login dialog needs to be displayed
        /// </summary>
        event Func<LoginDialogInfo, UserCredential> DisplayLogin;

        /// <summary>
        /// Called when a progress dialog needs to be displayed
        /// </summary>
        event Action<ProgressDialogInfo> DisplayProgress;

        /// <summary>
        /// Called when a question dialog needs to be displayed
        /// </summary>
        event Func<QuestionDialogInfo, UserSelection> DisplayQuestion;

        /// <summary>
        /// Called when a progress dialog needs to be updated
        /// </summary>
        event Action<ProgressUpdateInfo> UpdateProgress;
    }
}