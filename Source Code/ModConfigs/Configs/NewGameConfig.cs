using System;
using CoI.Mod.Better.Shared.Config;

namespace CoI.Mod.Better.ModConfigs.Configs
{
	[Serializable]
	public class NewGameConfig : ConfigBase
	{
		public bool OverrideStartSettings;
		public bool UnlockAll;

		public int StartingPopulation = 90;

		public int InitialVehiclesCap = 50;
		public int InitialTrucks      = 8;
		public int InitialExcavators;
		public int InitialTreeHarvesters = 1;
	}
}