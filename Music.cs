using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Audio;

namespace CandideTextAdventure.MusicSystem
{
    internal static class MusicSystem
    {
        private static MusicRedirect[] musicRedirects;
        public static string MusicDirectory;
        private static bool Switch;
        public static float Volume { get; private set; }
        public static bool IsPlaying { get; private set; }
        static MusicSystem()
        {
            MusicDirectory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) +
                             "\\music\\";
            Volume = 100;
            musicRedirects = new MusicRedirect[2];
            for (int i = 0; i < musicRedirects.Count(); i++)
            {
                musicRedirects[i] = new MusicRedirect();
            }
        }

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
            if(Filename.ToLower() == "mario game over.ogg")
                musicRedirects[i2].Loop = false;
        }

        public static void Pause()
        {
            foreach (MusicRedirect m in musicRedirects)
            {
                if(m.Status == SoundStatus.Playing)
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

        private class MusicRedirect : Music
        {
            public MusicRedirect(string filename)
                : base(MusicDirectory + filename)
            {
                Loop = true;
                this.Volume = MusicSystem.Volume;
            }

            public MusicRedirect()
                : this("silent.ogg")
            {
            }

        }
    }
}
