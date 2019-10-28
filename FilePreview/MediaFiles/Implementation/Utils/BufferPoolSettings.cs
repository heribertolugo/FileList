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

namespace Implementation.Utils
{
    struct BufferPoolSettings
    {
        /// <summary>
        /// Size of memory buffer in bytes.
        /// </summary>
        public int BufferSize { get; set; }

        /// <summary>
        /// Initial number of buffers to allocate.
        /// </summary>
        public int NumOfBuffers { get; set; }

        /// <summary>
        /// Default value is 0.3, means that requesting buffer when there is no available buffers, will
        /// allocate additional GrowthRatio * total allocated number of buffers.
        /// </summary>
        public double GrowthRatio { get; set; }

        /// <summary>
        /// Default value is 2, means that when buffer is returned to pool and number of allocated buffers 
        /// exceeds NumOfBuffers * ShrinkRatio, the buffer will be deallocated.
        /// </summary>
        public double ShrinkRatio { get; set; }

        /// <summary>
        /// Maximum number of memory buffers that may be created. Default value is int.MaxValue.
        /// </summary>
        public int MaxBuffers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bufferSize"></param>
        /// <param name="numOfBuffers"></param>
        /// <returns></returns>
        public static BufferPoolSettings CreateDefault(int bufferSize, int numOfBuffers)
        {
            var bps = new BufferPoolSettings();
            bps.BufferSize = bufferSize;
            bps.GrowthRatio = 0.3;
            bps.MaxBuffers = int.MaxValue;
            bps.NumOfBuffers = numOfBuffers;
            bps.ShrinkRatio = 2.0;
            return bps;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Validate()
        {
            if (BufferSize <= 0)
                throw new ArgumentOutOfRangeException("BufferSize", "Buffer size must be greater than 0");

            if (NumOfBuffers <= 0)
                throw new ArgumentOutOfRangeException("NumOfBuffers", "Number of buffers must be greater than 0");
        }
    }
}
