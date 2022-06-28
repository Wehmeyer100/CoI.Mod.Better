using Mafi;
using Mafi.Base;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CoI.Mod.Better.Utilities
{
    public static class ProductUtility
    {
        public const int Storage_Vanilla_Capacity_T1 = 180;
        public const int Storage_Vanilla_Capacity_T2 = 360;
        public const int Storage_Vanilla_Capacity_T3 = 2160;
        public const int Storage_Vanilla_Capacity_T4 = 4320;

        public const int Countable = 0;
        public const int Fluid = 1;
        public const int Loose = 2;

        private static Dictionary<int, List<(string, ProductProto)>> cache = new Dictionary<int, List<(string, ProductProto)>>();


        public static bool SteamFilter(ProductProto x)
        {
            return x.Id == Ids.Products.SteamHi || x.Id == Ids.Products.SteamLo || x.Id == Ids.Products.SteamDepleted || x.Id == Ids.Products.ChilledWater;
        }

        public static bool ProductFilter(ProductProto x)
        {
            return x.IsStorable ? x.Radioactivity == 0 : false;
        }

        public static bool RadioactiveProductFilter(ProductProto x)
        {
            return x.IsStorable ? x.Radioactivity > 0 : false;
        }


        public static StorageProtoBuilder.State SetTransferLimitByT(StorageProtoBuilder.State creator, int TLevel)
        {
            if (BetterMod.Config.Storage.UnlimitedTransferLimit)
            {
                creator.SetNoTransferLimit();
            }
            else
            {
                int count = BetterMod.Config.Storage.TransferLimitT1Count;
                int duration = BetterMod.Config.Storage.TransferLimitT1Duration;

                switch (TLevel)
                {
                    case 2:
                        count = BetterMod.Config.Storage.TransferLimitT2Count;
                        duration = BetterMod.Config.Storage.TransferLimitT2Duration;
                        break;
                    case 3:
                        count = BetterMod.Config.Storage.TransferLimitT3Count;
                        duration = BetterMod.Config.Storage.TransferLimitT3Duration;
                        break;
                    case 4:
                        count = BetterMod.Config.Storage.TransferLimitT4Count;
                        duration = BetterMod.Config.Storage.TransferLimitT4Duration;
                        break;
                }

                creator.SetTransferLimit(count, 1.Seconds() / duration);
            }
            return creator;
        }

        public static List<(string, ProductProto)> GetCountableProducts(ProtoRegistrator registrator, bool isRadioactivity = false, List<ProductProto.ID> ignorList = null)
        {
            if (!cache.ContainsKey(Countable))
            {
                cache.Add(Countable, new List<(string, ProductProto)>());
                foreach ((string fieldName, ProductProto.ID productID) in GetProductEnumerator<CountableProductAttribute>(registrator, ignorList))
                {
                    Option<ProductProto> resultProduct = registrator.PrototypesDb.Get<ProductProto>(productID);
                    if (resultProduct.HasValue && resultProduct.Value.IsStorable && ((isRadioactivity && resultProduct.Value.Radioactivity != 0) || (!isRadioactivity && resultProduct.Value.Radioactivity == 0)))
                    {
                        cache[Countable].Add((fieldName, resultProduct.Value));
                    }
                }
            }
            return cache[Countable];
        }

        public static List<(string, ProductProto)> GetLooseProducts(ProtoRegistrator registrator, List<ProductProto.ID> ignorList = null)
        {
            if (!cache.ContainsKey(Loose))
            {
                cache.Add(Loose, new List<(string, ProductProto)>());
                foreach ((string fieldName, ProductProto.ID productID) in GetProductEnumerator<LooseProductAttribute>(registrator, ignorList))
                {
                    Option<ProductProto> resultProduct = registrator.PrototypesDb.Get<ProductProto>(productID);
                    if (resultProduct.HasValue && resultProduct.Value.IsStorable)
                    {
                        cache[Loose].Add((fieldName, resultProduct.Value));
                    }
                }
            }
            return cache[Loose];
        }

        public static List<(string, ProductProto)> GetFluidProducts(ProtoRegistrator registrator, List<ProductProto.ID> ignorList = null)
        {
            if (!cache.ContainsKey(Fluid))
            {
                cache.Add(Fluid, new List<(string, ProductProto)>());
                foreach ((string fieldName, ProductProto.ID productID) in GetProductEnumerator<FluidProductAttribute>(registrator, ignorList))
                {
                    Option<ProductProto> resultProduct = registrator.PrototypesDb.Get<ProductProto>(productID);
                    if (resultProduct.HasValue && resultProduct.Value.IsStorable)
                    {
                        cache[Fluid].Add((fieldName, resultProduct.Value));
                    }
                }
            }
            return cache[Fluid];
        }

        public static IEnumerable<(string, ProductProto.ID)> GetProductEnumerator<T_ATTRIBUT>(ProtoRegistrator registrator, List<ProductProto.ID> ignorList = null) where T_ATTRIBUT : ProductAttribute
        {
            IEnumerable<FieldInfo> result = ReflectionUtility.GetAllFields(typeof(Ids.Products));
            foreach (FieldInfo field in result)
            {
                string fieldName = field.Name;
                object value = field.GetValue(null);
                if (field.IsStatic && value != null && value is ProductProto.ID productID && field.GetCustomAttributes(typeof(T_ATTRIBUT), false).Length > 0)
                {
                    if (ignorList != null && ignorList.Contains(productID))
                    {
                        continue;
                    }
                    yield return (fieldName, productID);
                }
            }
        }

        public static void ClearCache()
        {
            cache.Clear();
        }
    }
}
