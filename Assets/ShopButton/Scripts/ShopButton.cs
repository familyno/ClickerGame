using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ShopButton : MonoBehaviour, IPointerClickHandler
{
    [Header("POWER")]
    [SerializeField] private Text _powerText;
    [SerializeField] private int _power;
    [Header("PRICE")]
    [SerializeField] private Text _priceText;
    [SerializeField] private int _price;
    [Header("REGENERATION")]
    [SerializeField] private int _regeneration;
    [Header("WEAPON")]
    [SerializeField] private Weapons _weapon;


    private Player _player;



    void Start()
    {
        _player = Player.GetPlayer();

        _powerText.text = _power > 0 ? "+" + _power.ToString() : "+" + _regeneration.ToString();
        _priceText.text = _price.ToString();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        int currentMoney = _player.GetCoins();
        if (currentMoney >= _price)
        {
            int currentDamage = _player.GetDamage();
            currentDamage += _power;
            currentMoney -= _price;
            
            int currentRegeneration = _player.GetRegeneration();
            currentRegeneration += _regeneration;

            _player.SetDamage(currentDamage);
            _player.SetRegeneration(currentRegeneration);
            _player.SetCoins(currentMoney);
            _player.SetWeapon(_weapon);

            Destroy(gameObject);
        }
    }
}
