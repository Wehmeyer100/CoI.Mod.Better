using Mafi.Core.Mods;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CoI.Mod.Better.Custom
{
    [Serializable]
    public class CustomData
    {
        public List<TooblarData> Toolbars = new List<TooblarData>();
        public List<StorageData> Storages = new List<StorageData>();

        [NonSerialized]
        public string FilePath;

        public void Add(StorageData storageData)
        {
            Storages.Add(storageData);
        }

        public void Add(TooblarData tooblarData)
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
