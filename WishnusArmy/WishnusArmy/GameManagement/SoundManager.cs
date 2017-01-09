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
    class SoundManager
    {
        float sfxVolume = 0.5f;
        float musicVolume = 1f;
        Song prevSong;
        public SoundManager()
        {

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
        public void PlaySound(SoundEffect effect)
        {
            effect.Play(sfxVolume, 0, 0);
        }
    }
}
