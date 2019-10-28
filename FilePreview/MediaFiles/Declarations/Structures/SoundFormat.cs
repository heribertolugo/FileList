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
using Declarations.Enums;

namespace Declarations
{
    /// <summary>
    /// Specifies the parameters of the sound.
    /// </summary>
    [Serializable]
    public class SoundFormat : IEquatable<SoundFormat>
    {
        /// <summary>
        /// Initializes new instance of SoundFormat class
        /// </summary>
        /// <param name="soundType"></param>
        /// <param name="rate"></param>
        /// <param name="channels"></param>
        public SoundFormat(SoundType soundType, int rate, int channels)
        {
            SoundType = soundType;
            Format = soundType.ToString();
            Rate = rate;
            Channels = channels;
            Init();
            BlockSize = BitsPerSample / 8 * Channels;
            UseCustomAudioRendering = true;
        }

        private void Init()
        {
            switch (SoundType)
            {
                case SoundType.S16N:
                    BitsPerSample = 16;
                    break;

                case SoundType.f32l:
                    BitsPerSample = 32;
                    break;

                default:
                    throw new InvalidOperationException("Unknown sound type " + SoundType);
            }
        }

        /// <summary>
        /// Audio format
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// Sampling rate in Hz
        /// </summary>
        public int Rate { get; private set; }

        /// <summary>
        /// Number of channels used by audio sample
        /// </summary>
        public int Channels { get; private set; }

        /// <summary>
        /// Size of single audio sample in bytes
        /// </summary>
        public int BitsPerSample { get; private set; }

        /// <summary>
        /// Specifies sound sample format
        /// </summary>
        public SoundType SoundType { get; private set; }

        /// <summary>
        /// Size of audio block in bytes (BitsPerSample / 8 * Channels)
        /// </summary>
        public int BlockSize { get; private set; }

        /// <summary>
        /// Indicated whether to use custom audio renderer (True), or to use default audio output (False)
        /// </summary>
        public bool UseCustomAudioRendering { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SoundFormat other)
        {
            if (other == null)
                return false;

            if (this.Rate == other.Rate &&
                this.Channels == other.Channels &&
                this.SoundType == other.SoundType)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            SoundFormat format = obj as SoundFormat;
            if (format == null)
                return false;
            else
                return Equals(format);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.Rate.GetHashCode() ^ this.Channels.GetHashCode() ^ this.SoundType.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format1"></param>
        /// <param name="format2"></param>
        /// <returns></returns>
        public static bool operator ==(SoundFormat format1, SoundFormat format2)
        {
            if ((object)format1 == null || ((object)format2) == null)
                return Object.Equals(format1, format2);

            return format1.Equals(format2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format1"></param>
        /// <param name="format2"></param>
        /// <returns></returns>
        public static bool operator !=(SoundFormat format1, SoundFormat format2)
        {
            if (format1 == null || format2 == null)
                return !Object.Equals(format1, format2);

            return !(format1.Equals(format2));
        }
    }
}
