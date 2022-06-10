﻿using CoI.Mod.Better.ModConfigs.Configs;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CoI.Mod.Better.ModConfigs
{


    [Serializable]
    public class ModConfigV2
    {
        public DefaultConfig Default = new DefaultConfig();
        public EnableConfig Systems = new EnableConfig();
        public BeaconConfig Beacon = new BeaconConfig();
        public TowerConfig Tower = new TowerConfig();
        public StorageConfig Storage = new StorageConfig();

        public VehicleEdictsConfig VehicleEdicts = new VehicleEdictsConfig();
        public GenerellEdictsConfig GenerellEdicts = new GenerellEdictsConfig();

        public VoidDestroyConfig VoidDestroy = new VoidDestroyConfig();
        public VoidProducerConfig VoidProducer = new VoidProducerConfig();

        public VoidDieselConfig VoidDiesel = new VoidDieselConfig();
        public VoidPowerConfig VoidPower = new VoidPowerConfig();

        public ModConfigV2()
        {
        }

        public void Print()
        {
            Debug.Log("BetterMod(V: " + BetterMod.MyVersion + "): read ConfigV2 data");
            foreach (FieldInfo field in BetterMod.GetAllFields(typeof(ModConfigV2)))
            {
                object result = field.GetValue(this);
                if (result is IConfigBase)
                {
                    ((IConfigBase)result).Print(result);
                }
                else
                {
                    Debug.Log(" - " + field.Name + ": " + field.GetValue(this));
                }
            }
        }
    }
}