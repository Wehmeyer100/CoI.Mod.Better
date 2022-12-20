using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CoI.Mod.Better.Custom.Data;
using CoI.Mod.Better.Shared;
using CoI.Mod.Better.Shared.Utilities;
using Mafi;
using Mafi.Base;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoI.Mod.Better.Custom
{
	public class Customs : IModData
	{
		private readonly List<CustomData> customsData = new List<CustomData>
		{
			new CustomData(),
		};
		
		public delegate List<string> EventLoadFiles();

		public event EventLoadFiles OnLoadFiles;

		public delegate List<object> EventLoadCustoms();

		public event EventLoadCustoms OnLoadCustoms;

		public void RegisterData(ProtoRegistrator registrator)
		{
			if (!BetterMod.Config.Systems.Customs) return;

			LoadFiles(registrator);
			ExternalCustoms(registrator);
			Build(registrator);

			Test(registrator);

			customsData.Clear();
		}

		private void LoadFiles(ProtoRegistrator registrator)
		{
			var foundedFiles = new List<(string, Type)>();

			LoadData<CustomData>("", ref foundedFiles, false);
			LoadData<StorageData>("Storages", ref foundedFiles);
			LoadData<ToolbarData>("Toolbars", ref foundedFiles);

			if(OnLoadFiles != null)
			{
				foreach (Delegate call in OnLoadFiles.GetInvocationList())
				{
					if (call == null) continue;

					List<string> results = (List<string>)call.DynamicInvoke(registrator);
					foreach (string file_path in results)
					{
						if (file_path == null || file_path.IsEmpty())
							continue;

						if (File.Exists(file_path))
						{
							string ext = Path.GetExtension(file_path);
							if (ext == Constants.JsonExt)
							{
								foundedFiles.Add((file_path, typeof(CustomData)));
							}
							else
							{
								BetterDebug.Info("Customs >> Loading file(file: " + file_path + ") >> Custom file by OnLoadFiles has the wrong extension! Must .json!");
							}
						}
						else
						{
							BetterDebug.Info("Customs >> Loading file(file: " + file_path + ") >> Custom file by OnLoadFiles cannot find!");
						}
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
						CustomData readData = (CustomData)JsonConvert.DeserializeObject(content, type, new JsonSerializerSettings
						{
							Formatting = Formatting.Indented,
							NullValueHandling = NullValueHandling.Ignore,
						});
						if (readData != null)
						{
							readData.FilePath = file_path;
							customsData.Add(readData);
						}
					}
					else
					{
						if (type == typeof(StorageData))
						{
							StorageData data = (StorageData)JsonConvert.DeserializeObject(content, type, new JsonSerializerSettings
							{
								Formatting = Formatting.Indented,
								NullValueHandling = NullValueHandling.Ignore,
							});
							customsData[0].Add(data);
						}
						else if (type == typeof(ToolbarData))
						{
							ToolbarData data = (ToolbarData)JsonConvert.DeserializeObject(content, type, new JsonSerializerSettings
							{
								Formatting = Formatting.Indented,
								NullValueHandling = NullValueHandling.Ignore,
							});
							customsData[0].Add(data);
						}
					}
					BetterDebug.Info("Customs >> Loading file(file: " + file_path + ", type: " + type.FullName + ") >> Custom has loaded. ");
				}
				catch (Exception e)
				{
					BetterDebug.Warning("Customs >> Loading file(file: " + file_path + ", type: " + type.FullName + ") >> Custom cannot reading! >> " + e);
				}
			}
		}

		private void ExternalCustoms(ProtoRegistrator registrator)
		{
			if(OnLoadCustoms == null) return;
			
			foreach (Delegate call in OnLoadCustoms.GetInvocationList())
			{
				List<object> result = (List<object>)call.DynamicInvoke(registrator);
				foreach (object data in result)
				{
					if (data == null) continue;

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

		private void LoadData<T>(string directory, ref List<(string, Type)> foundedFiles, bool AllDirectories = true) where T : class
		{
			string dir_path = Path.Combine(BetterMod.CustomsDirPath, directory);

			if (!Directory.Exists(dir_path))
			{
				return;
			}

			string[] allFiles = Directory.GetFiles(dir_path, "*" + Constants.JsonExt, AllDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
			foreach (string file_path in allFiles)
			{
				foundedFiles.Add((file_path, typeof(T)));
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
					BetterDebug.Info("Customs >> Building Data(file: " + data.FilePath + ") >> Custom cannot build! >> " + e);
				}
			}
		}

		public void Test(ProtoRegistrator registrator)
		{
			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				Formatting = Formatting.Indented,
				MaxDepth = 500,
				MissingMemberHandling = MissingMemberHandling.Ignore,
				NullValueHandling = NullValueHandling.Ignore,
				ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
			};
			settings.Converters.Add(new StringEnumConverter());

			CustomData testData = new CustomData();

			List<StorageProto> storageProtoResults = BetterMod.Config.Custom.LoadOnlyVanilla ? ProtoUtility.AllVanillaBuildings<StorageProto>(registrator, true) : ProtoUtility.AllProtos<StorageProto>(registrator, true);
			foreach (StorageProto storageProto in storageProtoResults)
			{
				StorageData storageData = new StorageData();
				storageData.From(storageProto);
				testData.Add(storageData);
			}

			List<ToolbarCategoryProto> results = BetterMod.Config.Custom.LoadOnlyVanilla ? ProtoUtility.AllVanillaToolbars(registrator, true) : ProtoUtility.AllProtos<ToolbarCategoryProto>(registrator, true);
			foreach (ToolbarCategoryProto storageProto in results)
			{
				ToolbarData toolbarData = new ToolbarData();
				toolbarData.From(storageProto);
				testData.Add(toolbarData);
			}


			//var result = JsonUtility.ToJson(storageData, true);
			string file_path = Constants.ModDirPath + "/testStorages.json";
			string result = JsonConvert.SerializeObject(testData, settings);
			if (File.Exists(file_path))
			{
				File.Delete(file_path);
			}
			File.WriteAllText(file_path, result, Encoding.UTF8);
		}
	}
}