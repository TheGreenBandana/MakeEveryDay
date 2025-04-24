using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;

namespace MakeEveryDay.States
{
    internal class SoundsUtils
    {
        public static SoundEffect clickedBlockSound;
        public static SoundEffect connectedBlockSound;

        public static SoundEffect stepSound;
        public static SoundEffect thudSound;
        public static SoundEffect screamSound;
        public static SoundEffect blockSnapSound;
        public static SoundEffect paperCrumpleSound;
        public static SoundEffect statsLowWarning;

        public static SoundEffectInstance backgroundMusic;
        public static SoundEffectInstance menuMusic;
        public static SoundEffectInstance deathMusic;

        public static float mouseClickVolume = .4f;
        private static float musicVolume = .5f;
        public static float soundEffectsVolume = .75f;

        public static float MusicVolume
        {
            get { return musicVolume; }
            set { 
                musicVolume = Math.Clamp(value, 0f, 1f);
                InitializeBackgroundMusic();
            }
        }
        public static float SFXVolume
        {
            get { return soundEffectsVolume; }
            set
            {
                soundEffectsVolume = Math.Clamp(value, 0f, 1f);
            }
        }
        public static float ClickVolume
        {
            get { return mouseClickVolume; }
            set
            {
                mouseClickVolume = Math.Clamp(value, 0f, 1f);
            }
        }

        public static void InitializeBackgroundMusic()
        {
            menuMusic.IsLooped = true;
            menuMusic.Volume = .7f * musicVolume;

            backgroundMusic.IsLooped = true;
            backgroundMusic.Volume = .5f * musicVolume;

            deathMusic.IsLooped = true;
            deathMusic.Volume = .8f * musicVolume;

        }
    }
}
