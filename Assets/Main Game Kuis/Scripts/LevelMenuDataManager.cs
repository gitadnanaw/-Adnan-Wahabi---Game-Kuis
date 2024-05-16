using TMPro;
using UnityEngine;

public class LevelMenuDataManager : MonoBehaviour
{
    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private TextMeshProUGUI _tempatkoin = null;
    void Start()
    {
        _tempatkoin.text = $"{_playerProgress.progresData.koin}";
    }
}
