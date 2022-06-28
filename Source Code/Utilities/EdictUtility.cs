using CoI.Mod.Better.lang;
using Mafi;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Mods;
using Mafi.Core.Population.Edicts;
using Mafi.Core.PropertiesDb;
using Mafi.Core.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoI.Mod.Better.Utilities
{
    public static class EdictUtility
    {
        public static EdictWithPropertiesProto GenerateEdict2(ProtoRegistrator registrator, Proto.ID protoID, EdictCategoryProto category, string translateKey, float upkeep, PropertyId<Percent> modifer, int percent, Proto.ID? previousProtoID, string iconPath)
        {
            Option<EdictProto> previousTier = Option<EdictProto>.None;
            if (previousProtoID.HasValue)
            {
                previousTier = registrator.PrototypesDb.GetOrThrow<EdictProto>(previousProtoID.Value);
            }

            return GenerateEdict(registrator, protoID, category, translateKey, upkeep, modifer, percent, previousTier.Value, iconPath);
        }


        public static EdictWithPropertiesProto GenerateEdict(ProtoRegistrator registrator, Proto.ID protoID, EdictCategoryProto category, string translateKey, float upkeep, PropertyId<Percent> modifer, int percent, Option<EdictProto> previousProto, string iconPath)
        {
            string name = LangManager.Instance.Get(translateKey);
            string desc = LangManager.Instance.Get(translateKey + "_desc", (percent < 0 ? -percent : percent).ToString());

            Option<EdictProto> previousTier = Option<EdictProto>.None;
            if (previousProto.HasValue)
            {
                previousTier = Option<EdictProto>.Some(previousProto.Value);
            }

            EdictWithPropertiesProto proto = new EdictWithPropertiesProto
            (
                 protoID,
                 Proto.CreateStr(protoID, name, desc, ""),
                 category,
                 upkeep.Upoints(),
                 ImmutableArray.Create(Make.Kvp(modifer, percent.Percent())),
                 previousTier,
                 new EdictProto.Gfx(iconPath)
            );
            registrator.PrototypesDb.Add(proto);
            return proto;
        }
    }
}
