using System;
using System.Xml.Serialization;

namespace AlienQuest.Game;
public class Save
{
    [XmlAttribute("player")] 
    public String Player;
    [XmlAttribute("score")] 
    public int Score;
    [XmlAttribute("date")]
    public String Date;

    public Save(){}
    public Save(String playerName, int score, String date)
    {
        Player = playerName;
        Score = score;
        Date = date;
    }
}