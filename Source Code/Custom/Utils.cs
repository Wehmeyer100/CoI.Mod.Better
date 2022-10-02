using System;
using System.Collections.Generic;
using CoI.Mod.Better.Custom.Types;
using Mafi;
using Mafi.Base;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Prototypes;
using UnityEngine;

namespace CoI.Mod.Better.Custom
{
	public static class Utils
	{
		public static T SetTransferLimit<T>(ref T creator, TransferLimitData data) where T : StorageProtoBuilder.State
		{
			if (data.Unlimited)
			{
				creator.SetNoTransferLimit();
			}
			else
			{
				creator.SetTransferLimit(data.Count, data.Duration.Seconds());
			}
			return creator;
		}

		public static T SetCategory<T>(ref T creator, CategoryToolbarData category) where T : LayoutEntityBuilderState<T>
		{
			switch (category.Category)
			{
				case CategoryToolbar.Transports:
					creator.SetCategories(Ids.ToolbarCategories.Transports);
					break;
				case CategoryToolbar.Machines:
					creator.SetCategories(Ids.ToolbarCategories.Machines);
					break;
				case CategoryToolbar.MachinesWater:
					creator.SetCategories(Ids.ToolbarCategories.MachinesWater);
					break;
				case CategoryToolbar.MachinesFood:
					creator.SetCategories(Ids.ToolbarCategories.MachinesFood);
					break;
				case CategoryToolbar.MachinesMetallurgy:
					creator.SetCategories(Ids.ToolbarCategories.MachinesMetallurgy);
					break;
				case CategoryToolbar.MachinesOil:
					creator.SetCategories(Ids.ToolbarCategories.MachinesOil);
					break;
				case CategoryToolbar.MachinesElectricity:
					creator.SetCategories(Ids.ToolbarCategories.MachinesElectricity);
					break;
				case CategoryToolbar.Waste:
					creator.SetCategories(Ids.ToolbarCategories.Waste);
					break;
				case CategoryToolbar.Storages:
					creator.SetCategories(Ids.ToolbarCategories.Storages);
					break;
				case CategoryToolbar.Buildings:
					creator.SetCategories(Ids.ToolbarCategories.Buildings);
					break;
				case CategoryToolbar.BuildingsForVehicles:
					creator.SetCategories(Ids.ToolbarCategories.BuildingsForVehicles);
					break;
				case CategoryToolbar.Housing:
					creator.SetCategories(Ids.ToolbarCategories.Housing);
					break;
				case CategoryToolbar.Docks:
					creator.SetCategories(Ids.ToolbarCategories.Docks);
					break;
				case CategoryToolbar.Landmarks:
					creator.SetCategories(Ids.ToolbarCategories.Landmarks);
					break;
				case CategoryToolbar.Custom:
					creator.SetCategories(new Proto.ID(category.CustomCategory));
					break;
				case CategoryToolbar.None:
				default:
					creator.SetNoCategory();
					break;
			}
			return creator;
		}
		public static CategoryToolbar GetCategory(Proto.ID category)
		{
			if (category == Ids.ToolbarCategories.Storages)
			{
				return CategoryToolbar.Storages;
			}
			if (category == Ids.ToolbarCategories.Waste)
			{
				return CategoryToolbar.Waste;
			}
			if (category == Ids.ToolbarCategories.Buildings)
			{
				return CategoryToolbar.Buildings;
			}
			if (category == Ids.ToolbarCategories.Machines)
			{
				return CategoryToolbar.Machines;
			}
			if (category == Ids.ToolbarCategories.BuildingsForVehicles)
			{
				return CategoryToolbar.BuildingsForVehicles;
			}
			if (category == Ids.ToolbarCategories.Docks)
			{
				return CategoryToolbar.Docks;
			}
			if (category == Ids.ToolbarCategories.Housing)
			{
				return CategoryToolbar.Housing;
			}
			if (category == Ids.ToolbarCategories.Landmarks)
			{
				return CategoryToolbar.Landmarks;
			}
			if (category == Ids.ToolbarCategories.MachinesElectricity)
			{
				return CategoryToolbar.MachinesElectricity;
			}
			if (category == Ids.ToolbarCategories.MachinesFood)
			{
				return CategoryToolbar.MachinesFood;
			}
			if (category == Ids.ToolbarCategories.MachinesMetallurgy)
			{
				return CategoryToolbar.MachinesMetallurgy;
			}
			if (category == Ids.ToolbarCategories.MachinesOil)
			{
				return CategoryToolbar.MachinesOil;
			}
			if (category == Ids.ToolbarCategories.MachinesWater)
			{
				return CategoryToolbar.MachinesOil;
			}
			if (category != null)
			{
				return CategoryToolbar.Custom;
			}
			return CategoryToolbar.None;
		}

		public static T SetLayout<T>(ref T creator, List<string> Layout) where T : LayoutEntityBuilderState<T>
		{
			return SetLayout(ref creator, EntityLayoutParams.DEFAULT, Layout);
		}

		public static T SetLayout<T>(ref T creator, EntityLayoutParams entityLayoutParams, List<string> Layout) where T : LayoutEntityBuilderState<T>
		{
			int countlayout = Layout.Count;
			if (countlayout == 1)
			{
				creator.SetLayout(entityLayoutParams, Layout[0]);
			}
			else if (countlayout == 2)
			{
				creator.SetLayout(entityLayoutParams, Layout[0], Layout[1]);
			}
			else if (countlayout == 3)
			{
				creator.SetLayout(entityLayoutParams, Layout[0], Layout[1], Layout[2]);
			}
			else if (countlayout == 4)
			{
				creator.SetLayout(entityLayoutParams, Layout[0], Layout[1], Layout[2], Layout[3]);
			}
			else if (countlayout == 5)
			{
				creator.SetLayout(entityLayoutParams, Layout[0], Layout[1], Layout[2], Layout[3], Layout[4]);
			}
			else if (countlayout == 6)
			{
				creator.SetLayout(entityLayoutParams, Layout[0], Layout[1], Layout[2], Layout[3], Layout[4], Layout[5]);
			}
			else if (countlayout == 7)
			{
				creator.SetLayout(entityLayoutParams, Layout[0], Layout[1], Layout[2], Layout[3], Layout[4], Layout[5], Layout[6]);
			}
			else if (countlayout == 8)
			{
				creator.SetLayout(entityLayoutParams, Layout[0], Layout[1], Layout[2], Layout[3], Layout[4], Layout[5], Layout[6], Layout[7]);
			}
			else if (countlayout == 9)
			{
				creator.SetLayout(entityLayoutParams, Layout[0], Layout[1], Layout[2], Layout[3], Layout[4], Layout[5], Layout[6], Layout[7], Layout[8]);
			}
			else if (countlayout == 10)
			{
				creator.SetLayout(entityLayoutParams, Layout[0], Layout[1], Layout[2], Layout[3], Layout[4], Layout[5], Layout[6], Layout[7], Layout[8], Layout[9]);
			}
			else if (countlayout == 11)
			{
				creator.SetLayout(entityLayoutParams, Layout[0], Layout[1], Layout[2], Layout[3], Layout[4], Layout[5], Layout[6], Layout[7], Layout[8], Layout[9], Layout[10]);
			}
			else
			{
				Debug.Log("Data >> SetLayout >> Layout with more 11 entries is not supported!");
				throw new NotSupportedException("Data >> SetLayout >> Layout with more 11 entries is not supported!");
			}
			return creator;
		}
	}
}