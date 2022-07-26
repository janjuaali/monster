using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class Graber : MonoBehaviour
{
    [SerializeField] private SwipeZone _swipeZone;

    private Rotator _rotator;
    private Monster _monster;
    private IMonsterHolder _monsterHolder;
    private float _yPointerDistance;
    private float _yThreshold = 0.1f;

    public static bool Grabed { get; private set; }


    private void Awake()
    {
        int levelIndex = SaveSystem.LoadLevelsProgression();

        if (levelIndex < 1)
            gameObject.SetActive(false);
    }


    private void Update()
    {
        if (SwipeZone.Interacting || SwipeZone.IsMoving)
            return;

        if (Input.GetMouseButton(0))
        {
            if(Grabed == false)
                _yPointerDistance = Input.GetAxis("Mouse Y");

            if (IsBrakingThreshold())
                Grab();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Release();

            _yPointerDistance = 0;
        }

        if (Grabed)
        {
            _monster.transform.position = GetWorldPositionOnPlane(Input.mousePosition, 2f);
        }

        _yPointerDistance = 0;
    }

    private void Grab()
    {
        if (Grabed == true || EventSystem.current.IsPointerOverGameObject())
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits = Physics.RaycastAll(ray, 50f);

        foreach (var hitInfo in raycastHits)
        {
            if (hitInfo.collider.TryGetComponent(out IMonsterHolder monsterHolder))
            {
                if (monsterHolder.TryGrab(out Monster monster))
                {
                    Grabed = true;
                    _monster = monster;
                    _monsterHolder = monsterHolder;
                    _monster.MonsterAnimator.VictoryAnimation();                        

                    _rotator = monster.GetComponentInChildren<Rotator>();

                    if(_monster.transform.parent != null)
                        _monster.transform.parent = null;

                    _monster.transform.localScale = Vector3.one * 0.25f;

                    _swipeZone.Expand();

                    LightUp(monster);
                    _yPointerDistance = 0;
                }
            }
        }
    }

    private void Release()
    {
        if (Grabed == false)
            return;

       Grabed = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits = Physics.RaycastAll(ray, 50f);

        foreach (var hitInfo in raycastHits)
        {
            if (hitInfo.collider.TryGetComponent(out MonsterPlaceAccepter monsterAccepter))
            {
                if (monsterAccepter.TryAcquireMonster(_monster) == false)
                    Swap(monsterAccepter);
                else
                    _swipeZone.Shrink(false, 0);

                _monster.MonsterAnimator.MonsterPlaced();
                
                LighthDown();
                return;
            }
        }

        PlaceToInitialMonsterCell(_monster);
        ResetPosition();
        LighthDown();
        _monster.MonsterAnimator.MonsterPlaced();
    }

    private void ClearMonsterAccepter()
    {
        if (Grabed)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits = Physics.RaycastAll(ray, 50f);

        foreach (var hitInfo in raycastHits)
        {
            if (hitInfo.collider.TryGetComponent(out MonsterPlaceAccepter monsterPlaceAccepter))
                monsterPlaceAccepter.TryReturnMonster();
        }
    }

    private void LightUp(Monster monster)
    {

        var cells = FindObjectsOfType<MonsterCell>();

        foreach (var cell in cells)
        {
            if(cell.InitialMonster.GetType() == monster.GetType())
                cell.LightUp();
        }
    }

    private void LighthDown()
    {

        var cells = FindObjectsOfType<MonsterCell>();

        foreach (var cell in cells)
        {
            cell.LightDown();
        }
    }

    private bool TryGetMonsterHolder(IMonsterHolder monsterHolder, out IMonsterHolder targetMonsterHolder)
    {
        targetMonsterHolder = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits = Physics.RaycastAll(ray, 50f);

        foreach (var hitInfo in raycastHits)
        {
            if (hitInfo.collider.TryGetComponent(out IMonsterHolder tempMonsterHolder))
                if (tempMonsterHolder.GetType() == monsterHolder.GetType())
                    targetMonsterHolder = tempMonsterHolder;
        }

        return false;
    }

    private void Swap(MonsterPlaceAccepter monsterPlaceAccepter)
    {
        monsterPlaceAccepter.TryGrab(out Monster firstMonster);

        monsterPlaceAccepter.TryAcquireMonster(_monster);

        TryPlaceToInititalMonsterCell(firstMonster);

        if(_monsterHolder is MonsterCell == false)
            _swipeZone.Shrink(false, 0);
    }

    private bool TryPlaceToInititalMonsterCell(Monster monster)
    {
        if (_monsterHolder.TryAcquireMonster(monster) == false)
        {
            PlaceToInitialMonsterCell(monster);
            return false;
        }

        return true;
    }

    private void PlaceToInitialMonsterCell(Monster monster)
    {
        MonsterCell[] monsterCells = FindObjectsOfType<MonsterCell>();

        foreach (var monsterCell in monsterCells)
        {
            if (monsterCell.TryAcquireMonster(monster))
                return;
        }
    }

    private void Return(IMonsterHolder monsterHolder)
    {
        monsterHolder.TryAcquireMonster(_monster);
        _rotator.enabled = false;
        _swipeZone.Shrink(false, 0);
    }

    private void ResetPosition()
    {
        _monster.transform.localPosition = Vector3.zero;
    }

    private Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float yPostion)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xzPlane = new Plane(Vector3.forward, new Vector3(0, 0, Camera.main.transform.position.z+1.5f));
        float distance;
        xzPlane.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    private bool IsBrakingThreshold()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits = Physics.RaycastAll(ray, 50f);

        bool isPlaceAccepter = false;
        foreach (var hitInfo in raycastHits)
        {
            if(hitInfo.collider.TryGetComponent(out MonsterPlaceAccepter monsterPlaceAccepter))
            isPlaceAccepter = true;
        }

        return _yPointerDistance > _yThreshold || isPlaceAccepter;
    }
}
