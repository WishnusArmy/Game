using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ContentImporter.Music;

namespace WishnusArmy.GameManagement
{
    public class SoundManager
    {
        public List<SoundEffectInstance> instanceList;
        public SoundEffectInstance instance;
        float sfxVolume = 0.2f;
        float musicVolume = 1f;
        Song prevSong;
        public SoundManager()
        {
            instanceList = new List<SoundEffectInstance>();
        }
        public void PlayMusic(string state)
        {
            Song song = null;
            Boolean repeating = true;

            //check wich state we are in and assign it a song
            switch(state)
            {
                case "MainMenuState":
                case "HelpState":
                case "CreditsState":
                    song = SNG_MAINMENU; repeating = true;  break;
                default:
                    //het is blijkbaar onmogelijk om een list van songs te maken dus dan maar zo.
                    switch (Constant.RANDOM.Next(4))
                    {
                        case 0:
                            song = SNG_THE_GAME_IS_ON;
                            break;
                        case 1:
                            song = SNG_LAST_DAWN;
                            break;
                        case 2:
                            song = SNG_RUN;
                            break;
                        case 3:
                            song = SNG_FALL;
                            break;
                    }
                    repeating = false;
                    break;
            }

            //if the same song is playing, keep playing that song
            if (prevSong == song)
                return;
            prevSong = song;
            //if there is a song
            if (song != null)
            {
                MediaPlayer.Volume = musicVolume;
                MediaPlayer.Play(song);
                MediaPlayer.IsRepeating = repeating;
            }
            else
                MediaPlayer.Stop();
        }
        public void PlaySound(SoundEffect effect, Boolean looping = false)
        {
            if (!looping)
            {
                effect.Play(sfxVolume, 0, 0);
                return;
            }
            if (looping)
            {
                instance = effect.CreateInstance();
                instance.IsLooped = true;
                instanceList.Add(instance);
                instance.Play();
            }
        }
        public void StopSoundloops()
        {
            //deze shit werkt alleen als je hem meteen roept nadat je de sounds speelt, help.
            foreach (SoundEffectInstance x in instanceList)
                x.Dispose();
        }
        public Boolean Finished()
        {
            return (MediaPlayer.State == MediaState.Stopped);
        }
    }
}
