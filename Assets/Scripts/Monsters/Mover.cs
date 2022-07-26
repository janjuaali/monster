using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Monster _monster;

    public void MoveTo(Monster monster, MonsterPlaceAccepter monsterPlaceAccepter, Vector3 destination)
    {
        _monster = monster;
        StartCoroutine(MoveAnimation(destination, monsterPlaceAccepter));
    }

    public void MoveTo(Monster monster, MonsterCell monsterPlaceAccepter, Vector3 destination)
    {
        _monster = monster;
        StartCoroutine(MoveAnimation(destination, monsterPlaceAccepter));
    }

    private IEnumerator MoveAnimation(Vector3 destinationPoint, IMonsterHolder monsterHolder)
    {
        _monster.MonsterAnimator.RunAnimation();

        float distance = Vector3.Distance(_monster.transform.position, destinationPoint);

        while (distance > 0.2f)
        {
            Vector3 direction = (destinationPoint- transform.position).normalized;

            Rotate(direction);

            transform.position += (transform.forward * 10f * Time.deltaTime);

            distance = Vector3.Distance(_monster.transform.position, destinationPoint);

            yield return null;
        }


        monsterHolder.TryAcquireMonster(_monster);
        _monster.MonsterAnimator.IdleAnimation();
    }

    private void Rotate(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = lookRotation;
    }

}
