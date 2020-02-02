
public class LifeBarSystem
{
    private float health;
    private float healthMax;

    public LifeBarSystem(float healthMax,float startHealth)
    {
        this.healthMax = healthMax;
        health = startHealth;
    }
    public float GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return (float)health / healthMax ;
    }
    public void Damage(float damage)
    {
        health -= damage;
        if(health < 0)
        {
            health = 0;
        }
    }
    public void Heal(float heal)
    {
        health += heal; 
        if(health > healthMax)
        {
            health = healthMax;
        }
    }
}
