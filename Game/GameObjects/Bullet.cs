using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienBreed.Game.GameObjects;

public class Bullet:GameObject
{
    public Vector2 Direction { get; private set; }
    public float Speed { get; private set; }
    private bool _isEnemy;
    
    public Bullet( Vector2 direction, float speed,int currentFrame,bool isEnemy, Vector2 position, Vector2 size, Vector2 spriteSize) : base(currentFrame, position, size, spriteSize)
    {
        this.Direction = direction;
        this.Speed = speed;
        _isEnemy = isEnemy;
    }

    public override void Update(GameTime gameTime, LevelManager levelManager)
    {
        // Update bullet position
        Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (!_isEnemy)
        {
            foreach (Enemy enemy in levelManager.ItemSpawner.GetEnemies())
            {
                if (enemy.Rect.Intersects(Rect))
                {
                    levelManager.ItemSpawner.RemoveEnemy(enemy);
                    levelManager.IncreaseScore();
                    levelManager.RemoveBullet(this);
                }
            }
        }
        else
        {
            if (levelManager.Player.Rect.Intersects(Rect))
            {
                levelManager.Player.Health--;
                levelManager.RemoveBullet(this);
            }
        }
        

        if (!_isEnemy && Position.Y <0)
        {
            levelManager.RemoveBullet(this);
        }else if (Position.Y > levelManager.ScreenHeight)
        {
            levelManager.RemoveBullet(this);
        }
    }
    
}