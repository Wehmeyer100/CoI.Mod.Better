using System;
using CoI.Mod.Better.Shared.Config;

namespace CoI.Mod.Better.ModConfigs.Configs
{
	[Serializable]
	public class CustomConfig : ConfigBase
	{
		public bool LoadOnlyVanilla = false; // true for faster loading
		public bool OverrideAll     = false; // true ignor item property 'overrideProtoID' and set to true for all
	}
}