public interface IDamagble
{
    public int GetDamage();
    public int GetMaxHealth();
    public int GetHealth();
    public void SetDamage(int damage);
    public abstract void TakeDamage(int damage);
}
