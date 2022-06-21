using System;

namespace CoI.Mod.Better.Custom.Types
{
    [Serializable]
    public class TransferLimitData
    {
        public bool Unlimited;

        public int Count;
        public int Duration;
    }
}
