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
                levelManager.Player.Health--;
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