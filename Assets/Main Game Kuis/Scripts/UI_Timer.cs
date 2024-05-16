using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{

    public static event System.Action EventWaktuHabis;

    //[SerializeField]
    //private UI_PesanLevel _tempatPesan = null;

    [SerializeField]
    private Slider _timerBar = null;

    [SerializeField]
    private float _waktuJawab = 30f;

    private float _sisaWaktu = 0f;
    private bool _waktuBerjalan = true;

    public bool WaktuBerjalan
    {
        get => _waktuBerjalan;
        set => _waktuBerjalan = value;
    }

    private void Start()
    {
        UlangWaktu();
    }

    private void Update()
    {
        if (!_waktuBerjalan)
            return;
        _sisaWaktu -= Time.deltaTime;
        _timerBar.value = _sisaWaktu / _waktuJawab;

        if(_sisaWaktu<= 0f)
        {
            //Debug.Log("Waktu Habis");
            //_tempatPesan.Pesan = "Walauwe Waktu Habis";
            //_tempatPesan.gameObject.SetActive(true);
            EventWaktuHabis?.Invoke();
            _waktuBerjalan= false;
            return;
        }
        //Debug.Log(_sisaWaktu);
    }

    public void UlangWaktu()
    {
        _sisaWaktu = _waktuJawab;
    }
}
