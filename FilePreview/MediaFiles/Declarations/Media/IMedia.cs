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
using Declarations.Events;
using Declarations.Structures;
using System;
using System.Collections.Generic;

namespace Declarations.Media
{
    /// <summary>
    /// Represents a media object (file, network stream, capture device, etc.)
    /// </summary>
    public interface IMedia : IDisposable, IEqualityComparer<IMedia>
    {
        /// <summary>
        /// Sets or gets the media path.
        /// </summary>
        string Input { get; set; }

        /// <summary>
        /// Add options to media.
        /// </summary>
        /// <param name="options">Collection of options</param>
        void AddOptions(IEnumerable<string> options);

        /// <summary>
        /// Gets media state.
        /// </summary>
        MediaState State { get; }

        /// <summary>
        /// Adds option with a configuration flag.
        /// </summary>
        /// <param name="option">Option</param>
        /// <param name="flag">Configuration flag</param>
        void AddOptionFlag(string option, int flag);

        /// <summary>
        /// Duplicates the media instance
        /// </summary>
        /// <returns></returns>
        IMedia Duplicate();

        /// <summary>
        /// Parses media synchronously or asynchronously.
        /// </summary>
        /// <param name="aSync">True for sync ,false for async</param>
        void Parse(bool aSync);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parseFlags"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        void ParseAsyncWithOptions(MediaParseType parseFlags, int timeout);

        /// <summary>
        /// 
        /// </summary>
        MediaParseStatus MediaParseStatus { get; }

        /// <summary>
        /// 
        /// </summary>
        void ParseStop();

        /// <summary>
        /// Gets value indication whether the media is parsed.
        /// </summary>
        bool IsParsed { get; }

        /// <summary>
        /// Gets or sets user defined data for the media.
        /// </summary>
        IntPtr Tag { get; set; }

        /// <summary>
        /// Gets events fired by media instance.
        /// </summary>
        IMediaEvents Events { get; }

        /// <summary>
        /// Gets statistic data associated with current media.
        /// </summary>
        MediaStatistics Statistics { get; }

        /// <summary>
        /// Gets media's sub item nodes.
        /// </summary>
        IMediaList SubItems { get; }

        /// <summary>
        /// Gets information describing media elementary streams.
        /// </summary>
        /// <remarks>Returns array of media tracks info in case of success</remarks>
        MediaTrack[] TracksInfoEx { get; }

        /// <summary>
        /// Gets collection of slave medias which will play synchronously.
        /// </summary>
        ISlaveMediaCollection Slaves { get; }

        /// <summary>
        /// Gets the media type of the media descriptor object
        /// </summary>
        MediaType MediaType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediaTrack"></param>
        /// <returns></returns>
        string GetCodecDescription(MediaTrack mediaTrack);
    }
}
