using UnityEngine;

// 시프만 버튼을 누르면 계속 스폰됨, 레벨업시 데미지 증가하게 만들기
public class AllyCtrl : MonoBehaviour
{
    [SerializeField] Session _session;
    [SerializeField] Archer _archerPrefab;
    [SerializeField] DarkWizard _darkWizardPrefab;
    [SerializeField] FireWizard _fireWizardPrefab;
    [SerializeField] Thief _thiefPrefab;
    [SerializeField] Transform _archerSpawnPoint;
    [SerializeField] Transform _darkWizardSpawnPoint;
    [SerializeField] Transform _fireWizardSpawnPoint;
    [SerializeField] Transform _thiefSpawnPoint;

    Archer _archer;
    DarkWizard _darkWizard;
    FireWizard _fireWizard;
    Thief _thief;

    [SerializeField] UpgradeData[] _datas;
    [SerializeField] AllyUpgraderView[] _allyUpgraderViews;
    [SerializeField] int[] _levels;

    private void Start()
    {
        UpdateAllyViews();
    }

    public void UpgradeAlly(int index)
    {
        if (index < 0 || index >= _datas.Length)
            return;
        int level = _levels[index];
        float cost = _datas[index].GetCost(level);
        float currentValue = 0;
        float increaseAmount = _datas[index].GetIncreaseAmount(_levels[index]);
        if (_session.TryPayGold(cost))
        {
            switch (index)
            {
                case 0:
                    if (_archer != null)
                    {
                        _levels[index]++;
                        _archer.UpgradeArcher(increaseAmount);
                        currentValue = _archer.ArrowDamage;
                    }
                    else
                    {
                        SpawnArcher();
                        _levels[index]++;
                        currentValue = _archer.ArrowDamage;
                    }
                    break;
                case 1:
                    if (_darkWizard != null)
                    {
                        _levels[index]++;
                        _darkWizard.UpgradeDarkWizard(increaseAmount);
                        currentValue = _darkWizard.DarkBallDamage;
                    }
                    else
                    {
                        SpawnDarkWizard();
                        _levels[index]++;
                        currentValue = _darkWizard.DarkBallDamage;
                    }
                    break;
                case 2:
                    if (_fireWizard != null)
                    {
                        _levels[index]++;
                        _fireWizard.UpgradeFireWizard(increaseAmount);
                        currentValue = _fireWizard.FireBallDamage;
                    }
                    else
                    {
                        SpawnFireWizard();
                        _levels[index]++;
                        currentValue = _fireWizard.FireBallDamage;
                    }
                    break;
                case 3:
                    if (_thief != null)
                    {
                        _levels[index]++;
                        _thief.UpgradeThief(increaseAmount);
                        currentValue = _thief.Damage;
                    }
                    else
                    {
                        SpawnThief();
                        _levels[index]++;
                        currentValue = _thief.Damage;
                    }
                    break;
            }
            float nextCost = _datas[index].GetCost(_levels[index]);
            float nextIncreaseAmount = _datas[index].GetIncreaseAmount(_levels[index]);
            _allyUpgraderViews[index].UpdateView(_levels[index], currentValue, nextCost, nextIncreaseAmount);
        }
        else
        {
            Debug.Log("골드 부족");
        }
    }

    public void AllyUpdateView(int index)
    {
        if (index < 0 || index >= _datas.Length)
            return;
        int level = _levels[index];
        float increaseAmount = _datas[index].GetIncreaseAmount(level + 1);
        float cost = _datas[index].GetCost(level);
        float currentValue = 0;
        switch (index)
        {
            case 0:
                if (_archer != null)
                    currentValue = _archer.ArrowDamage;
                break;
            case 1:
                if (_darkWizard != null)
                    currentValue = _darkWizard.DarkBallDamage;
                break;
            case 2:
                if (_fireWizard != null)
                    currentValue = _fireWizard.FireBallDamage;
                break;
            case 3:
                if (_thief != null)
                    currentValue = _thief.Damage;
                break;
        }
        _allyUpgraderViews[index].UpdateView(level, currentValue, cost, increaseAmount);
    }

    public void UpdateAllyViews()
    {

        for (int i = 0; i < _datas.Length; i++)
        {
            AllyUpdateView(i);
        }
    }

    public void SpawnArcher()
    {
        _archer = Instantiate(_archerPrefab);
        _archer.transform.position = _archerSpawnPoint.position;
        _archer.Initialize(_session, _archerPrefab.ArrowSpeed, _archerPrefab.ArrowDamage, _archerPrefab.AttackSpan);
    }

    public void SpawnDarkWizard()
    {
        _darkWizard = Instantiate(_darkWizardPrefab);
        _darkWizard.transform.position = _darkWizardSpawnPoint.position;
        _darkWizard.Initialize(_session, _darkWizardPrefab.DarkBallSpeed, _darkWizardPrefab.DarkBallDamage, _darkWizardPrefab.AttackSpan);
    }

    public void SpawnFireWizard()
    {
        _fireWizard = Instantiate(_fireWizardPrefab);
        _fireWizard.transform.position = _fireWizardSpawnPoint.position;
        _fireWizard.Initialize(_session, _fireWizardPrefab.FireBallSpeed, _fireWizardPrefab.FireBallDamage, _fireWizardPrefab.AttackSpan);   
    }

    public void SpawnThief()
    {
        _thief = Instantiate(_thiefPrefab);
        _thief.transform.position = _thiefSpawnPoint.position;
        _thief.Initialize(_session, _thiefPrefab.Damage, _thiefPrefab.AttackSpan);
    }

    public void AllyAllDestroy()
    {
        if (_archer != null)
            Destroy(_archer.gameObject);
        if (_darkWizard != null)
            Destroy(_darkWizard.gameObject);
        if (_fireWizard != null)
            Destroy(_fireWizard.gameObject);
        if (_thief != null)
            Destroy(_thief.gameObject);
    }

    public void ResetAlly()
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i] = 0;
        }
        UpdateAllyViews();
    }
}
