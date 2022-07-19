using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    [SerializeField] private Text _textCoins;
    [SerializeField] private StructWeapon[] _weapon;

    public StructWeapon[] CurrentWeapon 
    {
        get { return _weapon; }
        set 
        {
            value = _weapon;
        }
    }

    private GameController _gameController;
    private Monster _currentMonster;

    
    
    private int _coins = 0;
    

    void Start()
    {
        _gameController = GameController.GetGameController();

        healthBar = GetHealthBar("BarFilledPlayer");
        SetMaxHealthBar();
        StartCoroutine(Regeneration(0.1f));
        
        SetCoins(_coins);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MonsterHit")
        {
            _currentMonster = _gameController.GetCurrentMonster();
            int monsterDamage = _currentMonster.GetDamage();
            TakeDamage(monsterDamage);
        }
    }

    public override void TakeDamage(int damage)
    {
        if (!Dead && Health <= 0)
        {
            GameController._PlayerDied.Invoke();
            Health = 0;
        }
        else
            Health -= damage;

        SetHealthBar(Health);
    }

    public void SetWeapon(Weapons currentWeapon)
    {
        string[] swords;
        for (int i = 0; i < CurrentWeapon.Length; i++)
        {
            swords = CurrentWeapon[i].Weapon.ToString().Split('_');

            //if(swords)
            //    CurrentWeapon[i].WeaponMesh.SetActive(false);

            if (CurrentWeapon[i].Weapon == currentWeapon)
            {
                CurrentWeapon[i].WeaponMesh.SetActive(true);
            }
        }
    }

    public int GetCoins()
    {
        return _coins;
    }

    public void SetCoins(int coin)
    {
        _coins = coin;
        _textCoins.text = _coins.ToString();
    }

    public static Player GetPlayer()
    {
        GameController _gameController = GameController.GetGameController();
        GameObject objPlayer = _gameController.GetPlayerObject();
        Player player = objPlayer.GetComponent<Player>();

        return player;
    }
}
