using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienBreed.Game.GameObjects;

public class Enemy:GameObject
{
    private Weapon _weapon;
    private Random _random;
    private bool _canFire;
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
    }

    public override void Update(GameTime gameTime, LevelManager objectManager)
    {
        if (_canFire)
        {
            _weapon.Fire(objectManager, Position);
        }
        Position.Y += 3f;
        
        if (objectManager.Player.Rect.Intersects(Rect))
        {
            objectManager.Player.Health-=10;
            objectManager.ExplosionSoundEffect.Play();
            objectManager.ItemSpawner.RemoveEnemy(this);
        }
    }
    
    
}