using Mafi;
using Mafi.Base;
using Mafi.Core.Buildings.Mine;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;
using Mafi.Core.UnlockingTree;
using UnityEngine;
using static Mafi.Base.Ids;

namespace CoI.Mod.Better
{
    internal class MineTower : IModData
    {
        private float towerAreaMultiplier = 1f;
        public void RegisterData(ProtoRegistrator registrator)
        {
            if (MoreRecipes.Config.DisableExtentedMineTowerRange) return;

            LoadData();

            OverrideMineTower(registrator);
        }

        private void LoadData()
        {
            towerAreaMultiplier = MoreRecipes.Config.TowerAreaMultiplier;
        }

        private void OverrideMineTower(ProtoRegistrator registrator)
        {
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.MineTower;

            // Remove from Database
            registrator.PrototypesDb.RemoveOrThrow(protoID);

            // Add override to Database
            registrator.MineTowerProtoBuilder.Start("Mine control tower", Ids.Buildings.MineTower)
                .Description("Enables assignment of excavators and trucks to designated mine areas. Only designated mining areas within the influence of the tower can be mined.")
                .SetCost(Costs.Buildings.MineTower, true)
                .ShowTerrainDesignatorsOnCreation()
                .SetLayout("(3)(3)(8)(8)", "(3)(8)(9)(9)", "(3)(8)(9)(9)", "(3)(3)(8)(8)")
                .SetMineArea(new MineTowerProto.MineArea(new RelTile2i(5, 2), new RelTile2i(60, 60), new RelTile1i(Mathf.FloorToInt(128 * towerAreaMultiplier))))
                .SetCategories(Ids.ToolbarCategories.Buildings)
                .SetPrefabPath("Assets/Base/Buildings/MineTower.prefab")
                .BuildAndAdd()
                .AddParam(new DrawArrowWileBuildingProtoParam(4f));
        }

    }
}