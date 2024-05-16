using UnityEngine;

public class UI_LevelPackList : MonoBehaviour
{

    [SerializeField]
    private InisialDataGameplay _inisialData = null;
    [SerializeField]
    private UI_LevelKuisList _levelList = null;
    [SerializeField]
    private UI_OpsiLevelPack _tombolLevelPack = null;
    [SerializeField]
    private RectTransform _content = null;
    [Space, SerializeField]
    private LevelPackKuis[] _levelPacks = new LevelPackKuis[0];

    private void Start()
    {
        LoadLevelPack();

        if (_inisialData.SaatKalah)
        {
            UI_OpsiLevelPack_EventSaatKlik1(_inisialData.levelPack);
        }

        UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik1;
    }

    private void UI_OpsiLevelPack_EventSaatKlik1(LevelPackKuis levelPack)
    {
        _levelList.gameObject.SetActive(true);
        _levelList.UnLoadLevelPack(levelPack);

        gameObject.SetActive(false);
        _inisialData.levelPack = levelPack;
    }

    private void OnDestroy()
    {
        UI_OpsiLevelPack.EventSaatKlik -= UI_OpsiLevelPack_EventSaatKlik1;
    }
    private void LoadLevelPack()
    {
        foreach (var lp in _levelPacks)
        {
            var t = Instantiate(_tombolLevelPack);

            t.setLevelPack(lp);

            t.transform.SetParent(_content);
            t.transform.localScale = Vector3.one;
        }
    }
}
