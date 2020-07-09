using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using UnityEngine;

public class TestSceneManager : MonoBehaviour
{
	[Header("References")]
	public Player Player;
	[Header("Others")]
	[EventID] public string BeatEventID;
	[EventID] public string HitEventID;

	private void Start()
	{
		Initialize();
	}

	private void Initialize()
	{
		Koreographer.Instance.RegisterForEvents(BeatEventID, MakePlayerHop);
		Koreographer.Instance.RegisterForEvents(HitEventID, MakePlayerHit);
	}

	private void MakePlayerHop(KoreographyEvent koreographyEvent)
	{
		Player.Hop();
	}

	private void MakePlayerHit(KoreographyEvent koreographyEvent)
	{
		Player.Hit();
	}
}