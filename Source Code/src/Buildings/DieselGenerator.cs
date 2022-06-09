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
using UnityEngine;

namespace CoI.Mod.Better.Buildings
{
    internal class DieselGenerator : IModData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {
            if (BetterMod.Config.DisablePowerGeneators || BetterMod.Config.DisableCheats) return;

            GenerateDieselMachine(registrator, MyIDs.Machines.VoidDieselEnergy10Cheat, BetterMod.Config.VoidDieselEnergyInputType, BetterMod.Config.VoidDieselEnergy10CheatInKW, BetterMod.Config.VoidDieselEnergy10CheatBufferCapactiy);
            GenerateDieselMachine(registrator, MyIDs.Machines.VoidDieselEnergy50Cheat, BetterMod.Config.VoidDieselEnergyInputType, BetterMod.Config.VoidDieselEnergy50CheatInKW, BetterMod.Config.VoidDieselEnergy50CheatBufferCapactiy);
            GenerateDieselMachine(registrator, MyIDs.Machines.VoidDieselEnergy100Cheat, BetterMod.Config.VoidDieselEnergyInputType, BetterMod.Config.VoidDieselEnergy100CheatInKW, BetterMod.Config.VoidDieselEnergy100CheatBufferCapactiy);
            GenerateDieselMachine(registrator, MyIDs.Machines.VoidDieselEnergy200Cheat, BetterMod.Config.VoidDieselEnergyInputType, BetterMod.Config.VoidDieselEnergy200CheatInKW, BetterMod.Config.VoidDieselEnergy200CheatBufferCapactiy);
            GenerateDieselMachine(registrator, MyIDs.Machines.VoidDieselEnergy1000Cheat, BetterMod.Config.VoidDieselEnergyInputType, BetterMod.Config.VoidDieselEnergy1000CheatInKW, BetterMod.Config.VoidDieselEnergy1000CheatBufferCapactiy);

            // Generate Research
            ResearchNodeProtoBuilder.State research_state_t1 = registrator.ResearchNodeProtoBuilder
                .Start("Void Energy CHEAT", MyIDs.Research.VoidDieselEnergyCheat)
                .SetCostsOne()
                .AddLayoutEntityToUnlock(MyIDs.Machines.VoidDieselEnergy10Cheat)
                .AddLayoutEntityToUnlock(MyIDs.Machines.VoidDieselEnergy50Cheat)
                .AddLayoutEntityToUnlock(MyIDs.Machines.VoidDieselEnergy100Cheat)
                .AddLayoutEntityToUnlock(MyIDs.Machines.VoidDieselEnergy200Cheat)
                .AddLayoutEntityToUnlock(MyIDs.Machines.VoidDieselEnergy1000Cheat);

            ResearchNodeProto research_t1 = research_state_t1.BuildAndAdd();


            // Add parent to my research CHEAT
            ResearchNodeProto master_cheat_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VehicleCapIncreaseID_CHEAT);
            research_t1.AddGridPos(master_cheat_research, BetterMod.UI_StepSize, -BetterMod.UI_StepSize);
        }

        private static void GenerateDieselMachine(ProtoRegistrator registrator, StaticEntityProto.ID protoID, int inputType, int kwAmount, int storageAmount)
        {
            Electricity kw_amount = kwAmount.Kw().ScaledBy(registrator.DifficultyConfig.PowerProductionMult);

            registrator.PrototypesDb.Add(new ElectricityGeneratorFromProductProto(
                protoID,
                Proto.CreateStr(protoID, "Diesel generator " + kw_amount.Format().ToString(), "Burns diesel to electricity."),
                registrator.LayoutParser.ParseLayoutOrThrow("[2][2][2]", "[2][2][2]", "^2F[2][2]", " @       "),
                Costs.Machines.SmokeStack.MapToEntityCosts(registrator),
                kw_amount,
                10,
                registrator.PrototypesDb.GetOrThrow<FluidProductProto>(GetInputConfigType(inputType)),
                registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Electricity),
                registrator.PrototypesDb.GetOrThrow<VirtualProductProto>(IdsCore.Products.PollutedAir),
                2.Quantity(),
                storageAmount.Quantity(),
                60.Seconds(),
                DestroyReason.UsedAsFuel,
                ImmutableArray.Create((AnimationParams)AnimationParams.Loop()),
                new ElectricityGeneratorFromProductProto.Gfx(
                    "Assets/Base/Machines/PowerPlant/CombustionEngine.prefab",
                    ImmutableArray.Create(ParticlesParams.Loop("DarkSmoke")),
                    "Assets/Base/Machines/PowerPlant/CombustionEngine/CombustionEngine_Sound.prefab",
                    registrator.GetCategoriesProtos(MyIDs.ToolbarCategories.MachinesElectricity),
                    BetterMod.GetIconPath<ElectricityGeneratorFromProductProto>(registrator, Ids.Machines.DieselGenerator)
                )
            ));

            Debug.Log("DieselGeneators >> GenerateDieselMachine (name: " + "Diesel generator " + kw_amount.Format().ToString() + ") >> created!");
        }

        public static ProductProto.ID GetInputConfigType(int inputType)
        {
            switch (inputType)
            {
                case 3:
                    return Ids.Products.CrudeOil;
                case 2:
                    return Ids.Products.Water;
                case 1:
                default:
                    return Ids.Products.Diesel;
            }
        }


    }
}
