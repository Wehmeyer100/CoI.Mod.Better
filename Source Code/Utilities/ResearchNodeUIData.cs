using Mafi.Core.Research;
using System;

namespace CoI.Mod.Better.Utilities
{
    public static partial class ResearchProtoUtility
    {
        public class ResearchNodeUIData 
        {
            public ResearchNodeProto parent;
            public bool parent_only_for_grid = false;
            public int ui_stepSize_x = BetterMod.UIStepSize;
            public int ui_stepSize_y = 0;

            public ResearchNodeUIData(ResearchNodeProto parent)
            {
                this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
            }

            public ResearchNodeUIData(ResearchNodeProto parent, bool parent_only_for_grid)
            {
                this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
                this.parent_only_for_grid = parent_only_for_grid;
            }

            public ResearchNodeUIData(ResearchNodeProto parent, int ui_stepSize_x, int ui_stepSize_y)
            {
                this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
                this.ui_stepSize_x = ui_stepSize_x;
                this.ui_stepSize_y = ui_stepSize_y;
            }

            public ResearchNodeUIData(ResearchNodeProto parent, bool parent_only_for_grid, int ui_stepSize_x, int ui_stepSize_y)
            {
                this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
                this.parent_only_for_grid = parent_only_for_grid;
                this.ui_stepSize_x = ui_stepSize_x;
                this.ui_stepSize_y = ui_stepSize_y;
            }
        }
    }
}
