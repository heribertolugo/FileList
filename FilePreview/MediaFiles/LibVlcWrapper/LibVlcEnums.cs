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

namespace LibVlcWrapper
{
    public enum libvlc_state_t
    {
        libvlc_NothingSpecial = 0,
        libvlc_Opening,
        libvlc_Buffering,
        libvlc_Playing,
        libvlc_Paused,
        libvlc_Stopped,
        libvlc_Ended,
        libvlc_Error
    }

    public enum libvlc_log_messate_t_severity
    {
        INFO = 0,
        ERR = 1,
        WARN = 2,
        DBG = 3
    }

    public enum libvlc_log_level
    {
        LIBVLC_DEBUG = 0,   /**< Debug message */
        LIBVLC_NOTICE = 2,  /**< Important informational message */
        LIBVLC_WARNING = 3, /**< Warning (potential error) message */
        LIBVLC_ERROR = 4    /**< Error message */
    }

    public enum libvlc_event_e
    {
        libvlc_MediaMetaChanged = 0,
        libvlc_MediaSubItemAdded,
        libvlc_MediaDurationChanged,
        libvlc_MediaParsedChanged,
        libvlc_MediaFreed,
        libvlc_MediaStateChanged,
        libvlc_MediaSubItemTreeAdded,

        libvlc_MediaPlayerMediaChanged = 0x100,
        libvlc_MediaPlayerNothingSpecial,
        libvlc_MediaPlayerOpening,
        libvlc_MediaPlayerBuffering,
        libvlc_MediaPlayerPlaying,
        libvlc_MediaPlayerPaused,
        libvlc_MediaPlayerStopped,
        libvlc_MediaPlayerForward,
        libvlc_MediaPlayerBackward,
        libvlc_MediaPlayerEndReached,
        libvlc_MediaPlayerEncounteredError,
        libvlc_MediaPlayerTimeChanged,
        libvlc_MediaPlayerPositionChanged,
        libvlc_MediaPlayerSeekableChanged,
        libvlc_MediaPlayerPausableChanged,
        libvlc_MediaPlayerTitleChanged,
        libvlc_MediaPlayerSnapshotTaken,
        libvlc_MediaPlayerLengthChanged,
        libvlc_MediaPlayerVout,
        libvlc_MediaPlayerScrambledChanged,
        libvlc_MediaPlayerESAdded,
        libvlc_MediaPlayerESDeleted,
        libvlc_MediaPlayerESSelected,
        libvlc_MediaPlayerCorked,
        libvlc_MediaPlayerUncorked,
        libvlc_MediaPlayerMuted,
        libvlc_MediaPlayerUnmuted,
        libvlc_MediaPlayerAudioVolume,
        libvlc_MediaPlayerAudioDevice,
        libvlc_MediaPlayerChapterChanged,

        libvlc_MediaListItemAdded = 0x200,
        libvlc_MediaListWillAddItem,
        libvlc_MediaListItemDeleted,
        libvlc_MediaListWillDeleteItem,
        libvlc_MediaListEndReached,

        libvlc_MediaListViewItemAdded = 0x300,
        libvlc_MediaListViewWillAddItem,
        libvlc_MediaListViewItemDeleted,
        libvlc_MediaListViewWillDeleteItem,

        libvlc_MediaListPlayerPlayed = 0x400,
        libvlc_MediaListPlayerNextItemSet,
        libvlc_MediaListPlayerStopped,

        libvlc_MediaDiscovererStarted = 0x500,
        libvlc_MediaDiscovererEnded,
        libvlc_RendererDiscovererItemAdded,
        libvlc_RendererDiscovererItemDeleted,

        libvlc_VlmMediaAdded = 0x600,
        libvlc_VlmMediaRemoved,
        libvlc_VlmMediaChanged,
        libvlc_VlmMediaInstanceStarted,
        libvlc_VlmMediaInstanceStopped,
        libvlc_VlmMediaInstanceStatusInit,
        libvlc_VlmMediaInstanceStatusOpening,
        libvlc_VlmMediaInstanceStatusPlaying,
        libvlc_VlmMediaInstanceStatusPause,
        libvlc_VlmMediaInstanceStatusEnd,
        libvlc_VlmMediaInstanceStatusError,
    }

    public enum libvlc_playback_mode_t
    {
        libvlc_playback_mode_default,
        libvlc_playback_mode_loop,
        libvlc_playback_mode_repeat
    }

