using CoI.Mod.Better.lang;
using CoI.Mod.Better.Shared;
using Mafi;
using Mafi.Base;
using Mafi.Core.Buildings.Mine;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Static;
using Mafi.Core.Mods;

namespace CoI.Mod.Better.Buildings
{
	internal class MineTower : IModData
	{
		private float towerAreaMultiplier = 1f;


		public void RegisterData(ProtoRegistrator registrator)
		{
			if (!BetterMod.Config.Systems.MineTower) return;

			LoadData();

			OverrideMineTower(registrator);
		}

		private void LoadData()
		{
			towerAreaMultiplier = BetterMod.Config.Tower.AreaMultiplier;
		}

		private void OverrideMineTower(ProtoRegistrator registrator)
		{
			// Set proto ids
			StaticEntityProto.ID protoID = Ids.Buildings.MineTower;

			// Remove from Database
			registrator.PrototypesDb.RemoveOrThrow(protoID);
			string Name = LangManager.Instance.Get("mine_tower");

			// Add override to Database
			if (BetterMod.Config.Tower.OverrideVanilla)
			{
				GenerateMineTower(registrator, protoID, Name, (int)(VanillaConstants.TowerRangeArea * towerAreaMultiplier));
				GenerateMineTower(registrator, MyIDs.Buildings.MineTowerNormal, Name + ": Vanilla", VanillaConstants.TowerRangeArea);
			}
			if (BetterMod.Config.Tower.ExtentedTowers)
			{
				GenerateMineTower(registrator, MyIDs.Buildings.MineTowerT2, Name + " x1.5", (int)(VanillaConstants.TowerRangeArea * (towerAreaMultiplier * 1.5f)));
				GenerateMineTower(registrator, MyIDs.Buildings.MineTowerT3, Name + " x2", (int)(VanillaConstants.TowerRangeArea * (towerAreaMultiplier * 2)));
			}
		}

		private void GenerateMineTower(ProtoRegistrator registrator, StaticEntityProto.ID protoID, string Name, int towerRange)
		{
			registrator.MineTowerProtoBuilder.Start(Name, protoID)
				.Description(LangManager.Instance.Get("mine_tower_desc"))
				.SetCost(Costs.Buildings.MineTower)
				.ShowTerrainDesignatorsOnCreation()
				.SetLayout("(3)(3)(8)(8)", "(3)(8)(9)(9)", "(3)(8)(9)(9)", "(3)(3)(8)(8)")
				.SetMineArea(new MineTowerProto.MineArea(new RelTile2i(5, 2), new RelTile2i(60, 60), new RelTile1i(towerRange)))
				.SetCategories(Ids.ToolbarCategories.Buildings)
				.SetPrefabPath("Assets/Base/Buildings/MineTower.prefab")
				.BuildAndAdd()
				.AddParam(new DrawArrowWileBuildingProtoParam(4f));

			BetterDebug.Info("GenerateMineTower (name: " + Name + ", id: " + protoID + ") >> created!");
		}
	}
}