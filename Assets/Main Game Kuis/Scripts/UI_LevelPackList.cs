using UnityEngine;

public class UI_LevelPackList : MonoBehaviour
{

    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private InisialDataGameplay _inisialData = null;
    [SerializeField]
    private UI_LevelKuisList _levelList = null;
    [SerializeField]
    private UI_OpsiLevelPack _tombolLevelPack = null;
    [SerializeField]
    private RectTransform _content = null;
    

    private void Start()
    {
        //LoadLevelPack();

        if (_inisialData.SaatKalah)
        {
            UI_OpsiLevelPack_EventSaatKlik1(null, _inisialData.levelPack, false);
        }

        UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik1;
    }

    private void UI_OpsiLevelPack_EventSaatKlik1(UI_OpsiLevelPack tombolLevelPack, LevelPackKuis levelPack, bool terkunci)
    {
        if (terkunci)
            return;

        //_levelList.gameObject.SetActive(true);
        _levelList.UnLoadLevelPack(levelPack);

        //gameObject.SetActive(false);
        _inisialData.levelPack = levelPack;
        _animator.SetTrigger("KeLevels");
    }

    private void OnDestroy()
    {
        UI_OpsiLevelPack.EventSaatKlik -= UI_OpsiLevelPack_EventSaatKlik1;
    }
    public void LoadLevelPack(LevelPackKuis[] levelPacks, PlayerProgress.MainData playerData)
    {
        foreach (var lp in levelPacks)
        {
            var t = Instantiate(_tombolLevelPack);

            t.setLevelPack(lp);

            t.transform.SetParent(_content);
            t.transform.localScale = Vector3.one;

            if (!playerData.progresLevel.ContainsKey(lp.name))
            {
                t.KunciLevelPack();
            }
        }
    }
}
