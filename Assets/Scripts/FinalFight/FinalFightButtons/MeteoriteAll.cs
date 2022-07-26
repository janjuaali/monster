using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteAll : AbilitiyButtonView
{
    [SerializeField] private Meteorite _meteorite;

    private MeteoriteTarget[] _targets;
    private MeteoritePoint[] _spawnPoints;
    public override void Cast()
    {
        _spawnPoints = Camera.main.transform.GetComponentsInChildren<MeteoritePoint>();
        _targets = Camera.main.transform.GetComponentsInChildren<MeteoriteTarget>();

        StartCoroutine(MeteoriteShower());
    }

    private IEnumerator MeteoriteShower()
    {
        int index = 0;

        foreach (var target in _targets)
        {
            var meteorite = Instantiate(_meteorite, _spawnPoints[index].transform.position, Quaternion.identity);

            meteorite.Init(ValueHandler.Value, target.transform.position, 0.3f);

            index++;

            if (index > _targets.Length)
                index = 0;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
