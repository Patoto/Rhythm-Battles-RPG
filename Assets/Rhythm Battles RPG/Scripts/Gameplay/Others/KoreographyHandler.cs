using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;
using UnityEngine;

public class KoreographyHandler : MonoBehaviour
{
    [HideInInspector] public List<Measure> Measures;
    [HideInInspector] public List<KoreographyEvent> MeasureEvents;

    [Header("References")]
    public Koreography Koreography;

    private List<KoreographyEvent> beatEvents;
    private List<KoreographyEvent> noteEvents;

    private void Awake()
    {
        SetupEvents();
        SetupMeasures();
    }

    private void SetupEvents()
    {
        MeasureEvents = Koreography.GetTrackByID(TrackNames.Measure).GetAllEvents();
        beatEvents = Koreography.GetTrackByID(TrackNames.Beat).GetAllEvents();
        noteEvents = Koreography.GetTrackByID(TrackNames.Note).GetAllEvents();
    }

    private void SetupMeasures()
    {
        Measures = new List<Measure>();
        foreach (KoreographyEvent tempMeasureEvent in MeasureEvents)
        {
            List<Beat> beats = CreateBeatsWithMeasureEvent(tempMeasureEvent);
            Measure newMeasure = new Measure(tempMeasureEvent, beats);
            Measures.Add(newMeasure);
        }
    }

    private List<Beat> CreateBeatsWithMeasureEvent(KoreographyEvent measureEvent)
    {
        List<Beat> beats = new List<Beat>();
        List<KoreographyEvent> beatEventsInMeasure = GetBeatEventsInMeasure(measureEvent);
        foreach (KoreographyEvent tempBeatEvent in beatEventsInMeasure)
        {
            BeatType beatType = GetBeatEventBeatType(tempBeatEvent);
            Beat newBeat = new Beat(tempBeatEvent, beatType);
            beats.Add(newBeat);
        }
        return beats;
    }

    private BeatType GetBeatEventBeatType(KoreographyEvent beatEvent)
    {
        BeatType beatType = BeatType.None;
        KoreographyEvent noteEvent = GetNoteEventAtSample(beatEvent.StartSample);
        if (noteEvent != null)
        {
            beatType = BeatType.A;
        }
        return beatType;
    }

    private KoreographyEvent GetNoteEventAtSample(int sample)
    {
        KoreographyEvent noteEventAtSample = null;
        foreach (KoreographyEvent tempNoteEvent in noteEvents)
        {
            if (tempNoteEvent.StartSample == sample)
            {
                noteEventAtSample = tempNoteEvent;
            }
        }
        return noteEventAtSample;
    }

    private List<KoreographyEvent> GetBeatEventsInMeasure(KoreographyEvent measureEvent)
    {
        List<KoreographyEvent> beatEventsInMeasure = new List<KoreographyEvent>();
        KoreographyEvent nextMeasureEvent = GetNextMeasureEvent(measureEvent);
        foreach (KoreographyEvent beatEvent in beatEvents)
        {
            if (beatEvent.StartSample >= measureEvent.StartSample)
            {
                if (nextMeasureEvent != null)
                {
                    if (beatEvent.StartSample < nextMeasureEvent.StartSample)
                    {
                        beatEventsInMeasure.Add(beatEvent);
                    }
                }
                else
                {
                    beatEventsInMeasure.Add(beatEvent);
                }
            }
        }
        return beatEventsInMeasure;
    }

    private KoreographyEvent GetNextMeasureEvent(KoreographyEvent measureEvent)
    {
        KoreographyEvent nextMeasureEvent = null;
        foreach (KoreographyEvent tempKoreographyEvent in MeasureEvents)
        {
            if (tempKoreographyEvent.StartSample > measureEvent.StartSample)
            {
                if (nextMeasureEvent == null)
                {
                    nextMeasureEvent = tempKoreographyEvent;
                }
                else if(tempKoreographyEvent.StartSample < nextMeasureEvent.StartSample)
                {
                    nextMeasureEvent = tempKoreographyEvent;
                }
            }
        }
        return nextMeasureEvent;
    }

    private TempoSectionDef GetTempoSection(string name)
    {
        TempoSectionDef tempoSection = null;
        int tempoSectionsAmount = Koreography.GetNumTempoSections();
        for (int i = 0; i < tempoSectionsAmount; i++)
        {
            TempoSectionDef tempTempoSection = Koreography.GetTempoSectionAtIndex(i);
            if (tempTempoSection.SectionName.Equals(name))
            {
                tempoSection = tempTempoSection;
            }
        }
        return tempoSection;
    }
}

public class Measure
{
    [HideInInspector] public KoreographyEvent MeasureEvent;
    [HideInInspector] public List<Beat> Beats;

    public Measure(KoreographyEvent measureEvent, List<Beat> beats)
    {
        MeasureEvent = measureEvent;
        Beats = beats;
    }
}

public class Beat
{
    [HideInInspector] public KoreographyEvent BeatEvent;
    [HideInInspector] public BeatType BeatType;

    public Beat(KoreographyEvent beatEvent, BeatType beatType)
    {
        BeatEvent = beatEvent;
        BeatType = beatType;
    }
}