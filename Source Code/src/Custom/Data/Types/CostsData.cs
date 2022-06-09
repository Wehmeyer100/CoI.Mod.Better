using Mafi;
using Mafi.Base;
using Mafi.Core;
using Mafi.Core.Entities.Static;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace CoI.Mod.Better.Custom
{

    [Serializable]
    public enum CostType : int
    {
        Workers,
        CP,
        CP2,
        CP3,
        CP4,
        Concrete,
        Copper,
        Iron,
        Steel,
        Glass,
        Electronics,
        Electronics2,
        MaintenanceT1,
        MaintenanceT2,
        MaintenanceT3,
        Product
    }

    [Serializable]
    public class CostItemData
    {
        public CostType type;
        public int value;

        public string CustomProductID;
    }

    [Serializable]
    public class CostsData
    {
        public int defaultPriority = 9;
        public string CustomFrom;
        public List<CostItemData> Costs;

        public void From(string costTpl)
        {
            CustomFrom = costTpl;
        }

        public void From(EntityCosts costs)
        {
            defaultPriority = costs.DefaultPriority;

            Costs = new List<CostItemData>();
            if (costs.Workers > 0)
            {
                Costs.Add(new CostItemData() { type = CostType.Workers, value = costs.Workers });
            }
            if (costs.Maintenance.Product != null)
            {
                if (costs.Maintenance.Product.Id == Ids.Products.MaintenanceT1)
                {
                    Costs.Add(new CostItemData() { type = CostType.MaintenanceT1, value = costs.Maintenance.MaintenancePerMonth.Value.RawValue });
                }
                else if (costs.Maintenance.Product.Id == Ids.Products.MaintenanceT2)
                {
                    Costs.Add(new CostItemData() { type = CostType.MaintenanceT2, value = costs.Maintenance.MaintenancePerMonth.Value.RawValue });
                }
                else if (costs.Maintenance.Product.Id == Ids.Products.MaintenanceT3)
                {
                    Costs.Add(new CostItemData() { type = CostType.MaintenanceT3, value = costs.Maintenance.MaintenancePerMonth.Value.RawValue });
                }
                else if (costs.Maintenance.Product.Id != VirtualProductProto.Phantom.Id)
                {
                    Debug.Log("CostsBuilder >> From >> Maintenance hasnt found!");
                }
            }

            foreach (ProductQuantity data in costs.Price.Products)
            {
                if (data.Product.Id == Ids.Products.ConcreteSlab)
                {
                    Costs.Add(new CostItemData() { type = CostType.Concrete, value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.Copper)
                {
                    Costs.Add(new CostItemData() { type = CostType.Copper, value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.ConstructionParts)
                {
                    Costs.Add(new CostItemData() { type = CostType.CP, value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.ConstructionParts2)
                {
                    Costs.Add(new CostItemData() { type = CostType.CP2, value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.ConstructionParts3)
                {
                    Costs.Add(new CostItemData() { type = CostType.CP3, value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.ConstructionParts4)
                {
                    Costs.Add(new CostItemData() { type = CostType.CP4, value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.Electronics)
                {
                    Costs.Add(new CostItemData() { type = CostType.Electronics, value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.Electronics2)
                {
                    Costs.Add(new CostItemData() { type = CostType.Electronics2, value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.Glass)
                {
                    Costs.Add(new CostItemData() { type = CostType.Glass, value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.Iron)
                {
                    Costs.Add(new CostItemData() { type = CostType.Iron, value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.Steel)
                {
                    Costs.Add(new CostItemData() { type = CostType.Steel, value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.MaintenanceT1)
                {
                    Costs.Add(new CostItemData() { type = CostType.MaintenanceT1, value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.MaintenanceT2)
                {
                    Costs.Add(new CostItemData() { type = CostType.MaintenanceT2, value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.MaintenanceT3)
                {
                    Costs.Add(new CostItemData() { type = CostType.MaintenanceT3, value = data.Quantity.Value });
                }
                else
                {
                    Costs.Add(new CostItemData() { type = CostType.Product, CustomProductID = data.Product.Id.ToString(), value = data.Quantity.Value });
                }
            }
        }

        public EntityCostsTpl Into()
        {
            EntityCostsTpl.Builder builder = Mafi.Base.Costs.Build;
            builder.Priority(defaultPriority);

            if (CustomFrom != null && !CustomFrom.Trim().IsEmpty())
            {
                CustomFrom = CustomFrom.Trim();
                IEnumerable<PropertyInfo> result = BetterMod.GetAllProperty(typeof(Mafi.Base.Costs));
                foreach (PropertyInfo data in result)
                {
                    if (data.Name == CustomFrom)
                    {
                        return (EntityCostsTpl)data.GetValue(null);
                    }
                }
                Debug.Log("CostsBuilder >> Into >> CustomFrom(" + CustomFrom + ") wasnt found, result is CP3=30 cost!");
                builder = builder.CP3(30);
            }
            else
            {
                foreach (CostItemData data in Costs)
                {
                    switch (data.type)
                    {
                        case CostType.Workers:
                            builder = builder.Workers(data.value);
                            break;
                        case CostType.CP:
                            builder = builder.CP(data.value);
                            break;
                        case CostType.CP2:
                            builder = builder.CP2(data.value);
                            break;
                        case CostType.CP3:
                            builder = builder.CP3(data.value);
                            break;
                        case CostType.CP4:
                            builder = builder.CP4(data.value);
                            break;
                        case CostType.Concrete:
                            builder = builder.Concrete(data.value);
                            break;
                        case CostType.Copper:
                            builder = builder.Copper(data.value);
                            break;
                        case CostType.Iron:
                            builder = builder.Iron(data.value);
                            break;
                        case CostType.Steel:
                            builder = builder.Steel(data.value);
                            break;
                        case CostType.Glass:
                            builder = builder.Glass(data.value);
                            break;
                        case CostType.Electronics:
                            builder = builder.Electronics(data.value);
                            break;
                        case CostType.Electronics2:
                            builder = builder.Electronics2(data.value);
                            break;
                        case CostType.MaintenanceT1:
                            builder = builder.MaintenanceT1(data.value);
                            break;
                        case CostType.MaintenanceT2:
                            builder = builder.MaintenanceT2(data.value);
                            break;
                        case CostType.MaintenanceT3:
                            builder = builder.MaintenanceT3(data.value);
                            break;
                        case CostType.Product:
                            if (data.CustomProductID == null || data.CustomProductID.IsEmpty())
                            {
                                Debug.Log("CostsBuilder >> Product >> CustomProductID is not set!");
                                continue;
                            }
                            builder = builder.Product(data.value, new ProductProto.ID(data.CustomProductID));
                            break;
                        default:
                            continue;
                    }
                }
            }
            return builder;
        }
    }
}
