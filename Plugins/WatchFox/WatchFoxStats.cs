using System;
using System.Text;

namespace Kbits.Demonbuddy.Plugins
{
    public class WatchFoxStats
    {
        public double GoldPerHour { get; set; }
        public int Level { get; set; }
        public string HeroClass { get; set; }
        public string Name { get; set; }
        public int Coinage { get; set; }

        public string AsJsonStyledString()
        {
            var sb = new StringBuilder();

            sb.Append("{");

            sb.Append("\"gph\": \"" + Math.Round(GoldPerHour) + "\", ");
            sb.Append("\"level\": \"" + Level + "\", ");
            sb.Append("\"heroClass\": \"" + HeroClass + "\", ");
            sb.Append("\"name\": \"" + Name + "\", ");
            sb.Append("\"coinage\": \"" + Coinage + "\"");



            sb.Append("}");

            return sb.ToString();
        }
    }
}