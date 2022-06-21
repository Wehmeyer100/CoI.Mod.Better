using Mafi.Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CoI.Mod.Better.ModConfigs.Configs
{
    public class ConfigBase : IConfigBase
    {
        public void Print(object sender)
        {
            Debug.Log("- Config: " + sender.GetType().Name);
            foreach (FieldInfo field in BetterMod.GetAllFields(sender.GetType()))
            {
                object result = field.GetValue(sender);
                if (result is IConfigBase)
                {
                    ((IConfigBase)result).Print(result);
                }
                else
                {
                    Debug.Log(" - " + field.Name + ": " + result);
                }
            }
        }
    }
}
