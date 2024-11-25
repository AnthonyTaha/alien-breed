using AlienBreed;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine;

public class GameObject {
    
    private Texture2D _texture;
    protected Vector2 _position;
    private int _size = 100;
    private static readonly int _sizeMin = 10;
    private static readonly int _sizeMax = 100;
    private Color _color = Color.White;
    
    public Texture2D _Texture { get => _texture; init => _texture = value; }
    public int _Size { get => _size; set => _size = value>=_sizeMin&&value<=_sizeMax?value:_sizeMin; }
    public Rectangle _Rect { get => new Rectangle((int) _position.X, (int) _position.Y, _size, _size); }

    public GameObject(Texture2D texture, Vector2 position, int size) {
        _Texture = texture;
        _position = position; 
        _Size = size;
    }
    
    public virtual void Update(GameTime gameTime, GameObjectManager gameObjectManager) {
        
    }
    
    public void Draw(SpriteBatch spriteBatch) {
        var origin = new Vector2(_texture.Width / 2f, _texture.Height / 2f);
        spriteBatch.Draw(   _texture, // Texture2D,
            _Rect, // Rectangle destinationRectangle,
            null, // Nullable<Rectangle> sourceRectangle,
            _color, //  Color,
            0.0f, //  float rotation,
            origin,  // Vector2 origin,
            SpriteEffects.None, // SpriteEffects effects,
            0f ); // float layerDepth
    }
    
}
