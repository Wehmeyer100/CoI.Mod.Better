using Mafi;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CoI.Mod.Better.Custom
{
    [Serializable]
    public class TooblarData
    {
        public bool overrideProtoID;
        public string ProtoID;

        public string Name;
        public int Order;

        public string IconPath;
        public bool isTransportBuildAllowed = true;

        public Option<ToolbarCategoryProto> Into(ProtoRegistrator registrator)
        {
            ProtoID.CheckNotNullOrEmpty();
            if (ProtoID == null || ProtoID.IsEmpty())
            {
                Debug.Log("StorageData >> Into >> name: " + Name + " >> Toolbar cannot generate, ProtoID must used!");
                return Option<ToolbarCategoryProto>.None;
            }

            Proto.ID protoID = new Proto.ID(ProtoID);
            if (!overrideProtoID && registrator.PrototypesDb.Get<ToolbarCategoryProto>(protoID).HasValue)
            {
                Debug.Log("TooblarData >> Into >> name: " + Name + " | id: " + protoID + " >> Toolbar cannot generate, ProtoID already used!");
                return Option<ToolbarCategoryProto>.None;
            }

            Name.CheckNotNullOrEmpty();
            IconPath.CheckNotNullOrEmpty();
            Order.CheckNotNegative();

            Proto.Str protoStr = Proto.CreateStr(protoID, Name);
            return Option<ToolbarCategoryProto>.Some(new ToolbarCategoryProto(
                        protoID,
                        protoStr,
                        Order,
                        IconPath,
                        isTransportBuildAllowed: isTransportBuildAllowed
                    ));
        }

        public void Build(ProtoRegistrator registrator)
        {
            Option<ToolbarCategoryProto> intoData = Into(registrator);
            if (!intoData.HasValue)
            {
                return;
            }
            registrator.PrototypesDb.Add(intoData.Value);
        }
    }
}