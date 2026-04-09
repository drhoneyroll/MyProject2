using System.Collections;
using UnityEngine;



public class Bar : MonoBehaviour
{
    [field:SerializeField]
    public float MaxValue {get; private set; }
    [field:SerializeField]
    public float Value {get; private set; }
    [SerializeField]
    private RectTransform _topBar;
    
    [SerializeField]
    private RectTransform _bottomBar;

    [SerializeField]
    private float _animationSpeed = 1f;

    public float _fullWidth;
    private float TargetWidth => Value * _fullWidth / MaxValue;
    
    private Coroutine _adjustBarWidthCoroutine;
    HealthSystem playerHealth;

    public void Start()
    {
        //_fullWidth=_topBar.rect.width; //ovde je neki problem, nece da uzme topBar width, stavi ga na 0
        playerHealth = FindAnyObjectByType<PlayerController>().GetComponent<HealthSystem>();
        MaxValue = playerHealth.GetHealth();
        Value = MaxValue;
    }

    private IEnumerator AdjustBarWidth(int amount)
    {
        var suddenChangeBar = amount >= 0 ?  _bottomBar : _topBar;
        var slowChangeBar = amount >= 0 ? _topBar : _bottomBar;
        suddenChangeBar.SetWidth(TargetWidth);
        
        while (Mathf.Abs(suddenChangeBar.rect.width - slowChangeBar.rect.width)>1f)
        {
            
            slowChangeBar.SetWidth(
                Mathf.Lerp(slowChangeBar.rect.width, TargetWidth, Time.deltaTime * _animationSpeed ));
            yield return null;
        }
        slowChangeBar.SetWidth(TargetWidth);

    }

    public void Change(int amount)
    {
        Value=Mathf.Clamp(Value + amount, 0, MaxValue);
        if (_adjustBarWidthCoroutine != null)
        {
            StopCoroutine(_adjustBarWidthCoroutine);           
        }

        _adjustBarWidthCoroutine = StartCoroutine(AdjustBarWidth(amount));
    }
}
