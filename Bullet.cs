using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienBreed;

public class Bullet:GameObject
{
    public Bullet(Texture2D texture2D, Vector2 position, int size) : base(texture2D, position, size)
    {
        
    }

    public override void Update(GameTime gameTime, GameObjectManager gameObjectManager)
    {
        _position.X += gameTime.ElapsedGameTime.Milliseconds * 2.5f;
    }
}