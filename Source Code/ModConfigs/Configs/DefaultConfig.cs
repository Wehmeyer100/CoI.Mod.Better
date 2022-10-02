using System;
using CoI.Mod.Better.Shared.Config;

namespace CoI.Mod.Better.ModConfigs.Configs
{
	[Serializable]
	public class DefaultConfig : ConfigBase
	{
		public bool  UnlockAllCheatsResearches;
		public float CheatUpkeepEdicts = -2.0f;

		public int CheatResearchCosts = 1;
	}
}