using System;
using Mafi.Core.Factory;

namespace CoI.Mod.Better.Custom.Types
{
	[Serializable]
	public class LoosePileTextureParamsData
	{
		public float scale;
		public float offsetX;
		public float offsetY;

		public LoosePileTextureParams Into()
		{
			return new LoosePileTextureParams(scale, offsetX, offsetY);
		}
	}
}