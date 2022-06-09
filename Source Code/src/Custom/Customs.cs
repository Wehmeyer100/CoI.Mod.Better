using Mafi;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Buildings.Beacons;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace CoI.Mod.Better.Custom
{

    internal class Customs : IModData
    {
        private List<CustomData> customsData = new List<CustomData>();

        public void RegisterData(ProtoRegistrator registrator)
        {
            string customFolder = BetterMod.DOCUMENTS_ROOT_DIR_PATH + "/Mods/CoI.Mod.Better/Customs/";
            List<string> foundedFiles = new List<string>();

            if (Directory.Exists(customFolder))
            {
                string[] allFiles = Directory.GetFiles(customFolder);
                foreach (string file_path in allFiles)
                {
                    string ext = Path.GetExtension(file_path);
                    if (ext == ".json")
                    {
                        foundedFiles.Add(file_path);
                    }
                }
            }

            foreach (string file_path in foundedFiles)
            {
                try
                {
                    string content = File.ReadAllText(file_path);
                    CustomData readData = new CustomData();
                    JsonUtility.FromJsonOverwrite(content, readData);
                    readData.FilePath = file_path;
                    customsData.Add(readData);
                }
                catch (Exception e)
                {
                    Debug.Log("Customs >> Loading file(file: " + file_path + ") >> Custom cannot reading! >> " + e.ToString());
                    continue;
                }
            }

            foreach (CustomData data in customsData)
            {
                try
                {
                    data.Build(registrator);
                }
                catch (Exception e)
                {
                    Debug.Log("Customs >> Building Data(file: " + data.FilePath + ") >> Custom cannot build! >> " + e.ToString());
                    continue;
                }
            }
        }
    }
}
