using CoI.Mod.Better.Extensions;
using Mafi;
using Mafi.Base;
using Mafi.Base.Prototypes.Machines.PowerGenerators;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Animations;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;

namespace CoI.Mod.Better.Toolbars
{
    public class MyToolbars : IModData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {
            if ((!BetterMod.Config.DisableDieselGeneators || !BetterMod.Config.DisablePowerGeneators) && !BetterMod.Config.DisableCheats)
            {
                GenerateToolbar(registrator, MyIDs.ToolbarCategories.MachinesElectricity, "Better mod: Electricity", "Assets/Unity/UserInterface/Toolbar/Power.svg", 31);
            }

            if (!BetterMod.Config.DisableBigStorage && !BetterMod.Config.OverrideVanillaStorages)
            {
                GenerateToolbar(registrator, MyIDs.ToolbarCategories.Storages, "Better mod: Storages", "Assets/Unity/UserInterface/Toolbar/Storages.svg", 211);
            }

            if (!BetterMod.Config.DisableVoidCrusher)
            {
                GenerateToolbar(registrator, MyIDs.ToolbarCategories.MachinesMetallurgy, "Better mod: Crusher/Producer", "Assets/Unity/UserInterface/Toolbar/Metallurgy.svg", 21);
            }
        }

        private static void GenerateToolbar(ProtoRegistrator registrator, Proto.ID protoID, string Name, string IconPath, int order)
        {
            registrator.PrototypesDb.Add(
                new ToolbarCategoryProto(
                    protoID,
                    Proto.CreateStr(protoID, Name, null, ""),
                    order,
                    IconPath,
                    isTransportBuildAllowed: true
                )
            );
        }
    }
}
