using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Zeta.Internals.Service;
using System.Drawing.Imaging;

namespace Kbits.Demonbuddy.Plugins
{
    public class WatchFox : IPlugin
    {
        private string _name;
        private ActorClass _heroClass;
        private SocketEventSender _sender;

        public void OnInitialize()
        {
        }

        void OnItemLooted(object sender, ItemLootedEventArgs e)
        {
            _sender.Looted(e);
        }

        void OnLevelUp(object sender, EventArgs e)
        {
            var currentLevel = ZetaDia.Service.CurrentHero.Level;

            _sender.LevelUp(_name, currentLevel);
        }

        public void OnPulse()
        {
            if (ZetaDia.IsInGame && ZetaDia.Me.IsValid)
            {
            }
            else
            {
                _sender.Error("[WatchFox] there's something wrong: you're not logged in.");

                Thread.Sleep(5000);
            }
        }

        void Stop(IBot bot)
        {
        }

        void Start(IBot bot)
        {
        }

        public void OnShutdown()
        {
            _sender.ShutDown();


            _sender.Dispose();
        }

        public bool Equals(IPlugin other)
        {
            return other.Name == Name;
        }
        
        public void OnEnabled()
        {
            var level = ZetaDia.Service.CurrentHero.Level;
            _name = ZetaDia.Service.CurrentHero.Name;
            _heroClass = ZetaDia.Service.CurrentHero.Class;

            Logging.Write("[WatchFox] watching " + _name + ": lvl " + level + " " + _heroClass + " @ " + ZetaDia.Service.CurrentHero.CurrentDifficulty);

            _sender = new SocketEventSender();

            BotMain.OnStart += Start;
            BotMain.OnStop += Stop;
            GameEvents.OnLevelUp += OnLevelUp;
            GameEvents.OnItemLooted += OnItemLooted;
            GameEvents.OnGameJoined += OnGameJoined;
            GameEvents.OnGameLeft += OnGameLeft;
            
            _sender.Enable();
            
            Logging.Write("WatchFox " + Version +" enabled");
        }

        void OnGameJoined(object sender, EventArgs e)
        {
            _sender.GameJoined();
        }

        void OnGameLeft(object sender, EventArgs e)
        {


            var stats = new WatchFoxStats
                            {
                                GoldPerHour = GameStats.GoldPerHour,
                                Level = ZetaDia.Service.CurrentHero.Level,
                                Name = ZetaDia.Service.CurrentHero.Name,
                                HeroClass = ZetaDia.Service.CurrentHero.Class.ToString(),
                                TotalGold = ZetaDia.Me.Inventory.Coinage
                            };

            _sender.GameLeft(stats);
        }

        public void OnDisabled()
        {
            BotMain.OnStart -= Start;
            BotMain.OnStop -= Stop;
            GameEvents.OnLevelUp -= OnLevelUp;
            GameEvents.OnItemLooted -= OnItemLooted;

            _sender.Disable("[WatchFox] Bot wurde gestoppt.");


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
