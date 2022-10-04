using System;
using CoI.Mod.Better.ModConfigs.Configs;
using CoI.Mod.Better.Shared.Config;

namespace CoI.Mod.Better.ModConfigs
{
	[Serializable]
	public class BetterModConfig : ConfigBase
	{
		public DefaultConfig Default = new DefaultConfig();
		public EnableConfig  Systems = new EnableConfig();

		public GameConfig    GameSettings  = new GameConfig();
		public NewGameConfig StartSettings = new NewGameConfig();

		public UIConfig UI = new UIConfig();

		public BeaconConfig  Beacon  = new BeaconConfig();
		public TowerConfig   Tower   = new TowerConfig();
		public StorageConfig Storage = new StorageConfig();

		public VehicleEdictsConfig  VehicleEdicts  = new VehicleEdictsConfig();
		public GenerellEdictsConfig GenerellEdicts = new GenerellEdictsConfig();

		public VoidDestroyConfig  VoidDestroy  = new VoidDestroyConfig();
		public VoidProducerConfig VoidProducer = new VoidProducerConfig();

		public VoidDieselConfig VoidDiesel = new VoidDieselConfig();
		public VoidPowerConfig  VoidPower  = new VoidPowerConfig();
		
		public CustomConfig Custom = new CustomConfig();
	}
}