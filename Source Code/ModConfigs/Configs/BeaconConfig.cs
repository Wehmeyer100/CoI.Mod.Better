using System;
using CoI.Mod.Better.Shared.Config;

namespace CoI.Mod.Better.ModConfigs.Configs
{
	[Serializable]
	public class BeaconConfig : ConfigBase
	{
		public int RefugeesMin = 1;
		public int RefugeesMax = 20;

		public int DurationMin = 2;
		public int DurationMax = 6;

		public float RewardBaseValueMultiplier = 1f;

		public float RewardIronBaseValue = 1.5f;
		public float RewardIronChance    = 1f;

		public float RewardCopperBaseValue = 1f;
		public float RewardCopperChance    = 0.9f;

		public float RewardRubberBaseValue = 0.75f;
		public float RewardRubberChance    = 0.8f;

		public float RewardOilBaseValue = 0.5f;
		public float RewardOilChance    = 0.4f;

		public float RewardDieselBaseValue = 0.25f;
		public float RewardDieselChance    = 0.4f;

		public float RewardFoodBaseValue = 1f;
		public float RewardFoodChance    = 0.7f;
	}
}