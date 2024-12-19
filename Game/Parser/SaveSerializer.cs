using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;

namespace AlienQuest.Game.Parser;

public class SaveSerializer
{
    public static void SerializeSave(ContentManager contentManager,int gameScore, String playerName,String dateTime)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(contentManager.RootDirectory+"/saves.xml");

        XmlNamespaceManager nsManager = new XmlNamespaceManager(doc.NameTable);
        nsManager.AddNamespace("ns", "http://example.com/saves"); 

        XmlNodeList saveNodes = doc.SelectNodes("//ns:save", nsManager);
        Saves saves = new Saves();
        if (saveNodes != null)
        {
            foreach (XmlNode saveNode in saveNodes)
            {
                Console.WriteLine("test");
                Save save = new Save(saveNode.Attributes["player"]?.Value,
                    int.Parse(saveNode.Attributes["score"]?.Value),saveNode.Attributes["date"]?.Value);
                
                saves.saves.Add(save);
            }
        }
        saves.saves.Add(new Save(playerName, gameScore,dateTime));
        XmlSerializer serializer = new XmlSerializer(typeof(Saves));
        TextWriter textWriter = new StreamWriter(contentManager.RootDirectory+"/saves.xml");
        serializer.Serialize(textWriter, saves);
        textWriter.Close();
    }
}