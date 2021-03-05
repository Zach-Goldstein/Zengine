using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Audio;

namespace Engine
{
    public class Sound
    {
        private static HashSet<string> sounds;
        private static Dictionary<string, SoundEffectInstance> soundEffects;

        public static void Initialize()
        {
            sounds = new HashSet<string>();
            soundEffects = new Dictionary<string, SoundEffectInstance>();
        }

        public static void LoadSoundEffect(string id, string filename)
        {
            sounds.Add(id);
            soundEffects.Add(id, Core.Instance.Content.Load<SoundEffectInstance>(filename));
        }

        public static void PlaySoundEffect(string id, bool repeat = false)
        {
            if (sounds.Contains(id))
            {
                SoundEffectInstance s = soundEffects[id];
                s.IsLooped = repeat;
                s.Play();
            }
        }

        public static void StopSoundEffect(string id)
        {
            if (sounds.Contains(id))
                if (soundEffects[id].State == SoundState.Playing)
                    soundEffects[id].Stop();
        }
    }
}
