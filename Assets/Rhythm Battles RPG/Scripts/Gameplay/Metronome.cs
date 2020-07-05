using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Metronome : MonoBehaviour
{
    [Header("Settings")]
    public float BPM;
    public int TimeSignatureNumerator;
    public int TimeSignatureDenominator;
    public Color OffColor;
    public Color BeatColor;
    public Color AccentColor;
    public float HideLightTime;
    public float BeatSoundPitch;
    public float AccentSoundPitch;

    [Header("References")]
    public Image LightImage;
    public AudioClip BeatSound;
    public TextMeshProUGUI BeatCounterText;

    private int beatCounter = 0;
    private float timeToNextBeat;

    private void Start()
    {
        InitializeMetronome();
    }

    private void InitializeMetronome()
    {
        HideLight();
        UpdateTimeToNextBeat();
        UpdateBeatCounterText();
    }

    public void TurnOn()
    {
        ResetMetronome();
        ActivateNextBeat();
    }

    public void TurnOff()
    {
        ResetMetronome();
    }

    private void ActivateNextBeat()
    {
        beatCounter++;
        if (BeatIsAccent())
        {
            ShowAccent();
            beatCounter = 1;
        }
        else
        {
            ShowBeat();
        }
        UpdateBeatCounterText();
        Invoke("HideLight", HideLightTime);
        Invoke("ActivateNextBeat", timeToNextBeat);
    }

    private void ShowBeat()
    {
        LightImage.color = BeatColor;
        GameManager.Instance.AudioManager.PlaySound(BeatSound,1,false,BeatSoundPitch);
    }

    private void ShowAccent()
    {
        LightImage.color = AccentColor;
        GameManager.Instance.AudioManager.PlaySound(BeatSound, 1, false, AccentSoundPitch);
    }

    private void HideLight()
    {
        LightImage.color = OffColor;
    }

    private bool BeatIsAccent()
    {
        bool beatIsAccent = false;
        if (beatCounter == 1 || beatCounter > TimeSignatureNumerator)
        {
            beatIsAccent = true;
        }
        return beatIsAccent;
    }

    private void CancelInvokes()
    {
        CancelInvoke("HideLight");
        CancelInvoke("ActivateNextBeat");
    }

    private void ResetBeatCounter()
    {
        beatCounter = 0;
    }

    private void ResetMetronome()
    {
        CancelInvokes();
        HideLight();
        ResetBeatCounter();
        UpdateBeatCounterText();
        UpdateTimeToNextBeat();
    }

    private void UpdateBeatCounterText()
    {
        BeatCounterText.text = beatCounter.ToString();
    }

    private void UpdateTimeToNextBeat()
    {
        timeToNextBeat = Utils.BPMToSeconds(BPM);
    }
}
