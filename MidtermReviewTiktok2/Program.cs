using System;
using System.Collections.Generic;
using System.IO;

public enum Audience
{
    World,
    Group,
    Special
}

public class TikTok
{
    private static int _ID = 1;

    public string Originator { get; }
    public int Length { get; }
    public string HashTag { get; }
    public Audience Audience { get; }
    public string Id { get; }

    public TikTok(string originator, int length, string hashTag, Audience audience)
    {
        Originator = originator;
        Length = length;
        HashTag = hashTag;
        Audience = audience;
        Id = $"TikTok{_ID}";
        _ID++;
    }

    private TikTok(string id, string originator, int length, string hashTag, Audience audience)
    {
        Id = id;
        Originator = originator;
        Length = length;
        HashTag = hashTag;
        Audience = audience;
        // Update _ID if necessary to ensure uniqueness
        int idNumber;
        if (int.TryParse(id.Replace("TikTok", ""), out idNumber))
        {
            _ID = Math.Max(_ID, idNumber + 1);
        }
    }

    public override string ToString()
    {
        return $"ID: {Id}, Originator: {Originator}, Length: {Length}, Hashtag: {HashTag}, Audience: {Audience}";
    }

    public static TikTok Parse(string line)
    {
        string[] parts = line.Split('\t');
        return new TikTok(parts[0], parts[1], int.Parse(parts[2]), parts[3], (Audience)Enum.Parse(typeof(Audience), parts[4]));
    }
}

 static class TikTokManager
{
    private static List<TikTok> TIKTOKS;
    private static string FILENAME = "tiktoks.txt";

    static TikTokManager()
    {
        TIKTOKS = new List<TikTok>();
        using (StreamReader reader = new StreamReader(FILENAME))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                TIKTOKS.Add(TikTok.Parse(line));
            }
        }
    }

    public static void Initialize()
    {
        TIKTOKS = new List<TikTok>();
        TIKTOKS.Add(new TikTok("User01", 10, "#fun", Audience.World));
        TIKTOKS.Add(new TikTok("User21", 25, "#dance", Audience.Group));
        TIKTOKS.Add(new TikTok("User31", 40, "#music", Audience.Special));
        TIKTOKS.Add(new TikTok("User41", 20, "#comedy", Audience.World));
        TIKTOKS.Add(new TikTok("User51", 35, "#fun", Audience.Group));
        TIKTOKS.Add(new TikTok("User61", 35, "#sports", Audience.Group));
        TIKTOKS.Add(new TikTok("User71", 35, "#fun", Audience.World));
        TIKTOKS.Add(new TikTok("User81", 15, "#media", Audience.Special));
        TIKTOKS.Add(new TikTok("User91", 55, "#sports", Audience.World));
        TIKTOKS.Add(new TikTok("User101", 85, "#dance", Audience.Special));



        // Write TikTok objects to the file
        using (StreamWriter writer = new StreamWriter(FILENAME, true))
        {
            foreach (TikTok tiktok in TIKTOKS)
            {
                writer.WriteLine($"{tiktok.Id}\t{tiktok.Originator}\t{tiktok.Length}\t{tiktok.HashTag}\t{tiktok.Audience}");
            }
        }

    }


    public static void Show()
    {
        foreach (TikTok tiktok in TIKTOKS)
        {
            Console.WriteLine(tiktok);
        }
    }

    public static void Show(string tag)
    {
        foreach (TikTok tiktok in TIKTOKS)
        {
            if (tiktok.HashTag.Equals(tag, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(tiktok);
            }
        }
    }

    public static void Show(int length)
    {
        foreach (TikTok tiktok in TIKTOKS)
        {
            if (tiktok.Length > length)
            {
                Console.WriteLine(tiktok);
            }
        }
    }

    public static void Show(Audience audience)
    {
        foreach (TikTok tiktok in TIKTOKS)
        {
            if (tiktok.Audience == audience)
            {
                Console.WriteLine(tiktok);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        TikTokManager.Initialize();
        Console.WriteLine("All TikToks:");
        TikTokManager.Show();
        Console.WriteLine("\nTikToks with hashtag #fun:");
        TikTokManager.Show("#fun");
        Console.WriteLine("\nTikToks with length greater than 30 seconds:");
        TikTokManager.Show(35);
        Console.WriteLine("\nTikToks for Special Audience:");
        TikTokManager.Show(Audience.Special);
        Console.ReadKey();
    }
}
