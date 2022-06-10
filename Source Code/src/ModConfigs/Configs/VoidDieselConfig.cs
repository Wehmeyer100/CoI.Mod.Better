using System;

namespace CoI.Mod.Better.ModConfigs.Configs
{
    [Serializable]
    public class VoidDieselConfig : ConfigBase
    {
        public int EnergyInputType = 1; // 1 = Diesel, 2 = Water, 3 = Rohöl

        public int EnergyT1ProduceInKw = 10000;
        public int EnergyT1BufferCapactiy = 540;

        public int EnergyT2ProduceInKw = 50000;
        public int EnergyT2BufferCapactiy = 540 * 2;

        public int EnergyT3ProduceInKw = 100000;
        public int EnergyT3BufferCapactiy = 540 * 3;

        public int EnergyT4ProduceInKw = 200000;
        public int EnergyT4BufferCapactiy = 540 * 4;

        public int EnergyT5ProduceInKw = 1000000;
        public int EnergyT5BufferCapactiy = 540 * 5;
    }
}
