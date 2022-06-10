﻿using CoI.Mod.Better.Custom.Types;
using Mafi;
using Mafi.Base;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities.Static;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CoI.Mod.Better.Custom.Data
{


    [Serializable]
    public class StorageData
    {
        public bool overrideProtoID;
        public string ProtoID;

        public string Name;
        public string Description;

        public CategoryToolbarData Category;
        public string NextTier;

        public CostsData Costs;

        public int Capacity;
        public StorageType StorageType;
        public TransferLimitData TransferLimit;

        public string PrefabPath;
        public string CustomIconPath;

        public List<string> Layout;
        public FluidIndicatorGfxParamsData FluidIndicatorGfxParams;
        public PileGfxParamsData PileGfxParams;


        public void From(ProtoRegistrator registrator, StorageProto loadData)
        {
            ProtoID = loadData.Id.ToString();

            Name = loadData.Strings.Name.ToString();
            Description = loadData.Strings.DescShort.ToString();

            Costs = new CostsData();
            Costs.From(loadData.Costs);

            Category = new CategoryToolbarData();
            Category.From(loadData.Graphics);

            if (loadData.NextTier.HasValue)
            {
                NextTier = loadData.NextTier.Value.Id.ToString();
            }

            Capacity = loadData.Capacity.Value;
            if (loadData.TransferLimit == Quantity.MaxValue && loadData.TransferLimitDuration == 1.Ticks())
            {
                TransferLimit = new TransferLimitData() { Unlimited = true };
            }
            else
            {
                TransferLimit = new TransferLimitData { Count = loadData.TransferLimit.Value, Duration = loadData.TransferLimitDuration.Seconds.RawValue };
            }

            PrefabPath = loadData.Graphics.PrefabPaths.First;
            if (loadData.Graphics.IconIsCustom)
            {
                CustomIconPath = loadData.Graphics.IconPath;
            }

            if (CustomIconPath != null && CustomIconPath.IsEmpty())
            {
                CustomIconPath = null;
            }

            ProductProto productSpentFuel = registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.SpentFuel);

            Layout = loadData.Layout.SourceLayoutStr.Split('\n').ToList();
            if (loadData.Graphics is LooseStorageProto.Gfx)
            {
                StorageType = StorageType.Loose;

                LooseStorageProto.Gfx looseGfx = (LooseStorageProto.Gfx)loadData.Graphics;
                PileGfxParams = new PileGfxParamsData();
                PileGfxParams.From(looseGfx);
            }
            else if (loadData.Graphics is FluidStorageProto.Gfx)
            {
                StorageType = StorageType.Fluid;

                FluidStorageProto.Gfx fluidGfx = (FluidStorageProto.Gfx)loadData.Graphics;
                FluidIndicatorGfxParams = new FluidIndicatorGfxParamsData();
                FluidIndicatorGfxParams.From(fluidGfx);
            }
            else if (loadData.IsProductSupported(productSpentFuel))
            {
                StorageType = StorageType.Radioactive;
            }
            else
            {
                StorageType = StorageType.Flat;
            }
        }

        public Option<StorageProtoBuilder.State> Into(ProtoRegistrator registrator)
        {
            ProtoID.CheckNotNullOrEmpty();
            if (ProtoID.Contains(" "))
            {
                Debug.Log("StorageData >> Into >> name: " + Name + " >> Storage cannot generate, ProtoID is not valid! >> There must be no blank characters.");
                return Option<StorageProtoBuilder.State>.None;
            }

            StaticEntityProto.ID protoID = new StaticEntityProto.ID(ProtoID);
            Option<StorageProto> overrideStorageProto = registrator.PrototypesDb.Get<StorageProto>(protoID);
            if (!overrideProtoID && overrideStorageProto.HasValue)
            {
                Debug.Log("StorageData >> Into >> name: " + Name + " | id: " + protoID + " >> Storage cannot generate, ProtoID already exists!");
                return Option<StorageProtoBuilder.State>.None;
            }
            else if (overrideProtoID && !overrideStorageProto.HasValue)
            {
                Debug.Log("StorageData >> Into >> name: " + Name + " | id: " + protoID + " >> Storage cannot override, ProtoID is not exists!");
                return Option<StorageProtoBuilder.State>.None;
            }

            if (overrideProtoID && overrideStorageProto.HasValue)
            {
                StorageData overrideData = new StorageData();
                overrideData.From(registrator, overrideStorageProto.Value);
                if (Name == null || Name.IsEmpty())
                {
                    Name = overrideData.Name;
                }
                else
                {
                    Name.CheckNotNullOrEmpty();
                }

                if (Description == null || Description.IsEmpty())
                {
                    Description = overrideData.Description;
                }
                else
                {
                    Description.CheckNotNullOrEmpty();
                }


                if (PrefabPath == null || PrefabPath.IsEmpty())
                {
                    PrefabPath = overrideData.Description;
                }
                else
                {
                    PrefabPath.CheckNotNullOrEmpty();
                }

                if (Category == null || Category == default)
                {
                    Category = overrideData.Category;
                }

                if (Capacity == default)
                {
                    Capacity = overrideData.Capacity;
                }
                else
                {
                    Capacity.CheckGreater(0);
                }

                if (TransferLimit == null || TransferLimit == default)
                {
                    TransferLimit = overrideData.TransferLimit;
                }
                else
                {
                    TransferLimit.CheckNotNull();
                    TransferLimit.Count.CheckGreater(0);
                    TransferLimit.Duration.CheckGreater(0);
                }

                if (Layout == null || Layout.Count == 0)
                {
                    Layout = overrideData.Layout;
                }
                else
                {
                    Layout.Count.CheckGreater(0);
                }

                if ((PileGfxParams == null || PileGfxParams == default) && overrideData.PileGfxParams != null)
                {
                    PileGfxParams = overrideData.PileGfxParams;
                }

                if ((FluidIndicatorGfxParams == null || FluidIndicatorGfxParams == default) && overrideData.FluidIndicatorGfxParams != null)
                {
                    FluidIndicatorGfxParams = overrideData.FluidIndicatorGfxParams;
                }

                if (CustomIconPath == null || CustomIconPath.IsEmpty())
                {
                    CustomIconPath = overrideData.CustomIconPath;
                }

                if (StorageType == StorageType.None)
                {
                    StorageType = overrideData.StorageType;
                }

                if (Costs == null || Costs == default)
                {
                    Costs = overrideData.Costs;
                }

                if ((NextTier == null || NextTier == default) && (overrideData.NextTier != null && overrideData.NextTier != default))
                {
                    NextTier = overrideData.NextTier;
                }
            }
            else
            {
                Name.CheckNotNullOrEmpty();
                Description.CheckNotNullOrEmpty();
                PrefabPath.CheckNotNullOrEmpty();
                Capacity.CheckGreater(0);
                TransferLimit.CheckNotNull();
                TransferLimit.Count.CheckGreater(0);
                TransferLimit.Duration.CheckGreater(0);
                Layout.Count.CheckGreater(0);
            }

            Proto.Str protoStr = Proto.CreateStr(protoID, Name, Description);

            StorageProtoBuilder.State creator = registrator.StorageProtoBuilder
                .Start(protoStr.Name.ToString(), protoID)
                .Description(protoStr.DescShort)
                .SetCapacity(Capacity)
                .SetPrefabPath(PrefabPath);

            if (PileGfxParams != null && PileGfxParams != default && StorageType == StorageType.Loose)
            {
                PileGfxParams.smoothPileObjectPath.CheckNotNullOrEmpty();
                PileGfxParams.roughPileObjectPath.CheckNotNullOrEmpty();

                PileGfxParams.pileTextureParams.CheckNotNull();
                PileGfxParams.pileTextureParams.scale.CheckGreaterOrEqual(0);
                PileGfxParams.pileTextureParams.offsetX.CheckGreaterOrEqual(0);
                PileGfxParams.pileTextureParams.offsetY.CheckGreaterOrEqual(0);

                creator.SetPileGfxParams(PileGfxParams.smoothPileObjectPath, PileGfxParams.roughPileObjectPath, PileGfxParams.pileTextureParams.Into());
            }

            if (FluidIndicatorGfxParams != null && FluidIndicatorGfxParams != default && StorageType == StorageType.Fluid)
            {
                FluidIndicatorGfxParams.indicatorObjectPath.CheckNotNullOrEmpty();
                FluidIndicatorGfxParams.detailsScale.CheckGreaterOrEqual(0);
                FluidIndicatorGfxParams.sizePerTextureWidthMeters.CheckGreaterOrEqual(0);
                FluidIndicatorGfxParams.stillMovementScale.CheckGreaterOrEqual(0);

                creator.SetFluidIndicatorGfxParams(FluidIndicatorGfxParams.indicatorObjectPath, FluidIndicatorGfxParams.Into());
            }

            if (CustomIconPath != null || !CustomIconPath.IsEmpty())
            {
                CustomIconPath.CheckNotNullOrEmpty();

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
                creator.SetCost(Costs.Into(), Costs.CostsDisabled);
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

        public void Build(ProtoRegistrator registrator)
        {
            Option<StorageProtoBuilder.State> creator = Into(registrator);
            if (!creator.HasValue)
            {
                return;
            }

            if (overrideProtoID)
            {
                registrator.PrototypesDb.RemoveOrThrow(new StaticEntityProto.ID(ProtoID));
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
