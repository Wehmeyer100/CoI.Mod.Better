using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CoI.Mod.Better.Custom.Types;
using CoI.Mod.Better.Shared;
using CoI.Mod.Better.Shared.Lang;
using Mafi;
using Mafi.Base;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Localization;
using UnityEngine;

namespace CoI.Mod.Better.Custom.Data
{
	[Serializable]
	public enum FilterType
	{
		ProductFilter            = 0,
		RadioactiveProductFilter = 1,
		SteamFilter              = 2,
		Custom                   = 3
	}

	[Serializable]
	public enum RadioactiveCustomFilter
	{
		Ignore = 0,
		Yes    = 1,
		No     = 2
	}

	[Serializable]
	public class StorageData : AData<StorageProto>
	{
		public string Name;
		public string Description;

		public CategoryToolbarData Category;
		public string              NextTier;

		public CostsData Costs;

		public int               Capacity;
		public StorageType       StorageType;
		public TransferLimitData TransferLimit;

		public FilterType              Filter;
		public List<string>            CustomFilterProducts    = new List<string>();
		public bool                    CustomFilterStorable    = true;
		public RadioactiveCustomFilter CustomFilterRadioactive = RadioactiveCustomFilter.Ignore;

		public string PrefabPath;
		public string CustomIconPath;

		public List<string>                Layout;
		public FluidIndicatorGfxParamsData FluidIndicatorGfxParams;
		public PileGfxParamsData           PileGfxParams;


		public override void From(StorageProto loadData)
		{
			ProtoID = loadData.Id.ToString();

			Name = loadData.Strings.Name.Id.ToString();
			Description = loadData.Strings.DescShort.Id.ToString();

			Costs = new CostsData();
			Costs.From(loadData.Costs);

			Category = new CategoryToolbarData();
			Category.From(loadData.Graphics);

			if (loadData.nextTier.HasValue)
			{
				NextTier = new UpgradeData<StorageProto>(this, loadData.nextTier.Value.Id).ToString();
			}

			Capacity = loadData.Capacity.Value;
			if (loadData.TransferLimit == Quantity.MaxValue && loadData.TransferLimitDuration == 1.Ticks())
			{
				TransferLimit = new TransferLimitData()
				{
					Unlimited = true,
				};
			}
			else
			{
				TransferLimit = new TransferLimitData
				{
					Count = loadData.TransferLimit.Value,
					Duration = loadData.TransferLimitDuration.Seconds.RawValue,
				};
			}

			PrefabPath = loadData.Graphics.PrefabPath;
			if (loadData.Graphics.IconIsCustom)
			{
				CustomIconPath = loadData.Graphics.IconPath;
			}

			if (CustomIconPath != null && CustomIconPath.IsEmpty())
			{
				CustomIconPath = null;
			}

			Layout = loadData.Layout.SourceLayoutStr.Split('\n').ToList();
			Filter = FilterType.ProductFilter;
			if (loadData.Graphics is LooseStorageProto.Gfx looseGfx)
			{
				StorageType = StorageType.Loose;

				PileGfxParams = new PileGfxParamsData();
				PileGfxParams.From(looseGfx);
			}
			else if (loadData.Graphics is FluidStorageProto.Gfx fluidGfx)
			{
				StorageType = StorageType.Fluid;

				FluidIndicatorGfxParams = new FluidIndicatorGfxParamsData();
				FluidIndicatorGfxParams.From(fluidGfx);

				if (loadData.Id == MyIDs.Buildings.StorageSteamT1 || loadData.Id == MyIDs.Buildings.StorageSteamT2 || loadData.Id == MyIDs.Buildings.StorageSteamT3 || loadData.Id == MyIDs.Buildings.StorageSteamT4)
				{
					Filter = FilterType.SteamFilter;
				}
			}
			else if (loadData.EntityType == typeof(NuclearWasteStorage))
			{
				StorageType = StorageType.Radioactive;
				Filter = FilterType.RadioactiveProductFilter;
			}
			else
			{
				StorageType = StorageType.Flat;
			}
		}

		public override Option<StorageProto> Into(ProtoRegistrator registrator)
		{
			throw new NotSupportedException();
		}

