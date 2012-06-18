using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zeta.Internals.Actors;

namespace Kbits.Demonbuddy.Plugins
{
    public class LootedItem
    {
        public ACDItem Item { get; set; }

        public LootedItem(ACDItem item)
        {
            Item = item;
        }

        public string AsJsonStyledString()
        {
            var sb = new StringBuilder();

            sb.Append("{");

            sb.Append("\"itemType\": \"" + Item.ItemType + "\", ");
            
            sb.Append("\"quality\": \"" + Item.ItemQualityLevel + "\", ");

            sb.Append("\"name\": \"" + Item.Name + "\", ");

            sb.Append("\"gold\": \"" + Item.Gold + "\", ");

            sb.Append("\"level\": \"" + Item.Level + "\", ");
            
            sb.Append("\"color\": \"" + Item.ItemLink.Substring(5,6) + "\"");



            sb.Append("}");

            return sb.ToString();
        }
    }
}
