using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AlienBreed.Game.GameObjects;

public class Weapon
{

    private float _coolDown;
    private float _currentCoolDown;
    private float _speed;
    private WeaponType _weaponType;

    private float _powerUpCoolDown; 
    private float _currentPowerUpCoolDown;
    
    public Weapon(float coolDown, float speed, float powerUpCoolDown)
    {
        _coolDown = coolDown;
        _currentCoolDown = coolDown;
        _speed = speed;
        _weaponType = WeaponType.Default;
        _powerUpCoolDown = powerUpCoolDown;
        _currentPowerUpCoolDown = powerUpCoolDown;
    }

    public Weapon(float coolDown, float speed, float powerUpCoolDown,WeaponType type)
    {
        _coolDown = coolDown;
        _currentCoolDown = coolDown;
        _speed = speed;
        _weaponType = type;
        _powerUpCoolDown = powerUpCoolDown;
        _currentPowerUpCoolDown = powerUpCoolDown;
    }
    public bool Fire(LevelManager levelManager, Vector2 startingPosition)
    {
        if (_currentCoolDown == _coolDown)
        {
            if (_weaponType == WeaponType.Default)
            {
                levelManager.AddGameObject(new Bullet(new Vector2(0, -1), _speed,17,false,startingPosition,
                    new Vector2(2,2),new Vector2(16,16)));
            }
            else if(_weaponType == WeaponType.PowerUp)
            {
                levelManager.AddGameObject(new Bullet(new Vector2(1, -1), _speed,17,false,startingPosition,
                    new Vector2(2,2),new Vector2(16,16)));
                levelManager.AddGameObject(new Bullet(new Vector2(0, -1), _speed,17,false,startingPosition,
                    new Vector2(2,2),new Vector2(16,16)));
                levelManager.AddGameObject(new Bullet(new Vector2(-1, -1), _speed,17,false,startingPosition,
                    new Vector2(2,2),new Vector2(16,16)));
            }
            else
            {
                levelManager.AddGameObject(new Bullet(new Vector2(0, 1), _speed,17,true,startingPosition,
                    new Vector2(2,2),new Vector2(16,16)));
            }
            
            _currentCoolDown = 0;
            return true;
        }
        
        _currentCoolDown += 0.5f;
        return false;
    }

    public void updatePowerUpCoolDown(GameTime gameTime)
    {
        if (_weaponType == WeaponType.PowerUp)
        {
            _currentPowerUpCoolDown -= gameTime.ElapsedGameTime.Milliseconds;
            if (_currentPowerUpCoolDown <= 0)
            {
                _weaponType = WeaponType.Default;
            }
        }
        
    }
    public void PowerUp()
    {
        if (_weaponType == WeaponType.Default)
        {
            _weaponType = WeaponType.PowerUp;
            _currentPowerUpCoolDown = _powerUpCoolDown;
        }
    }

}