using System;
using System.Collections.Generic;
using System.IO;
using CoI.Mod.Better.Shared;
using Mafi.Localization;
using Newtonsoft.Json;

namespace CoI.Mod.Better.lang
{
	public class LangManager
	{
		private static readonly Lazy<LangManager> lazy        = new Lazy<LangManager>(() => new LangManager());
		public readonly         string            CurrentLang = "en-US";

		private readonly Dictionary<string, string> langData = new Dictionary<string, string>();


		public LangManager()
		{
			BetterDebug.Info("LangManager >> Init");
			CurrentLang = LocalizationManager.CurrentLangInfo.CultureInfoId;
			if (!Directory.Exists(Path.Combine(BetterMod.LangDirPath, CurrentLang)))
			{
				CurrentLang = "en-US";
			}

			BetterDebug.Info("LangManager >> Current Lang >> " + CurrentLang);
		}

		public static LangManager Instance => lazy.Value;

		public string Get(string key, params string[] replace)
		{
			key = key.ToLower().Trim();
			string txt = GetRaw(key);
			if (replace.Length > 0)
			{
				for (int isr = 0; isr < replace.Length; isr++)
				{
					txt = txt.Replace("{" + isr + "}", replace[isr]);
				}
			}
			return txt;
		}

		public string GetRaw(string key)
		{
			key = key.ToLower().Trim();
			if (!langData.ContainsKey(key))
			{
				BetterDebug.Warning("LangManager >> Get(key: " + key + ") >> Key not found! >> ");
				return key;
			}
			return langData[key];
		}

		public void Load()
		{
			string dirPath = Path.Combine(BetterMod.LangDirPath, CurrentLang);
			string[] foundFiles = Directory.GetFiles(dirPath, "*" + Constants.JsonExt, SearchOption.AllDirectories);

			foreach (string file_path in foundFiles)
			{
				try
				{
					string content = File.ReadAllText(file_path);
					List<LangItem> readData = JsonConvert.DeserializeObject<List<LangItem>>(content, new JsonSerializerSettings
					{
						Formatting = Formatting.Indented,
						NullValueHandling = NullValueHandling.Ignore,
                    });

					foreach (LangItem data in readData)
					{
						if (langData.ContainsKey(data.Key))
						{
							langData[data.Key] = data.Value;
						}
						else
						{
							langData.Add(data.Key, data.Value);
						}
					}
					BetterDebug.Info("LangManager >> Loaded file >> " + file_path);
				}
				catch (Exception e)
				{
					BetterDebug.Warning("LangManager >> Loading file(file: " + file_path + ") >> Lang cannot reading! >> " + e);
				}
			}
		}
	}
}