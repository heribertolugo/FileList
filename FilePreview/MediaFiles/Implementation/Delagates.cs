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
using System.Runtime.InteropServices;
using LibVlcWrapper;

namespace Implementation
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void VlcEventHandlerDelegate(ref libvlc_event_t libvlc_event, IntPtr userData);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void* LockEventHandler(void* opaque, void** plane);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void UnlockEventHandler(void* opaque, void* picture, void** plane);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void DisplayEventHandler(void* opaque, void* picture);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void* CallbackEventHandler(void* data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate int VideoFormatCallback(void** opaque, char* chroma, int* width, int* height, int* pitches, int* lines);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void CleanupCallback(void* opaque);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void PlayCallbackEventHandler(void* data, void* samples, uint count, long pts);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void VolumeCallbackEventHandler(void* data, float volume, bool mute);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate int SetupCallbackEventHandler(void** data, char* format, int* rate, int* channels);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void AudioCallbackEventHandler(void* data, long pts);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void AudioDrainCallbackEventHandler(void* data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate int ImemGet(void* data, char* cookie, long* dts, long* pts, int* flags, uint* dataSize, void** ppData);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void ImemRelease(void* data, char* cookie, uint dataSize, void* pData); 

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void LogCallback(void* data, libvlc_log_level level, void* ctx, char* fmt, char* args);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate int MediaOpenCallbackEventHandler(void* opaque, void** datap, UInt64* sizep);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate int MediaReadCallbackEventHandler(void* opaque, byte* buf, int len);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate int MediaSeekCallbackEventHandler(void* opaque, long offset);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void MediaCloseCallbackEventHandler(void* opaque);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void DisplayErrorCallback(void* p_data, char* psz_title, char* psz_text);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void DisplayLoginCallback(void* p_data, IntPtr p_id,
                         char* psz_title, char* psz_text,
                         char* psz_default_username,
                         bool b_ask_store);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void DisplayQuestionCallback(void* p_data, IntPtr p_id,
                            char* psz_title, char* psz_text,
                            libvlc_dialog_question_type i_type,
                            char* psz_cancel, char* psz_action1,
                            char* psz_action2);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void DisplayProgressCallback(void* p_data, IntPtr p_id,
                            char* psz_title, char* psz_text,
                            bool b_indeterminate, float f_position,
                            char* psz_cancel);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void CancelCallback(void* p_data, IntPtr p_id);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void UpdateProgressCallback(void* p_data, IntPtr p_id, float f_position, char* psz_text);
}
