using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kbits.Demonbuddy.Plugins
{
    public class WatchFoxLootedItem
    {
        public string Quality { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        public string AsJsonStyledString()
        {
            var sb = new StringBuilder();

            sb.Append("{");

            sb.Append("\"quality\": \"" + Quality + "\", ");
            sb.Append("\"type\": \"" + Type + "\", ");
            sb.Append("\"name\": \"" + Name + "\"");

            sb.Append("}");

            return sb.ToString();
        }
    }
}
