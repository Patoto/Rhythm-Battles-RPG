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
        AddCues();
    }

    private void AddCues()
    {
        List<Koreography> koreographies = GetAllLoadedKoreographies();
        foreach (Koreography tempKoreography in koreographies)
        {
            AddCuesToKoreography(tempKoreography);
        }
    }

    private void AddCuesToKoreography(Koreography koreography)
    {
        KoreographyTrackBase accentKoreographyTrack = koreography.GetTrackByID("Accent");
        KoreographyTrackBase cueKoreographyTrack = koreography.GetTrackByID("Cue");
        TempoSectionDef tempoSection = GetTempoSection(koreography, "Main");
        List<KoreographyEvent> accentKoreographyEvents = accentKoreographyTrack.GetAllEvents();
        cueKoreographyTrack.RemoveAllEvents();
        foreach (KoreographyEvent tempAccentKoreographyEvent in accentKoreographyEvents)
        {
            KoreographyEvent cueKoreographyEvent = CreateCueEventBasedOnAccentEvent(tempAccentKoreographyEvent, tempoSection);
            cueKoreographyTrack.AddEvent(cueKoreographyEvent);
        }
    }

    private KoreographyEvent CreateCueEventBasedOnAccentEvent(KoreographyEvent accentEvent, TempoSectionDef tempoSection)
    {
        KoreographyEvent cueEvent = new KoreographyEvent();
        int measureSamples = (int)tempoSection.SamplesPerBeat * tempoSection.BeatsPerMeasure;
        cueEvent.StartSample = accentEvent.StartSample - measureSamples;
        cueEvent.EndSample = cueEvent.StartSample;
        return cueEvent;
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
}