    public enum libvlc_meta_t
    {
        libvlc_meta_Title,
        libvlc_meta_Artist,
        libvlc_meta_Genre,
        libvlc_meta_Copyright,
        libvlc_meta_Album,
        libvlc_meta_TrackNumber,
        libvlc_meta_Description,
        libvlc_meta_Rating,
        libvlc_meta_Date,
        libvlc_meta_Setting,
        libvlc_meta_URL,
        libvlc_meta_Language,
        libvlc_meta_NowPlaying,
        libvlc_meta_Publisher,
        libvlc_meta_EncodedBy,
        libvlc_meta_ArtworkURL,
        libvlc_meta_TrackID,
        libvlc_meta_TrackTotal,
        libvlc_meta_Director,
        libvlc_meta_Season,
        libvlc_meta_Episode,
        libvlc_meta_ShowName,
        libvlc_meta_Actors,
        libvlc_meta_AlbumArtist,
        libvlc_meta_DiscNumber,
        libvlc_meta_DiscTotal
    }

    public enum libvlc_track_type_t
    {
        libvlc_track_unknown = -1,
        libvlc_track_audio = 0,
        libvlc_track_video = 1,
        libvlc_track_text = 2,
    }

    public enum libvlc_video_marquee_option_t
    {
        libvlc_marquee_Enable = 0,

        /// <summary>
        /// Marquee text to display.
        /// (Available format strings:
        /// Time related: %Y = year, %m = month, %d = day, %H = hour,
        /// %M = minute, %S = second, ... 
        /// Meta data related: $a = artist, $b = album, $c = copyright,
        /// $d = description, $e = encoded by, $g = genre,
        /// $l = language, $n = track num, $p = now playing,
        /// $r = rating, $s = subtitles language, $t = title,
        /// $u = url, $A = date,
        /// $B = audio bitrate (in kb/s), $C = chapter,
        /// $D = duration, $F = full name with path, $I = title,
        /// $L = time left,
        /// $N = name, $O = audio language, $P = position (in %), $R = rate,
        /// $S = audio sample rate (in kHz),
        /// $T = time, $U = publisher, $V = volume, $_ = new line) 
        /// </summary>
        libvlc_marquee_Text,

        /// <summary>
        /// Color of the text that will be rendered on 
        /// the video. This must be an hexadecimal (like HTML colors). The first two
        /// chars are for red, then green, then blue. #000000 = black, #FF0000 = red,
        ///  #00FF00 = green, #FFFF00 = yellow (red + green), #FFFFFF = white
        /// </summary>
        libvlc_marquee_Color,

        /// <summary>
        /// Opacity (inverse of transparency) of overlayed text. 0 = transparent, 255 = totally opaque. 
        /// </summary>
        libvlc_marquee_Opacity,

        /// <summary>
        /// You can enforce the marquee position on the video.
        /// </summary>
        libvlc_marquee_Position,

        /// <summary>
        /// Number of milliseconds between string updates. This is mainly useful when using meta data or time format string sequences.
        /// </summary>
        libvlc_marquee_Refresh,

        /// <summary>
        /// Font size, in pixels. Default is -1 (use default font size).
        /// </summary>
        libvlc_marquee_Size,

        /// <summary>
        /// Number of milliseconds the marquee must remain displayed. Default value is 0 (remains forever).
        /// </summary>
        libvlc_marquee_Timeout,

        /// <summary>
        /// X offset, from the left screen edge.
        /// </summary>
        libvlc_marquee_X,

        /// <summary>
        /// Y offset, down from the top.
        /// </summary>
        libvlc_marquee_Y
    }

    public enum libvlc_video_logo_option_t
    {
        libvlc_logo_enable,

        /// <summary>
        /// Full path of the image files to use.
        /// </summary>
        libvlc_logo_file,

        /// <summary>
        /// X coordinate of the logo. You can move the logo by left-clicking it.
        /// </summary>
        libvlc_logo_x,

        /// <summary>
        /// Y coordinate of the logo. You can move the logo by left-clicking it.
        /// </summary>
        libvlc_logo_y,

        /// <summary>
        /// Individual image display time of 0 - 60000 ms.
        /// </summary>
        libvlc_logo_delay,

        /// <summary>
        /// Number of loops for the logo animation. -1 = continuous, 0 = disabled.
        /// </summary>
        libvlc_logo_repeat,

        /// <summary>
        /// Logo opacity value (from 0 for full transparency to 255 for full opacity).
        /// </summary>
        libvlc_logo_opacity,

        /// <summary>
        /// Logo position
        /// </summary>
        libvlc_logo_position,
    }

    public enum libvlc_video_adjust_option_t
    {
        libvlc_adjust_Enable = 0,
        libvlc_adjust_Contrast,
        libvlc_adjust_Brightness,
        libvlc_adjust_Hue,
        libvlc_adjust_Saturation,
        libvlc_adjust_Gamma,
    }

