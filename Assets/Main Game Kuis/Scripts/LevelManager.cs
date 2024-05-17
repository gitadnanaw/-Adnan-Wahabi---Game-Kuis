using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;
    [SerializeField]
    private PlayerProgress _playerProgress = null;
    [SerializeField]
    private LevelPackKuis _soalSoal = null;
    [SerializeField]
    private UI_Pertanyaan _pertanyaan = null;
    [SerializeField]
    private UI_PoinJawaban[] _pilihanJawaban = new UI_PoinJawaban[0];
    [SerializeField]
    private GameSceneManager _gameSceneManager = null;
    [SerializeField]
    private string _namaScenePilihMenu = string.Empty;

    private int _indexSoal = -1;

    private void Start()
    {

        
        //_soalSoal = _inisialData.levelPack;
        _indexSoal = _inisialData.levelIndex - 1;

        NextLevel();

        AudioManager.instance.PlayBGM(1);

        UI_PoinJawaban.EventJawabSoal += UI_PoinJawaban_EventJawabSoal;
    }
    private void OnDestroy()
    {
        UI_PoinJawaban.EventJawabSoal -= UI_PoinJawaban_EventJawabSoal;
    }

    private void OnApplicationQuit()
    {
        _inisialData.SaatKalah = false;
    }
    private void UI_PoinJawaban_EventJawabSoal(string jawaban, bool adalahBenar)
    {
        //var namaLevelPack = _inisialData.levelPack.name;
        if (adalahBenar)
        {
            _playerProgress.progresData.koin += 20;

            //_playerProgress.progresData.progresLevel[namaLevelPack] = _indexSoal + 2;
            //_playerProgress.SimpanProgres();
        }
    }
    public void NextLevel()
    {
        _indexSoal++;

        if(_indexSoal >= _soalSoal.BanyakLevel)
        {
            //_indexSoal = 0;
            _gameSceneManager.BukaScene(_namaScenePilihMenu);
            return;
        }

        LevelSoalKuis soal = _soalSoal.AmbilLevelKe(_indexSoal);

        _pertanyaan.SetPertanyaan($"Soal {_indexSoal + 1}", soal.pertanyaan, soal.petunjukJawaban);

        for(int i = 0; i < _pilihanJawaban.Length; i++)
        {
            UI_PoinJawaban poin = _pilihanJawaban[i];
            LevelSoalKuis.OpsiJawaban opsi = soal.opsiJawaban[i];
            poin.SetJawaban(opsi.jawabanTeks, opsi.adalahBenar);
        }
    }
}
