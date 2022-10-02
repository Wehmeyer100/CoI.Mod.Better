using System;
using CoI.Mod.Better.Shared.Config;

namespace CoI.Mod.Better.ModConfigs.Configs
{
	[Serializable]
	public class TowerConfig : ConfigBase
	{
		public bool  OverrideVanilla = true;
		public bool  ExtentedTowers;
		public float AreaMultiplier = 1.5f;
	}
}