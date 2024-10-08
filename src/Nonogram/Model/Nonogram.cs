namespace NonoGramGen.Model;

using System;
using System.Collections.Generic;
using System.IO;

public class Nonogram
{
    public string Catalogue { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string CopyrightInfo { get; set; }
    public string LicenseInfo { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public List<List<int>> Rows { get; set; } = new();
    public List<List<int>> Columns { get; set; } = new();
    public string Goal { get; set; }

    // Methode, um die Datei aus einem Stream zu laden
    public static Nonogram LoadFromStream(Stream stream)
    {
        Nonogram nonogram = new Nonogram();
        using (StreamReader reader = new StreamReader(stream))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("catalogue "))
                {
                    nonogram.Catalogue = line.Substring(10).Trim();
                }
                else if (line.StartsWith("title "))
                {
                    nonogram.Title = line.Substring(6).Trim();
                }
                else if (line.StartsWith("by "))
                {
                    nonogram.Author = line.Substring(3).Trim();
                }
                else if (line.StartsWith("copyright "))
                {
                    nonogram.CopyrightInfo = line.Substring(10).Trim();
                }
                else if (line.StartsWith("license "))
                {
                    nonogram.LicenseInfo = line.Substring(8).Trim();
                }
                else if (line.StartsWith("height "))
                {
                    nonogram.Height = int.Parse(line.Substring(7).Trim());
                }
                else if (line.StartsWith("width "))
                {
                    nonogram.Width = int.Parse(line.Substring(6).Trim());
                }
                else if (line.StartsWith("rows"))
                {
                    nonogram.Rows = ParseMatrix(reader);
                }
                else if (line.StartsWith("columns"))
                {
                    nonogram.Columns = ParseMatrix(reader);
                }
                else if (line.StartsWith("goal "))
                {
                    nonogram.Goal = line.Substring(5).Trim();
                }
            }
        }

        return nonogram;
    }

    // Hilfsmethode, um Listen von Zahlen zu parsen (für Rows und Columns)
    private static List<List<int>> ParseMatrix(StreamReader reader)
    {
        List<List<int>> matrix = new List<List<int>>();
        string line;
        while ((line = reader.ReadLine()) != null && !string.IsNullOrEmpty(line))
        {
            var numbers = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
            List<int> parsedNumbers = new List<int>();
            foreach (var number in numbers)
            {
                parsedNumbers.Add(int.Parse(number.Trim()));
            }
            matrix.Add(parsedNumbers);
        }
        return matrix;
    }
}
