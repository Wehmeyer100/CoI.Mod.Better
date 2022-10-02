using System;
using CoI.Mod.Better.lang;
using CoI.Mod.Better.Shared;
using Mafi;
using Mafi.Core;
using Mafi.Core.GameLoop;
using Mafi.Unity.InputControl.TopStatusBar;
using Mafi.Unity.UiFramework;
using Mafi.Unity.UiFramework.Components;
using Mafi.Unity.UserInterface;
using Mafi.Unity.UserInterface.Style;
using UnityEngine;

namespace CoI.Mod.Better.UserInterface
{
	[GlobalDependency(RegistrationMode.AsAllInterfaces)]
	internal class RealLifeTime : IUnityUi, IUiElement, IDynamicSizeElement
	{
		private readonly StatusBar m_statusBar;
		private          Txt       m_dateLabel;

		public RealLifeTime(IGameLoopEvents gameLoop, StatusBar statusBar)
		{
			if (!BetterMod.Config.UI.ShowRealTime) return;

			BetterDebug.Info("Register RealLifeTime...");

			m_statusBar = statusBar;
			gameLoop.SyncUpdate.AddNonSaveable(this, syncUpdate);

			BetterDebug.Info("Register RealLifeTime done.");
		}

		public event Action<IUiElement> SizeChanged;

		public GameObject GameObject => m_dateLabel.GameObject;

		public RectTransform RectTransform => m_dateLabel.RectTransform;

		public void RegisterUi(UiBuilder builder)
		{
			if (!BetterMod.Config.UI.ShowRealTime) return;

			BetterDebug.Info("Register UI RealLifeTime...");

			UiStyle style = builder.Style;
			m_dateLabel = builder.NewTxt("RealTime").SetText("").SetAlignment(TextAnchor.MiddleRight)
				.AllowHorizontalOverflow()
				.SetHeight(30f)
				.SetWidth(80f)
				.SetTextStyle(style.Global.TextBig);
			m_statusBar.AddElementToRight(this, 101f, true);

			BetterDebug.Info("Register UI RealLifeTime done.");
		}

		private void syncUpdate(GameTime time)
		{
			DateTime now = DateTime.Now;
			string timeStr = (now.Hour < 10 ? "0" : "") + now.Hour + ":" + (now.Minute < 10 ? "0" : "") + now.Minute + ":" + (now.Second < 10 ? "0" : "") + now.Second;
			m_dateLabel.SetText(LangManager.Instance.Get("clock", timeStr));

			float width = (m_dateLabel.GetPreferedWidth() + 10f).Max(80f);
			m_dateLabel.SetWidth(width);
			SizeChanged?.Invoke(this);
		}
	}
}