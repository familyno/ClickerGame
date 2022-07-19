using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamagble
{
    public bool Dead { get; set; }

    [SerializeField] protected int Damage;
    [SerializeField] protected int MaxHealth;
    [SerializeField] protected int Health;
    [SerializeField] protected int RegenerationAmount;

    protected HealthBar healthBar;

    protected HealthBar GetHealthBar(string nameHealthBar)
    {
        return GameObject.Find(nameHealthBar).GetComponent<HealthBar>();
    }


    public int GetDamage() { return Damage; }
    public int GetMaxHealth() { return MaxHealth; }
    public int GetHealth() { return Health; }
    public void SetDamage(int damage) { Damage = damage; }
    protected void SetMaxHealthBar() { healthBar.SetMaxHealth(MaxHealth); }
    protected void SetHealthBar(int health) { healthBar.SetCurrentHealth(MaxHealth, health); }
    public int GetRegeneration() { return RegenerationAmount; }
    public void SetRegeneration(int amount) { RegenerationAmount = amount; }

    public virtual void TakeDamage(int damage) { }

    protected IEnumerator Regeneration(float seconds)
    {
        while (Health >= 0)
        {
            yield return new WaitForSecondsRealtime(seconds);

            Health += RegenerationAmount;

            if (Health >= MaxHealth)
                Health = MaxHealth;

            SetHealthBar(Health);
        }
    }
}
