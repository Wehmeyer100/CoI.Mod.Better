using CoI.Mod.Better.Shared;
using CoI.Mod.Better.Shared.Lang;
using Mafi;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Localization;

namespace CoI.Mod.Better.Custom.Data
{
	public abstract class AData<T> where T : Proto
	{
		public bool   Override;
		public string ProtoID;

		public abstract void From(T proto);

		public abstract Option<T> Into(ProtoRegistrator registrator);

		//public abstract void Build(ProtoRegistrator registrator);

		public virtual void Build(ProtoRegistrator registrator)
		{
			Option<T> intoData = Into(registrator);
			if (!intoData.HasValue)
			{
				return;
			}
			
			if (IsOverrideProtoID())
			{
				registrator.PrototypesDb.RemoveOrThrow(new Proto.ID(ProtoID));
			}
			registrator.PrototypesDb.Add(intoData.Value);
		}
		
		public bool IsOverrideProtoID()
		{
			return BetterMod.Config.Custom.OverrideAll || Override;
		}
		public bool CheckProtoID()
		{
			if (ProtoID.IsNullOrEmpty())
			{
				BetterDebug.Info("Custom >> id: " + ProtoID + " >> Data cannot generate, ProtoID is not set!");
				return false;
			}
			else if (ProtoID.Contains(" "))
			{
				BetterDebug.Info("Custom >> id: " + ProtoID + " >> Data cannot generate, ProtoID is not valid! >> There must be no blank characters.");
				return false;
			}
			return true;
		}

		protected Option<T> GetOverrideProto(ProtoRegistrator registrator)
		{
			if (!CheckProtoID())
			{
				return Option<T>.None;
			}

			Option<T> overrideProto = registrator.PrototypesDb.Get<T>(new Proto.ID(ProtoID));

			if (!IsOverrideProtoID() && overrideProto.HasValue)
			{
				BetterDebug.Info("Custom >> id: " + ProtoID + " >> Data cannot generate, ProtoID already exists!");
				return Option<T>.None;
			}
			if (IsOverrideProtoID() && !overrideProto.HasValue)
			{
				BetterDebug.Info("Custom >> id: " + ProtoID + " >> Data cannot override, ProtoID is not exists!");
				return Option<T>.None;
			}

			return overrideProto;
		}

		protected bool IsProtoExists(ProtoRegistrator registrator)
		{
			return GetProto(registrator).HasValue;
		}

		protected Option<T> GetProto(ProtoRegistrator registrator)
		{
			Proto.ID protoID = new Proto.ID(ProtoID);
			return !CheckProtoID() ? Option<T>.None : registrator.PrototypesDb.Get<T>(protoID);
		}

		protected Proto.Str GetStrings(ProtoRegistrator registrator, string name, string description = "")
		{
			Proto.Str strings;
			if (IsOverrideProtoID())
			{
				strings = new Proto.Str(
					LocalizationManager.LoadOrCreateLocalizedString0(name, name), 
					LocalizationManager.LoadOrCreateLocalizedString0(description, description)
				);
			}
			else
			{
				strings = new Proto.Str(
					Loc.Str(ProtoID + "__name", name.IsEmpty() ? ProtoID : name, ""), 
					Loc.Str(ProtoID + "__desc", description.IsEmpty() ? ProtoID : description, "")
				);
			}

			if (LangManager.Instance.Has(ProtoID + "__name"))
			{
				LocStr descShort = LocStr.Empty;
				if (LangManager.Instance.Has(ProtoID + "__desc"))
				{
					descShort = Loc.Str(ProtoID + "__desc", LangManager.Instance.Get(ProtoID + "__desc"), "");
				}
				strings = new Proto.Str(Loc.Str(ProtoID + "__name", LangManager.Instance.Get(ProtoID + "__name"), ""), descShort);
			}
			return strings;
		}
	}
}