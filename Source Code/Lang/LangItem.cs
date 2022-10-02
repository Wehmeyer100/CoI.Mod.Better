using System;

namespace CoI.Mod.Better.lang
{
	public class LangItem
	{
		public string Key;
		public string Value;

		public LangItem()
		{
		}

		public LangItem(string key, string value)
		{
			Key = key ?? throw new ArgumentNullException(nameof(key));
			Value = value ?? throw new ArgumentNullException(nameof(value));
		}
	}
}