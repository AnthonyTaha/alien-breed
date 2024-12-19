using Microsoft.Xna.Framework;

namespace AlienBreed.Game.GameObjects;

public class PowerUp(Vector2 position)
    : GameObject(1, position, new Vector2(2,2), new Vector2(16,16))
{
    public override void Update(GameTime gameTime, LevelManager levelManager)
    {
        Position.Y += 2f;
        
        if (levelManager.Player.Rect.Intersects(Rect))
        {
            levelManager.Player.PowerUp();
            levelManager.PowerUpSoundEffect.Play();
            levelManager.ItemSpawner.RemovePowerUp(this);
        }
    }
}