using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlienQuest.Game.Engine;

public class SpriteSheet
{
    private Texture2D _spriteSheet;
    private int _sheetWidth;
    private int _sheetHeight;

    
    public Texture2D SpriteSheetRef => _spriteSheet;
    
    public void LoadSheet(ContentManager content,String sheetName,int sheetWidth,int sheetHeight)
    {
        _spriteSheet = content.Load<Texture2D>(sheetName);
        _sheetWidth = sheetWidth;
        _sheetHeight = sheetHeight;
    }

    public Rectangle GetSprite(int id, int width, int height)
    {
        Rectangle sourceRect = new((id%_sheetWidth)*width, ((id+1)/width)*height, width, height);
        return sourceRect;
    }
}