using System;
using CoI.Mod.Better.Shared.Config;

namespace CoI.Mod.Better.ModConfigs.Configs
{
	[Serializable]
	public class GameConfig : ConfigBase
	{
		public bool OverrideGameConfig;

		public bool IsInstaBuild;
		public bool IsGodMode;

		public int BattleRoundsToEscape = 25;

		public bool DisableFuelConsumption;
		public int  FreeElectricity;
	}
}