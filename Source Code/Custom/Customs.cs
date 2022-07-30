using CoI.Mod.Better.Custom.Data;
using CoI.Mod.Better.Extensions;
using CoI.Mod.Better.Utilities;
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
        public List<Func<ProtoRegistrator, List<object>>> OnLoadCustoms = new List<Func<ProtoRegistrator, List<object>>>();
        public List<Func<ProtoRegistrator, List<string>>> OnLoadFiles = new List<Func<ProtoRegistrator, List<string>>>();

        private List<CustomData> customsData = new List<CustomData>() { new CustomData() };

        public const string FILE_EXT = ".json";

        public void RegisterData(ProtoRegistrator registrator)
        {
            if (!BetterMod.Config.Systems.Customs || true) return;

            LoadFiles(registrator);
            ExternalCustoms(registrator);
            Build(registrator);

            Test(registrator);

            customsData.Clear();
        }

        private void LoadFiles(ProtoRegistrator registrator)
        {
            List<(string, Type)> foundedFiles = new List<(string, Type)>();

            LoadData<CustomData>("", ref foundedFiles, false);
            LoadData<StorageData>("Storages", ref foundedFiles, true);
            LoadData<ToolbarData>("Toolbars", ref foundedFiles, true);

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
                        if (ext == FILE_EXT)
                        {
                            foundedFiles.Add((file_path, typeof(CustomData)));
                        }
                        else
                        {
                            Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> Customs >> Loading file(file: " + file_path + ") >> Custom file by OnLoadFiles has the wrong extension! Must .json!");
                        }
                    }
                    else
                    {
                        Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> Customs >> Loading file(file: " + file_path + ") >> Custom file by OnLoadFiles cannot find!");
                    }
                }
            }



            foreach ((string file_path, Type type) in foundedFiles)
            {
                try
                {
                    string content = File.ReadAllText(file_path);
                    if (type == typeof(CustomData))
                    {
                        CustomData readData = (CustomData)JsonConvert.DeserializeObject(content, type, new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
                        readData.FilePath = file_path;
                        customsData.Add(readData);
                    }
                    else
                    {
                        if (type == typeof(StorageData))
                        {
                            StorageData data = (StorageData)JsonConvert.DeserializeObject(content, type, new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
                            customsData[0].Add(data);
                        }
                        else if (type == typeof(ToolbarData))
                        {
                            ToolbarData data = (ToolbarData)JsonConvert.DeserializeObject(content, type, new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
                            customsData[0].Add(data);
                        }
                    }
                    Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> Customs >> Loading file(file: " + file_path + ", type: " + type.FullName + ") >> Custom has loaded. ");
                }
                catch (Exception e)
                {
                    Debug.LogWarning("BetterMod(V: " + BetterMod.MyVersion + ") >> Customs >> Loading file(file: " + file_path + ", type: " + type.FullName + ") >> Custom cannot reading! >> " + e.ToString());
                    continue;
                }
            }
        }

        private void ExternalCustoms(ProtoRegistrator registrator)
        {
            foreach (Func<ProtoRegistrator, List<object>> call in OnLoadCustoms)
            {
                List<object> result = call?.Invoke(registrator);
                foreach (object data in result)
                {
                    if (data == null || data == default)
                        continue;

                    if (data is CustomData castData)
                    {
                        customsData.Add(castData);
                    }
                    else
                    {
                        if (data is StorageData castData1)
                        {
                            customsData[0].Add(castData1);
                        }
                        else if (data is ToolbarData castData2)
                        {
                            customsData[0].Add(castData2);
                        }
                    }
                }
            }
        }

        private void LoadData<T>(string directory, ref List<(string, Type)> foundedFiles, bool recusive) where T : class
        {
            string dir_path = Path.Combine(BetterMod.CustomsDirPath, directory);

            if (!Directory.Exists(dir_path))
            {
                return;
            }

            if (recusive)
            {
                foreach (string dir in Directory.GetDirectories(dir_path))
                {
                    LoadData<T>(dir, ref foundedFiles, recusive);
                }
            }

            string[] allFiles = Directory.GetFiles(dir_path);
            foreach (string file_path in allFiles)
            {
                string ext = Path.GetExtension(file_path);
                if (ext == FILE_EXT)
                {
                    foundedFiles.Add((file_path, typeof(T)));
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
                    Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> Customs >> Building Data(file: " + data.FilePath + ") >> Custom cannot build! >> " + e.ToString());
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
            string file_path = BetterMod.ModDirPath + "/testStorages.json";
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
            IEnumerable<FieldInfo> result = ReflectionUtility.GetAllFields(typeof(Ids.Buildings));

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
                        Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> Customs >> AllVanillaBuildings<" + typeof(Prototype) + "> >> name: " + resultProduct.Value.Strings.Name.ToString() + " | id: " + resultProduct.Value.Id);
                    }
                }
            }
            return results;
        }


        private List<ToolbarCategoryProto> AllVanillaToolbars(ProtoRegistrator registrator)
        {
            List<ToolbarCategoryProto> results = new List<ToolbarCategoryProto>();
            IEnumerable<FieldInfo> result = ReflectionUtility.GetAllFields(typeof(Ids.ToolbarCategories));

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
                        Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> Customs >> AllVanillaToolbars<" + typeof(ToolbarCategoryProto) + "> >> name: " + resultProduct.Value.Strings.Name.ToString() + " | id: " + resultProduct.Value.Id);
                    }
                }
            }
            return results;
        }
    }
}
