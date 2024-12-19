using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using AlienQuest.Game.utils;
using Microsoft.Xna.Framework.Content;

namespace AlienQuest.Game;

public class PlayerLoader
{
    private int _health;
    private float _speed;
    private float _powerUpCoolDown;
    private float _weaponCoolDown;

    public int Health => _health;

    public float Speed => _speed;

    public float PowerUpCoolDown => _powerUpCoolDown;

    public float WeaponCoolDown => _weaponCoolDown;

    public void LoadPlayer(string playerName, ContentManager content)
    {
        var settings = new XmlReaderSettings();
        settings.Schemas.Add("http://example.com/players",content.RootDirectory+"/players.xsd");
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationEventHandler += XmlUtils.ValidationCallBack;
        string filePath = Path.Combine(content.RootDirectory, "players.xml");

        using (XmlReader reader = XmlReader.Create(filePath,settings))
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "player")
                {
                    string name = reader.GetAttribute("name");
                    if (name == playerName)
                    {
                        _health = int.Parse(reader.GetAttribute("health"));
                        _speed = float.Parse(reader.GetAttribute("speed"));
                        _weaponCoolDown = float.Parse(reader.GetAttribute("weaponCoolDown"));
                        _powerUpCoolDown = float.Parse(reader.GetAttribute("powerUpCoolDown"));
                        
                        Console.WriteLine($"Name: {name}");
                        Console.WriteLine($"Health: {_health}");
                        Console.WriteLine($"Speed: {_speed}");
                        Console.WriteLine($"Weapon Cool Down: {_weaponCoolDown}");
                        Console.WriteLine($"Power Up Cool Down: {_powerUpCoolDown}");
                        break;
                    }
                }
            }
        }
    }
    
}