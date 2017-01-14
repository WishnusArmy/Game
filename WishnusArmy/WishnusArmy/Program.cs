using System;
using System.Threading;

namespace WishnusArmy
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            using (var game = new WishnusArmy())
            {
                game.Run();
            }
        }
    }
#endif
}
