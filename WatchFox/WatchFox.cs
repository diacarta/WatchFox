using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Text;
using Zeta;
using Zeta.Common;
using Zeta.Common.Plugins;
using Zeta.Internals.Actors;
using Zeta.CommonBot;
using Zeta.Internals;
using Zeta.Internals.Actors;
using Zeta.Internals.Service;
using System.Drawing.Imaging;

namespace Kbits.Demonbuddy.Plugins
{
    public class WatchFox : IPlugin
    {
        private string _name;
        private ActorClass _heroClass;
        private IDbEventHandler _emitter;

        public void OnInitialize()
        {
            var level = ZetaDia.Service.CurrentHero.Level;
            _name = ZetaDia.Service.CurrentHero.Name;
            _heroClass = ZetaDia.Service.CurrentHero.Class;

            Logging.Write("[WatchFox] " + Version + " initializing...");
            Logging.Write("[WatchFox] watching " + _name + ": lvl " + level + " " + _heroClass + " @ " + ZetaDia.Service.CurrentHero.CurrentDifficulty);

            _emitter = new EventsToLogSender();

            BotMain.OnStart += Start;
            BotMain.OnStop += Stop;
            GameEvents.OnLevelUp += OnLevelUp;
            GameEvents.OnItemLooted += OnItemLooted;
        }

        void OnItemLooted(object sender, ItemLootedEventArgs e)
        {
            _emitter.Looted(e);
        }

        void OnLevelUp(object sender, EventArgs e)
        {
            var currentLevel = ZetaDia.Service.CurrentHero.Level;

            _emitter.LevelUp(_name, currentLevel);
        }

        public void OnPulse()
        {
            if (ZetaDia.IsInGame && ZetaDia.Me.IsValid)
            {
                // DoThingsHere
            }
            else
            {
                _emitter.Error("[WatchFox] there's something wrong: you're not logged in.");

                Thread.Sleep(5000);
            }
        }

        void Stop(IBot bot)
        {
            _emitter.Stop("[WatchFox] Bot wurde gestoppt.");
        }

        void Start(IBot bot)
        {
            _emitter.Start("[WatchFox] Bot wurde gestartet.");
        }

        public void OnShutdown()
        {
            _emitter.ShutDown();

            BotMain.OnStart -= Start;
            BotMain.OnStop -= Stop;
            GameEvents.OnLevelUp -= OnLevelUp;
            GameEvents.OnItemLooted -= OnItemLooted;

            _emitter.Dispose();
        }

        public bool Equals(IPlugin other)
        {
            return other.Name == Name;
        }
        
        public void OnEnabled()
        {
            Logging.Write("WatchFox " + Version +" enabled");
        }

        public void OnDisabled()
        {
            Logging.Write("WatchFox " + Version + " disabled");
        }

        public string Author
        {
            get { return "xfox"; }
        }

        public Version Version
        {
            get { return new Version(0,2); }
        }

        public string Name
        {
            get { return "WatchFox"; }
        }

        public string Description
        {
            get { return "the foxy watchdog for demonbuddy"; }
        }

        public Window DisplayWindow
        {
            get { return null; }
        }
    }
}
