using UnityEngine;

public class UI_LevelKuisList : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;
    [SerializeField]
    private UI_OpsiLevelKuis _tombolLevel = null;
    [SerializeField]
    private RectTransform _content = null;
    [Space, SerializeField]
    private LevelPackKuis _levelPack= null;
    [SerializeField]
    private GameSceneManager _gameSceneManager = null;
    [SerializeField]
    private string _gameplayScene = null;   

    private void Start()
    {
        // if (_levelPack != null)
        //{
        //    UnLoadLevelPack(_levelPack);
        //}
        UI_OpsiLevelKuis.EventSaatKlik += UI_OpsiLevelKuis_EventSaatKlik;
    }
    private void OnDestroy()
    {
        UI_OpsiLevelKuis.EventSaatKlik -= UI_OpsiLevelKuis_EventSaatKlik;
    }

    private void UI_OpsiLevelKuis_EventSaatKlik(int index)
    {
        _inisialData.levelIndex = index;
        _gameSceneManager.BukaScene(_gameplayScene);
    }

    public void UnLoadLevelPack(LevelPackKuis levelPack)
    {
        HapusIsiContent();

        _levelPack = levelPack;
        for (int i = 0; i<levelPack.BanyakLevel; i++)
        {
            var t = Instantiate(_tombolLevel);

            t.setLevelKuis(levelPack.AmbilLevelKe(i), i);

            t.transform.SetParent(_content);
            t.transform.localScale = Vector3.one;
        }
    }

    private void HapusIsiContent()
    {
        var cc = _content.childCount;
        for(int i = 0; i<cc;i++)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
    }
}
