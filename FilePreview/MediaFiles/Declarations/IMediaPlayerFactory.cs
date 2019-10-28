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
using Declarations.Players;
using Declarations.Media;
using Declarations.VLM;
using Declarations.Discovery;
using Declarations.MediaLibrary;
using Declarations.Attributes;
using Declarations.Structures;
using Declarations.Dialogs;

namespace Declarations
{
    /// <summary>
    /// Defines methods for creating media and player objects
    /// </summary>
    public interface IMediaPlayerFactory : IDisposable, IEqualityComparer<IMediaPlayerFactory>
    {
        /// <summary>
        /// Creates new instance of player.
        /// </summary>
        /// <typeparam name="T">Type of the player to create</typeparam>
        /// <returns>Newly created player</returns>
        T CreatePlayer<T>() where T : IPlayer;

        /// <summary>
        /// Creates new instance of media.
        /// </summary>
        /// <typeparam name="T">Type of media to create</typeparam>
        /// <param name="input">The media input string</param>
        /// <param name="options">Optional media options</param>
        /// <returns>Newly created media</returns>
        T CreateMedia<T>(string input, params string[] options) where T : IMedia;

        /// <summary>
        /// Creates new instance of media with custom data source
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        T CreateMedia<T, TSource>(TSource source, params string[] options) where T : ICustomSourceMedia<TSource>;

        /// <summary>
        /// Creates new instance of media list.
        /// </summary>
        /// <typeparam name="T">Type of media list</typeparam>
        /// <param name="mediaItems">Collection of media inputs</param>       
        /// <param name="options">Options applied on every media instance of the list</param>
        /// <returns>Newly created media list</returns>
        T CreateMediaList<T>(IEnumerable<string> mediaItems, params string[] options) where T : IMediaList;

        /// <summary>
        /// Creates empty media list instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T CreateMediaList<T>() where T : IMediaList;

        /// <summary>
        /// Creates new instance of media list player
        /// </summary>
        /// <typeparam name="T">Type of media list player</typeparam>
        /// <param name="mediaList">Media list</param>
        /// <returns>Newly created media list player</returns>
        T CreateMediaListPlayer<T>(IMediaList mediaList) where T : IMediaListPlayer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IMediaDiscoverer CreateMediaDiscoverer(string name);

        /// <summary>
        /// /
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IRendererDiscovery CreateRendererDiscoverer(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IMediaLibrary CreateMediaLibrary();

        /// <summary>
        /// Gets the libVLC version.
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Gets list of available netwrok rendering services
        /// </summary>
        IEnumerable<RendererService> RendererDiscoveryServices { get; }

        /// <summary>
        /// Gets list of available audio filters
        /// </summary>
        IEnumerable<FilterInfo> AudioFilters { get; }

        /// <summary>
        /// Gets list of available video filters
        /// </summary>
        IEnumerable<FilterInfo> VideoFilters { get; }

        /// <summary>
        /// Gets the VLM instance
        /// </summary>
        IVideoLanManager VideoLanManager { get; }

        /// <summary>
        /// Gets list of available audio output modules
        /// </summary>
        IEnumerable<AudioOutputModuleInfo> AudioOutputModules { get; }

        /// <summary>
        /// Gets list of audio output devices for specified output module
        /// </summary>
        /// <param name="audioOutputModule"></param>
        /// <returns></returns>
        [MaxLibVlcVersion("2.0.50", "GetAudioOutputDevices", Replacement = "GetAudioOutputDevicesEx")]
        IEnumerable<AudioOutputDeviceInfo> GetAudioOutputDevices(AudioOutputModuleInfo audioOutputModule);

        /// <summary>
        /// Gets list of audio output devices for specified output module
        /// </summary>
        /// <param name="audioOutputModule"></param>
        /// <returns></returns>
        [MinimalLibVlcVersion("2.1.0")]
        IEnumerable<AudioOutputDeviceInfo> GetAudioOutputDevicesEx(AudioOutputModuleInfo audioOutputModule);

        /// <summary>
        /// Return the current time in microseconds
        /// </summary>
        long Clock { get; }

        /// <summary>
        /// Return the delay (in microseconds) until a certain timestamp
        /// </summary>
        /// <param name="pts"></param>
        /// <returns></returns>
        long Delay(long pts);

        /// <summary>
        /// Gets error message for the last LibVLC error in the calling thread
        /// </summary>
        string LastErrorMsg { get; }

        /// <summary>
        /// Subscribe to log notifications after they are logged according to the app.config configuration
        /// </summary>
        /// <param name="callback">Callback invoked for every log message</param>
        /// <param name="filter">Filter applied to every log message. If returns true - callback is invoked</param>
        /// <returns>Subscription token</returns>
        IDisposable SubscribeToLogMessages(Action<LogMessage> callback, Predicate<LogMessage> filter = null);

        /// <summary>
        /// Provides pop up dialog events for error, login, progress and other dialog notifications
        /// </summary>
        [MinimalLibVlcVersion("3.0.0")]
        IDialogNotifications DialogNotifications { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yaw"></param>
        /// <param name="pitch"></param>
        /// <param name="roll"></param>
        /// <param name="fieldOfView"></param>
        /// <returns></returns>
        IViewPoint CreateViewPoint(float yaw, float pitch, float roll, float fieldOfView);
    }
}
