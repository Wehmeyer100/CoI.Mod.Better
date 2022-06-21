using Mafi;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoI.Mod.Better.Utilities
{
    public static class EntityProtoUtility
    {
        public static string GetIconPath<T>(ProtoRegistrator registrator, Proto.ID id) where T : LayoutEntityProto
        {
            Option<T> proto = registrator.PrototypesDb.Get<T>(id);
            return (proto.Value as LayoutEntityProto).Graphics.IconPath;
        }
    }
}
