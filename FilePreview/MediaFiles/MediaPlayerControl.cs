using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Declarations.Players;
using Declarations;
using Declarations.Media;
using Implementation;
using Declarations.Events;

namespace FilePreview.MediaFiles
{
    public partial class MediaPlayerControl : UserControl
    {
        IMediaPlayerFactory _factory;
        IDiskPlayer _player;
        IMediaFromFile _media;
     
        public MediaPlayerControl()
        {
            InitializeComponent();

            this._factory = new MediaPlayerFactory(true);
            this._player = this._factory.CreatePlayer<IDiskPlayer>();

            this._player.Events.PlayerPositionChanged += new EventHandler<MediaPlayerPositionChanged>(Events_PlayerPositionChanged);
            this._player.Events.TimeChanged += new EventHandler<MediaPlayerTimeChanged>(Events_TimeChanged);
            this._player.Events.MediaEnded += new EventHandler(Events_MediaEnded);
            this._player.Events.PlayerStopped += new EventHandler(Events_PlayerStopped);
            this._player.Volume = 50;
            this._player.WindowHandle = this.viewerPanel.Handle;
            this.volumeTrackBar.Value = this._player.Volume > 0 ? this._player.Volume : 0;

            UISync.Init(this);
        }

        public void Play(string path)
        {
            this.Reset();
            this._media = this._factory.CreateMedia<IMediaFromFile>(path);
            this._media.Events.DurationChanged += new EventHandler<MediaDurationChange>(Events_DurationChanged);
            this._media.Events.StateChanged += new EventHandler<MediaStateChange>(Events_StateChanged);
            this._media.Events.ParsedChanged += new EventHandler<MediaParseChange>(Events_ParsedChanged);

            this._player.Open(_media);
            this._media.Parse(true);

            this._player.Play();
            this.playButton.BackgroundImage = Properties.Resources.pauseButton;
        }

        public void Reset()
        {
            if (this._player.IsPlaying)
                this._player.Stop();
            this.InitControls();
        }

        void Events_PlayerStopped(object sender, EventArgs e)
        {
            UISync.Execute(() => InitControls());
        }

        void Events_MediaEnded(object sender, EventArgs e)
        {
            UISync.Execute(() => InitControls());
        }

        private void InitControls()
        {
            if (this._media != null)
            {
                this._player.Open(this._media);
                this._media.Parse(true);
            }
            this.framesTrackBar.Value = 0;
            this.timeLabel.Text = "00:00:00";
            this.durationLabel.Text = "00:00:00";
            this.playButton.BackgroundImage = Properties.Resources.palyButton;
        }

        void Events_TimeChanged(object sender, MediaPlayerTimeChanged e)
        {
            UISync.Execute(() => this.timeLabel.Text = TimeSpan.FromMilliseconds(e.NewTime).ToString().Substring(0, 8));
        }

        void Events_PlayerPositionChanged(object sender, MediaPlayerPositionChanged e)
        {
            UISync.Execute(() => this.framesTrackBar.Value = (int)(e.NewPosition * 100));
        }

        void Events_StateChanged(object sender, MediaStateChange e)
        {
            //UISync.Execute(() => stateLabel.Text = e.NewState.ToString());
        }

        void Events_DurationChanged(object sender, MediaDurationChange e)
        {
            UISync.Execute(() => this.durationLabel.Text = TimeSpan.FromMilliseconds(e.NewDuration).ToString().Substring(0, 8));
        }

        void Events_ParsedChanged(object sender, MediaParseChange e)
        {
            Console.WriteLine(e.Parsed);
        }

        private void volumeTrackBar_Scroll(object sender, EventArgs e)
        {
            this._player.Volume = this.volumeTrackBar.Value;
        }

        private void framesTrackBar_Scroll(object sender, EventArgs e)
        {
            this._player.Position = (float)framesTrackBar.Value / 100;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this._player.Stop();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (this._player.IsPlaying)
            {
                this._player.Pause();
                this.playButton.BackgroundImage = Properties.Resources.palyButton;
            }
            else
            {
                this._player.Play();
                this.playButton.BackgroundImage = Properties.Resources.pauseButton;
            }
        }

        private void muteButton_Click(object sender, EventArgs e)
        {
            this._player.ToggleMute();

            this.muteButton.BackgroundImage = this._player.Mute ? Properties.Resources.noSoundButton : Properties.Resources.soundButton;
        }



        ~MediaPlayerControl()
        {
            if (this._factory != null)
                this._factory.Dispose();
            if (this._player != null)
                this._player.Dispose();
            if (this._media != null)
                this._media.Dispose();
            Dispose(false);
        }

        private class UISync
        {
            private static ISynchronizeInvoke Sync;

            public static void Init(ISynchronizeInvoke sync)
            {
                Sync = sync;
            }

            public static void Execute(Action action)
            {
                Sync.BeginInvoke(action, null);
            }
        }
    }
}
