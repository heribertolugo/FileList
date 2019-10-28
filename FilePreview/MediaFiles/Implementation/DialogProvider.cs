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

using Declarations.Dialogs;
using Declarations.Enums;
using Implementation.Exceptions;
using LibVlcWrapper;
using System;
using System.Runtime.InteropServices;

namespace Implementation
{
    /// <summary>
    /// 
    /// </summary>
    internal unsafe sealed class DialogProvider : IDialogNotifications
    {
        private readonly IntPtr m_hLib;

        /// <summary>
        /// 
        /// </summary>
        public event Action<DialogInfo> DisplayError;

        /// <summary>
        /// 
        /// </summary>
        public event Func<LoginDialogInfo, UserCredential> DisplayLogin;

        /// <summary>
        /// 
        /// </summary>
        public event Func<QuestionDialogInfo, UserSelection> DisplayQuestion;

        /// <summary>
        /// 
        /// </summary>
        public event Action<ProgressDialogInfo> DisplayProgress;

        /// <summary>
        /// 
        /// </summary>
        public event Action DisplayCancel;

        /// <summary>
        /// 
        /// </summary>
        public event Action<ProgressUpdateInfo> UpdateProgress;

        public void Dispose()
        {
            LibVlcMethods.libvlc_dialog_set_callbacks(m_hLib, null, IntPtr.Zero);
        }

        internal DialogProvider(IntPtr hLib)
        {
            m_hLib = hLib;
            libvlc_dialog_cbs callbacks = new libvlc_dialog_cbs();
            InitCallbacks(ref callbacks);
            LibVlcMethods.libvlc_dialog_set_callbacks(hLib, callbacks, IntPtr.Zero);
        }

        private void InitCallbacks(ref libvlc_dialog_cbs cbs)
        {
            DisplayErrorCallback error = new DisplayErrorCallback(pf_display_error);
            DisplayLoginCallback login = new DisplayLoginCallback(pf_display_login);
            DisplayQuestionCallback question = new DisplayQuestionCallback(pf_display_question);
            DisplayProgressCallback progress = new DisplayProgressCallback(pf_display_progress);
            CancelCallback cancel = new CancelCallback(pf_cancel);
            UpdateProgressCallback updateProgress = new UpdateProgressCallback(pf_update_progress);

            cbs.pf_cancel = Marshal.GetFunctionPointerForDelegate(cancel);
            cbs.pf_display_error = Marshal.GetFunctionPointerForDelegate(error);
            cbs.pf_display_login = Marshal.GetFunctionPointerForDelegate(login);
            cbs.pf_display_progress = Marshal.GetFunctionPointerForDelegate(progress);
            cbs.pf_display_question = Marshal.GetFunctionPointerForDelegate(question);
            cbs.pf_update_progress = Marshal.GetFunctionPointerForDelegate(updateProgress);
        }

        private unsafe void pf_display_error(void* p_data, char* psz_title, char* psz_text)
        {
            if(DisplayError == null)
                return;

            DisplayError(new DialogInfo() { Text = new string(psz_text), Title = new string(psz_title) });
        }

        private unsafe void pf_display_login(void* p_data, IntPtr p_id, char* psz_title, char* psz_text, char* psz_default_username, bool b_ask_store)
        {
            if (DisplayLogin == null)
                return;

            var loginInfo = DisplayLogin(new LoginDialogInfo()
            {
                DefaultUsername = new string(psz_default_username),
                DoStore = b_ask_store,
                Text = new string(psz_text),
                Title = new string(psz_title)
            });

            int success = LibVlcMethods.libvlc_dialog_post_login(p_id, loginInfo.UserName.ToUtf8(), loginInfo.Password.ToUtf8(), b_ask_store);
            if(success == -1)
            {
                throw new LibVlcException("Failed to display login dialog");
            }
        }

        private unsafe void pf_display_question(void* p_data, IntPtr p_id, char* psz_title, char* psz_text, libvlc_dialog_question_type i_type,
                                char* psz_cancel, char* psz_action1, char* psz_action2)
        {
            if (DisplayQuestion == null)
                return;

            var questionResult = DisplayQuestion(new QuestionDialogInfo()
            {
                Action1Text = new string(psz_action1),
                Action2Text = new string(psz_action2),
                CancelButtonText = new string(psz_cancel),
                DialogQuestionType = ConvertEnum(i_type),
                Text = new string(psz_text),
                Title = new string(psz_title)
            });

            int success = LibVlcMethods.libvlc_dialog_post_action(p_id, questionResult.Selection);
            if (success == -1)
            {
                throw new LibVlcException("Failed to display dialog");
            }
        }

        private unsafe void pf_display_progress(void* p_data, IntPtr p_id, char* psz_title, char* psz_text, bool b_indeterminate, float f_position,
                                char* psz_cancel)
        {
            if (DisplayProgress == null)
                return;

            DisplayProgress(new ProgressDialogInfo()
            {
                CancelText = new string(psz_cancel),
                Indeterminate = b_indeterminate,
                Position = f_position,
                Text = new string(psz_text),
                Title = new string(psz_title)
            });
        }

        private unsafe void pf_cancel(void* p_data, IntPtr p_id)
        {
            if (DisplayCancel == null)
                return;

            DisplayCancel();
        }

        private unsafe void pf_update_progress(void* p_data, IntPtr p_id, float f_position, char* psz_text)
        {
            if (UpdateProgress == null)
                return;

            UpdateProgress(new ProgressUpdateInfo()
            {
                Position = f_position,
                Text = new string(psz_text)
            });
        }

        private static DialogQuestionType ConvertEnum(libvlc_dialog_question_type i_type)
        {
            switch (i_type)
            {
                case libvlc_dialog_question_type.LIBVLC_DIALOG_QUESTION_NORMAL:
                    return DialogQuestionType.DialogQuestionNormal;
                case libvlc_dialog_question_type.LIBVLC_DIALOG_QUESTION_WARNING:
                    return DialogQuestionType.DialogQuestionWarning;
                case libvlc_dialog_question_type.LIBVLC_DIALOG_QUESTION_CRITICAL:
                    return DialogQuestionType.DialogQuestionCritical;
            }

            throw new InvalidCastException("Unexpected enum value " + i_type);
        }
    }
}
