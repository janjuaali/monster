using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitParticle;
    [SerializeField] private float _speed;

    public void Init(Monster target, float damage)
    {
        StartCoroutine(Fly(target, damage));
    }

    private IEnumerator Fly(Monster target, float damage)
    {
        float distance = Vector3.Distance(transform.position, target.PointForProjectile.position);

        while(distance> 1f)
        {
            if (target.IsAllive == false || target == null)
                Explode();

            transform.position += transform.forward * _speed * Time.deltaTime;
            transform.LookAt(target.PointForProjectile);

            distance = Vector3.Distance(transform.position, target.PointForProjectile.position);

            yield return null;
        }

        OnHit(target, damage);
    }

    private void OnHit(Monster target, float damage)
    {
        if (target.IsAllive == false || target == null)
        {
            Explode();
            return;
        }
            

        target.ApplyDamage(damage);
        Explode();
    }

    private void Explode()
    {
        Instantiate(_hitParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
