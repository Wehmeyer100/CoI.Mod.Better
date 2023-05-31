using CoI.Mod.Better.MyIDs;
using Mafi;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Mods;
using Mafi.Core.Population.Edicts;
using Mafi.Core.Prototypes;
using Mafi.Localization;

namespace CoI.Mod.Better.Edicts
{
	internal partial class VehicleEdicts : IModData
	{
		private void AddTruckFuelCons(ProtoRegistrator registrator)
		{
			GenerateTruckFuelCons(registrator, Eticts.Trucks.FuelReductionT1, 30, 2f, null);
			GenerateTruckFuelCons(registrator, Eticts.Trucks.FuelReductionT2, 45, 3f, Eticts.Trucks.FuelReductionT1);
			GenerateTruckFuelCons(registrator, Eticts.Trucks.FuelReductionT3, 60, 4f, Eticts.Trucks.FuelReductionT2);
			GenerateTruckFuelCons(registrator, Eticts.Trucks.FuelReductionT4, 75, 5f, Eticts.Trucks.FuelReductionT3);

			if (!BetterMod.Config.Systems.Cheats) return;

			// Add Cheats
			GenerateTruckFuelCons(registrator, Eticts.Trucks.FuelReductionT1_CHEAT, 25, CheatUpkeepEdicts, null, true);
			GenerateTruckFuelCons(registrator, Eticts.Trucks.FuelReductionT2_CHEAT, 50, CheatUpkeepEdicts, Eticts.Trucks.FuelReductionT1_CHEAT, true);
			GenerateTruckFuelCons(registrator, Eticts.Trucks.FuelReductionT3_CHEAT, 70, CheatUpkeepEdicts, Eticts.Trucks.FuelReductionT2_CHEAT, true);
			GenerateTruckFuelCons(registrator, Eticts.Trucks.FuelReductionT4_CHEAT, 100, CheatUpkeepEdicts, Eticts.Trucks.FuelReductionT3_CHEAT, true);
		}

		private void GenerateTruckFuelCons(ProtoRegistrator registrator, Proto.ID protoID, int consume, float monthlyUpointsCost, Proto.ID? previusEdict, bool cheat = false)
		{
			countTruckFuelConsEdicts++;
			Percent fuelMultiplierReduction = consume.Percent();

			LocStr1 locStr3 = Loc.Str1(
				protoID + "__desc",
				"Vehicles fuel consumption reduced by {0}%",
				"policy / edict which can enabled by the player in their Captain's office. {0}=" + fuelMultiplierReduction + "%"
			);

			LocStr descShort3 = LocalizationManager.CreateAlreadyLocalizedStr(
				protoID + "_formatted",
				locStr3.Format(fuelMultiplierReduction.ToString()).Value
			);

			Option<EdictProto> previousTier = Option<EdictProto>.None;
			if (previusEdict.HasValue)
			{
				previousTier = registrator.PrototypesDb.GetOrThrow<EdictProto>(previusEdict.Value);
			}

			registrator.PrototypesDb.Add(new EdictWithPropertiesProto(
				protoID,
				Proto.CreateStr(protoID, "Fuel saver T" + countTruckFuelConsEdicts, descShort3, translationComment),
				cheat ? GenerelEdicts.CategoryCheats : GenerelEdicts.Category,
				monthlyUpointsCost.Upoints(),
				ImmutableArray.Create(Make.Kvp(IdsCore.PropertyIds.VehiclesFuelConsumptionMultiplier, -consume.Percent())),
				"",
				previousTier,
				new EdictProto.Gfx(Mafi.Base.Assets.Base.Icons.Edicts.FoodReduced_png))
			);
		}
	}
}