using System;

namespace Win32.Constants
{
    public enum STREAM_SEEK
    {
        /// <summary>
        /// The new seek pointer is an offset relative to the beginning of the stream. In this case, the dlibMove parameter is the new seek position relative to the beginning of the stream.
        /// </summary>
        STREAM_SEEK_SET = 0,
        /// <summary>
        /// The new seek pointer is an offset relative to the current seek pointer location. In this case, the dlibMove parameter is the signed displacement from the current seek position.
        /// </summary>
        STREAM_SEEK_CUR = 1,
        /// <summary>
        /// The new seek pointer is an offset relative to the end of the stream. In this case, the dlibMove parameter is the new seek position relative to the end of the stream.
        /// </summary>
        STREAM_SEEK_END = 2
    }
}
