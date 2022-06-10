using Mafi;
using Mafi.Base;
using Mafi.Core;
using Mafi.Core.Entities.Static;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace CoI.Mod.Better.Custom.Types
{

    [Serializable]
    public enum CostType : int
    {
        Workers = 0,
        CP = 1,
        CP2 = 2,
        CP3 = 3,
        CP4 = 4,
        Concrete = 5,
        Copper = 6,
        Iron = 7,
        Steel = 8,
        Glass = 9,
        Electronics = 10,
        Electronics2 = 11,
        MaintenanceT1 = 12,
        MaintenanceT2 = 13,
        MaintenanceT3 = 14,
        Product = 15
    }

    [Serializable]
    public class CostItemData
    {
        public string type;
        public int value;
        public string CustomProductID;
    }

    [Serializable]
    public class CostsData
    {
        public int defaultPriority = 9;
        public string CustomFrom;
        public List<CostItemData> Costs = new List<CostItemData>();

        public void From(string costTpl)
        {
            CustomFrom = costTpl;
        }

        public void From(EntityCosts costs)
        {
            defaultPriority = costs.DefaultPriority;

            if (costs.Workers > 0)
            {
                Costs.Add(new CostItemData() { type = FromCostType(CostType.Workers), value = costs.Workers });
            }

            if (costs.Maintenance.Product != null)
            {
                if (costs.Maintenance.Product.Id == Ids.Products.MaintenanceT1)
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.MaintenanceT1), value = costs.Maintenance.MaintenancePerMonth.Value.RawValue });
                }
                else if (costs.Maintenance.Product.Id == Ids.Products.MaintenanceT2)
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.MaintenanceT2), value = costs.Maintenance.MaintenancePerMonth.Value.RawValue });
                }
                else if (costs.Maintenance.Product.Id == Ids.Products.MaintenanceT3)
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.MaintenanceT3), value = costs.Maintenance.MaintenancePerMonth.Value.RawValue });
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
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.Concrete), value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.Copper)
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.Copper), value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.ConstructionParts)
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.CP), value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.ConstructionParts2)
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.CP2), value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.ConstructionParts3)
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.CP3), value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.ConstructionParts4)
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.CP4), value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.Electronics)
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.Electronics), value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.Electronics2)
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.Electronics2), value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.Glass)
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.Glass), value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.Iron)
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.Iron), value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.Steel)
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.Steel), value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.MaintenanceT1)
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.MaintenanceT1), value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.MaintenanceT2)
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.MaintenanceT2), value = data.Quantity.Value });
                }
                else if (data.Product.Id == Ids.Products.MaintenanceT3)
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.MaintenanceT3), value = data.Quantity.Value });
                }
                else
                {
                    Costs.Add(new CostItemData() { type = FromCostType(CostType.Product), CustomProductID = data.Product.Id.ToString(), value = data.Quantity.Value });
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
                    if (data.type == null || data.type.IsEmpty())
                    {
                        Debug.Log("CostsBuilder >> Into >> CustomFrom(" + CustomFrom + ") wasnt found, result is CP3=30 cost!");
                        continue;
                    }

                    AddCost(builder, data);
                }
            }
            return builder;
        }

        private static EntityCostsTpl.Builder AddCost(EntityCostsTpl.Builder builder, CostItemData data)
        {
            CostType costType = ToCostType(data.type);
            switch (costType)
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
                        Debug.Log("CostsBuilder >> AddCost >> CustomProductID is not set!");
                        return builder;
                    }
                    builder = builder.Product(data.value, new ProductProto.ID(data.CustomProductID));
                    break;
                default:
                    Debug.Log("CostsBuilder >> AddCost >> type not found => " + data.type);
                    break;
            }
            return builder;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static CostType ToCostType(string typeStr)
        {
            string typeString = typeStr.Trim().ToLower();
            CostType type = CostType.Product;

            switch (typeString)
            {
                case "workers":
                    type = CostType.Workers;
                    break;
                case "cp":
                    type = CostType.CP;
                    break;
                case "cp2":
                    type = CostType.CP2;
                    break;
                case "cp3":
                    type = CostType.CP3;
                    break;
                case "cp4":
                    type = CostType.CP4;
                    break;
                case "concrete":
                    type = CostType.Concrete;
                    break;
                case "copper":
                    type = CostType.Copper;
                    break;
                case "iron":
                    type = CostType.Iron;
                    break;
                case "steel":
                    type = CostType.Steel;
                    break;
                case "glass":
                    type = CostType.Glass;
                    break;
                case "electronics":
                    type = CostType.Electronics;
                    break;
                case "electronics2":
                    type = CostType.Electronics2;
                    break;
                case "maintenancet1":
                    type = CostType.MaintenanceT1;
                    break;
                case "maintenancet2":
                    type = CostType.MaintenanceT2;
                    break;
                case "maintenancet3":
                    type = CostType.MaintenanceT3;
                    break;
                case "product":
                    type = CostType.Product;
                    break;
                default:
                    Debug.Log("CostsBuilder >> ToCostType >> type not found => " + typeString);
                    break;
            }
            return type;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static string FromCostType(CostType type)
        {
            string typeStr;
            switch (type)
            {
                case CostType.Workers:
                    typeStr = "workers";
                    break;
                case CostType.CP:
                    typeStr = "cp";
                    break;
                case CostType.CP2:
                    typeStr = "cp2";
                    break;
                case CostType.CP3:
                    typeStr = "cp3";
                    break;
                case CostType.CP4:
                    typeStr = "cp4";
                    break;
                case CostType.Concrete:
                    typeStr = "concrete";
                    break;
                case CostType.Copper:
                    typeStr = "copper";
                    break;
                case CostType.Iron:
                    typeStr = "iron";
                    break;
                case CostType.Steel:
                    typeStr = "steel";
                    break;
                case CostType.Glass:
                    typeStr = "glass";
                    break;
                case CostType.Electronics:
                    typeStr = "electronics";
                    break;
                case CostType.Electronics2:
                    typeStr = "electronics2";
                    break;
                case CostType.MaintenanceT1:
                    typeStr = "maintenancet1";
                    break;
                case CostType.MaintenanceT2:
                    typeStr = "maintenancet2";
                    break;
                case CostType.MaintenanceT3:
                    typeStr = "maintenancet3";
                    break;
                case CostType.Product:
                    typeStr = "product";
                    break;
                default:
                    typeStr = "product";
                    Debug.Log("CostsBuilder >> FromCostType >> type not found => " + type);
                    break;
            }
            return typeStr;
        }
    }
}
