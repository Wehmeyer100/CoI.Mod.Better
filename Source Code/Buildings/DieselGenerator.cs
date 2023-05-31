using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared;
using CoI.Mod.Better.Shared.Extensions;
using CoI.Mod.Better.Shared.Lang;
using CoI.Mod.Better.Shared.Utilities;
using Mafi;
using Mafi.Base;
using Mafi.Base.Prototypes.Machines.PowerGenerators;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Animations;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Game;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;

namespace CoI.Mod.Better.Buildings
{
    internal class DieselGenerator : IModData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {
            if (!BetterMod.Config.Systems.PowerGeneators || !BetterMod.Config.Systems.Cheats) return;

            GenerateDieselMachine(registrator, Machines.VoidDieselEnergy10Cheat,
                BetterMod.Config.VoidDiesel.EnergyInputType, BetterMod.Config.VoidDiesel.EnergyT1ProduceInKw,
                BetterMod.Config.VoidDiesel.EnergyT1BufferCapactiy);
            GenerateDieselMachine(registrator, Machines.VoidDieselEnergy50Cheat,
                BetterMod.Config.VoidDiesel.EnergyInputType, BetterMod.Config.VoidDiesel.EnergyT2ProduceInKw,
                BetterMod.Config.VoidDiesel.EnergyT2BufferCapactiy);
            GenerateDieselMachine(registrator, Machines.VoidDieselEnergy100Cheat,
                BetterMod.Config.VoidDiesel.EnergyInputType, BetterMod.Config.VoidDiesel.EnergyT3ProduceInKw,
                BetterMod.Config.VoidDiesel.EnergyT3BufferCapactiy);
            GenerateDieselMachine(registrator, Machines.VoidDieselEnergy200Cheat,
                BetterMod.Config.VoidDiesel.EnergyInputType, BetterMod.Config.VoidDiesel.EnergyT4ProduceInKw,
                BetterMod.Config.VoidDiesel.EnergyT4BufferCapactiy);
            GenerateDieselMachine(registrator, Machines.VoidDieselEnergy1000Cheat,
                BetterMod.Config.VoidDiesel.EnergyInputType, BetterMod.Config.VoidDiesel.EnergyT5ProduceInKw,
                BetterMod.Config.VoidDiesel.EnergyT5BufferCapactiy);

            // Generate Research
            ResearchNodeProtoBuilder.State research_state_t1 = registrator.ResearchNodeProtoBuilder
                .Start(LangManager.Instance.Get("research_diesel_generator"), MyIDs.Research.VoidDieselEnergyCheat)
                .AddLayoutEntityToUnlock(Machines.VoidDieselEnergy10Cheat)
                .AddLayoutEntityToUnlock(Machines.VoidDieselEnergy50Cheat)
                .AddLayoutEntityToUnlock(Machines.VoidDieselEnergy100Cheat)
                .AddLayoutEntityToUnlock(Machines.VoidDieselEnergy200Cheat)
                .AddLayoutEntityToUnlock(Machines.VoidDieselEnergy1000Cheat);

            if (BetterMod.Config.Default.UnlockAllCheatsResearches)
            {
                research_state_t1.SetCostsFree();
            }
            else
            {
                research_state_t1.SetCostsOne();
            }

            ResearchNodeProto research_t1 = research_state_t1.BuildAndAdd();


            // Add parent to my research CHEAT
            ResearchNodeProto master_cheat_research =
                registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VehicleCapIncreaseID_CHEAT);
            research_t1.AddGridPos(master_cheat_research, Constants.UIStepSize, -Constants.UIStepSize);
        }

        private static void GenerateDieselMachine(ProtoRegistrator registrator, StaticEntityProto.ID protoID,
            int inputType, int kwAmount, int storageAmount)
        {
            Electricity kw_amount = kwAmount.Kw().ScaledBy(registrator.DifficultyConfig.PowerProductionMult);

            string Name = LangManager.Instance.Get("diesel_generator", kw_amount.Format().ToString());
            string desc = LangManager.Instance.Get("diesel_generator_desc");

            ElectricityGeneratorFromProductProto.Gfx graphics = new ElectricityGeneratorFromProductProto.Gfx( 
                "Assets/Base/Machines/PowerPlant/CombustionEngine.prefab", 
                ImmutableArray.Create<ParticlesParams>(ParticlesParams.Loop("DarkSmoke")), 
                (Option<string>)"Assets/Base/Machines/PowerPlant/CombustionEngine/CombustionEngine_Sound.prefab", 
                registrator.GetCategoriesProtos(Ids.ToolbarCategories.MachinesElectricity)
                );
            
            ElectricityGeneratorFromProductProto proto1 = new ElectricityGeneratorFromProductProto(
                protoID,
                Proto.CreateStr(protoID, Name + " " + kw_amount.Format().ToString(), desc), 
                registrator.LayoutParser.ParseLayoutOrThrow("[3][3][2][2]", "[3][3][2][2]", "[2][2][2][2]","F@^         "), 
                Costs.Machines.DieselGenerator.MapToEntityCosts(registrator), 
                kw_amount, 
                10, 
                registrator.PrototypesDb.GetOrThrow<FluidProductProto>(GetInputConfigType(inputType)).WithQuantity(1), 
                new ProductQuantity?(registrator.PrototypesDb.GetOrThrow<VirtualProductProto>((Proto.ID)IdsCore.Products.PollutedAir).WithQuantity(1)),
                registrator.PrototypesDb.GetOrThrow<ProductProto>((Proto.ID)IdsCore.Products.Electricity), 
                storageAmount, 
                20.Seconds(), 
                DestroyReason.UsedAsFuel, 
                ImmutableArray.Create<AnimationParams>((AnimationParams)AnimationParams.Loop()), 
                graphics
                );

            registrator.PrototypesDb.Add(proto1);
            BetterDebug.Info("GenerateDieselMachine (name: " + "Diesel generator " + kw_amount.Format().ToString() + ") >> created!");
        }

        public static Proto.ID GetInputConfigType(int inputType)
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