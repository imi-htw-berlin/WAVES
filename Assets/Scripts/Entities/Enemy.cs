﻿using System;
using Interfaces;
using UnityEngine;
using Utils;

namespace Entities
{
    public class Enemy : Entity
    {
        [SerializeField] private Transform target;
        [SerializeField] private float maxVelocity;
        [SerializeField] private Range attackJumpForceX;
        [SerializeField] private Range attackJumpForceY;
        [SerializeField] private Range attackRepulseForce;

        private bool TargetToLeft => transform.position.x > target.position.x;

        private void FixedUpdate() => MoveToTarget();


        public void SetTarget(Transform targetTransform) => this.target = targetTransform;

        public void Attack()
        {
            Rb.AddForce(new Vector2(
                attackJumpForceX.GetRandom() * (TargetToLeft ? -1 : 1),
                attackJumpForceY.GetRandom())
            );
        }

        private void MoveToTarget()
        {
            Vector3 position = transform.position;
            if (Math.Abs(Rb.velocity.x) < maxVelocity)
                Rb.AddForce(new Vector2(baseMovementSpeed * Time.fixedDeltaTime * (TargetToLeft ? -1 : 1), 0));
        }

        private void GetRepulsed() =>
            Rb.AddForce(new Vector2(attackRepulseForce.GetRandom() * (TargetToLeft ? 1 : -1), 0f));

        private void OnCollisionEnter2D(Collision2D other)
        {
            Damageable damageable = other.gameObject.GetComponent<Damageable>();
            if (damageable == null) return;
            damageable.ApplyDamage(baseDamage);
            GetRepulsed();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Damageable damageable = other.gameObject.GetComponent<Damageable>();
            if (damageable == null) return;
            Attack();
        }
    }
}