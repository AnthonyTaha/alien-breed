using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlienBreed;

public class Player:GameObject
{
    public Player(Texture2D texture, Vector2 position, int size):base(texture,position,size)
    {
            
    }

    public override void Update(GameTime gameTime, GameObjectManager objectManager)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            _position.Y -= 1;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            _position.Y += 1;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            _position.X -= 1;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            _position.X += 1;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            objectManager.addGameObject(new Bullet(_Texture,_position,10));
        }
    }
}