﻿using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SFML.Audio;

namespace CandideTextAdventure.MusicSystem
{
    internal static class MusicSystem
    {
        private static readonly MusicRedirect[] musicRedirects;
        public static string MusicDirectory;
        private static bool Switch;

        static MusicSystem()
        {
            MusicDirectory = Path.GetDirectoryName(Application.ExecutablePath) +
                             "\\music\\";
            Volume = 100;
            LastSong = "NONE";
            musicRedirects = new MusicRedirect[2];
            for (int i = 0; i < musicRedirects.Count(); i++)
            {
                musicRedirects[i] = new MusicRedirect();
            }
        }

        public static float Volume { get; private set; }
        public static bool IsPlaying { get; private set; }
        public static string LastSong { get; private set; }

        public static void ChangeSong(string Filename)
        {
            int i1, i2;
            i1 = Switch ? 0 : 1;
            i2 = Switch ? 1 : 0;
            musicRedirects[i1].Stop();
            musicRedirects[i2].Stop();
            musicRedirects[i2] = new MusicRedirect(Filename);
            musicRedirects[i2].Play();
            Switch = !Switch;
            IsPlaying = true;
            if (Filename.ToLower() == "mario game over.ogg")
                musicRedirects[i2].Loop = false;
            LastSong = Filename;
        }

        public static void Pause()
        {
            foreach (MusicRedirect m in musicRedirects)
            {
                if (m.Status == SoundStatus.Playing)
                    m.Pause();
            }
            IsPlaying = false;
        }

        public static void Resume()
        {
            foreach (MusicRedirect m in musicRedirects)
            {
                if (m.Status == SoundStatus.Paused)
                    m.Play();
            }
            IsPlaying = true;
        }

        public static void SetVolume(float vol)
        {
            Volume = Math.Max(0, Math.Min(100, vol));
            foreach (MusicRedirect m in musicRedirects)
                m.Volume = Volume;
        }

        #region Nested type: MusicRedirect

        private class MusicRedirect : Music
        {
            public MusicRedirect(string filename)
                : base(MusicDirectory + filename)
            {
                Loop = true;
                Volume = MusicSystem.Volume;
            }

            public MusicRedirect()
                : this("silent.ogg")
            {
            }

            public new void Play()
            {
                var t = new Thread(_play);
                t.Start(this);
            }

            private static void _play(object obj)
            {
                var m = (MusicRedirect) obj;
                m.Volume = 0;
                ((Music) obj).Play();
                for (int i = 0; i < MusicSystem.Volume; i++)
                {
                    m.Volume = i;
                    Thread.Sleep(50);
                }
            }

            public new void Stop()
            {
                var t = new Thread(_stop);
                t.Start(this);
            }

            private static void _stop(object obj)
            {
                var m = (Music) obj;
                for (float i = m.Volume - 1; i > 0; i--)
                {
                    m.Volume = i;
                    Thread.Sleep(50);
                }
            }
        }

        #endregion
    }
}