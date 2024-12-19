using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AlienBreed.Game.GameObjects;

public class Player:GameObject
{
    private float _speed;
    private Weapon _weapon;
    public int Health { get; set; }
    
    public Player(Vector2 position,float speed):base(2,position,new Vector2(2,2),new Vector2(16,24))
    {
        Health = 30;
        _speed = speed;
        _weapon = new Weapon(5,300,6000);
        
    }

    public override void Update(GameTime gameTime, LevelManager levelManager)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.A) && Position.X != Rect.Width)
        {
            Position.X -= _speed;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D)&& Position.X != (levelManager.ScreenWidth - Rect.Width))
        {
            Position.X += _speed;
        }
        
        if (Keyboard.GetState().IsKeyDown(Keys.Space)&& _weapon.Fire(levelManager,Position))
        {
            levelManager.Camera.Shake(7f, 0.1f);
            levelManager.FiringSoundEffect.Play();
        }
        
        if (Health <= 0)
        {
            levelManager.PlayerDeath();
        }
        _weapon.updatePowerUpCoolDown(gameTime);
    }

    public void PowerUp()
    {
        _weapon.PowerUp();
    }
    
}