using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterShop : MonoBehaviour
{
    [SerializeField] private MonsterCell[] _initialMonsterCells;
    [SerializeField] private MonsterPersistence _monsterPersistence = new MonsterPersistence();
    [SerializeField] private Monster _initialMonster;
    [SerializeField] private MonsterCell[] _monsterCells;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _levelToOpen;
    [SerializeField] private ShopTutorial _shopTutorial;

    private MonsterPlaceAccepter[] _monsterPlaceAcepters;

    private ValueHandler _monsterCount = new ValueHandler(1, 3, "MonsterCountShopSaveName");
    private ValueHandler _openedMonstersCellCount = new ValueHandler(1, 9, "OpenedMonsterCellCountShopSaveName");
    private ValueHandler _openedMonsterPlaces = new ValueHandler(3, 5, "OpenMonsterPlacesSave");

    public static bool IsReady { get; private set; }

    public int OpenedMonsterPlaces => (int)_openedMonsterPlaces.Value;

    private void Awake()
    {
        _monsterPlaceAcepters = FindObjectOfType<MonstersHandler>().GetComponentsInChildren<MonsterPlaceAccepter>();
        _monsterCount.LoadAmount();
        _openedMonstersCellCount.LoadAmount();
        _openedMonsterPlaces.LoadAmount();

        StartCoroutine(Delay());

        int levelIndex = SaveSystem.LoadLevelsProgression();

        if (levelIndex < _levelToOpen)
        {
            
            _animator.enabled = false;
            transform.parent = null;
            transform.position = new Vector3(0, 100, 0);
        }
        else
        {
            _shopTutorial.gameObject.SetActive(true);
        }

    }

    private void OnEnable()
    {
        foreach (var monsterCell in _monsterCells)
        {
            monsterCell.MonsterUpgraderHandler.Opened += OnCellOpened;
        }
    }

    private void OnDisable()
    {
        foreach (var monsterCell in _monsterCells)
        {
            monsterCell.MonsterUpgraderHandler.Opened -= OnCellOpened;
        }
    }

    public bool TryGetMonstersForPool(out List<Monster> monsters)
    {
        int count = RandomMonstersCount();
        monsters = new List<Monster>();

        if (count <= 0)
            return false;
        
        var tempMonsters = from MonsterCell monsterCell in _monsterCells
                    where monsterCell.IsOpened && monsterCell.IsMonsterPlaced() == false
                    select monsterCell.InitialMonster;

        List<Monster> tempMonstersList = tempMonsters.ToList();

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, tempMonstersList.Count);

            monsters.Add(tempMonstersList[index]);
            tempMonstersList.RemoveAt(index);
        } 

        return count>0;
    }

    public int RandomMonstersCount()
    {
        int count = 0;

        foreach (var monsterCell in _monsterCells)
        {
            if (monsterCell.IsMonsterPlaced())
                count++;
        }

        return Mathf.Abs((int)_openedMonsterPlaces.Value - count);
    }

    public bool TryOpenCellWithMonster(Monster monster)
    {
        var monsteCell = _monsterCells.FirstOrDefault(cell => cell.Monster.GetType() == monster.GetType() && cell.IsOpened == false);

        if(monsteCell != default)
        {
            monsteCell.Open();
            OnCellOpened();

            return true;
        }

        return false;
    }

    public void SaveMonsterParty()
    {
        for (int i = 0; i < _monsterPlaceAcepters.Length; i++)
        {
            if (_monsterPlaceAcepters[i].Monster != null)
                _monsterPersistence.Save(_monsterPlaceAcepters[i].Monster, i);
            else
                _monsterPersistence.DeleteSave(i);
        }
    }

    public void LoadMonsterParty()
    {
        if(_monsterPersistence.HaveSavedMonster == false)
        {
            var cell = _initialMonsterCells.FirstOrDefault(cell => cell.InitialMonster.GetType() == _initialMonster.GetType());

            if (cell.TryGrab(out Monster monster, true))
                _monsterPlaceAcepters[0].TryAcquireMonster(monster);
        }

        for (int i = 0; i < _monsterPlaceAcepters.Length; i++)
        {
            if (_monsterPlaceAcepters[i].CanAcquireMonster)
            {
                if (_monsterPersistence.TryLoad(i, out Monster tempMonster))
                {
                    if (_monsterCells.FirstOrDefault(cell => cell.InitialMonster.GetType() == tempMonster.GetType()).TryGrab(out Monster monster, true))
                        _monsterPlaceAcepters[i].TryAcquireMonster(monster);
                }
            }
        }

    }

    private void OnCellOpened()
    {
        _monsterCount.Increase(1);
        _openedMonsterPlaces.Increase(1);
        int placeToOpenIndex = (int)_monsterCount.Value - 1;
        int placeToActivateIndex = (int)_openedMonsterPlaces.Value - 1;
        _monsterPlaceAcepters[placeToActivateIndex].Activate();
        _monsterPlaceAcepters[placeToOpenIndex].Open();
    }

    private void PrepareMonsterHolders(IMonsterHolder[] monsterHolders, int progressionCount, int openedPlacesCount)
    {
        for (int i = 0; i < monsterHolders.Length; i++)
        {
            monsterHolders[i].Activate();

            if (i < progressionCount)
                monsterHolders[i].Open();

            if (i >= progressionCount && i >= openedPlacesCount)
                monsterHolders[i].Hide();
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (var monsterCell in _monsterCells)
        {
            monsterCell.Init();
        }

        foreach (var initialMonsterCell in _initialMonsterCells)
        {
            initialMonsterCell.Open();
        }

        PrepareMonsterHolders(_monsterPlaceAcepters, (int)_monsterCount.Value, (int)_openedMonsterPlaces.Value);
        LoadMonsterParty();

        IsReady = true;
    }
}
