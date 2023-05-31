using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared;
using Mafi;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Buildings.Beacons;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using UnityEngine;

namespace CoI.Mod.Better.Buildings
{
    internal class Beacon : IModData
    {
        internal const float max_index = 200;
        private float all_baseValue_multiplier = 1f;

        private ProductProto nothing, diesel, copper, rubber, iron, oil, food;
        private ImmutableArray<ProductQuantity> nothing_data = null;

        public void RegisterData(ProtoRegistrator registrator)
        {
            if (!BetterMod.Config.Systems.RefugeesSystem) return;

            LoadData(registrator);

            registrator.PrototypesDb.RemoveOrThrow(MyIDs.Utilities.BeaconSchedule);
            registrator.PrototypesDb.Add(new BeaconScheduleProto(MyIDs.Utilities.BeaconSchedule, GenerateReward));

            BetterDebug.Info("Beacon >> RegisterData >> replace schedule!");
        }

        private void LoadData(ProtoRegistrator registrator)
        {
            all_baseValue_multiplier = Mathf.Clamp(BetterMod.Config.Beacon.RewardBaseValueMultiplier, 0.1f, 100f);
            ;

            registrator.FluidProductProtoBuilder
                .Start("Nothing", Products.Nothing)
                .SetIsStorable(false)
                .SetCanBeDiscarded(false)
                .SetIsWaste(false)
                .SetColor(10061858)
                .SetCustomIconPath("Assets/Base/Products/Icons/Fertilizer.svg")
                .BuildAndAdd();

            nothing = registrator.PrototypesDb.GetOrThrow<ProductProto>(Products.Nothing);
            iron = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Iron);
            copper = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Copper);
            diesel = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Diesel);
            rubber = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Rubber);
            oil = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.CrudeOil);
            food = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Potato);
        }

        private Option<RefugeesReward> GenerateReward(int index)
        {
            var reward_multiply = 1 + (index == 0 ? 1 : index) / max_index;

            // Calc Refugees Range
            var refugeesMin = BetterMod.Config.Beacon.RefugeesMin;
            refugeesMin = Mathf.Clamp(refugeesMin, 1, int.MaxValue);

            var refugeesMax = BetterMod.Config.Beacon.RefugeesMax;
            refugeesMax = Mathf.Clamp(refugeesMax, refugeesMin, int.MaxValue);

            // Calc Durations Range
            var durationMin = BetterMod.Config.Beacon.DurationMin;
            durationMin = Mathf.Clamp(durationMin, 1, int.MaxValue);

            var durationMax = BetterMod.Config.Beacon.DurationMax;
            durationMax = Mathf.Clamp(durationMax, durationMin, int.MaxValue);

            // Calc Amount of Refugees
            var amountOfRefugees = Mathf.FloorToInt(Random.Range(refugeesMin, refugeesMax) * reward_multiply);
            amountOfRefugees += Random.Range(amountOfRefugees, amountOfRefugees);
            amountOfRefugees = Mathf.Clamp(amountOfRefugees, 0, 100);


            BetterDebug.Info("Beacon >> GenerateReward >> reward_multiply: " + reward_multiply +
                             " >> amountOfRefugees: " + amountOfRefugees + " >> durationMinMax: ( " + durationMin +
                             " : " + durationMax + ") >> refugeesMinMax: ( " + refugeesMin + " : " + refugeesMax +
                             " )");

            // To nothing then amount zero
            if (amountOfRefugees == 0)
            {
                BetterDebug.Info("Beacon >> GenerateReward >> generate noting.");

                return new RefugeesReward(
                    possibleRewards: ImmutableArray.Create(GetRewardNothing()),
                    duration: Random.Range(durationMin, durationMax).Months(),
                    amountOfRefugees: amountOfRefugees,
                    minimalTier: 1);
            }

            BetterDebug.Info("Beacon >> GenerateReward >> generate randon reward.");
            return new RefugeesReward(
                possibleRewards: ImmutableArray.Create(
                    GetReward(reward_multiply, amountOfRefugees),
                    GetReward(reward_multiply, amountOfRefugees),
                    GetReward(reward_multiply, amountOfRefugees),
                    GetReward(reward_multiply, amountOfRefugees),
                    GetReward(reward_multiply * 0.75f, amountOfRefugees),
                    GetReward(reward_multiply * 0.5f, amountOfRefugees),
                    GetRewardNothing(),
                    GetRewardNothing()
                ),
                duration: Random.Range(durationMin, durationMax).Months(),
                amountOfRefugees: amountOfRefugees,
                minimalTier: 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ImmutableArray<ProductQuantity> GetRewardNothing()
        {
            // Is Nothing data cached?
            if (nothing_data == null)
                // Caching
                nothing_data = ImmutableArray.Create(GetProductQuantity(nothing, 0, 0, 0));
            // return from Cache
            return nothing_data;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ImmutableArray<ProductQuantity> GetReward(float reward_multiply, int amountOfRefugees)
        {
            var availableRewards = new List<ProductQuantity>();

            // Add Products by Chance
            AddByChance(ref availableRewards, iron, BetterMod.Config.Beacon.RewardIronBaseValue,
                BetterMod.Config.Beacon.RewardIronChance, reward_multiply, amountOfRefugees);
            AddByChance(ref availableRewards, copper, BetterMod.Config.Beacon.RewardCopperBaseValue,
                BetterMod.Config.Beacon.RewardCopperChance, reward_multiply, amountOfRefugees);
            AddByChance(ref availableRewards, rubber, BetterMod.Config.Beacon.RewardRubberBaseValue,
                BetterMod.Config.Beacon.RewardRubberChance, reward_multiply, amountOfRefugees);
            AddByChance(ref availableRewards, diesel, BetterMod.Config.Beacon.RewardDieselBaseValue,
                BetterMod.Config.Beacon.RewardDieselChance, reward_multiply, amountOfRefugees);
            AddByChance(ref availableRewards, oil, BetterMod.Config.Beacon.RewardOilBaseValue,
                BetterMod.Config.Beacon.RewardOilChance, reward_multiply, amountOfRefugees);
            AddByChance(ref availableRewards, food, BetterMod.Config.Beacon.RewardFoodBaseValue,
                BetterMod.Config.Beacon.RewardFoodChance, reward_multiply, amountOfRefugees);

            // Check spawn rewards when zero then nothing
            return availableRewards.Count == 0 ? GetRewardNothing() : ImmutableArray.CreateRange(availableRewards);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddByChance(ref List<ProductQuantity> availableRewards, ProductProto product, float key_base_value,
            float key_chance, float reward_multiply, int amountOfRefugees)
        {
            // Clamp Chance in the range 0-1 float
            var base_value = Mathf.Clamp(key_base_value, 0f, float.MaxValue);
            var chance = Mathf.Clamp(key_chance, 0f, 1f);

            // No Chance, go return
            if (chance <= 0.0f) return;

            if (base_value != 0 && chance >= Random.Range(0f, 1f))
                availableRewards.Add(GetProductQuantity(product, base_value, reward_multiply, amountOfRefugees));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ProductQuantity GetProductQuantity(ProductProto product, float baseValue, float reward_multiply,
            int amountOfRefugees)
        {
            var value = CalcProductReward(baseValue * all_baseValue_multiplier, reward_multiply, amountOfRefugees);
            return new ProductQuantity(product, new Quantity(Mathf.FloorToInt(value)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float CalcProductReward(float baseValue, float reward_multiply, int amountOfRefugees)
        {
            return baseValue * Random.Range(.5f, 1.5f) * (amountOfRefugees * reward_multiply);
        }
    }
}