using UnityEngine;

namespace CoI.Mod.Better
{
	public class MyDebug
	{
		public static void Warning(string message)
		{
			Debug.LogWarning("BetterMod(V: " + BetterMod.MyVersion + ") >> " + message);
		}
		
		public static void Info(string message)
		{
			Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> " + message);
		}
	}
}