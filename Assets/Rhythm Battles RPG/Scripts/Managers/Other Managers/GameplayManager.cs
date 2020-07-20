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
	[EventID] public string BeatEventID;

	private void Start()
	{
		SetupKoreographyEvents();
		CreateMeasureWindows();
	}

	private void SetupKoreographyEvents()
	{
		Koreographer.Instance.RegisterForEvents(BeatEventID, MakePlayerHop);
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