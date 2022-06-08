using System;

namespace CoI.Mod.Better
{
    [Serializable]
    public class ModConfigItem : IEquatable<ModConfigItem>
    {
        public string Key;
        public object Value;

        public bool Equals(ModConfigItem other)
        {
            return this.Key == other.Key;
        }

        public override bool Equals(object obj)
        {
            return obj is ModConfigItem && Equals(obj as ModConfigItem);
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode() + Value.GetHashCode();
        }

        public override string ToString()
        {
            return Key + "=" + Value.ToString();
        }
    }
}
