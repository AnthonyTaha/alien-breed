using System;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework.Content;

namespace AlienBreed.Game;

public class PlayerLoader
{
    public int Health;
    public float Speed;
    public void LoadPlayer(string playerName, ContentManager content)
    {
        string filePath = Path.Combine(content.RootDirectory, "players.xml");
        // Replace with your XML file's path
        string playerNameToFind = "player1"; // Specify the player's name you want to find

        using (XmlReader reader = XmlReader.Create(filePath))
        {
            while (reader.Read())
            {
                // Check if the current node is an element and is named "Player"
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "player")
                {
                    string name = reader.GetAttribute("name");
                    if (name == playerNameToFind)
                    {
                        // Retrieve other attributes
                        Health = int.Parse(reader.GetAttribute("health"));
                        Speed = float.Parse(reader.GetAttribute("speed"));

                        // Output the player's attributes
                        Console.WriteLine($"Name: {name}");
                        Console.WriteLine($"Health: {Health}");
                        Console.WriteLine($"Speed: {Speed}");
                        break;
                    }
                }
            }
        }
    }
}