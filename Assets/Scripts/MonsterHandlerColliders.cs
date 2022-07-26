using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHandlerColliders : MonoBehaviour
{
    private Dictionary<MonsterPlace, BoxCollider> _boxColliders = new Dictionary<MonsterPlace, BoxCollider>();

    public void CreateBoxCollider(MonsterPlace monsterPlace)
    {
        if (_boxColliders.TryGetValue(monsterPlace, out BoxCollider existCollider))
        {
            EnableCollider(monsterPlace);
            return;
        }

        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        SetColliderPosition(monsterPlace, boxCollider);
        _boxColliders.Add(monsterPlace, boxCollider);
    }

    public void DisableCollider(MonsterPlace monsterPlace)
    {
        if (_boxColliders.TryGetValue(monsterPlace, out BoxCollider existCollider))
            existCollider.enabled = false;
    }

    public void EnableCollider(MonsterPlace monsterPlace)
    {
        if (_boxColliders.TryGetValue(monsterPlace, out BoxCollider existCollider))
            existCollider.enabled = true;
    }

    public void MoveCollider(MonsterPlace monsterPlace)
    {
        if (_boxColliders.TryGetValue(monsterPlace, out BoxCollider existCollider))
        {
            SetColliderPosition(monsterPlace, existCollider);
        }
    }

    private void SetColliderPosition(MonsterPlace monsterPlace, BoxCollider boxCollider)
    {
        boxCollider.center = monsterPlace.transform.localPosition;
        boxCollider.center = new Vector3(boxCollider.center.x, 0.5f, boxCollider.center.z);
    }
}
