using CoI.Mod.Better.Extensions;
using Mafi;
using Mafi.Base;
using Mafi.Base.Prototypes.Machines.PowerGenerators;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Animations;
using Mafi.Core.Entities.Static;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;

namespace CoI.Mod.Better.Buildings
{
    internal class DieselGeneators : IModData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {
            if (BetterMod.Config.DisableDieselGeneators || BetterMod.Config.DisableCheats || true) return;

            GenerateDieselMachine(registrator, MyIDs.Machines.VoidProducerEnergy10Cheat, BetterMod.Config.VoidProducerEnergyInputType, BetterMod.Config.VoidProducerEnergy10CheatInKW, BetterMod.Config.VoidProducerEnergy10CheatBufferCapactiy);
            GenerateDieselMachine(registrator, MyIDs.Machines.VoidProducerEnergy50Cheat, BetterMod.Config.VoidProducerEnergyInputType, BetterMod.Config.VoidProducerEnergy50CheatInKW, BetterMod.Config.VoidProducerEnergy50CheatBufferCapactiy);
            GenerateDieselMachine(registrator, MyIDs.Machines.VoidProducerEnergy100Cheat, BetterMod.Config.VoidProducerEnergyInputType, BetterMod.Config.VoidProducerEnergy100CheatInKW, BetterMod.Config.VoidProducerEnergy100CheatBufferCapactiy);
            GenerateDieselMachine(registrator, MyIDs.Machines.VoidProducerEnergy200Cheat, BetterMod.Config.VoidProducerEnergyInputType, BetterMod.Config.VoidProducerEnergy200CheatInKW, BetterMod.Config.VoidProducerEnergy200CheatBufferCapactiy);
            GenerateDieselMachine(registrator, MyIDs.Machines.VoidProducerEnergy1000Cheat, BetterMod.Config.VoidProducerEnergyInputType, BetterMod.Config.VoidProducerEnergy1000CheatInKW, BetterMod.Config.VoidProducerEnergy1000CheatBufferCapactiy);

            // Generate Research
            ResearchNodeProtoBuilder.State research_state_t1 = registrator.ResearchNodeProtoBuilder
                .Start("Void Energy CHEAT", MyIDs.Research.VoidEnergyCheat)
                .SetCostsOne()
                .AddLayoutEntityToUnlock(MyIDs.Machines.VoidProducerEnergy10Cheat)
                .AddLayoutEntityToUnlock(MyIDs.Machines.VoidProducerEnergy50Cheat)
                .AddLayoutEntityToUnlock(MyIDs.Machines.VoidProducerEnergy100Cheat)
                .AddLayoutEntityToUnlock(MyIDs.Machines.VoidProducerEnergy200Cheat)
                .AddLayoutEntityToUnlock(MyIDs.Machines.VoidProducerEnergy1000Cheat);

            ResearchNodeProto research_t1 = research_state_t1.BuildAndAdd();

            // Add parent to my research T1
            ResearchNodeProto master_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.BasicFarming);
            research_t1.AddGridPos(master_research, 0, 10);
        }

        private static void GenerateDieselMachine(ProtoRegistrator registrator, StaticEntityProto.ID protoID, int inputType, int kwAmount, int storageAmount)
        {
            registrator.PrototypesDb.Add(new ElectricityGeneratorFromProductProto(
                protoID,
                Proto.CreateStr(protoID, "Diesel generator", "Burns diesel to create electricity."),
                registrator.LayoutParser.ParseLayoutOrThrow("[2][2][2]", "[2][2][2]", "^2F[2][2]", " @       "),
                Costs.Machines.SmokeStack.MapToEntityCosts(registrator),
                kwAmount.Kw().ScaledBy(registrator.DifficultyConfig.PowerProductionMult),
                10,
                registrator.PrototypesDb.GetOrThrow<FluidProductProto>(GetInputConfigType(inputType)),
                registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Electricity),
                registrator.PrototypesDb.GetOrThrow<VirtualProductProto>(IdsCore.Products.PollutedAir),
                1.Quantity(),
                storageAmount.Quantity(),
                60.Seconds(),
                DestroyReason.UsedAsFuel,
                ImmutableArray.Create((AnimationParams)AnimationParams.Loop()),
                new ElectricityGeneratorFromProductProto.Gfx(
                    "Assets/Base/Machines/PowerPlant/CombustionEngine.prefab",
                    ImmutableArray.Create(ParticlesParams.Loop("DarkSmoke")),
                    "Assets/Base/Machines/PowerPlant/CombustionEngine/CombustionEngine_Sound.prefab",
                    registrator.GetCategoriesProtos(Ids.ToolbarCategories.MachinesElectricity),
                    BetterMod.GetIconPath<ElectricityGeneratorFromProductProto>(registrator, Ids.Machines.DieselGenerator)
                )
            ));
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
