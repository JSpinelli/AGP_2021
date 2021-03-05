using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using SimpleJSON;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class Week5 : MonoBehaviour
{
    /*
     * Write a CSV parser - that takes in a CSV file of players and returns a list of those players as class objects.
     * To help you out, I've defined the player class below.  An example save file is in the folder "CSV Examples".
     *
     * NOTES:
     *     - the first row of the file has the column headings: don't include those!
     *     - location is tricky - because the location has a comma in it!!
     */

    private class Player
    {
        public enum Class : byte
        {
            Undefined = 0,
            Monk,
            Wizard,
            Druid,
            Thief,
            Sorcerer
        }

        public Class classType;
        public string name;
        public uint maxHealth;
        public int[] stats;
        public bool alive;
        public Vector2 location;
    }

    private List<Player> CSVParser(TextAsset toParse)
    {
        var toReturn = new List<Player>();

        string[] lines = toParse.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            if (values.Length > 4)
            {
                Player pl = new Player();
                pl.name = values[0];
                pl.maxHealth = uint.Parse(values[2]);
                pl.stats = Array.ConvertAll(SubArray(values, 3, 5), int.Parse);
                pl.alive = values[8] == "TRUE";
                pl.location = new Vector2(
                    float.Parse((values[9].Substring(1, values[9].Length - 1))),
                    float.Parse(values[10].Substring(0, values[10].Length - 2)));
                switch (values[1])
                {
                    case "Monk":
                    {
                        pl.classType = Player.Class.Monk;
                        break;
                    }
                    case "Wizard":
                    {
                        pl.classType = Player.Class.Wizard;
                        break;
                    }
                    case "Druid":
                    {
                        pl.classType = Player.Class.Druid;
                        break;
                    }
                    case "Thief":
                    {
                        pl.classType = Player.Class.Thief;
                        break;
                    }
                    case "Sorcerer":
                    {
                        pl.classType = Player.Class.Sorcerer;
                        break;
                    }
                    default:
                    {
                        pl.classType = 0;
                        break;
                    }
                }

                toReturn.Add(pl);
            }
        }

        return toReturn;
    }

    public string[] SubArray(string[] array, int offset, int length)
    {
        string[] result = new string[length];
        Array.Copy(array, offset, result, 0, length);
        return result;
    }

    /*
     * Provided is a high score list as a JSON file.  Create the functions that will find the highest scoring name, and
     * the number of people with a score above a score.
     *
     * I've included a library "SimpleJSON", which parses JSON into a dictionary of strings to strings or dictionaries
     * of strings.
     *
     * JSON.Parse(someJSONString)["someKey"] will return either a string value, or a Dictionary of strings to
     * JSON objects.
     */

    public int NumberAboveScore(TextAsset jsonFile, int score)
    {
        var toReturn = 0;
        var myObject = JSON.Parse(jsonFile.text);
        foreach (var scores in myObject["highScores"])
        {
            if (scores.Value["score"] > score)
            {
                toReturn++;
            }
        }

        return toReturn;
    }

    public string GetHighScoreName(TextAsset jsonFile)
    {
        var myObject = JSON.Parse(jsonFile.text);
        var currentTop = 0;
        var currentTopHolder = "";
        foreach (var scores in myObject["highScores"])
        {
            if (scores.Value["score"] > currentTop)
            {
                currentTop = scores.Value["score"];
                currentTopHolder = scores.Value["player"];
            }
        }

        return currentTopHolder;
    }

    // =========================== DON'T EDIT BELOW THIS LINE =========================== //

    public TextMeshProUGUI csvTest, networkTest;
    public TextAsset csvExample, jsonExample;
    private Coroutine checkingScores;

    private void Update()
    {
        csvTest.text = "CSV Parser\n<align=left>\n";

        var parsedPlayers1 = CSVParser(csvExample);

        if (parsedPlayers1.Count == 0)
        {
            csvTest.text += "Need to return some players.";
        }
        else
        {
            csvTest.text += Success(parsedPlayers1.Any(p => p.name == "Jeff") &&
                                    parsedPlayers1.Any(p => p.name == "Stonks")
            ) + " read in player names correctly.\n";
            csvTest.text +=
                Success(parsedPlayers1.First(p => p.name == "Jeff").alive &&
                        !parsedPlayers1.First(p => p.name == "Stonks").alive) + " Correctly read 'alive'.\n";
            csvTest.text +=
                Success(parsedPlayers1.First(p => p.name == "Stonks").classType == Player.Class.Wizard &&
                        parsedPlayers1.First(p => p.name == "Twergle").classType == Player.Class.Thief) +
                " Correctly read player class.\n";
            csvTest.text +=
                Success(parsedPlayers1.First(p => p.name == "Fortune").location == new Vector2(12.322f, 42f)) +
                " Correctly read in location.\n";
            csvTest.text += Success(parsedPlayers1.First(p => p.name == "Jeff").maxHealth == 23) +
                            " Correctly read in max health.\n";
            csvTest.text +=
                Success(parsedPlayers1.First(p => p.name == "Fortune").location == new Vector2(12.322f, 42f)) +
                " Correctly read in location.\n";
        }

        networkTest.text = "JSON Data\n<align=left>\n";
        networkTest.text += Success(NumberAboveScore(jsonExample, 10) == 6) + " number above score worked correctly.\n";
        networkTest.text +=
            Success(GetHighScoreName(jsonExample) == "GUW") + " get high score name worked correctly.\n";
    }

    private string Success(bool test)
    {
        return test ? "<color=\"green\">PASS</color>" : "<color=\"red\">FAIL</color>";
    }
}