using System;
using System.IO;
using UnityEngine;

public static class CSVImporter
{
    public static string ImportCSV(string path)
    {
        StreamReader streamReader = new StreamReader(Application.streamingAssetsPath + "/DialogueCSV.csv", System.Text.Encoding.UTF8, false);
        string csv = streamReader.ReadToEnd();
        return csv;
    }
}
