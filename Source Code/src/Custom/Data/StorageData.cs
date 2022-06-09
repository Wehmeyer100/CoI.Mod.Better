using Mafi;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities.Static;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Localization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CoI.Mod.Better.Custom
{

    [Serializable]
    public class StorageData : IStorageData<StorageProto, StorageProtoBuilder.State>
    {
        public bool overrideProtoID = false;
        public string ProtoID;

        public string Name;
        public string Description;
        public CostsData Costs;

        public CategoryToolbar Category;
        public string NextTier;

        public int Capacity;
        public StorageType StorageType;
        public TransferLimitData TransferLimit;

        public string PrefabPath;
        public string CustomIconPath;

        public List<string> Layout;
        public FluidIndicatorGfxParamsData FluidIndicatorGfxParams;
        public PileGfxParamsData PileGfxParams;


        public void Load(StorageProto loadData)
        {
            overrideProtoID = true;
            ProtoID = loadData.Id.ToString();

            Name = loadData.Strings.Name.ToString();
            Description = loadData.Strings.DescShort.ToString();

            Costs = new CostsData();
            Costs.From(loadData.Costs);

            if (loadData.Graphics.Categories.Length == 0) 
            {
                Category = CategoryToolbar.None;
            }
            else
            {
                Category = Utils.GetCategory(loadData.Graphics.Categories.First.Id);
            }

            if (loadData.NextTier.HasValue)
            {
                NextTier = loadData.NextTier.Value.Id.ToString();
            }

            Capacity = loadData.Capacity.Value;
        }

        public Option<StorageProtoBuilder.State> Into(ProtoRegistrator registrator)
        {
            StaticEntityProto.ID protoID = new StaticEntityProto.ID(ProtoID);
            if (
                (!overrideProtoID && registrator.PrototypesDb.Get<StorageProto>(protoID).HasValue)
                || Capacity < 0
                || ProtoID.IsEmpty()
                || Name.IsEmpty()
                || Description.IsEmpty()
                || PrefabPath.IsEmpty()
                || TransferLimit.Duration <= 0
                || TransferLimit.Count <= 0
                || Layout.Count == 0)
            {
                Debug.Log("StorageData >> Into >> name: " + Name + " | id: " + protoID + " >> Storage cannot generate!");
                return Option<StorageProtoBuilder.State>.None;
            }

            StorageProtoBuilder.State creator = registrator.StorageProtoBuilder
                .Start(Name, protoID)
                .Description(LocalizationManager.CreateAlreadyLocalizedStr(ProtoID + "Desc", Description))
                .SetCapacity(Capacity)
                .SetPrefabPath(PrefabPath);

            if (PileGfxParams != null && PileGfxParams != default && StorageType == StorageType.Loose)
            {
                creator.SetPileGfxParams(PileGfxParams.smoothPileObjectPath, PileGfxParams.roughPileObjectPath, PileGfxParams.pileTextureParams.Into());
            }

            if (FluidIndicatorGfxParams != null && FluidIndicatorGfxParams != default && StorageType == StorageType.Fluid)
            {
                creator.SetFluidIndicatorGfxParams(FluidIndicatorGfxParams.indicatorObjectPath, FluidIndicatorGfxParams.Into());
            }

            if (CustomIconPath != null || !CustomIconPath.IsEmpty())
            {
                creator.SetCustomIconPath(CustomIconPath);
            }

            //.SetCost(Costs.Buildings.StorageLoose)
            if (StorageType == StorageType.Radioactive)
            {
                creator.SetProductsFilter(RadioactiveProductFilter);
            }
            else
            {
                creator.SetProductsFilter(ProductFilter);
            }

            if (Costs != null && Costs != default)
            {
                creator.SetCost(Costs.Into());
            }

            Option<StorageProto> result_nextTier = registrator.PrototypesDb.Get<StorageProto>(new StaticEntityProto.ID(NextTier));
            if (result_nextTier.HasValue)
            {
                creator.SetNextTier(result_nextTier.Value);
            }
            else
            {
                Debug.Log("StorageData >> Into >> NextTier cannot found!");
            }

            creator = Utils.SetCategory(creator, Category);
            creator = Utils.SetTransferLimit(creator, TransferLimit);

            var resultLayout = Utils.SetLayout(creator, Layout);
            if (resultLayout.HasValue)
            {
                creator = resultLayout.Value;
            }

            return creator;
        }

        public Option<StorageProto> Build(ProtoRegistrator registrator)
        {
            Option<StorageProtoBuilder.State> creator = Into(registrator);
            if (!creator.HasValue)
            {
                return Option<StorageProto>.None;
            }
            Option<StorageProto> result = Option<StorageProto>.None;
            switch (StorageType)
            {
                case StorageType.Flat:
                    result = creator.Value.BuildAndAdd(CountableProductProto.ProductType);
                    break;
                case StorageType.Fluid:
                    result = creator.Value.BuildAsFluidAndAdd();
                    break;
                case StorageType.Loose:
                    result = creator.Value.BuildAsLooseAndAdd();
                    break;
                case StorageType.Radioactive:
                    result = creator.Value.BuildAndAdd(CountableProductProto.ProductType);
                    break;
            }
            return result;
        }


        private static bool ProductFilter(ProductProto x)
        {
            return x.IsStorable ? x.Radioactivity == 0 : false;
        }

        private static bool RadioactiveProductFilter(ProductProto x)
        {
            return x.IsStorable ? x.Radioactivity > 0 : false;
        }
    }
}
