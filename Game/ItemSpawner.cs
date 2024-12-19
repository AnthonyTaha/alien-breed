using System;
using System.Collections.Generic;
using System.Linq;
using AlienBreed.Game.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienBreed.Game;

public class ItemSpawner
{
    private List<Enemy> _enemies;
    private List<PowerUp> _powerUps;
    
    private Random _random;
    private float _spawnTimer;
    private float _spawnInterval;

    private int _screenWidth, _screenHeight;
    
    private Vector2 _enemyShipSpriteSize;
    private Vector2 _enemyObstacleSpriteSize;
    
    private Vector2 _enemyShipSize;
    private Vector2 _enemyObstacleSize;

    public ItemSpawner(float spawnInterval, int screenWidth, int screenHeight)
    {
        _enemies = new();
        _powerUps = new();
        _random = new Random();
        _spawnTimer = 0f;
        _spawnInterval = spawnInterval;
        _screenWidth = screenWidth;
        _screenHeight = screenHeight;
        _enemyShipSpriteSize = new Vector2(16, 16);
        _enemyShipSize = new Vector2(2, 2);
    }
    public void Update(GameTime gameTime, LevelManager levelManager)
    {
        //Item Spawning
        _spawnTimer += gameTime.ElapsedGameTime.Milliseconds/10.0f;
        if (_spawnTimer >= _spawnInterval)
        {
            int itemChoice = _random.Next(0,100);
            if (itemChoice <= 75)
            {
                AddEnemy();
            }else
            {
                AddPowerUp();
            }
            
            _spawnTimer = 0f;
        }
        
        foreach (Enemy enemy in _enemies.ToList())
        {
            enemy.Update(gameTime, levelManager);
            if (enemy.Rect.Y > _screenHeight)
            {
                RemoveEnemy(enemy);
            }
        }
        foreach (PowerUp powerUp in _powerUps.ToList())
        {
            powerUp.Update(gameTime, levelManager);
            if (powerUp.Rect.Y > _screenHeight)
            {
                RemovePowerUp(powerUp);
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch, SpriteSheet spriteSheet)
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.Draw(spriteBatch,spriteSheet);
        }

        foreach (PowerUp powerUp in _powerUps)
        {
            powerUp.Draw(spriteBatch, spriteSheet);
        }
    }

    public void DecreaseInterval()
    {
        if (_spawnInterval > 50)
        {
            _spawnInterval -= 10;
        }
    }
    public void AddEnemy()
    {
        float xPosition = _random.Next(0, (int)(_screenWidth - (_enemyShipSize.X*_enemyShipSpriteSize.X)));
        Vector2 startPosition = new Vector2(xPosition, -(_enemyShipSize.Y*_enemyShipSpriteSize.Y));
        _enemies.Add(new Enemy(5,startPosition,_enemyShipSize,_enemyShipSpriteSize));
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
    public void AddPowerUp()
    {
        float xPosition = _random.Next(0, (int)(_screenWidth - (_enemyShipSize.X*_enemyShipSpriteSize.X)));
        Vector2 startPosition = new Vector2(xPosition, -(_enemyShipSize.Y*_enemyShipSpriteSize.Y));
        _powerUps.Add(new PowerUp(startPosition));
    }

    public void RemovePowerUp(PowerUp powerUp)
    {
        _powerUps.Remove(powerUp);
    }
    public Enemy[] GetEnemies() => _enemies.ToArray();
}