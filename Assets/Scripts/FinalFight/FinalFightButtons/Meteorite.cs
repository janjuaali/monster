using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField] private LayerMask _ground;
    [SerializeField] private LayerMask _enemy;
    [SerializeField] private float _radius;
    [SerializeField] private ParticleSystem[] _particleSystems;

    private float _time;
    private bool _collided;
    private float _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground))
        {
            Exploode();
            PlayParticleOnImpact();
            StartCoroutine(FadeAnimation(transform.GetChild(0)));
            _collided = true;
        }
    }

    public void Init(float damage, Vector3 destination, float time)
    {
        _damage = damage;
        _time = time;

        StartCoroutine(FlyAnimation(destination));
    }

    private void PlayParticleOnImpact()
    {
        foreach (var particle in _particleSystems)
        {
            particle.Play();
        }
    }

    private IEnumerator FadeAnimation(Transform model)
    {
        float changeSpeed = 1/0.5f;

        yield return new WaitForSeconds(1f);

        while (model.transform.localScale.x > 0)
        {
            model.transform.localScale = Vector3.MoveTowards(model.transform.localScale, Vector3.zero, changeSpeed * Time.deltaTime);

            yield return null;
        }

        Destroy(model.gameObject);
    }

    private void Exploode()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, _radius, _enemy);

        foreach (var collider in enemyColliders)
        {
            if(collider.TryGetComponent(out Monster monster))
            {
                monster.ApplyDamage(_damage);
            }
        }
    }

    private IEnumerator FlyAnimation(Vector3 destination)
    {
        float speed = Vector3.Distance(transform.position, destination) / _time;
        Vector3 direction = destination - transform.position;

        while(_collided == false)
        {
            transform.position += direction.normalized * speed * Time.deltaTime;

            yield return null;
        }
    }
}
