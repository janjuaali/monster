using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Interactable : MonoBehaviour, IUsable
{
    [SerializeField] protected bool _infiniteUse;

    private BoxCollider _boxCollider;
    private bool _isUsed;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out MonstersHandler monstersHandler))
        {
            if (_isUsed && _infiniteUse == false)
                return;

            _isUsed = true;

            Use(monstersHandler);
        }

        if(other.TryGetComponent(out Monster monster))
        {
            Use(monster);
        }

        if (other.TryGetComponent(out MonsterPlace monsterPlace))
            Use(monsterPlace);
    }

    public virtual void Use(MonstersHandler monstersHandler) { }
    public virtual void Use(Monster monster) { }
    public virtual void Use(MonsterPlace monsterPlace) { }
}
