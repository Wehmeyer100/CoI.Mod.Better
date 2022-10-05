using System;
using CoI.Mod.Better.Shared.Lang;
using Mafi;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Localization;
using UnityEngine;

namespace CoI.Mod.Better.Custom.Data
{
	[Serializable]
	public class ToolbarData : AData<ToolbarCategoryProto>
	{
		public string Name;
		public int    Order;

		public string IconPath;
		public bool   isTransportBuildAllowed = true;
		public string ShortcutID;

		public override void From(ToolbarCategoryProto toolbarCategoryProto)
		{
			ProtoID = toolbarCategoryProto.Id.ToString();
			Name = toolbarCategoryProto.Strings.Name.Id;
			Order = (int)toolbarCategoryProto.Order;
			IconPath = toolbarCategoryProto.IconPath;
			isTransportBuildAllowed = toolbarCategoryProto.IsTransportBuildAllowed;
			
			ShortcutID = toolbarCategoryProto.ShortcutId;
			ShortcutID = ShortcutID.IsEmpty() ? null : ShortcutID;
		}

		public override Option<ToolbarCategoryProto> Into(ProtoRegistrator registrator)
		{
			Option<ToolbarCategoryProto> overrideProto =  GetOverrideProto(registrator);

			if (IsOverrideProtoID())
			{
				OverrideData(overrideProto);
			}
			else
			{
				Name.CheckNotNullOrEmpty();
				IconPath.CheckNotNullOrEmpty();
				Order.CheckNotNegative();
			}
			
			return Option<ToolbarCategoryProto>.Some(new ToolbarCategoryProto(
				new Proto.ID(ProtoID),
				GetStrings(registrator, Name),
				Order,
				IconPath,
				isTransportBuildAllowed,
				shortcutId:  ShortcutID ?? ""
			));
		}

		private void OverrideData(Option<ToolbarCategoryProto> overrideProto)
		{
			ToolbarData overrideData = new ToolbarData();
			overrideData.From(overrideProto.Value);

			if (Name.IsNullOrEmpty())
			{
				Name = overrideData.Name;
			}
			else
			{
				Name.CheckNotNullOrEmpty();
			}

			if (IconPath.IsNullOrEmpty())
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

			if (ShortcutID.IsNullOrEmpty() && !overrideData.ShortcutID.IsNullOrEmpty())
			{
				ShortcutID = overrideData.ShortcutID;
			}
			
			isTransportBuildAllowed = overrideData.isTransportBuildAllowed;
		}
	}
}