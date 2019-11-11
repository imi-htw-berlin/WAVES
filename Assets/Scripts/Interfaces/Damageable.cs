using UI;
using UnityEngine;

namespace Interfaces
{
    public abstract class Damageable : MonoBehaviour
    {
        [SerializeField] protected int maxHealth;
        [SerializeField] private HealthBar healthBar;


        protected int CurrentHealth;

        /// <summary>
        /// Applies the given amount of damage to the object
        /// </summary>
        /// <param name="damage">The amount of damage to apply</param>
        public virtual void ApplyDamage(int damage)
        {
            if (healthBar != null) healthBar.SetHealthBar(CurrentHealth, maxHealth);
        }
    }
}