using System;
using CoI.Mod.Better.Shared.Config;

namespace CoI.Mod.Better.ModConfigs.Configs
{
	[Serializable]
	public class StorageConfig : ConfigBase
	{
		public bool OverrideVanilla = true;
		public bool UnlimitedTransferLimit;

		public int CapacityT1              = 540; // vanilla * 3
		public int TransferLimitT1Count    = 2; // vanilla
		public int TransferLimitT1Duration = 5; // vanilla

		public int CapacityT2              = 1080; // vanilla * 3
		public int TransferLimitT2Count    = 4; // vanilla
		public int TransferLimitT2Duration = 5; // vanilla

		public int CapacityT3              = 6480; // vanilla * 3
		public int TransferLimitT3Count    = 8; // vanilla
		public int TransferLimitT3Duration = 5; // vanilla

		public int CapacityT4              = 12960; // vanilla * 3
		public int TransferLimitT4Count    = 10; // vanilla
		public int TransferLimitT4Duration = 5; // vanilla

		public int FluidCapacityMultiplier = 2; // vanilla * 2
		public int SteamCapacityMultiplier = 2; // FluidCapacity * 2

		public int NuclearCapacity         = 4800; // vanilla * 3
		public int NuclearRetiredWasteCapacity    = 540; // vanilla * 3
	}
}