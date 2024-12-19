using System;
using System.Xml.Schema;

namespace AlienQuest.Game.utils;

public class XmlUtils
{
    public static void ValidationCallBack(object? sender, ValidationEventArgs e)
    {
        if (e.Severity.Equals(XmlSeverityType.Warning))
        {
            Console.Write(" WARNING : ");
            Console.WriteLine(e.Message);
        }
        else if (e.Severity.Equals(XmlSeverityType.Error))
        {
            Console.Write(" ERROR : ");
            Console.WriteLine(e.Message);
        }
    }
}