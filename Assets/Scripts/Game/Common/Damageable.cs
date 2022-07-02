using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Tooltip("Multiplier to apply to the received damage")]
    private float damageMultiplier = 1f;

    [Range(0, 1)]
    [Tooltip("Multiplier to apply to self damage")]
    private float sensibilityToSelfdamage = 0.5f;

    public Health Health { get; private set; }
    public float DamageMultiplier { get => DamageMultiplier1; set => DamageMultiplier1 = value; }
    public float SensibilityToSelfdamage { get => SensibilityToSelfdamage1; set => SensibilityToSelfdamage1 = value; }
    public float DamageMultiplier1 { get => damageMultiplier; set => damageMultiplier = value; }
    public float SensibilityToSelfdamage1 { get => sensibilityToSelfdamage; set => sensibilityToSelfdamage = value; }

    void Start()
    {
        // find the health component either at the same level, or higher in the hierarchy
        Health = GetComponent<Health>();
        if (!Health)
        {
            Health = GetComponentInParent<Health>();
        }
    }

    public void InflictDamage(float damage, bool isExplosionDamage, GameObject damageSource)
    {
        if (Health)
        {
            var totalDamage = damage;

            // skip the crit multiplier if it's from an explosion
            if (!isExplosionDamage)
            {
                totalDamage *= DamageMultiplier;
            }

            // potentially reduce damages if inflicted by self
            if (Health.gameObject == damageSource)
            {
                totalDamage *= SensibilityToSelfdamage;
            }

            // apply the damages only if affiliation is different
            int HitterAffiliation = damageSource.GetComponent<Actor>().Affiliation;
            int OwnerAffiliation = GetComponent<Actor>().Affiliation;
            if (OwnerAffiliation != HitterAffiliation)
            {
                Debug.Log($"Just got a hit registered of {totalDamage}");

                Health.TakeDamage(totalDamage, damageSource);
            }
        }
    }
}
