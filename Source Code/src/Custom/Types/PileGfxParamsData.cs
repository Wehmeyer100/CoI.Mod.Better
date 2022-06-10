using Mafi.Core.Buildings.Storages;
using System;

namespace CoI.Mod.Better.Custom.Types
{
    [Serializable]
    public class PileGfxParamsData
    {
        public string smoothPileObjectPath;
        public string roughPileObjectPath;
        public LoosePileTextureParamsData pileTextureParams;

        public void From(LooseStorageProto.Gfx gfx) 
        {
            smoothPileObjectPath = gfx.SmoothPileObjectPath;
            roughPileObjectPath = gfx.RoughPileObjectPath;
            pileTextureParams = new LoosePileTextureParamsData()
            {
                scale = gfx.PileTextureParams.Scale,
                offsetX = gfx.PileTextureParams.OffsetX,
                offsetY = gfx.PileTextureParams.OffsetY
            };
        }
    }
}
