using Mafi;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Buildings.Beacons;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace CoI.Mod.Better
{
    internal class Beacon : IModData
    {
        internal const float max_index = 200;

        private ProductProto nothing, diesel, copper, rubber, iron, oil, food;
        private float all_baseValue_multiplier = 1f;
        private ImmutableArray<ProductQuantity> nothing_data = null;

        public void RegisterData(ProtoRegistrator registrator)
        {
            if (MoreRecipes.Config.DisableNewRefugeesSystem) return;

            LoadData(registrator);

            registrator.PrototypesDb.RemoveOrThrow(MyIDs.Utilities.BeaconSchedule);
            registrator.PrototypesDb.Add(new BeaconScheduleProto(MyIDs.Utilities.BeaconSchedule, GenerateReward));
        }

        private void LoadData(ProtoRegistrator registrator)
        {
            all_baseValue_multiplier = Mathf.Clamp(MoreRecipes.Config.BeaconRewardBaseValueMultiplier, 0.1f, 100f); ;

            registrator.FluidProductProtoBuilder
               .Start("Nothing", MyIDs.Products.Nothing)
               .SetIsStorable(false)
               .SetCanBeDiscarded(false)
               .SetIsWaste(false)
               .SetColor(10061858)
               .SetCustomIconPath("Assets/Base/Products/Icons/Fertilizer.svg")
               .BuildAndAdd();

            nothing = registrator.PrototypesDb.GetOrThrow<ProductProto>(MyIDs.Products.Nothing);
            iron = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Iron);
            copper = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Copper);
            diesel = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Diesel);
            rubber = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Rubber);
            oil = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.CrudeOil);
            food = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Food);
        }

        private Option<RefugeesReward> GenerateReward(int index)
        {
            float reward_multiply = 1 + ((index == 0 ? 1 : index) / max_index);

            // Calc Refugees Range
            int refugeesMin = MoreRecipes.Config.BeaconRefugeesMin;
            refugeesMin = Mathf.Clamp(refugeesMin, 1, int.MaxValue);

            int refugeesMax = MoreRecipes.Config.BeaconRefugeesMax;
            refugeesMax = Mathf.Clamp(refugeesMax, refugeesMin, int.MaxValue);

            // Calc Durations Range
            int durationMin = MoreRecipes.Config.BeaconDurationMin;
            durationMin = Mathf.Clamp(durationMin, 1, int.MaxValue);

            int durationMax = MoreRecipes.Config.BeaconDurationMax;
            durationMax = Mathf.Clamp(durationMax, durationMin, int.MaxValue);

            // Calc Amount of Refugees
            int amountOfRefugees = Mathf.FloorToInt(Random.Range(refugeesMin, refugeesMax) * reward_multiply);
            amountOfRefugees += Random.Range(amountOfRefugees, amountOfRefugees);
            amountOfRefugees = Mathf.Clamp(amountOfRefugees, 0, 100);

            // To nothing then amount zero
            if (amountOfRefugees == 0)
            {
                return new RefugeesReward(
                    possibleRewards: ImmutableArray.Create(GetRewardNothing()),
                    duration: Random.Range(durationMin, durationMax).Months(),
                    amountOfRefugees: amountOfRefugees,
                    minimalTier: 1);
            }

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
            {
                // Caching
                nothing_data = ImmutableArray.Create(GetProductQuantity(nothing, 0, 0, 0));
            }
            // return from Cache
            return nothing_data;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ImmutableArray<ProductQuantity> GetReward(float reward_multiply, int amountOfRefugees)
        {
            List<ProductQuantity> availableRewards = new List<ProductQuantity>();

            // Add Products by Chance
            AddByChance(ref availableRewards, iron,   MoreRecipes.Config.BeaconRewardIronBaseValue,   MoreRecipes.Config.BeaconRewardIronChance, reward_multiply, amountOfRefugees);
            AddByChance(ref availableRewards, copper, MoreRecipes.Config.BeaconRewardCopperBaseValue, MoreRecipes.Config.BeaconRewardCopperChance, reward_multiply, amountOfRefugees);
            AddByChance(ref availableRewards, rubber, MoreRecipes.Config.BeaconRewardRubberBaseValue, MoreRecipes.Config.BeaconRewardRubberChance, reward_multiply, amountOfRefugees);
            AddByChance(ref availableRewards, diesel, MoreRecipes.Config.BeaconRewardDieselBaseValue, MoreRecipes.Config.BeaconRewardDieselChance, reward_multiply, amountOfRefugees);
            AddByChance(ref availableRewards, oil,    MoreRecipes.Config.BeaconRewardOilBaseValue,    MoreRecipes.Config.BeaconRewardOilChance, reward_multiply, amountOfRefugees);
            AddByChance(ref availableRewards, food,   MoreRecipes.Config.BeaconRewardFoodBaseValue,   MoreRecipes.Config.BeaconRewardFoodChance, reward_multiply, amountOfRefugees);

            // Check spawn rewards when zero then nothing
            return availableRewards.Count == 0 ? GetRewardNothing() : ImmutableArray.CreateRange(availableRewards);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddByChance(ref List<ProductQuantity> availableRewards, ProductProto product, float key_base_value, float key_chance, float reward_multiply, int amountOfRefugees)
        {
            // Clamp Chance in the range 0-1 float
            float base_value = Mathf.Clamp(key_base_value, 0f, float.MaxValue);
            float chance = Mathf.Clamp(key_chance, 0f, 1f);

            // No Chance, go return
            if (chance == 0.0f) return;

            if (base_value != 0 && chance >= Random.Range(0f, 1f))
            {
                availableRewards.Add(GetProductQuantity(product, base_value, reward_multiply, amountOfRefugees));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ProductQuantity GetProductQuantity(ProductProto product, float baseValue, float reward_multiply, int amountOfRefugees)
        {
            float value = CalcProductReward(baseValue * all_baseValue_multiplier, reward_multiply, amountOfRefugees);
            return new ProductQuantity(product, new Quantity(Mathf.FloorToInt(value)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float CalcProductReward(float baseValue, float reward_multiply, int amountOfRefugees)
        {
            return ((baseValue * Random.Range(.5f, 1.5f)) * (amountOfRefugees * reward_multiply));
        }
    }
}
