using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mafi;
using Mafi.Localization;
using Newtonsoft.Json;
using UnityEngine;

namespace CoI.Mod.Better.lang
{
    public class LangManager
    {

        public readonly string CurrentLang = "en-US";

        private Dictionary<string, string> langData = new Dictionary<string, string>();

        public static LangManager Instance { get { return lazy.Value; } }

        private static readonly System.Lazy<LangManager> lazy = new System.Lazy<LangManager>(() => new LangManager());


        public LangManager()
        {
            Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> LangManager >> Init");
            CurrentLang = LocalizationManager.CurrentLangInfo.CultureInfoId;
            if (!Directory.Exists(Path.Combine(BetterMod.LANG_DIR_PATH, CurrentLang)))
            {
                CurrentLang = "en-US";
            }

            Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> LangManager >> Current Lang >> " + CurrentLang);
        }

        public string Get(string key, params string[] replace)
        {
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
            if (!langData.ContainsKey(key))
            {
                Debug.LogWarning("BetterMod(V: " + BetterMod.MyVersion + ") >> LangManager >> Get(key: " + key + ") >> Key not found! >> ");
                return key;
            }
            return langData[key];
        }

        public void Load()
        {
            List<string> foundedFiles = new List<string>();
            LoadData(CurrentLang, ref foundedFiles);

            foreach (string file_path in foundedFiles)
            {
                try
                {
                    string content = File.ReadAllText(file_path);
                    List<LangItem> readData = JsonConvert.DeserializeObject<List<LangItem>>(content, new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });

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
                    Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> LangManager >> Loaded file >> " + file_path);
                }
                catch (Exception e)
                {
                    Debug.LogWarning("BetterMod(V: " + BetterMod.MyVersion + ") >> LangManager >> Loading file(file: " + file_path + ") >> Lang cannot reading! >> " + e.ToString());
                }
            }
        }

        private void LoadData(string directory, ref List<string> foundedFiles, bool recusive = true)
        {
            string dir_path = Path.Combine(BetterMod.LANG_DIR_PATH, directory);

            if (!Directory.Exists(dir_path))
            {
                return;
            }

            if (recusive)
            {
                foreach (string dir in Directory.GetDirectories(dir_path))
                {
                    LoadData(dir.Replace(dir_path + (dir_path.EndsWith("/") ? "" : "/"), ""), ref foundedFiles, recusive);
                }
            }

            string[] allFiles = Directory.GetFiles(dir_path);
            foreach (string file_path in allFiles)
            {
                string ext = Path.GetExtension(file_path);
                if (ext == BetterMod.JSON_EXT)
                {
                    foundedFiles.Add(file_path);
                }
            }
        }
    }
}
