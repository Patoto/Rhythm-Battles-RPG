using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using UnityEngine;

public class KoreographerManager : MonoBehaviour
{
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        AddCuesToAllKoreographies();
    }

    private void AddCuesToAllKoreographies()
    {
        List<Koreography> koreographies = GetAllLoadedKoreographies();
        foreach (Koreography tempKoreography in koreographies)
        {
            AddCuesToKoreography(tempKoreography);
        }
    }

    private void AddCuesToKoreography(Koreography koreography)
    {
        TempoSectionDef tempoSection = GetTempoSection(koreography, "Main");
        KoreographyTrackBase accentKoreographyTrack = koreography.GetTrackByID("Accent");
        KoreographyTrackBase cueKoreographyTrack = koreography.GetTrackByID("Cue");
        cueKoreographyTrack.RemoveAllEvents();
        int measuresAmount = GetMeasuresAmount(koreography, tempoSection);
        for (int i = 0; i < measuresAmount; i++)
        {
            List<KoreographyEvent> measureSequence = GetMeasureSequence(i, accentKoreographyTrack, tempoSection);
            bool sequenceIsNew = true;
            if (i > 0)
            {
                List<KoreographyEvent> previousMeasureSequence = GetMeasureSequence(i-1, accentKoreographyTrack, tempoSection);
                sequenceIsNew = SequenceIsNew(measureSequence, previousMeasureSequence, tempoSection);
            }
            if (sequenceIsNew)
            {
                AddCuesToMeasure(i, tempoSection, cueKoreographyTrack, accentKoreographyTrack);
            }
        }
    }

    private bool SequenceIsNew(List<KoreographyEvent> sequence, List<KoreographyEvent> previousSequence, TempoSectionDef tempoSection)
    {
        bool sequenceIsNew = true;
        if (SequencesAreEqual(sequence, previousSequence, tempoSection))
        {
            sequenceIsNew = false;
        }
        return sequenceIsNew;
    }

    private void AddCuesToMeasure(int measureIndex, TempoSectionDef tempoSection, KoreographyTrackBase cueKoreographyTrack, KoreographyTrackBase accentKoreographyTrack)
    {
        List<KoreographyEvent> accentKoreographyEvents = GetMeasureEvents(measureIndex, accentKoreographyTrack, tempoSection);
        foreach (KoreographyEvent tempAccentKoreographyEvent in accentKoreographyEvents)
        {
            KoreographyEvent cueKoreographyEvent = CreateCueEventBasedOnAccentEvent(tempAccentKoreographyEvent, tempoSection);
            cueKoreographyTrack.AddEvent(cueKoreographyEvent);
        }
    }

    private List<KoreographyEvent> GetMeasureEvents(int measureIndex, KoreographyTrackBase koreographyTrack, TempoSectionDef tempoSection)
    {
        List<KoreographyEvent> measureEvents = new List<KoreographyEvent>();
        int measureStartSample = GetMeasureStartSample(measureIndex, tempoSection);
        int measureEndSample = GetMeasureEndSample(measureIndex, tempoSection);
        koreographyTrack.GetEventsInRange(measureStartSample, measureEndSample, measureEvents);
        return measureEvents;
    }

    private KoreographyEvent CreateCueEventBasedOnAccentEvent(KoreographyEvent accentEvent, TempoSectionDef tempoSection)
    {
        KoreographyEvent cueEvent = new KoreographyEvent();
        int measureSamples = GetMeasureSamples(tempoSection);
        cueEvent.StartSample = accentEvent.StartSample - measureSamples;
        cueEvent.EndSample = cueEvent.StartSample;
        return cueEvent;
    }

    private int GetMeasureSamples(TempoSectionDef tempoSection)
    {
        return (int)tempoSection.SamplesPerBeat * tempoSection.BeatsPerMeasure;
    }

    private List<Koreography> GetAllLoadedKoreographies()
    {
        List<Koreography> loadedKoreographies = new List<Koreography>();
        Koreographer.Instance.GetAllLoadedKoreography(loadedKoreographies);
        return loadedKoreographies;
    }

    private TempoSectionDef GetTempoSection(Koreography koreography, string name)
    {
        TempoSectionDef tempoSection = null;
        int tempoSectionsAmount = koreography.GetNumTempoSections();
        for (int i = 0; i < tempoSectionsAmount; i++)
        {
            TempoSectionDef tempTempoSection = koreography.GetTempoSectionAtIndex(i);
            if (tempTempoSection.SectionName.Equals(name))
            {
                tempoSection = tempTempoSection;
            }
        }
        return tempoSection;
    }

    private int GetEventMeasureIndex(KoreographyEvent koreographyEvent, TempoSectionDef tempoSection)
    {
        int measureSamples = GetMeasureSamples(tempoSection);
        int eventMeasureIndex = Mathf.FloorToInt(koreographyEvent.StartSample / measureSamples);
        return eventMeasureIndex;
    }

    private int GetMeasureStartSample(int measureIndex, TempoSectionDef tempoSection)
    {
        int measureSamples = GetMeasureSamples(tempoSection);
        int measureStartSample = tempoSection.StartSample + (measureIndex * measureSamples);
        return measureStartSample;
    }

    private int GetMeasureEndSample(int measureIndex, TempoSectionDef tempoSection)
    {
        int measureStartSample = GetMeasureStartSample(measureIndex, tempoSection);
        int measureSamples = GetMeasureSamples(tempoSection);
        int measureEndSample = measureStartSample + measureSamples - 1;
        return measureEndSample;
    }

    private List<KoreographyEvent> GetMeasureSequence(int measureIndex, KoreographyTrackBase accentTrack, TempoSectionDef tempoSection)
    {
        List<KoreographyEvent> measureSequence = new List<KoreographyEvent>();
        int measureStartSample = GetMeasureStartSample(measureIndex, tempoSection);
        int measureEndSample = GetMeasureEndSample(measureIndex, tempoSection);
        List<KoreographyEvent> accentEvents = accentTrack.GetAllEvents();
        foreach (KoreographyEvent tempAccentEvent in accentEvents)
        {
            int accentStartSample = tempAccentEvent.StartSample;
            if (accentStartSample >= measureStartSample && accentStartSample <= measureEndSample)
            {
                measureSequence.Add(tempAccentEvent);
            }
        }
        measureSequence = SortSequence(measureSequence);
        return measureSequence;
    }

    private bool SequencesAreEqual(List<KoreographyEvent> sequenceA, List<KoreographyEvent> sequenceB, TempoSectionDef tempoSection)
    {
        bool sequencesAreEqual = true;
        if (sequenceA.Count == sequenceB.Count)
        {
            if (sequenceA.Count > 0)
            {
                int sequenceAFirstEventMeasureIndex = GetEventMeasureIndex(sequenceA[0], tempoSection);
                int sequenceAMeasureStartSample = GetMeasureStartSample(sequenceAFirstEventMeasureIndex, tempoSection);
                int sequenceBFirstEventMeasureIndex = GetEventMeasureIndex(sequenceB[0], tempoSection);
                int sequenceBMeasureStartSample = GetMeasureStartSample(sequenceBFirstEventMeasureIndex, tempoSection);
                for (int i = 0; i < sequenceA.Count; i++)
                {
                    KoreographyEvent tempSequenceAEvent = sequenceA[i];
                    KoreographyEvent tempSequenceBEvent = sequenceB[i];
                    int relativeSequenceAEventStartSample = tempSequenceAEvent.StartSample - sequenceAMeasureStartSample;
                    int relativeSequenceBEventStartSample = tempSequenceBEvent.StartSample - sequenceBMeasureStartSample;
                    if (relativeSequenceAEventStartSample != relativeSequenceBEventStartSample)
                    {
                        sequencesAreEqual = false;
                    }
                }
            }
        }
        else
        {
            sequencesAreEqual = false;
        }
        return sequencesAreEqual;
    }

    private List<KoreographyEvent> SortSequence(List<KoreographyEvent> sequence)
    {
        List<KoreographyEvent> sortedSequence = new List<KoreographyEvent>(sequence);
        for (int j = 0; j <= sortedSequence.Count - 2; j++)
        {
            for (int i = 0; i <= sortedSequence.Count - 2; i++)
            {
                if (sortedSequence[i].StartSample > sortedSequence[i + 1].StartSample)
                {
                    KoreographyEvent tempEvent = sortedSequence[i + 1];
                    sortedSequence[i + 1] = sortedSequence[i];
                    sortedSequence[i] = tempEvent;
                }
            }
        }
        return sortedSequence;
    }

    private int GetMeasuresAmount(Koreography koreography ,TempoSectionDef tempoSection)
    {
        int measureSamples = GetMeasureSamples(tempoSection);
        int totalSamples = koreography.SourceClip.samples;
        totalSamples -= tempoSection.StartSample;
        int measuresAmount = Mathf.FloorToInt(totalSamples / measureSamples);
        return measuresAmount;
    }
}