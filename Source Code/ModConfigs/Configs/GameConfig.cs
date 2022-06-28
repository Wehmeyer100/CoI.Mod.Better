using CoI.Mod.Better.ModConfigs.Configs;
using System;

namespace CoI.Mod.Better.ModConfigs.Configs
{
    [Serializable]
    public class GameConfig : ConfigBase
    {
        public bool OverrideGameConfig = false;

        public bool IsInstaBuild = false;
        public bool IsGodMode = false;

        public int BattleRoundsToEscape = 25;

        public bool DisableFuelConsumption = false;
        public int FreeElectricity = 0;
    }
}
