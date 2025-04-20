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

        public static void InitializeBackgroundMusic()
        {
            backgroundMusic.IsLooped = true;
            backgroundMusic.Volume = .1f;
            backgroundMusic.Play();
        }
    }
}
