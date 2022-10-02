using Mafi;
using Mafi.Core.Buildings.Storages;

namespace CoI.Mod.Better.Utilities
{
	public static class ProductUtility
	{
		public static StorageProtoBuilder.State SetTransferLimitByT(StorageProtoBuilder.State creator, int level)
		{
			if (BetterMod.Config.Storage.UnlimitedTransferLimit)
			{
				creator.SetNoTransferLimit();
			}
			else
			{
				int count = BetterMod.Config.Storage.TransferLimitT1Count;
				int duration = BetterMod.Config.Storage.TransferLimitT1Duration;

				switch (level)
				{
					case 2:
						count = BetterMod.Config.Storage.TransferLimitT2Count;
						duration = BetterMod.Config.Storage.TransferLimitT2Duration;
						break;
					case 3:
						count = BetterMod.Config.Storage.TransferLimitT3Count;
						duration = BetterMod.Config.Storage.TransferLimitT3Duration;
						break;
					case 4:
						count = BetterMod.Config.Storage.TransferLimitT4Count;
						duration = BetterMod.Config.Storage.TransferLimitT4Duration;
						break;
				}

				creator.SetTransferLimit(count, 1.Seconds() / duration);
			}
			return creator;
		}
	}
}