		private Option<StorageProtoBuilder.State> IntoInternal(ProtoRegistrator registrator)
		{
			if (!CheckProtoID())
			{
				return Option<StorageProtoBuilder.State>.None;
			}

			Option<StorageProto> overrideStorageProto = GetOverrideProto(registrator);

			LocStr nameStr = LocalizationManager.LoadOrCreateLocalizedString0(Name, Name);
			LocStr descriptionStr = LocalizationManager.LoadOrCreateLocalizedString0(Description, Description);


			if (IsOverrideProtoID() && overrideStorageProto.HasValue)
			{
				OverrideData(registrator, overrideStorageProto);

				nameStr = LocalizationManager.LoadOrCreateLocalizedString0(overrideStorageProto.Value.Strings.Name.Id, overrideStorageProto.Value.Strings.Name.Id);
				descriptionStr = LocalizationManager.LoadOrCreateLocalizedString0(overrideStorageProto.Value.Strings.DescShort.Id, overrideStorageProto.Value.Strings.DescShort.Id);
			}
			else
			{
				Name.CheckNotNullOrEmpty();
				Description.CheckNotNullOrEmpty();
				PrefabPath.CheckNotNullOrEmpty();
				Capacity.CheckGreater(0);
				TransferLimit.CheckNotNull();
				Layout.Count.CheckGreater(0);
			}

			if (LangManager.Instance.Has(ProtoID + "__name"))
			{
				nameStr = Loc.Str(ProtoID + "__name", LangManager.Instance.Get(ProtoID + "__name"), "");
			}

			if (LangManager.Instance.Has(ProtoID + "__desc"))
			{
				descriptionStr = Loc.Str(ProtoID + "__desc", LangManager.Instance.Get(ProtoID + "__desc"), "");
			}

			StorageProtoBuilder.State creator = registrator.StorageProtoBuilder
				.Start(nameStr.ToString(), new StaticEntityProto.ID(ProtoID))
				.Description(descriptionStr)
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

			if (Filter == FilterType.RadioactiveProductFilter)
			{
				creator.SetProductsFilter(Shared.Utilities.ProductUtility.RadioactiveProductFilter);
			}
			else if (Filter == FilterType.ProductFilter)
			{
				creator.SetProductsFilter(Shared.Utilities.ProductUtility.ProductFilter);
			}
			else if (Filter == FilterType.SteamFilter)
			{
				creator.SetProductsFilter(Shared.Utilities.ProductUtility.SteamFilter);
			}
			else if (Filter == FilterType.Custom)
			{
				creator.SetProductsFilter(customFilter);
			}

			if (Costs != null && Costs != default)
			{
				creator.SetCost(Costs.Into(), Costs.CostsDisabled);
			}

			if (!NextTier.IsNullOrEmpty())
			{
				Option<StorageProto> result_nextTier = registrator.PrototypesDb.Get<StorageProto>(new StaticEntityProto.ID(NextTier));
				if (result_nextTier.HasValue)
				{
					creator.SetNextTier(result_nextTier.Value);
				}
				else
				{
					Debug.Log("StorageData >> Into >> NextTier cannot found!");
				}
			}

			Utils.SetCategory(ref creator, Category);
			Utils.SetTransferLimit(ref creator, TransferLimit);
			if (StorageType == StorageType.Radioactive)
			{
				CustomLayoutToken[] customTokens = new CustomLayoutToken[2]
				{
					new CustomLayoutToken("-0]", (EntityLayoutParams p, int h) => new LayoutTokenSpec(-h, 4, LayoutTileConstraint.Ground, -h)), new CustomLayoutToken("-0|", (EntityLayoutParams p, int h) => new LayoutTokenSpec(-h, 6, LayoutTileConstraint.Ground, -h)),
				};
				Utils.SetLayout(ref creator, new EntityLayoutParams(null, useNewLayoutSyntax: true, customTokens), Layout);
			}
			else
			{
				Utils.SetLayout(ref creator, Layout);
			}

			return Option<StorageProtoBuilder.State>.Some(creator);
		}

		private bool customFilter(ProductProto x)
		{
			return CustomFilterProducts.Any(p =>
				p == x.Id.ToString() &&
				(CustomFilterStorable && x.IsStorable) &&
				(CustomFilterRadioactive == RadioactiveCustomFilter.Ignore ||
					(
						(CustomFilterRadioactive == RadioactiveCustomFilter.Yes && x.Radioactivity > 0) ||
						(CustomFilterRadioactive == RadioactiveCustomFilter.No && x.Radioactivity == 0)
					)
				)
			);
		}

		private void OverrideData(ProtoRegistrator registrator, Option<StorageProto> overrideStorageProto)
		{
			StorageData overrideData = new StorageData();
			overrideData.From(overrideStorageProto.Value);
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

		public override void Build(ProtoRegistrator registrator)
		{
			Option<StorageProtoBuilder.State> creator = IntoInternal(registrator);
			if (!creator.HasValue)
			{
				return;
			}

			if (IsOverrideProtoID())
			{
				registrator.PrototypesDb.RemoveOrThrow(new StaticEntityProto.ID(ProtoID));
			}

			switch (StorageType)
			{
				case StorageType.Radioactive:
				case StorageType.Flat:
					BetterDebug.Info("Custom generate flat/Radioactive storage >> id: " + ProtoID);
					creator.Value.BuildAndAdd(CountableProductProto.ProductType);
					break;
				case StorageType.Fluid:
					BetterDebug.Info("Custom generate fluid storage >> id: " + ProtoID);
					creator.Value.BuildAsFluidAndAdd();
					break;
				case StorageType.Loose:
					BetterDebug.Info("Custom generate loose storage >> id: " + ProtoID);
					creator.Value.BuildAsLooseAndAdd();
					break;
			}
		}
	}
}