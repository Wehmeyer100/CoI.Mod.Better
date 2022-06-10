using Mafi.Core.Mods;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CoI.Mod.Better.Custom.Data
{
    [Serializable]
    public class CustomData
    {
        public List<ToolbarData> Toolbars = new List<ToolbarData>();
        public List<StorageData> Storages = new List<StorageData>();

        [NonSerialized]
        public string FilePath;

        public void Add(StorageData storageData)
        {
            Storages.Add(storageData);
        }

        public void Add(ToolbarData tooblarData)
        {
            Toolbars.Add(tooblarData);
        }

        public void Build(ProtoRegistrator registrator)
        {
            foreach (StorageData storageData in Storages)
            {
                storageData.Build(registrator);
            }
        }
    }
}
