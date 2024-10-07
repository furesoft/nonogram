using System;
using System.Collections.Generic;
using System.IO;

namespace NonoGramGen.Model;

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

    public void ReadFromFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath);

        // Read metadata
        Catalogue = lines[0].Split('"')[1];
        Title = lines[1].Split('"')[1];
        Author = lines[2].Split('"')[1];
        CopyrightInfo = lines[3].Split('"')[1];
        LicenseInfo = lines[4].Split(' ')[1];
        Height = int.Parse(lines[5].Split(' ')[1]);
        Width = int.Parse(lines[6].Split(' ')[1]);

        // Read rows
        var rowIndex = Array.IndexOf(lines, "rows") + 2;
        while (!string.IsNullOrWhiteSpace(lines[rowIndex]))
        {
            Rows.Add(new List<int>(Array.ConvertAll(lines[rowIndex].Split(','), int.Parse)));
            rowIndex++;
        }

        // Read columns
        var colIndex = Array.IndexOf(lines, "columns") + 2;
        while (!string.IsNullOrWhiteSpace(lines[colIndex]))
        {
            Columns.Add(new List<int>(Array.ConvertAll(lines[colIndex].Split(','), int.Parse)));
            colIndex++;
        }

        // Read goal
        Goal = lines[Array.IndexOf(lines, "goal") + 1].Split('"')[1];
    }

    public void WriteToFile(string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        {
            writer.WriteLine($"catalogue \"{Catalogue}\"");
            writer.WriteLine($"title \"{Title}\"");
            writer.WriteLine($"by \"{Author}\"");
            writer.WriteLine($"copyright \"{CopyrightInfo}\"");
            writer.WriteLine($"license {LicenseInfo}");
            writer.WriteLine($"height {Height}");
            writer.WriteLine($"width {Width}");
            writer.WriteLine();

            writer.WriteLine("rows");
            foreach (var row in Rows) writer.WriteLine(string.Join(",", row));

            writer.WriteLine();
            writer.WriteLine("columns");
            foreach (var column in Columns) writer.WriteLine(string.Join(",", column));

            writer.WriteLine();
            writer.WriteLine($"goal \"{Goal}\"");
        }
    }
}