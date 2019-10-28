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

namespace LibVlcWrapper
{
    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_log_message_t
    {
        public UInt32 sizeof_msg;
        public Int32 i_severity;
        public IntPtr psz_type;
        public IntPtr psz_name;
        public IntPtr psz_header;
        public IntPtr psz_message;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_media_stats_t
    {
        /* Input */
        public int i_read_bytes;
        public float f_input_bitrate;

        /* Demux */
        public int i_demux_read_bytes;
        public float f_demux_bitrate;
        public int i_demux_corrupted;
        public int i_demux_discontinuity;

        /* Decoders */
        public int i_decoded_video;
        public int i_decoded_audio;

        /* Video Output */
        public int i_displayed_pictures;
        public int i_lost_pictures;

        /* Audio output */
        public int i_played_abuffers;
        public int i_lost_abuffers;

        /* Stream output */
        public int i_sent_packets;
        public int i_sent_bytes;
        public float f_send_bitrate;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_media_track_info_t
    {
        public UInt32 i_codec;
        public int i_id;
        public libvlc_track_type_t i_type;
        public int i_profile;
        public int i_level;

        public libvlc_media_track_info_type audio_video;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct libvlc_media_track_info_type
    {
        [FieldOffset(0)]
        public audio audio;

        [FieldOffset(0)]
        public video video;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct audio
    {
        public int i_channels;
        public int i_rate;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct video
    {
        public int i_height;
        public int i_width;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_event_t
    {
        public libvlc_event_e type;
        public IntPtr p_obj;
        public MediaDescriptorUnion MediaDescriptor;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct MediaDescriptorUnion
    {
        [FieldOffset(0)]
        public media_meta_changed media_meta_changed;

        [FieldOffset(0)]
        public media_subitem_added media_subitem_added;

        [FieldOffset(0)]
        public media_duration_changed media_duration_changed;

        [FieldOffset(0)]
        public media_parsed_changed media_parsed_changed;

        [FieldOffset(0)]
        public media_freed media_freed;

        [FieldOffset(0)]
        public media_state_changed media_state_changed;

        [FieldOffset(0)]
        public media_player_position_changed media_player_position_changed;

        [FieldOffset(0)]
        public media_player_time_changed media_player_time_changed;

        [FieldOffset(0)]
        public media_player_title_changed media_player_title_changed;

        [FieldOffset(0)]
        public media_player_seekable_changed media_player_seekable_changed;

        [FieldOffset(0)]
        public media_player_pausable_changed media_player_pausable_changed;

        [FieldOffset(0)]
        public media_list_item_added media_list_item_added;

        [FieldOffset(0)]
        public media_list_will_add_item media_list_will_add_item;

        [FieldOffset(0)]
        public media_list_item_deleted media_list_item_deleted;

        [FieldOffset(0)]
        public media_list_will_delete_item media_list_will_delete_item;

        [FieldOffset(0)]
        public media_list_player_next_item_set media_list_player_next_item_set;

        [FieldOffset(0)]
        public media_player_snapshot_taken media_player_snapshot_taken;

        [FieldOffset(0)]
        public media_player_length_changed media_player_length_changed;

        [FieldOffset(0)]
        public vlm_media_event vlm_media_event;

        [FieldOffset(0)]
        public media_player_media_changed media_player_media_changed;

        [FieldOffset(0)]
        public media_player_es_changed media_player_es_changed;

        [FieldOffset(0)]
        public media_player_audio_volume media_player_audio_volume;

        [FieldOffset(0)]
        public media_player_audio_device media_player_audio_device;

        [FieldOffset(0)]
        public renderer_discoverer_item_added renderer_discoverer_item_added;

        [FieldOffset(0)]
        public renderer_discoverer_item_deleted renderer_discoverer_item_deleted;
    }

    /* media descriptor */
    [StructLayout(LayoutKind.Sequential)]
    public struct media_meta_changed
    {
        public libvlc_meta_t meta_type;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct media_subitem_added
    {
        public IntPtr new_child;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct media_duration_changed
    {
        public long new_duration;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct media_parsed_changed
    {
        public int new_status;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct media_freed
    {
        public IntPtr md;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct media_state_changed
    {
        public libvlc_state_t new_state;
    }

    /* media instance */
    [StructLayout(LayoutKind.Sequential)]
    public struct media_player_position_changed
    {
        public float new_position;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct media_player_time_changed
    {
        public long new_time;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct media_player_title_changed
    {
        public int new_title;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct media_player_seekable_changed
    {
        public int new_seekable;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct media_player_pausable_changed
    {
        public int new_pausable;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct media_player_scrambled_changed
    {
        int new_scrambled;
    } 

    [StructLayout(LayoutKind.Sequential)]
    public struct media_player_vout
    {
        int new_count;
    } 

    /* media list */
    [StructLayout(LayoutKind.Sequential)]
    public struct media_list_item_added
    {
        public IntPtr item;
        public int index;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct media_list_will_add_item
    {
        public IntPtr item;
        public int index;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct media_list_item_deleted
    {
        public IntPtr item;
        public int index;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct media_list_will_delete_item
    {
        public IntPtr item;
        public int index;
    }

    /* media list player */
    [StructLayout(LayoutKind.Sequential)]
    public struct media_list_player_next_item_set
    {
        public IntPtr item;
    }

    /* snapshot taken */
    [StructLayout(LayoutKind.Sequential)]
    public struct media_player_snapshot_taken
    {
        public IntPtr psz_filename;
    }

    /* Length changed */
    [StructLayout(LayoutKind.Sequential)]
    public struct media_player_length_changed
    {
        public long new_length;
    }

    /* VLM media */
    [StructLayout(LayoutKind.Sequential)]
    public struct vlm_media_event
    {
        public IntPtr psz_media_name;
        public IntPtr psz_instance_name;
    }

    /* Extra MediaPlayer */
    [StructLayout(LayoutKind.Sequential)]
    public struct media_player_media_changed
    {
        public IntPtr new_media;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct media_player_es_changed
    {
        libvlc_track_type_t i_type;
        int i_id;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct media_player_audio_volume
    {
        float volume;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct media_player_audio_device
    {
        IntPtr device;
    } 

    [StructLayout(LayoutKind.Sequential)]
    public struct renderer_discoverer_item_added
    {
        public IntPtr item;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct renderer_discoverer_item_deleted
    {
        public IntPtr item;
    } 

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_module_description_t
    {
        public IntPtr psz_name;
        public IntPtr psz_shortname;
        public IntPtr psz_longname;
        public IntPtr psz_help;
        public IntPtr p_next;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_audio_output_t
    {
        public IntPtr psz_name;
        public IntPtr psz_description;
        public IntPtr p_next;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_track_description_t
    {
        public int i_id;
        public IntPtr psz_name;
        public IntPtr p_next;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_audio_track_t
    {
        public uint i_channels;
        public uint i_rate;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_video_track_t
    {
        public uint i_height;
        public uint i_width;
        public uint i_sar_num;
        public uint i_sar_den;
        public uint i_frame_rate_num;
        public uint i_frame_rate_den;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_subtitle_track_t
    {
        public IntPtr psz_encoding;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_media_track_t
    {
        public uint i_codec;
        public uint i_original_fourcc;
        public int i_id;
        public libvlc_track_type_t i_type;
        public int i_profile;
        public int i_level;
        public IntPtr media;
        public uint i_bitrate;
        public IntPtr psz_language;
        public IntPtr psz_description;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_audio_output_device_t
    {
        public IntPtr p_next;
        public IntPtr psz_device;
        public IntPtr psz_description;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_title_description_t
    {
        Int64 i_duration; /**< duration in milliseconds */
        IntPtr psz_name; /**< title name */
        bool b_menu; /**< info if item was recognized as a menu by the demuxer */
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_chapter_description_t
    {
        Int64 i_time_offset; /**< time-offset of the chapter in milliseconds */
        Int64 i_duration; /**< duration of the chapter in milliseconds */
        IntPtr psz_name; /**< chapter name */
    }

    [StructLayout(LayoutKind.Sequential)]
    public class libvlc_dialog_cbs
    {
        public IntPtr pf_display_error;
        public IntPtr pf_display_login;
        public IntPtr pf_display_question;
        public IntPtr pf_display_progress;
        public IntPtr pf_cancel;
        public IntPtr pf_update_progress;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_media_slave_t
    {
        public IntPtr psz_uri;
        public libvlc_media_slave_type_t i_type;
        public uint i_priority;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_media_discoverer_description_t
    {
        public IntPtr psz_name;
        public IntPtr psz_longname;
        public libvlc_media_discoverer_category_t i_cat;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_video_viewpoint_t
    {
        public float f_yaw;           /**< view point yaw in degrees  ]-180;180] */
        public float f_pitch;         /**< view point pitch in degrees  ]-90;90] */
        public float f_roll;          /**< view point roll in degrees ]-180;180] */
        public float f_field_of_view; /**< field of view in degrees ]0;180[ (default 80.)*/
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_rd_description_t
    {
        public IntPtr psz_name;
        public IntPtr psz_longname;
    }
}