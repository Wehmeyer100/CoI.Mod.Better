using CoI.Mod.Better.ModConfigs.Configs;
using System;

namespace CoI.Mod.Better.ModConfigs.Configs
{
    [Serializable]
    public class NewGameConfig : ConfigBase
    {
        public bool OverrideStartSettings = false;
        public bool UnlockAll = false;

        public int StartingPopulation = 90;

        public int InitialVehiclesCap = 50;
        public int InitialTrucks = 8;
        public int InitialExcavators = 0;
        public int InitialTreeHarvesters = 1;
    }
}
