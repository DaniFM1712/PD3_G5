using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Extret del tutorial:
// Building a Localisation Tool in Unity - Part 1, de Game Dev Guide
// https://www.youtube.com/watch?v=c-dzg4M20wY

public class LocalisationSystem
{
    public enum Language
    {
        Catala,
        Castellano,
        English
    }
    public static Language language = Language.Catala;

    private static Dictionary<string, string> localisedCA;
    private static Dictionary<string, string> localisedES;
    private static Dictionary<string, string> localisedEN;

    public static bool isInit;

    public static void Init()
    {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        localisedCA = csvLoader.GetDictionaryValues("ca");
        localisedES = csvLoader.GetDictionaryValues("es");
        localisedEN = csvLoader.GetDictionaryValues("en");

        isInit = true;
    }

    public static string GetLocalisedValue(string key)
    {
        if (!isInit) { Init(); }
        string value = key;
        switch (language)
        {
            case Language.Catala:
                localisedCA.TryGetValue(key, out value);
                break;
            case Language.Castellano:
                localisedES.TryGetValue(key, out value);
                break;
            case Language.English:
                localisedEN.TryGetValue(key, out value);
                break;
        }
        return value;
    }
}
