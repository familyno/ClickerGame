using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("[HEALTH BARS]")]
    [SerializeField] private HealthBar HealthBarPlayer;
    [SerializeField] private HealthBar HealthBarMonster;

    [Header("[BACKGROUNDS]")]
    [SerializeField] private Sprite[] Backgrounds;
    [SerializeField] private GameObject BackgroundObject;

    [Header("[MONSTERS]")]
    [SerializeField] private GameObject MonsterPosition;
    [SerializeField] private GameObject[] Monsters;

    [Header("[PLAYER]")]
    [SerializeField] private GameObject Player;

    [Header("[COINS]")]
    [SerializeField] private GameObject _coinPrefab;

    [Header("[HUD]")]
    [SerializeField] private Text _playerPowerHUD;
    [SerializeField] private Text _playerRegenerationHUD;
    [SerializeField] private Text _monsterPowerHUD;
    [SerializeField] private Text _monsterRegenerationHUD;

    private Player _player;

    private Monster _currentMonster;

    private Animator _animatorKnight;
    private Animator _animatorMonster;

    public static UnityEvent _monsterDied = new UnityEvent();
    public static UnityEvent _PlayerDied = new UnityEvent();

    private int indexBackground;
    private int indexMonster;

    private float scaleMonster = 1.2f;



    void Start()
    {

        _player = Player.GetComponent<Player>();
                
        NewGame();

        _animatorKnight = Player.GetComponent<Animator>();

        StartCoroutine(ControlGame());

        _monsterDied.AddListener(OnMonsterDied);
        _PlayerDied.AddListener(OnPlayerDied);
        
    }

    private void OnMonsterDied()
    {
        GameObject coin = Instantiate(_coinPrefab) as GameObject;
        

        _animatorMonster.SetTrigger("isDied");

        StartCoroutine(MonsterDied());

        Destroy(coin, 2f);
    }

    private void OnPlayerDied()
    {
        _animatorKnight.SetTrigger("isDied");
    }

    public GameObject GetPlayerObject() { return Player; }
    public Monster GetCurrentMonster() { return _currentMonster; }

    public void SetBackground(int indexBckg)
    {
        BackgroundObject.GetComponent<SpriteRenderer>().sprite = Backgrounds[indexBckg];
    }

    public void SetMonster(int idMonster)
    {
        int maxHealth;
        GameObject monster = Instantiate(Monsters[idMonster]) as GameObject;
        monster.transform.position = MonsterPosition.transform.position;
        monster.transform.localScale = new Vector3(scaleMonster, scaleMonster, scaleMonster);

        Monster monster_ = monster.GetComponent<Monster>();
        _currentMonster = monster_;

        maxHealth = _currentMonster.GetMaxHealth();

        HealthBarMonster.SetMaxHealth(maxHealth);
        HealthBarMonster.SetCurrentHealth(maxHealth, maxHealth);

        _animatorMonster = _currentMonster.GetComponent<Animator>();

    }

    void NewGame()
    {
        indexBackground = Randomize(0, Backgrounds.Length);
        indexMonster = Randomize(0, Monsters.Length);

        SetBackground(indexBackground);
        SetMonster(indexMonster);
        StartCoroutine(MonsterHit(1));
    }

    public static int Randomize(int min, int max)
    {
        System.Random random = new System.Random();
        return random.Next(min, max);
    }

    public static GameController GetGameController() { return GameObject.FindObjectOfType<GameController>(); }

    IEnumerator MonsterHit(float timeSleep)
    {
        while (true)
        {
            yield return new WaitForSeconds(timeSleep);
            _animatorMonster.SetTrigger("isAttack");
        }
    }

    IEnumerator MonsterDied()
    {
        int currentCoins = _player.GetCoins();
        currentCoins += _currentMonster.Reward;

        _player.SetCoins(currentCoins);
        
        yield return new WaitForSeconds(5);
        
        Destroy(_currentMonster.gameObject);
        _currentMonster.Dead = false;
        NewGame();
    }

    IEnumerator ControlGame()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.001f);

            if (Input.GetMouseButtonDown(0))
            {
                _animatorKnight.SetTrigger("isAttack");
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
            _playerPowerHUD.text = _player.GetDamage().ToString();
            _playerRegenerationHUD.text = _player.GetRegeneration().ToString();

            _monsterPowerHUD.text = _currentMonster.GetDamage().ToString();
            _monsterRegenerationHUD.text = _currentMonster.GetRegeneration().ToString();
        }
    }

}
