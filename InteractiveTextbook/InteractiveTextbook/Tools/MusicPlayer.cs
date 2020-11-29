using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InteractiveTextbook.Singleton
{
    public class MusicPlayer
    {
        private static MusicPlayer instance = null;

        public bool KeepPlaying = false;

        protected MusicPlayer()
        {
        }

        public static MusicPlayer GetInstance()
        {
            if (instance == null)
            {
                instance = new MusicPlayer();
            }
            return instance;
        }
    }
}
