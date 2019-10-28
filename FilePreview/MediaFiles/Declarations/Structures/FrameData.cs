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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Declarations
{
    /// <summary>
    /// Data structure for single frame of elementary stream
    /// </summary>
    public struct FrameData : IEquatable<FrameData>
    {
        /// <summary>
        /// Pointer to the frame data
        /// </summary>
        public IntPtr Data { get; set; }

        /// <summary>
        /// Data size in bytes
        /// </summary>
        public int DataSize { get; set; }

        /// <summary>
        /// Decoding time stamp in microseconds. -1 means unknown
        /// </summary>
        public long DTS { get; set; }

        /// <summary>
        /// Presentation time stamp in microseconds.
        /// </summary>
        public long PTS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void Validate()
        {
            if (Data == IntPtr.Zero)
            {
                throw new ArgumentNullException("Data");
            }

            if (DataSize == 0)
            {
                throw new ArgumentException("DataSize value must be greater than zero", "DataSize");
            }

            if (PTS < 0)
            {
                throw new ArgumentException("Pts value must be greater than zero", "PTS");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(FrameData other)
        {
            return (other.Data == this.Data && other.DataSize == this.DataSize);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEOS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is FrameData && this == (FrameData)obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Pointer address {0} and length {1}", Data.ToInt64(), DataSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator ==(FrameData x, FrameData y)
        {
            return x.Data == y.Data && x.DataSize == y.DataSize;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator !=(FrameData x, FrameData y)
        {
            return !(x == y);
        }
    }
}
