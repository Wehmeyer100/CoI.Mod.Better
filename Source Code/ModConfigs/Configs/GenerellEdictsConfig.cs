using System;
using CoI.Mod.Better.Shared.Config;

namespace CoI.Mod.Better.ModConfigs.Configs
{
	[Serializable]
	public class GenerellEdictsConfig : ConfigBase
	{
		public int ResearchCostT1 = 2;
		public int ResearchCostT2 = 4;
		public int ResearchCostT3 = 6;
		public int ResearchCostT4 = 8;
		public int ResearchCostT5 = 10;
	}
}