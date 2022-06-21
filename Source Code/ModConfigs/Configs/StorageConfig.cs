using System;

namespace CoI.Mod.Better.ModConfigs.Configs
{
    [Serializable]
    public class StorageConfig : ConfigBase
    {
        public bool OverrideVanilla = true;
        public bool UnlimitedTransferLimit = false;

        public int CapacityT1 = 540; // vanilla * 3
        public int TransferLimitT1Count = 2; // vanilla
        public int TransferLimitT1Duration = 5; // vanilla

        public int CapacityT2 = 1080; // vanilla * 3
        public int TransferLimitT2Count = 4; // vanilla
        public int TransferLimitT2Duration = 5; // vanilla

        public int CapacityT3 = 6480; // vanilla * 3
        public int TransferLimitT3Count = 8; // vanilla
        public int TransferLimitT3Duration = 5; // vanilla

        public int CapacityT4 = 12960; // vanilla * 3
        public int TransferLimitT4Count = 10; // vanilla
        public int TransferLimitT4Duration = 5; // vanilla

        public int FluidCapacityMultiplier = 3; // vanilla * 2

        public int CapacityNuclearWaste = 12000; // vanilla * 3
        public int NuclearWasteCapacityMultiplier = 2; // = Vanilla * 2
    }
}
