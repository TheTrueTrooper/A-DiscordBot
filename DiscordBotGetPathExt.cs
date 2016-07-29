using System;

namespace DiscordBot2._0
{
    /// <summary>
    /// a static (non instanced class of Path getter)
    /// </summary>
    static class PathGetter
    {
        public static string GetSoundBoardPath(string file)
        {
            return Environment.CurrentDirectory + @"\..\..\_SoundBoard_\" + file;
        }

        public static string GetImagePath(string file)
        {
            return Environment.CurrentDirectory + @"\..\..\_Images_\" + file;
        }
    }

}
