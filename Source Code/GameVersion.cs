using System;
using System.Collections.Generic;

namespace CoI.Mod.Better
{
    public class GameVersion : IEquatable<GameVersion>
    {
        public string NAME;
        public string MAJOR_VERSION;
        public string MINOR_VERSION;
        public string REVISION_VERSION;
        public string HOTFIX_NAME;

        public GameVersion()
        {
            this.NAME = Mafi.GameVersion.NAME;
            this.MAJOR_VERSION = Mafi.GameVersion.MAJOR_VERSION;
            this.MINOR_VERSION = Mafi.GameVersion.MINOR_VERSION;
            this.REVISION_VERSION = Mafi.GameVersion.REVISION_VERSION;
            this.HOTFIX_NAME = Mafi.GameVersion.HOTFIX_NAME;
        }

        public GameVersion(string Name, string MAJOR_VERSION, string MINOR_VERSION, string REVISION_VERSION, string HOTFIX_NAME)
        {
            this.NAME = Name ?? throw new ArgumentNullException(nameof(Name));
            this.MAJOR_VERSION = MAJOR_VERSION ?? throw new ArgumentNullException(nameof(MAJOR_VERSION));
            this.MINOR_VERSION = MINOR_VERSION ?? throw new ArgumentNullException(nameof(MINOR_VERSION));
            this.REVISION_VERSION = REVISION_VERSION ?? throw new ArgumentNullException(nameof(REVISION_VERSION));
            this.HOTFIX_NAME = HOTFIX_NAME ?? throw new ArgumentNullException(nameof(HOTFIX_NAME));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GameVersion);
        }

        public bool Equals(GameVersion other)
        {
            return Equals(other, false);
        }

        public bool Equals(GameVersion other, bool ignoreHotfix)
        {
            return other != null &&
                   NAME == other.NAME &&
                   MAJOR_VERSION == other.MAJOR_VERSION &&
                   MINOR_VERSION == other.MINOR_VERSION &&
                   REVISION_VERSION == other.REVISION_VERSION &&
                   (ignoreHotfix ? true : HOTFIX_NAME == other.HOTFIX_NAME);
        }

        public override int GetHashCode()
        {
            int hashCode = -1262093482;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NAME);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MAJOR_VERSION);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MINOR_VERSION);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(REVISION_VERSION);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(HOTFIX_NAME);
            return hashCode;
        }

        public override string ToString()
        {
            return NAME + "(v"+MAJOR_VERSION+"."+MINOR_VERSION+"."+REVISION_VERSION + HOTFIX_NAME +")";
        }

        public static bool operator ==(GameVersion left, GameVersion right)
        {
            return EqualityComparer<GameVersion>.Default.Equals(left, right);
        }

        public static bool operator !=(GameVersion left, GameVersion right)
        {
            return !(left == right);
        }
    }
}
