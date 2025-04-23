using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MakeEveryDay.States
{
    internal class SoundsUtils
    {
        public static SoundEffect clickedBlockSound;
        public static SoundEffect connectedBlockSound;

        public static SoundEffect stepSound;
        public static SoundEffect thudSound;
        public static SoundEffect screamSound;

        public static SoundEffectInstance backgroundMusic;
        public static SoundEffectInstance menuMusic;

        public static float mouseClickVolume = .5f;
        private static float musicVolume = .5f;
        public static float soundEffectsVolume = .5f;

        public static float MusicVolume
        {
            get { return musicVolume; }
            set { 
                musicVolume = value;
                InitializeBackgroundMusic();
            }
        }

        public static void InitializeBackgroundMusic()
        {
            backgroundMusic.IsLooped = true;
            backgroundMusic.Volume = .2f * musicVolume;

            menuMusic.IsLooped = true;
            menuMusic.Volume = .2f * musicVolume;

        }
    }
}
