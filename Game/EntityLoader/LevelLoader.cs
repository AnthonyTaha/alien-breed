using System;
using System.IO;
using System.Xml;
using AlienQuest.Game.utils;
using Microsoft.Xna.Framework.Content;

namespace AlienQuest.Game.EntityLoader;

public class LevelLoader
{
    private int _playerYOffset;
    private int _scoreInterval;
    private int _spawnInterval;
    
    public void LoadPlayer(string playerName, ContentManager content)
    {
        var settings = new XmlReaderSettings();
        settings.Schemas.Add("http://example.com/level",content.RootDirectory+"/level.xsd");
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationEventHandler += XmlUtils.ValidationCallBack;
        string filePath = Path.Combine(content.RootDirectory, "level.xml");

        using (XmlReader reader = XmlReader.Create(filePath,settings))
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "level")
                {
                    _playerYOffset = int.Parse(reader.GetAttribute("playerYOffset"));
                    _scoreInterval = int.Parse(reader.GetAttribute("scoreInterval"));
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "itemSpawner")
                {
                    _spawnInterval = int.Parse(reader.GetAttribute("spawnInterval"));
                }
            }
            Console.WriteLine($"PlayerYOffset: {_playerYOffset}");
            Console.WriteLine($"ScoreInterval: {_scoreInterval}");
            Console.WriteLine($"SpawnInterval: {_spawnInterval}");
        }
    }
}