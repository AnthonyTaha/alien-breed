using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienQuest.Game.GameObjects;

public class Enemy:GameObject
{
    private Weapon _weapon;
    private Random _random;
    private bool _canFire;
    private Animation _animation;
    public Enemy(int currentFrame, Vector2 position, Vector2 size, Vector2 spriteSize) : base(currentFrame, position, size, spriteSize)
    {
        _weapon = new Weapon(100,500,0,WeaponType.Enemy);
        _random = new Random();
        if (_random.Next(0, 10) < 3)
        {
            _canFire = true;
        }
        else
        {
            _canFire = false;
        }

        _animation = new Animation(new[] { 5, 6 }, 200);
        Sprite.AnimationPlayer.Play(_animation);
    }

    public override void Update(GameTime gameTime, LevelManager levelManager)
    {
        base.Update(gameTime, levelManager);
        if (_canFire)
        {
            _weapon.Fire(levelManager, Position);
        }
        Position.Y += 3f;
        
        if (levelManager.Player.Rect.Intersects(Rect))
        {
            levelManager.Player.Health-=10;
            levelManager.ExplosionSoundEffect.Play();
            levelManager.RemoveGameObject(this);
        }
    }
    
    
}