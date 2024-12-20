using System.Collections.Generic;
using System.Xml.Serialization;

namespace AlienQuest.Game.Utils;
[XmlRoot(ElementName="saves",Namespace = "http://example.com/saves", IsNullable=false)] 
public class Saves
{
    [XmlElement("save")] public List<Save> saves;

    public Saves()
    {
        saves = new List<Save>();
    }


}