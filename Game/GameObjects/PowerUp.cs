using Microsoft.Xna.Framework;

namespace AlienQuest.Game.GameObjects;

public class PowerUp:GameObject
{
    private Animation _animation;
    public PowerUp(Vector2 position) : base(3, position, new Vector2(2,2), new Vector2(16,16))
    {
        _animation = new Animation(new []{3,4},200);
        Sprite.AnimationPlayer.Play(_animation);
    }

    public override void Update(GameTime gameTime, LevelManager levelManager)
    {
        base.Update(gameTime, levelManager);
        Position.Y += 2f;
        
        if (levelManager.Player.Rect.Intersects(Rect))
        {
            levelManager.Player.PowerUp();
            levelManager.PowerUpSoundEffect.Play();
            levelManager.RemoveGameObject(this);
        }
        if (Rect.Y > levelManager.ScreenHeight)
        {
            levelManager.RemoveGameObject(this);
        }
    }
}