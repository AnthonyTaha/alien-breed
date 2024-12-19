using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienBreed.Game;

public class Sprite
{
    private Color _color;
    private Vector2 _size;
    private int _currentSprite;
    public Vector2 Size => _size;
    public Sprite(int currentSprite, Vector2 size, Color color)
    {
        _currentSprite = currentSprite;
        _color = color;
        _size = size;
    }

   
    public void Draw(SpriteBatch spriteBatch, SpriteSheet spriteSheet, Rectangle rect)
    {
        var origin = new Vector2(rect.Width / 2f, rect.Height / 2f);
        spriteBatch.Draw(   spriteSheet.SpriteSheetRef, // Texture2D,
            rect, // Rectangle destinationRectangle,
            spriteSheet.GetSprite(_currentSprite,(int)_size.X,(int)_size.Y), // Nullable<Rectangle> sourceRectangle,
            _color, //  Color,
            0, //  float rotation,
            origin,  // Vector2 origin,
            SpriteEffects.None, // SpriteEffects effects,
            0f ); // float layerDepth
    }
    
}