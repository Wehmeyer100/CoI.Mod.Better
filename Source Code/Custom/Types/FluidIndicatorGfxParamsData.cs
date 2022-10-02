using System;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Factory;

namespace CoI.Mod.Better.Custom.Types
{
	[Serializable]
	public class FluidIndicatorGfxParamsData
	{
		public string indicatorObjectPath;

		public float sizePerTextureWidthMeters;
		public float detailsScale;
		public float stillMovementScale;

		public void From(FluidStorageProto.Gfx gfx)
		{
			indicatorObjectPath = gfx.FluidIndicatorObjectPath;
			detailsScale = gfx.FluidIndicatorParams.DetailsScale;
			sizePerTextureWidthMeters = gfx.FluidIndicatorParams.SizePerTextureWidthMeters;
			stillMovementScale = gfx.FluidIndicatorParams.StillMovementScale;
		}

		public FluidIndicatorGfxParams Into()
		{
			return new FluidIndicatorGfxParams(sizePerTextureWidthMeters, detailsScale, stillMovementScale);
		}
	}
}