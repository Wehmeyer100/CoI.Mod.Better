using CoI.Mod.Better.Extensions;
using Mafi;
using Mafi.Base;
using Mafi.Base.Prototypes.Machines.PowerGenerators;
using Mafi.Collections.ImmutableCollections;
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
            if (MoreRecipes.Config.DisableDieselGeneators || MoreRecipes.Config.DisableCheats) return;

            GenerateDieselMachine(registrator, MyIDs.Machines.VoidProducerEnergy10Cheat, MoreRecipes.Config.VoidProducerEnergyInputType, MoreRecipes.Config.VoidProducerEnergy10CheatInKW, MoreRecipes.Config.VoidProducerEnergy10CheatBufferCapactiy);
            GenerateDieselMachine(registrator, MyIDs.Machines.VoidProducerEnergy50Cheat, MoreRecipes.Config.VoidProducerEnergyInputType, MoreRecipes.Config.VoidProducerEnergy50CheatInKW, MoreRecipes.Config.VoidProducerEnergy50CheatBufferCapactiy);
            GenerateDieselMachine(registrator, MyIDs.Machines.VoidProducerEnergy100Cheat, MoreRecipes.Config.VoidProducerEnergyInputType, MoreRecipes.Config.VoidProducerEnergy100CheatInKW, MoreRecipes.Config.VoidProducerEnergy100CheatBufferCapactiy);
            GenerateDieselMachine(registrator, MyIDs.Machines.VoidProducerEnergy200Cheat, MoreRecipes.Config.VoidProducerEnergyInputType, MoreRecipes.Config.VoidProducerEnergy200CheatInKW, MoreRecipes.Config.VoidProducerEnergy200CheatBufferCapactiy);
            GenerateDieselMachine(registrator, MyIDs.Machines.VoidProducerEnergy1000Cheat, MoreRecipes.Config.VoidProducerEnergyInputType, MoreRecipes.Config.VoidProducerEnergy1000CheatInKW, MoreRecipes.Config.VoidProducerEnergy1000CheatBufferCapactiy);

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
            ResearchNodeProto master_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VoidProducerCheat);
            research_t1.AddGridPos(master_research);
        }

        private static void GenerateDieselMachine(ProtoRegistrator registrator, StaticEntityProto.ID protoID, int inputType, int kwAmount, int storageAmount)
        {
            registrator.PrototypesDb.Add(new ElectricityGeneratorFromProductProto(
                protoID,
                Proto.CreateStr(protoID, "Diesel generator", "Burns diesel to create electricity."),
                registrator.LayoutParser.ParseLayoutOrThrow("[2][2][2]", "[2][2][2]", "^2F[2][2]", " @       "),
                Costs.Machines.SmokeStack.MapToEntityCosts(registrator.PrototypesDb),
                kwAmount.Kw().ScaledBy(registrator.GameDifficulty.PowerProductionMult),
                10,
                registrator.PrototypesDb.GetOrThrow<FluidProductProto>(GetInputConfigType(inputType)),
                registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Electricity),
                storageAmount.Quantity(),
                60.Seconds(),
                DestroyReason.UsedAsFuel,
                ImmutableArray.Create((AnimationParams)AnimationParams.Loop()),
                new ElectricityGeneratorFromProductProto.Gfx(
                    "Assets/Base/Machines/PowerPlant/CombustionEngine.prefab",
                    ImmutableArray.Create(ParticlesParams.Loop("DarkSmoke")),
                    "Assets/Base/Machines/PowerPlant/CombustionEngine/CombustionEngine_Sound.prefab",
                    registrator.GetCategoriesProtos(Ids.ToolbarCategories.MachinesElectricity),
                    MoreRecipes.GetIconPath<ElectricityGeneratorFromProductProto>(registrator, Ids.Machines.DieselGenerator)
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
