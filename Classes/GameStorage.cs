using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class PlayerData
{
    public string Name { get; set; } = "";
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Pushes { get; set; }
}

public class GameStorage
{
    private string filePath = "players.json";

    public string ErrorMessage { get; private set; } = "";

    public bool Save(List<Player> players)
    {
        ErrorMessage = "";

        List<PlayerData> savedPlayers = new List<PlayerData>();

        foreach (Player player in players)
        {
            PlayerData data = new PlayerData();

            data.Name = player.Name;
            data.Wins = player.Wins;
            data.Losses = player.Losses;
            data.Pushes = player.Pushes;

            savedPlayers.Add(data);
        }

        try
        {
            string json = JsonSerializer.Serialize(savedPlayers);

            File.WriteAllText(filePath, json);

            return true;
        }
        catch (IOException)
        {
            ErrorMessage = "Det gick inte att spara spelarna.";
        }
        catch (UnauthorizedAccessException)
        {
            ErrorMessage = "Programmet får inte skriva till filen.";
        }

        return false;
    }

    public List<Player> Load()
    {
        ErrorMessage = "";

        List<Player> players = new List<Player>();

        try
        {
            if (!File.Exists(filePath))
            {
                return players;
            }

            string json = File.ReadAllText(filePath);

            List<PlayerData>? savedPlayers =
                JsonSerializer.Deserialize<List<PlayerData>>(json);

            if (savedPlayers == null)
            {
                ErrorMessage = "Sparfilen innehåller ingen spelarlista.";
                return players;
            }

            foreach (PlayerData data in savedPlayers)
            {
                if (data == null ||
                    string.IsNullOrWhiteSpace(data.Name) ||
                    data.Wins < 0 ||
                    data.Losses < 0 ||
                    data.Pushes < 0)
                {
                    ErrorMessage =
                        "Sparfilen innehåller felaktiga spelaruppgifter.";

                    return new List<Player>();
                }
            }

            foreach (PlayerData data in savedPlayers)
            {
                Player player = new Player(data.Name);

                player.Wins = data.Wins;
                player.Losses = data.Losses;
                player.Pushes = data.Pushes;

                players.Add(player);
            }

            return players;
        }
        catch (JsonException)
        {
            ErrorMessage = "Sparfilen har fel format.";
        }
        catch (IOException)
        {
            ErrorMessage = "Det gick inte att läsa sparfilen.";
        }
        catch (UnauthorizedAccessException)
        {
            ErrorMessage = "Programmet får inte läsa sparfilen.";
        }

        return players;
    }
}