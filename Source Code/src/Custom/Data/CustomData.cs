using Mafi.Core.Mods;
using System;
using System.Collections.Generic;

namespace CoI.Mod.Better.Custom
{
    [Serializable]
    public class CustomData
    {
        [NonSerialized]
        public string FilePath;
        public List<StorageData> Storages = new List<StorageData>();

        public void Build(ProtoRegistrator registrator)
        {
            foreach (StorageData storageData in Storages)
            {
                storageData.Build(registrator);
            }
        }
    }
}
