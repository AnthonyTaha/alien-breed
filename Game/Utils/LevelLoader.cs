using System;
using System.IO;
using System.Xml;
using AlienQuest.Game.utils;
using Microsoft.Xna.Framework.Content;

namespace AlienQuest.Game.Utils;

public class LevelLoader
{
    private int _playerYOffset;
    private int _scoreInterval;
    private int _spawnInterval;

    public int PlayerYOffset => _playerYOffset;

    public int ScoreInterval => _scoreInterval;

    public int SpawnInterval => _spawnInterval;

    public void LoadLevel(ContentManager content)
    {
        var settings = new XmlReaderSettings();
        settings.Schemas.Add("http://www.uga.fr/l3-miage/alien-quest/levelSettings",content.RootDirectory+"/level_settings.xsd");
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationEventHandler += XmlUtils.ValidationCallBack;
        string filePath = Path.Combine(content.RootDirectory, "level_settings.xml");

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
                    break;
                }
            }
            Console.WriteLine($"PlayerYOffset: {_playerYOffset}");
            Console.WriteLine($"ScoreInterval: {_scoreInterval}");
            Console.WriteLine($"SpawnInterval: {_spawnInterval}");
        }
    }
}