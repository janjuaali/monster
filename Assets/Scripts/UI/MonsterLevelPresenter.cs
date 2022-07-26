using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MonsterLevelPresenter : LevelPresenter
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _levelRectTransform;
    [SerializeField] private FloatingText _floatingText;
    [SerializeField] private Transform _spawnPosition;

    private MonstersHandler _monstersHandler;
    private Coroutine _coroutine;
    private Vector3 _enlargeScale;
    private Vector3 _intialScale;

    private const float MaxScale = 0.7f;

    private void Awake()
    {
        _intialScale = _levelRectTransform.localScale;
        _enlargeScale = new Vector3(MaxScale, MaxScale, MaxScale);
        _monstersHandler = GetComponent<MonstersHandler>();
    }

    private void OnEnable()
    {
        _monstersHandler.MightChanged += OnMightChange;
        Show(_monstersHandler.MonsterMight);
    }

    private void OnDisable()
    {
        _monstersHandler.MightChanged -= OnMightChange;
    }

    public void Disable()
    {
        _canvas.gameObject.SetActive(false);
    }

    public void Enable()
    {
        _canvas.gameObject.SetActive(true);
        Show(_monstersHandler.MonsterMight);
    }

    public void OnMightChange(int might, int addedMight)
    {
        if (_canvas.gameObject.activeInHierarchy == false)
            return;

        Show(might);

        var floatingText = Instantiate(_floatingText, _spawnPosition);
        floatingText.Init(addedMight);

        if (addedMight < 0)
            return;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(WoopAnimation(_levelRectTransform));
    }

    private IEnumerator WoopAnimation(RectTransform rectTransform)
    {
        float changeSpeed = Mathf.Abs((_intialScale.x - MaxScale) / 0.1f);

        while (rectTransform.localScale.x < MaxScale)
        {
            rectTransform.localScale = Vector3.MoveTowards(rectTransform.localScale, _enlargeScale, changeSpeed * Time.deltaTime);
            yield return null;
        }

        while (transform.localScale.x > _intialScale.x)
        {
            rectTransform.localScale = Vector3.MoveTowards(rectTransform.localScale, _intialScale, changeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
