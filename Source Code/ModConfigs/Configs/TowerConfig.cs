using System;

namespace CoI.Mod.Better.ModConfigs.Configs
{
    [Serializable]
    public class TowerConfig : ConfigBase
    {
        public bool  OverrideVanilla = true;
        public bool  ExtentedTowers  = false;
        public float AreaMultiplier  = 1.5f;
    }
}
