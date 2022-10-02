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
		private void AddTruckCap(ProtoRegistrator registrator)
		{
			GenerateTruckCap(registrator, Eticts.Trucks.CapacityIncT1, 50, 1, null);
			GenerateTruckCap(registrator, Eticts.Trucks.CapacityIncT2, 75, 2, Eticts.Trucks.CapacityIncT1);
			GenerateTruckCap(registrator, Eticts.Trucks.CapacityIncT3, 100, 3, Eticts.Trucks.CapacityIncT2);
			GenerateTruckCap(registrator, Eticts.Trucks.CapacityIncT4, 200, 4, Eticts.Trucks.CapacityIncT3);

			if (!BetterMod.Config.Systems.Cheats) return;

			// Add Cheats
			GenerateTruckCap(registrator, Eticts.Trucks.CapacityIncT1_CHEAT, 100, CheatUpkeepEdicts, null, true);
			GenerateTruckCap(registrator, Eticts.Trucks.CapacityIncT2_CHEAT, 200, CheatUpkeepEdicts, Eticts.Trucks.CapacityIncT1_CHEAT, true);
			GenerateTruckCap(registrator, Eticts.Trucks.CapacityIncT3_CHEAT, 300, CheatUpkeepEdicts, Eticts.Trucks.CapacityIncT2_CHEAT, true);
			GenerateTruckCap(registrator, Eticts.Trucks.CapacityIncT4_CHEAT, 400, CheatUpkeepEdicts, Eticts.Trucks.CapacityIncT3_CHEAT, true);
			GenerateTruckCap(registrator, Eticts.Trucks.CapacityIncT5_CHEAT, 500, CheatUpkeepEdicts, Eticts.Trucks.CapacityIncT4_CHEAT, true);
		}

		private void GenerateTruckCap(ProtoRegistrator registrator, Proto.ID protoID, int Capacity, float monthlyUpointsCost, Proto.ID? previusEdict, bool cheat = false)
		{
			countTruckCapEdicts++;

			Percent trucksCapacityDiff = Capacity.Percent();

			LocStr2 locStr4 = Loc.Str2(
				protoID + "__desc",
				"Trucks can get overloaded by {0} but they require extra {1} maintenance",
				"policy / edict which can enabled by the player in their Captain's office. {0}=" + trucksCapacityDiff + "%"
			);

			LocStr descShort4 = LocalizationManager.CreateAlreadyLocalizedStr(
				protoID + "_formatted",
				locStr4.Format(trucksCapacityDiff.ToString(), trucksCapacityDiff.ToString()).Value
			);

			Option<EdictProto> previousTier = Option<EdictProto>.None;
			if (previusEdict.HasValue)
			{
				previousTier = registrator.PrototypesDb.GetOrThrow<EdictProto>(previusEdict.Value);
			}

			registrator.PrototypesDb.Add(new EdictWithPropertiesProto(
				protoID,
				Proto.CreateStr(protoID, "Overloaded trucks T" + countTruckCapEdicts, descShort4, translationComment),
				cheat ? GenerelEdicts.CategoryCheats : GenerelEdicts.Category,
				monthlyUpointsCost.Upoints(),
				ImmutableArray.Create(Make.Kvp(IdsCore.PropertyIds.TrucksCapacityMultiplier, Capacity.Percent())),
				previousTier,
				new EdictProto.Gfx(Mafi.Base.Assets.Base.Icons.Edicts.TrucksCapacity_svg))
			);
		}
	}
}