using Mafi;
using Mafi.Base;
using Mafi.Core;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Mods;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CoI.Mod.Better.Custom
{

    public class Customs : IModData
    {
        public static readonly string customFolder = BetterMod.DOCUMENTS_ROOT_DIR_PATH + "/Mods/CoI.Mod.Better/Customs/";
        public List<Func<ProtoRegistrator, List<CustomData>>> OnLoadCustoms = new List<Func<ProtoRegistrator, List<CustomData>>>();
        public List<Func<ProtoRegistrator, List<string>>> OnLoadFiles = new List<Func<ProtoRegistrator, List<string>>>();

        private List<CustomData> customsData = new List<CustomData>();

        public void RegisterData(ProtoRegistrator registrator)
        {
            if (BetterMod.Config.DisableCustoms) return;

            LoadFiles(registrator);
            ExternalCustoms(registrator);
            Build(registrator);

            Test(registrator);
        }

        private void LoadFiles(ProtoRegistrator registrator)
        {
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


            foreach (Func<ProtoRegistrator, List<string>> call in OnLoadFiles)
            {
                List<string> results = call?.Invoke(registrator);
                foreach (string file_path in results)
                {
                    if (file_path == null || file_path.IsEmpty())
                        continue;

                    if (File.Exists(file_path))
                    {
                        string ext = Path.GetExtension(file_path);
                        if (ext == ".json")
                        {
                            foundedFiles.Add(file_path);
                        }
                        else
                        {
                            Debug.Log("Customs >> Loading file(file: " + file_path + ") >> Custom file by OnLoadFiles has the wrong extension! Must .json!");
                        }
                    }
                    else
                    {
                        Debug.Log("Customs >> Loading file(file: " + file_path + ") >> Custom file by OnLoadFiles cannot find!");
                    }
                }
            }



            foreach (string file_path in foundedFiles)
            {
                try
                {
                    string content = File.ReadAllText(file_path);
                    CustomData readData = JsonConvert.DeserializeObject<CustomData>(content, new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
                    readData.FilePath = file_path;
                    customsData.Add(readData);
                }
                catch (Exception e)
                {
                    Debug.Log("Customs >> Loading file(file: " + file_path + ") >> Custom cannot reading! >> " + e.ToString());
                    continue;
                }
            }
        }

        private void ExternalCustoms(ProtoRegistrator registrator)
        {
            foreach (Func<ProtoRegistrator, List<CustomData>> call in OnLoadCustoms)
            {
                List<CustomData> result = call?.Invoke(registrator);
                foreach (CustomData data in result)
                {
                    if (data == null || data == default)
                        continue;

                    customsData.Add(data);
                }
            }
        }

        private void Build(ProtoRegistrator registrator)
        {
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

        public void Test(ProtoRegistrator registrator)
        {
            CustomData testData = new CustomData();
            StorageData storageData = new StorageData();

            StorageProto storagesTest = registrator.PrototypesDb.GetOrThrow<StorageProto>(Ids.Buildings.StorageUnit);
            storageData.From(storagesTest);
            testData.Add(storageData);


            JsonSerializerSettings settings = new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore };
            settings.Converters.Add(new StringEnumConverter());

            //var result = JsonUtility.ToJson(storageData, true);
            var result = JsonConvert.SerializeObject(testData, settings);
            File.WriteAllText(BetterMod.MOD_ROOT_DIR_PATH + "/serializeObject.json", result);


            string content = File.ReadAllText(BetterMod.MOD_ROOT_DIR_PATH + "/serializeObject.json");
            CustomData readData = JsonConvert.DeserializeObject<CustomData>(content, settings);


            result = JsonConvert.SerializeObject(readData, settings);
            File.WriteAllText(BetterMod.MOD_ROOT_DIR_PATH + "/deserializeObject.json", result);
        }
    }
}
