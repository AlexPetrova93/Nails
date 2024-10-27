using System;

public enum SpeechType { Negative, Neutral, Positive }

public static class SpeechContainer
{
    public static string[] NegativeLines = 
    {
        "Je mi z tebe fyzicky nevolno.",
        "Kdybys byl gay, tak bych to ještě chápal.",
        "Kdybys takhle přišel na pohovor, tak bysme tě nevzali.",
        "TOHLE ti jako přijde hezký jo?",
        "Kdybysme spolu někam jeli, normálně bych se za tebe styděl.",
        "To je úplně mimo hranice jakýkoli normality už.",
        "Co to jako máš na rukách?!",
        "Kdybys nebyl můj syn, tak bysme spolu skončili.",
        "Vždyckys byl tak trochu metrouš, ale tohle je daleko přes čáru.",
    };
    
    public static string[] NeutralLines = 
    {
        "Tak když myslíš, no…",
        "Hezký, ale nebudou si pak myslet, že seš teplej?",
        "Mně se to teda nelíbí, ale když chceš…",
        "No hezky sis to zmaloval ty pracky.",
        "To se teďka mezi mladejma dělá, jo?",
        "Co na to řeknou holky?"
    };
    
    public static string[] PositiveLines = 
    {
        "Ta barva ti de skvěle k očím!",
        "Hele ta je jak ze vzorníku 2024!",
        "Mně se líbí, když se kluci fintěj.",
        "Pěkný, už to děláš dlouho?",
        "Máš to hezky rovný!",
        "Super, půjdeme spolu někdy nakupovat laky!",
        "Tahle je moc hezká, jak se leskne."
    };

    public static string[] GetLines(SpeechType speechType)
    {
        return speechType switch
        {
            SpeechType.Negative => NegativeLines,
            SpeechType.Neutral => NeutralLines,
            SpeechType.Positive => PositiveLines,
            _ => Array.Empty<string>()
        };
    }
}
