using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private GameObject _player;
    private GameController _gameController;
    private Monster _currentMonster;
    private Player _classPlayer;


    private void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();

        _player = _gameController.GetPlayerObject();
        _classPlayer = _player.GetComponent<Player>();

        StartCoroutine(ControlDamage());

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "MonsterHit" && other.name != "Knight")
        {
            if (_currentMonster == null)
                return;

            MakeDamage(_currentMonster, _classPlayer.GetDamage());

            //_player.transform.position = new Vector3(-5.12f, -3.48f, 0);
            //Debug.Log("Name: " + other.name);
        }
    }

    void MakeDamage(Monster monster, int damage)
    {
        monster.TakeDamage(damage);
    }

    IEnumerator ControlDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.001f);
            if (_currentMonster == null)
            {
                _currentMonster = _gameController.GetCurrentMonster();
                //break;
            }
        }
    }
}
