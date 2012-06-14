using System;
using System.IO;
using System.Linq;
using Zeta.Common;
using Zeta.CommonBot;

namespace Kbits.Demonbuddy.Plugins
{
    class LogMessageWriter : IEventEmitter
    {
        public void Dispose()
        {
        }

        public void Error(string errorMessage)
        {
            Logging.Write(errorMessage);
        }

        public void LevelUp(string name, int level)
        {
            Logging.Write("[Watchfox] " + name + " reached lvl " + level);
        }

        public void Stop(string message)
        {
            Logging.Write(message);
        }

        public void Looted(ItemLootedEventArgs itemLootedEventArgs)
        {
            Logging.Write("[Watchfox] looted: " + itemLootedEventArgs.Item.Name + " x " + itemLootedEventArgs.Item.ItemStackQuantity);
        }

        public void ShutDown()
        {
            Logging.Write("[WatchFox] DemonBuddy is shutting down");
        }

        public void Start(string message)
        {
            Logging.Write(message);
        }
    }
}