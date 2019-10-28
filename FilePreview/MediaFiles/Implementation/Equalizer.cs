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

//using LibVlcWrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class Equalizer : DisposableBase
    {
        private IntPtr _handle;
        private ReadOnlyCollection<Band> _bands;

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<Preset> Presets
        {
            get
            {
                int count = LibVlcWrapper.LibVlcMethods.libvlc_audio_equalizer_get_preset_count();
                for (int i = 0; i < count; i++)
                {
                    yield return new Preset(i, Marshal.PtrToStringAnsi(LibVlcWrapper.LibVlcMethods.libvlc_audio_equalizer_get_preset_name(i)));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Equalizer()
        {
            _handle = LibVlcWrapper.LibVlcMethods.libvlc_audio_equalizer_new();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="preset"></param>
        public Equalizer(Preset preset)
        {
            _handle = LibVlcWrapper.LibVlcMethods.libvlc_audio_equalizer_new_from_preset(preset.Index);
        }

        /// <summary>
        /// 
        /// </summary>
        public double Preamp
        {
            get 
            {
                return LibVlcWrapper.LibVlcMethods.libvlc_audio_equalizer_get_preamp(_handle); 
            }
            set 
            {
                LibVlcWrapper.LibVlcMethods.libvlc_audio_equalizer_set_preamp(_handle, value);
            }
        }

        protected override void Dispose(bool disposing)
        {
            LibVlcWrapper.LibVlcMethods.libvlc_audio_equalizer_release(_handle);
        }

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<Band> Bands
        {
            get
            {
                if (_bands == null)
                {
                    int count = LibVlcWrapper.LibVlcMethods.libvlc_audio_equalizer_get_band_count();
                    List<Band> temp = new List<Band>(count);
                    for (int i = 0; i < count; i++)
                    {
                        temp.Add(new Band(i, LibVlcWrapper.LibVlcMethods.libvlc_audio_equalizer_get_band_frequency(i), _handle));
                    }

                    _bands = new ReadOnlyCollection<Band>(temp);
                }

                return _bands;
            }
        }

        internal IntPtr Handle
        {
            get
            {
                return _handle;
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public sealed class Preset
    {
        internal Preset(int index, string name)
        {
            Index = index;
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class Band
    {
        private IntPtr _handle;

        internal Band(int index, double frequency, IntPtr hEqualizer)
        {
            Index = index;
            Frequency = frequency;
            _handle = hEqualizer;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public double Frequency { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public double Amplitude 
        { 
            get
            {
                return LibVlcWrapper.LibVlcMethods.libvlc_audio_equalizer_get_amp_at_index(_handle, Index);
            }
            set
            {
                LibVlcWrapper.LibVlcMethods.libvlc_audio_equalizer_set_amp_at_index(_handle, value, Index);
            }
        }

        public override string ToString()
        {
            return string.Format("Frequency : {0}, Amplitude : {1}", Frequency, Amplitude);
        }
    }
}
