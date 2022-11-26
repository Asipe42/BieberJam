using UnityEngine;
using UnityEngine.UI;

public class FeverManager : MonoBehaviour
{
    public static FeverManager instance;

    // Inspector
    [SerializeField] int _feverRequireMax = 50;
    [SerializeField] int _feverRequireInit = 30;
    [SerializeField] int _feverRequireIncrease = 2;
    [SerializeField] Animator _anim;
    [SerializeField] float _feverSpeed = 0.33f;
    [SerializeField] GameObject _firewoodPrefab;
    [SerializeField] GameObject[] _firewoodGOs;
    [SerializeField] Transform _firewoodGroupTR;
    [SerializeField] Vector2 _firewoodSize;
    [SerializeField] int _firewoodStackCount;
    [SerializeField] Transform _fireEffectTR;

    // Property
    public bool isFever
    {
        get { return _isFever; }
    }

    // Value
    int _feverRequireCurrent;
    int _feverCount;
    bool _isFever;
    float _feverTime = 0;

    // Event
    private void Awake()
    {
        instance = this;

        CreateFirewood();
        _fireEffectTR.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (_isFever)
        {
            _feverTime += Time.deltaTime;
            if (_feverTime >= _feverSpeed)
            {
                _feverTime -= _feverSpeed;
                _feverCount--;
                if (_feverCount <= 0)
                {
                    EndFever(); 
                }
            }
        }
        SyncFever();
    }
    public void OnStartGame()
    {
        _feverCount = 0;
        _feverRequireCurrent = _feverRequireInit;
    }

    // Function - Public
    public void PlusFeverValue()
    {
        if (_isFever)
            return;

        _feverCount++;

        if (_feverCount >= _feverRequireCurrent)
        {
            StartFever();
        }
    }

    // Function - Private
    void SyncFever()
    {
        for (var i = 0; i < _feverCount; i++)
        {
            _firewoodGOs[i].SetActive(true);
        }
        for (var i = _feverCount; i < _firewoodGOs.Length; i++)
        {
            _firewoodGOs[i].SetActive(false);
        }

        if (_feverCount > 0 && _feverCount < _feverRequireCurrent)
        {
            var newPos = _fireEffectTR.position;
            newPos.y = _firewoodGOs[_feverCount - 1].transform.position.y;
            _fireEffectTR.position = newPos;
        }
    }

    void StartFever()
    {
        _isFever = true;
        _feverRequireCurrent += _feverRequireIncrease;
        _feverTime = 0;
        _anim.SetBool("Fever", true);
        HP.instance.RecoverAllHP();
        _fireEffectTR.gameObject.SetActive(true);

        AudioManager.instance.PlaySFX(SFXDefiniton.SFX_CHAINSAWSTART);
        BGMManager.instance.DefaultToFever();
        BGMManager.instance.StartChainsaw();
    }
    void EndFever()
    {
        _isFever = false;
        _anim.SetBool("Fever", false);
        _fireEffectTR.gameObject.SetActive(false);

        BGMManager.instance.FeverToDefault();
        BGMManager.instance.StopChainsaw();
    }

    [ContextMenu("CreateFirewood")]
    void CreateFirewood()
    {
        _firewoodGOs = new GameObject[_feverRequireMax];
        Vector2 spawnOffset = new Vector2(0, 0);
        Vector2 firewoodSize = _firewoodSize;

        for (var i = 0; i < _feverRequireMax; i++)
        {
            Vector3 spawnPos = _firewoodGroupTR.position + new Vector3((spawnOffset.x - (_firewoodStackCount - 1)/2) * _firewoodSize.x, spawnOffset.y * _firewoodSize.y, -(i + 1)/100f);
            var spawnedFirewoodGO = Instantiate(_firewoodPrefab, spawnPos, Quaternion.identity);
            spawnedFirewoodGO.transform.SetParent(_firewoodGroupTR);

            var newScale = spawnedFirewoodGO.transform.localScale;
            newScale.x = spawnOffset.y % 2 == 0 ? 1 : -1;
            spawnedFirewoodGO.transform.localScale = newScale;

            spawnOffset.x += spawnOffset.y % 2 == 0 ? 1 : -1;
            if (spawnOffset.x == _firewoodStackCount || spawnOffset.x == -1)
            {
                spawnOffset.y += 1;
                spawnOffset.x = Mathf.Clamp(spawnOffset.x, 0, _firewoodStackCount - 1);
            }

            _firewoodGOs[i] = spawnedFirewoodGO;
        }
    }
}
