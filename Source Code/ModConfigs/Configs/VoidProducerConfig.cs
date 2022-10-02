using System;
using CoI.Mod.Better.Shared.Config;

namespace CoI.Mod.Better.ModConfigs.Configs
{
	[Serializable]
	public class VoidProducerConfig : ConfigBase
	{
		public int PowerConsume = 75;
		public int AmountInput  = 40;
		public int Duration     = 20;
	}
}