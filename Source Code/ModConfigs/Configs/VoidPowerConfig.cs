using System;
using CoI.Mod.Better.Shared.Config;

namespace CoI.Mod.Better.ModConfigs.Configs
{
	[Serializable]
	public class VoidPowerConfig : ConfigBase
	{
		public int EnergyT1InputMechPower = 500; // vanilla
		public int EnergyT1OutputPower    = 900; // vanilla * 3

		public int EnergyT2InputMechPower = 500; // vanilla
		public int EnergyT2OutputPower    = 1800; // vanilla * 6

		public int EnergyT3InputMechPower = 500; // vanilla
		public int EnergyT3OutputPower    = 2700; // vanilla * 9

		public int EnergyT4InputMechPower = 500; // vanilla
		public int EnergyT4OutputPower    = 3600; // vanilla * 12

		public int EnergyT5InputMechPower = 500; // vanilla
		public int EnergyT5OutputPower    = 4500; // vanilla * 15
	}
}