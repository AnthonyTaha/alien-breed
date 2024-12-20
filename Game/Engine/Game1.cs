using System;
using AlienQuest.Game.Parser;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlienQuest.Game.Engine;

public class Game1 : Microsoft.Xna.Framework.Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private LevelManager _levelManager;
    private SpriteSheet _spriteSheet;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _spriteSheet = new SpriteSheet();
    }

    protected override void Initialize()
    {
        _levelManager = new LevelManager();
        
        base.Initialize();
        
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _spriteSheet.LoadSheet(Content,"sprites/sprite_sheet",12,2);
        _levelManager.LoadContent(Content,GraphicsDevice,_spriteSheet);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        if (_levelManager.GameState == GameState.Dead)
        {
            SaveSerializer.SerializeSave(Content,_levelManager.Score,_levelManager.Player.Name,
                DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
            Exit();
        }
        _levelManager.UpdateGameObjects(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black );
        _spriteBatch.Begin(transformMatrix:_levelManager.Camera.GetTransformMatrix(),samplerState: SamplerState.PointClamp);
        _levelManager.DrawGameObjects(_spriteBatch,_spriteSheet);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}