using Zeta.CommonBot;

namespace Kbits.Demonbuddy.Plugins
{
    public interface IDbEventHandler
    {
        void Error(string errorMessage);
        void Dispose();
        void LevelUp(string name, int level);
        void Start(string message);
        void Stop(string message);
        void Looted(ItemLootedEventArgs itemLootedEventArgs);
        void ShutDown();
    }
}