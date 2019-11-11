﻿using Interfaces;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Entity : Damageable
    {
        [SerializeField] protected int maxHealth;
        [SerializeField] protected int baseMovementSpeed;
        [SerializeField] protected int baseDamage;
        public int BaseDamage => baseDamage;

        protected Rigidbody2D Rb;
        protected Collider2D Collider2D;

        protected int CurrentHealth;

        protected virtual void Awake()
        {
            Collider2D = GetComponent<Collider2D>();
            Rb = GetComponent<Rigidbody2D>();
            CurrentHealth = maxHealth;
        }

        /// <summary>
        /// Destroys the game object. Allows behaviour before death.
        /// </summary>
        protected virtual void OnDeath() => Destroy(this.gameObject);

        /// <inheritdoc />
        public override void ApplyDamage(int damage)
        {
            base.ApplyDamage(damage);
            CurrentHealth -= damage;
            if (CurrentHealth > 0) return;

            OnDeath();
        }

        public void ResetHealth() => CurrentHealth = maxHealth;
    }
}