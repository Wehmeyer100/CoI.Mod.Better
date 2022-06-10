using CoI.Mod.Better.Custom.Data;
using Mafi;
using Mafi.Base;
using Mafi.Core;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory.Recipes;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
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
            if (!BetterMod.Config.Systems.Customs || true) return;

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
            JsonSerializerSettings settings = new JsonSerializerSettings() { Formatting = Formatting.Indented, MaxDepth = 500, MissingMemberHandling = MissingMemberHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Serialize };
            settings.Converters.Add(new StringEnumConverter());

            CustomData testData = new CustomData();

            List<StorageProto> storageProtoResults = AllVanillaBuildings<StorageProto>(registrator);

            foreach (StorageProto storageProto in storageProtoResults)
            {
                StorageData storageData = new StorageData();
                storageData.From(registrator, storageProto);
                testData.Add(storageData);
            }

            List<ToolbarCategoryProto> results = AllVanillaToolbars(registrator);
            foreach (ToolbarCategoryProto storageProto in results)
            {
                ToolbarData toolbarData = new ToolbarData();
                toolbarData.From(storageProto);
                testData.Add(toolbarData);
            }


            //var result = JsonUtility.ToJson(storageData, true);
            string file_path = BetterMod.MOD_ROOT_DIR_PATH + "/testStorages.json";
            var result = JsonConvert.SerializeObject(testData, settings);
            if (File.Exists(file_path)) 
            {
                File.Delete(file_path);
            }
            File.WriteAllText(file_path, result, Encoding.UTF8);
        }


        private List<Prototype> AllVanillaBuildings<Prototype>(ProtoRegistrator registrator) where Prototype : Proto
        {
            List<Prototype> results = new List<Prototype>();
            IEnumerable<FieldInfo> result = BetterMod.GetAllFields(typeof(Ids.Buildings));

            foreach (FieldInfo field in result)
            {
                string fieldName = field.Name;
                object value = field.GetValue(null);
                if (field.IsStatic && value != null && value is StaticEntityProto.ID)
                {
                    StaticEntityProto.ID fieldValueProtoID = (StaticEntityProto.ID)value;
                    Option<Prototype> resultProduct = registrator.PrototypesDb.Get<Prototype>(fieldValueProtoID);

                    if (resultProduct.HasValue)
                    {
                        results.Add(resultProduct.Value);
                        Debug.Log("Customs >> AllVanillaBuildings<" + typeof(Prototype) + "> >> name: " + resultProduct.Value.Strings.Name.ToString() + " | id: " + resultProduct.Value.Id);
                    }
                }
            }
            return results;
        }


        private List<ToolbarCategoryProto> AllVanillaToolbars(ProtoRegistrator registrator) 
        {
            List<ToolbarCategoryProto> results = new List<ToolbarCategoryProto>();
            IEnumerable<FieldInfo> result = BetterMod.GetAllFields(typeof(Ids.ToolbarCategories));

            foreach (FieldInfo field in result)
            {
                string fieldName = field.Name;
                object value = field.GetValue(null);
                if (field.IsStatic && value != null && value is Proto.ID)
                {
                    Proto.ID fieldValueProtoID = (Proto.ID)value;
                    Option<ToolbarCategoryProto> resultProduct = registrator.PrototypesDb.Get<ToolbarCategoryProto>(fieldValueProtoID);

                    if (resultProduct.HasValue)
                    {
                        results.Add(resultProduct.Value);
                        Debug.Log("Customs >> AllVanillaToolbars<" + typeof(ToolbarCategoryProto) + "> >> name: " + resultProduct.Value.Strings.Name.ToString() + " | id: " + resultProduct.Value.Id);
                    }
                }
            }
            return results;
        }
    }
}
