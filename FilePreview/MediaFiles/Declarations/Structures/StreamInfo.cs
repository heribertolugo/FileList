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
using System.Diagnostics;
using System.Linq;
using System.Text;
using Declarations.Enums;

namespace Declarations
{
    /// <summary>
    /// Specifies the attributes of an elementary stream
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Category={Category}, ID={ID}, Codec={Codec}")]
    public class StreamInfo : IComparable<StreamInfo>
    {
        public StreamInfo()
        {
            ID = 1;
            Group = 1;
        }
        /// <summary>
        /// Set the category of the elementary stream
        /// </summary>
        public StreamCategory Category { get; set; }

        /// <summary>
        /// Set the ID of the elementary stream
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Set the group of the elementary stream
        /// </summary>
        public int Group { get; set; }

        /// <summary>
        /// Size of stream in bytes
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Frame rate of a video elementary stream
        /// </summary>
        public int FPS { get; set; }

        /// <summary>
        /// Width of video or subtitle elementary streams
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of video or subtitle elementary streams
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Set the codec of the elementary stream
        /// </summary>
        public Enum Codec { get; set; }  
   
        /// <summary>
        /// Display aspect ratio of a video elementary stream
        /// </summary>
        public AspectRatioMode AspectRatio { get; set; }

        /// <summary>
        /// Sample rate of an audio elementary stream
        /// </summary>
        public int Samplerate { get; set; }

        /// <summary>
        /// Channels count of an audio elementary stream
        /// </summary>
        public int Channels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static StreamInfo FromBitmapFormat(BitmapFormat format)
        {
            return new StreamInfo()
            {
                Category = StreamCategory.Video,
                Codec = VideoCodecs.BGR24,
                Width = format.Width,
                Height = format.Height,
                Size = format.ImageSize
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static StreamInfo FromSoundFormat(SoundFormat format)
        {
            return new StreamInfo()
            {
                Category = StreamCategory.Audio,
                Channels = 2, // as of libVLC 2.1 imem accepts only 2 channel audio
                Codec = SoundType.f32l,
                Samplerate = format.Rate,
                Size = 15 * 1024 // need to find a way to estimate audio frame size....
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public void Validate()
        {
            switch (Category)
            {
                case StreamCategory.Audio:
                    if (Codec == null || Channels <= 0 || Samplerate <= 0 || Size <= 0)
                        throw new InvalidOperationException("Missing settings in audio stream information");
                    break;
                case StreamCategory.Video:
                    if (Codec == null || Width <= 0 || Height <= 0 || FPS <= 0 || Size <= 0)
                        throw new InvalidOperationException("Missing settings in video stream information");
                    break;
                case StreamCategory.Subtitle:
                    break;
                case StreamCategory.Data:
                    break;

                case StreamCategory.Unknown:
                default:
                    throw new InvalidOperationException("Unknown media type");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(StreamInfo other)
        {
            if (other == null)
            {
                return 1;
            }

            if (other.ID > this.ID)
                return -1;
            if (other.ID == this.ID)
                return 0;
            else
                return 1;
        }
    }
}
