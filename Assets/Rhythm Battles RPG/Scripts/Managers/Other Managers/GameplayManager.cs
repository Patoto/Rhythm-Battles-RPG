using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using UnityEngine;

public class GameplayManager : Manager
{
	[Header("References")]
	public Player Player;
	public GameObject CueStar;
	public AudioClip CueSound;
	[Header("Settings")]
	public float CueStarTime;
	[Header("Others")]
	[EventID] public string BeatEventID;
	[EventID] public string CueEventID;

	private void Start()
	{
		Initialize();
	}

	private void Initialize()
	{
		Koreographer.Instance.RegisterForEvents(BeatEventID, MakePlayerHop);
		Koreographer.Instance.RegisterForEvents(CueEventID, ShowCueStar);
	}

	private void MakePlayerHop(KoreographyEvent koreographyEvent)
	{
		Player.Hop();
	}

	private void ShowCueStar(KoreographyEvent koreographyEvent)
	{
		CueStar.SetActive(true);
		GameManager.Instance.AudioManager.PlaySound(CueSound, 0.2f);
		CancelInvoke("HideCueStar");
		Invoke("HideCueStar", CueStarTime);
	}

	private void HideCueStar()
	{
		CueStar.SetActive(false);
	}

    public override void ConnectManager()
    {
		GameManager.Instance.GameplayManager = this;
    }
}