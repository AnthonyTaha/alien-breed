using System;
using System.Collections.Generic;
using System.Linq;
using AlienQuest.Game.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienQuest.Game;

public class ItemSpawner
{
    
    private Random _random;
    private float _spawnTimer;
    private float _spawnInterval;
    private float _minSpawnInterval;
    
    
    private Vector2 _enemyShipSpriteSize;
    private Vector2 _enemyShipSize;

    public ItemSpawner(float spawnInterval)
    {
        _random = new Random();
        _spawnTimer = 0f;
        _spawnInterval = spawnInterval;
        _enemyShipSpriteSize = new Vector2(16, 16);
        _enemyShipSize = new Vector2(2, 2);
        _minSpawnInterval = 20;
    }
    public void Update(GameTime gameTime, LevelManager levelManager)
    {
        //Item Spawning
        _spawnTimer += gameTime.ElapsedGameTime.Milliseconds/10.0f;
        if (_spawnTimer >= _spawnInterval)
        {
            int itemChoice = _random.Next(0,100);
            if (itemChoice <= 90)
            {
                AddEnemy(levelManager);
            }else
            {
                AddPowerUp(levelManager);
            }
            _spawnTimer = 0f;
        }
        
    }
    

    public void DecreaseInterval()
    {
        if (_spawnInterval > _minSpawnInterval)
        {
            _spawnInterval -= 10;
        }
    }
    public void AddEnemy(LevelManager levelManager)
    {
        float xPosition = _random.Next((int)(_enemyShipSize.X*_enemyShipSpriteSize.X), (int)(levelManager.ScreenWidth - (_enemyShipSize.X*_enemyShipSpriteSize.X)));
        Vector2 startPosition = new Vector2(xPosition, -(_enemyShipSize.Y*_enemyShipSpriteSize.Y));
        levelManager.AddGameObject(new Enemy(5,startPosition,_enemyShipSize,_enemyShipSpriteSize));
    }
    
    public void AddPowerUp(LevelManager levelManager)
    {
        float xPosition = _random.Next(0, (int)(levelManager.ScreenWidth - (_enemyShipSize.X*_enemyShipSpriteSize.X)));
        Vector2 startPosition = new Vector2(xPosition, -(_enemyShipSize.Y*_enemyShipSpriteSize.Y));
        levelManager.AddGameObject(new PowerUp(startPosition));
    }
    
}