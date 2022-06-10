using Mafi;
using Mafi.Base;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Prototypes;
using System;

namespace CoI.Mod.Better.Custom.Types
{
    [Serializable]
    public enum CategoryToolbar : int
    {
        None = 0,
        Transports = 1,
        Machines = 2,
        MachinesWater = 3,
        MachinesFood = 4,
        MachinesMetallurgy = 5,
        MachinesOil = 6,
        MachinesElectricity = 7,
        Waste = 8,
        Storages = 9,
        Buildings = 10,
        BuildingsForVehicles = 11,
        Housing = 12,
        Docks = 13,
        Landmarks = 14,
        Custom = 15
    }


    [Serializable]
    public class CategoryToolbarData
    {
        public CategoryToolbar Category;
        public string CustomCategory;

        public void From(LayoutEntityProto.Gfx gfx)
        {
            if (gfx.Categories == null || gfx.Categories.Length == 0)
            {
                Category = CategoryToolbar.None;
            }
            else
            {
                Category = Utils.GetCategory(gfx.Categories.First.Id);
            }
        }

        public void From(string customCategory)
        {
            CustomCategory = customCategory;
            Category = CategoryToolbar.Custom;
        }

        public Proto.ID Into() 
        {
            switch (Category)
            {
                case CategoryToolbar.Transports:
                    return Ids.ToolbarCategories.Transports;
                case CategoryToolbar.Machines:
                    return Ids.ToolbarCategories.Machines;
                case CategoryToolbar.MachinesWater:
                    return Ids.ToolbarCategories.MachinesWater;
                case CategoryToolbar.MachinesFood:
                    return Ids.ToolbarCategories.MachinesFood;
                case CategoryToolbar.MachinesMetallurgy:
                    return Ids.ToolbarCategories.MachinesMetallurgy;
                case CategoryToolbar.MachinesOil:
                    return Ids.ToolbarCategories.MachinesOil;
                case CategoryToolbar.MachinesElectricity:
                    return Ids.ToolbarCategories.MachinesElectricity;
                case CategoryToolbar.Waste:
                    return Ids.ToolbarCategories.Waste;
                case CategoryToolbar.Storages:
                    return Ids.ToolbarCategories.Storages;
                case CategoryToolbar.Buildings:
                    return Ids.ToolbarCategories.Buildings;
                case CategoryToolbar.BuildingsForVehicles:
                    return Ids.ToolbarCategories.BuildingsForVehicles;
                case CategoryToolbar.Housing:
                    return Ids.ToolbarCategories.Housing;
                case CategoryToolbar.Docks:
                    return Ids.ToolbarCategories.Docks;
                case CategoryToolbar.Landmarks:
                    return Ids.ToolbarCategories.Landmarks;
                case CategoryToolbar.Custom:
                    return new Proto.ID(CustomCategory);
            }
            return default;
        }
    }
}
