using System;
using Mafi;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Localization;
using UnityEngine;

namespace CoI.Mod.Better.Custom.Data
{
	[Serializable]
	public class ToolbarData
	{
		public bool   overrideProtoID;
		public string ProtoID;

		public string Name;
		public int    Order;

		public string IconPath;
		public bool   isTransportBuildAllowed = true;
		public string ShortcutID;

		public void From(ToolbarCategoryProto toolbarCategoryProto)
		{
			ProtoID = toolbarCategoryProto.Id.ToString();
			Name = toolbarCategoryProto.Strings.Name.Id;
			Order = (int)toolbarCategoryProto.Order;
			IconPath = toolbarCategoryProto.IconPath;
			isTransportBuildAllowed = toolbarCategoryProto.IsTransportBuildAllowed;
			ShortcutID = toolbarCategoryProto.ShortcutId;

			if (ShortcutID == "")
			{
				ShortcutID = null;
			}
		}

		public Option<ToolbarCategoryProto> Into(ProtoRegistrator registrator)
		{
			ProtoID.CheckNotNullOrEmpty();
			if (ProtoID == null || ProtoID.IsEmpty())
			{
				Debug.Log("StorageData >> Into >> name: " + Name + " >> Toolbar cannot generate, ProtoID is not set!");
				return Option<ToolbarCategoryProto>.None;
			}

			Proto.ID protoID = new Proto.ID(ProtoID);
			Option<ToolbarCategoryProto> overrideProto = registrator.PrototypesDb.Get<ToolbarCategoryProto>(protoID);
			if (!overrideProtoID && overrideProto.HasValue)
			{
				Debug.Log("TooblarData >> Into >> name: " + Name + " | id: " + protoID + " >> Toolbar cannot generate, ProtoID already exists!");
				return Option<ToolbarCategoryProto>.None;
			}
			if (overrideProtoID && !overrideProto.HasValue)
			{
				Debug.Log("TooblarData >> Into >> name: " + Name + " | id: " + protoID + " >> Toolbar cannot override, ProtoID is not exists!");
				return Option<ToolbarCategoryProto>.None;
			}

			if (overrideProtoID)
			{
				OverrideData(overrideProto);
			}
			else
			{
				Name.CheckNotNullOrEmpty();
				IconPath.CheckNotNullOrEmpty();
				Order.CheckNotNegative();
			}


			if (ShortcutID == null)
			{
				ShortcutID = "";
			}

			//Loc.Str(id.Value + "__name", name, "name" + translationComment), (descShort != null) ? Loc.Str(id.Value + "__desc", descShort, "short description" + translationComment) : LocStr.Empty

			Proto.Str strings;
			if (overrideProtoID)
			{
				strings = new Proto.Str(LocalizationManager.LoadOrCreateLocalizedString0(Name, Name), LocStr.Empty);
			}
			else
			{
				strings = new Proto.Str(Loc.Str(protoID + "__name", protoID.ToString(), ""), LocStr.Empty);
			}
			return Option<ToolbarCategoryProto>.Some(new ToolbarCategoryProto(
				protoID,
				strings,
				Order,
				IconPath,
				isTransportBuildAllowed,
				shortcutId: ShortcutID
			));
		}

		private void OverrideData(Option<ToolbarCategoryProto> overrideProto)
		{
			ToolbarData overrideData = new ToolbarData();
			overrideData.From(overrideProto.Value);

			if (Name == null || Name.IsEmpty())
			{
				Name = overrideData.Name;
			}
			else
			{
				Name.CheckNotNullOrEmpty();
			}

			if (IconPath == null || IconPath.IsEmpty())
			{
				IconPath = overrideData.IconPath;
			}
			else
			{
				IconPath.CheckNotNullOrEmpty();
			}

			if (Order == default)
			{
				Order = overrideData.Order;
			}
			else
			{
				Order.CheckNotNegative();
			}

			if ((ShortcutID == null || ShortcutID.IsEmpty()) && overrideData.ShortcutID != null && !overrideData.ShortcutID.IsEmpty())
			{
				ShortcutID = overrideData.ShortcutID;
			}

			isTransportBuildAllowed = overrideData.isTransportBuildAllowed;
		}

		public void Build(ProtoRegistrator registrator)
		{
			Option<ToolbarCategoryProto> intoData = Into(registrator);
			if (!intoData.HasValue)
			{
				return;
			}

			if (overrideProtoID)
			{
				registrator.PrototypesDb.RemoveOrThrow(new Proto.ID(ProtoID));
			}
			registrator.PrototypesDb.Add(intoData.Value);
		}
	}
}