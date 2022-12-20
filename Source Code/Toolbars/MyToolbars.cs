using System;
using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared;
using CoI.Mod.Better.Shared.Utilities;
using Mafi.Core.Mods;
using UnityEngine;

namespace CoI.Mod.Better.Toolbars
{
	public class MyToolbars : IModData
	{
		public void RegisterData(ProtoRegistrator registrator)
		{
			if ((BetterMod.Config.Systems.DieselGeneators || BetterMod.Config.Systems.PowerGeneators) && BetterMod.Config.Systems.Cheats)
			{
				ToolbarUtility.GenerateToolbar(registrator, ToolbarCategories.MachinesElectricity, "Better mod: Electricity", "Assets/Unity/UserInterface/Toolbar/Power.svg", 31);
			}

			if (BetterMod.Config.Systems.BigStorage && BetterMod.Config.Storage.OverrideVanilla)
			{
				ToolbarUtility.GenerateToolbar(registrator, ToolbarCategories.Storages, "Better mod: Storages", "Assets/Unity/UserInterface/Toolbar/Storages.svg", 211);
			}

			if (BetterMod.Config.Systems.VoidCrusher || BetterMod.Config.Systems.VoidProducer)
			{
				ToolbarUtility.GenerateToolbar(registrator, ToolbarCategories.MachinesMetallurgy, "Better mod: Crusher/Producer", "Assets/Unity/UserInterface/Toolbar/Metallurgy.svg", 21);
			}
		}
	}
}