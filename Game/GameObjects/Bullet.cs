using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienQuest.Game.GameObjects;

public class Bullet:GameObject
{
    private Vector2 _direction;
    private float _speed;
    private bool _isEnemy;
    private int _playerDamage;
    
    public Bullet( Vector2 direction, float speed,bool isEnemy, Vector2 position) : base(17, position, new Vector2(2,2),new Vector2(16,16))
    {
        _direction = direction;
        _speed = speed;
        _isEnemy = isEnemy;
        _playerDamage = 5;
    }

    public override void Update(GameTime gameTime, LevelManager levelManager)
    {
        Position += _direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (!_isEnemy)
        {
            foreach (Enemy enemy in levelManager.GameObjects.OfType<Enemy>().ToList())
            {
                if (enemy.Rect.Intersects(Rect))
                {
                    levelManager.RemoveGameObject(enemy);
                    levelManager.IncreaseScore();
                    levelManager.AddGameObject(new Explosion(Position));
                    levelManager.ExplosionSoundEffect.Play();
                    levelManager.RemoveGameObject(this);
                }
            }
        }
        else
        {
            if (levelManager.Player.Rect.Intersects(Rect))
            {
                levelManager.Player.Damage(_playerDamage);
                levelManager.RemoveGameObject(this);
            }
        }
        

        if (!_isEnemy && Position.Y <0)
        {
            levelManager.RemoveGameObject(this);
        }else if (Position.Y > levelManager.ScreenHeight)
        {
            levelManager.RemoveGameObject(this);
        }
    }
    
}