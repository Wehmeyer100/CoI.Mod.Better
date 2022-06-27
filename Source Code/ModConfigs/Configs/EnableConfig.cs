using System;

namespace CoI.Mod.Better.ModConfigs.Configs
{
    [Serializable]
    public class EnableConfig : ConfigBase
    {
        public bool Cheats = true;
        public bool BigStorage = true;
        public bool VehicleEdicts = true;
        public bool GenerellEdicts = true;
        public bool RefugeesSystem = true;
        public bool MineTower = true;
        public bool VehicleCapIncrease = true;
        public bool VoidCrusher = true;
        public bool VoidProducer = true;
        public bool DieselGeneators = true;
        public bool PowerGeneators = true;
        public bool Customs = true;
        public bool SteamStorage = true;
    }
}
