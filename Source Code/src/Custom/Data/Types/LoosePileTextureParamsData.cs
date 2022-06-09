using Mafi.Core.Factory;
using System;

namespace CoI.Mod.Better.Custom
{
    [Serializable]
    public class LoosePileTextureParamsData
    {
        public float scale;
        public float offsetX = 0;
        public float offsetY = 0;

        public LoosePileTextureParams Into()
        {
            return new LoosePileTextureParams(scale, offsetX, offsetY);
        }
    }
}