    public enum libvlc_audio_output_device_types_t
    {
        libvlc_AudioOutputDevice_Error = -1,
        libvlc_AudioOutputDevice_Mono = 1,
        libvlc_AudioOutputDevice_Stereo = 2,
        libvlc_AudioOutputDevice_2F2R = 4,
        libvlc_AudioOutputDevice_3F2R = 5,
        libvlc_AudioOutputDevice_5_1 = 6,
        libvlc_AudioOutputDevice_6_1 = 7,
        libvlc_AudioOutputDevice_7_1 = 8,
        libvlc_AudioOutputDevice_SPDIF = 10
    }

    public enum libvlc_audio_output_channel_t
    {
        libvlc_AudioChannel_Error = -1,
        libvlc_AudioChannel_Stereo = 1,
        libvlc_AudioChannel_RStereo = 2,
        libvlc_AudioChannel_Left = 3,
        libvlc_AudioChannel_Right = 4,
        libvlc_AudioChannel_Dolbys = 5
    }

    public enum libvlc_navigate_mode_t
    {
        libvlc_navigate_activate = 0,
        libvlc_navigate_up,
        libvlc_navigate_down,
        libvlc_navigate_left,
        libvlc_navigate_right
    }

    public enum libvlc_media_type_t
    {
        libvlc_media_type_unknown = 0,
        libvlc_media_type_file,
        libvlc_media_type_directory,
        libvlc_media_type_disc,
        libvlc_media_type_stream,
        libvlc_media_type_playlist,
    }

    public enum libvlc_media_parse_flag_t
    {
        /**
         * Parse media if it's a local file
         */
        libvlc_media_parse_local = 0x00,
        /**
         * Parse media even if it's a network file
         */
        libvlc_media_parse_network = 0x01,
        /**
         * Fetch meta and covert art using local resources
         */
        libvlc_media_fetch_local = 0x02,
        /**
         * Fetch meta and covert art using network resources
         */
        libvlc_media_fetch_network = 0x04,

        /**
         * Interact with the user (via libvlc_dialog_cbs) when preparsing this item
         * (and not its sub items). Set this flag in order to receive a callback
         * when the input is asking for credentials.
         */
        libvlc_media_do_interact = 0x08,
    }

    public enum libvlc_media_parsed_status_t
    {
        libvlc_media_parsed_status_skipped = 1,
        libvlc_media_parsed_status_failed,
        libvlc_media_parsed_status_timeout,
        libvlc_media_parsed_status_done,
    }

    public enum libvlc_dialog_question_type
    {
        LIBVLC_DIALOG_QUESTION_NORMAL,
        LIBVLC_DIALOG_QUESTION_WARNING,
        LIBVLC_DIALOG_QUESTION_CRITICAL,
    }

    public enum libvlc_media_slave_type_t
    {
        libvlc_media_slave_type_subtitle,
        libvlc_media_slave_type_audio,
    }

    public enum libvlc_media_player_role
    {
        libvlc_role_None = 0, /**< Don't use a media player role */
        libvlc_role_Music,   /**< Music (or radio) playback */
        libvlc_role_Video, /**< Video playback */
        libvlc_role_Communication, /**< Speech, real-time communication */
        libvlc_role_Game, /**< Video game */
        liblvc_role_Notification, /**< User interaction feedback */
        libvlc_role_Animation, /**< Embedded animation (e.g. in web page) */
        libvlc_role_Production, /**< Audio editting/production */
        libvlc_role_Accessibility, /**< Accessibility */
        libvlc_role_Test, /** Testing */
        libvlc_role_Last
    }

    public enum libvlc_media_discoverer_category_t
    {
        /** devices, like portable music player */
        libvlc_media_discoverer_devices,
        /** LAN/WAN services, like Upnp, SMB, or SAP */
        libvlc_media_discoverer_lan,
        /** Podcasts */
        libvlc_media_discoverer_podcasts,
        /** Local directories, like Video, Music or Pictures directories */
        libvlc_media_discoverer_localdirs,
    }

    public enum libvlc_video_orient_t
    {
        libvlc_video_orient_top_left,       /**< Normal. Top line represents top, left column left. */
        libvlc_video_orient_top_right,      /**< Flipped horizontally */
        libvlc_video_orient_bottom_left,    /**< Flipped vertically */
        libvlc_video_orient_bottom_right,   /**< Rotated 180 degrees */
        libvlc_video_orient_left_top,       /**< Transposed */
        libvlc_video_orient_left_bottom,    /**< Rotated 90 degrees clockwise (or 270 anti-clockwise) */
        libvlc_video_orient_right_top,      /**< Rotated 90 degrees anti-clockwise */
        libvlc_video_orient_right_bottom    /**< Anti-transposed */
    }

    public enum libvlc_video_projection_t
    {
        libvlc_video_projection_rectangular,
        libvlc_video_projection_equirectangular, /**< 360 spherical */

        libvlc_video_projection_cubemap_layout_standard = 0x100,
    }
}
