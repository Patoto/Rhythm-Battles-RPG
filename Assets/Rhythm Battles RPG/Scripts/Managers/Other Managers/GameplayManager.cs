using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using UnityEngine;

public class GameplayManager : Manager
{
	[Header("References")]
	public GameplayUI GameplayUI;
	public Player Player;
	public KoreographyHandler KoreographyHandler;
	[Header("Others")]
	[EventID] public string HopEventID;
	[EventID] public string MeasureEventID;
	[EventID] public string BeatEventID;

	private void Start()
	{
		SetupKoreographyEvents();
		CreateMeasureWindows();
	}

	private void SetupKoreographyEvents()
	{
		Koreographer.Instance.RegisterForEvents(HopEventID, MakePlayerHop);
		Koreographer.Instance.RegisterForEvents(MeasureEventID, RemoveMeasureWindow);
		Koreographer.Instance.RegisterForEvents(BeatEventID, TurnOnBeatUI);
	}

	private void TurnOnBeatUI(KoreographyEvent koreographyEvent)
	{
		GameplayUI.TurnOnBeatUI(koreographyEvent);
	}

	private void RemoveMeasureWindow(KoreographyEvent koreographyEvent)
	{
		if (koreographyEvent != KoreographyHandler.MeasureEvents[0])
		{
			GameplayUI.RemoveMeasureWindow();
		}
	}

	private void CreateMeasureWindows()
	{
        foreach (Measure tempMeasure in KoreographyHandler.Measures)
        {
			GameplayUI.CreateMeasureWindow(tempMeasure);
		}
	}

	private void MakePlayerHop(KoreographyEvent koreographyEvent)
	{
		Player.Hop();
	}

    public override void ConnectManager()
    {
		GameManager.Instance.GameplayManager = this;
    }
}