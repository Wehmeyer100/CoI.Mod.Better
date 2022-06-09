using Mafi.Core.Factory;
using System;

namespace CoI.Mod.Better.Custom
{
    [Serializable]
    public class FluidIndicatorGfxParamsData
    {
        public string indicatorObjectPath;

        public float sizePerTextureWidthMeters;
        public float detailsScale;
        public float stillMovementScale;

        public FluidIndicatorGfxParams Into()
        {
            return new FluidIndicatorGfxParams(sizePerTextureWidthMeters, detailsScale, stillMovementScale);
        }
    }
}
