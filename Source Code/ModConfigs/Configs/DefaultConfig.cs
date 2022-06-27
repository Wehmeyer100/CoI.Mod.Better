using CoI.Mod.Better.ModConfigs.Configs;
using System;

namespace CoI.Mod.Better.ModConfigs.Configs
{
    [Serializable]
    public class DefaultConfig : ConfigBase
    {
        public bool UnlockAllCheatsResearches = false;
        public float CheatUpkeepEdicts = -2.0f;

        public int CheatResearchCosts = 1;
    }
}
