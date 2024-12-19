using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AlienQuest.Game.GameObjects;

public class Player:GameObject
{
    private float _speed;
    private Weapon _weapon;
    
    private PlayerLoader _playerLoader;
    private int _health;
    private Animation _idleAnimation;
    
    public int Health => _health;
    
    public Player(Vector2 position,String name, ContentManager contentManager):base(2,position,new Vector2(2,2),new Vector2(16,24))
    {
        _playerLoader = new PlayerLoader();
        _playerLoader.LoadPlayer(name,contentManager);
        _health = _playerLoader.Health;
        _speed =_playerLoader.Speed ;
        _weapon = new Weapon(_playerLoader.WeaponCoolDown,300,_playerLoader.PowerUpCoolDown);
        _idleAnimation = new Animation(new []{1,2},200);
        Sprite.AnimationPlayer.Play(_idleAnimation);
    }

    public override void Update(GameTime gameTime, LevelManager levelManager)
    {
        base.Update(gameTime, levelManager);
        
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
        
        if (_health <= 0)
        {
            levelManager.PlayerDeath();
        }
        _weapon.updatePowerUpCoolDown(gameTime);
    }

    public void Damage(int damage)
    {
        _health-=damage;
    }
    public void PowerUp()
    {
        _weapon.PowerUp();
    }
    
}