using System;
using System.Xml.Serialization;

namespace AlienQuest.Game;
public class Save
{
    [XmlAttribute("player")] 
    public String Player;
    [XmlAttribute("score")] 
    public int Score;

    public Save(){}
    public Save(string playerName, int score)
    {
        Player = playerName;
        Score = score;
    }
}