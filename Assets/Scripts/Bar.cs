using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;



public class Bar : MonoBehaviour
{
    //[field:SerializeField]
    //public int MaxValue {get;private set;}
    //[field:SerializeField]
    //public int Value {get;private set;}
    //public void Change(int amount)
    [field:SerializeField]
    public float MaxValue {get; private set; }
    [field:SerializeField]
    public float Value {get; private set; }
    private GameObject obj;
    [SerializeField]
    private RectTransform _topBar;
    
    [SerializeField]
    private RectTransform _bottomBar;

    [SerializeField]
    private float _animationSpeed = 1f;

    public float _fullWidth;
    private float TargetWidth => Value * _fullWidth / MaxValue;
    
    private Coroutine _adjustBarWidthCoroutine;

    GameObject instance;

    public void Start()
    {
        //_fullWidth=_topBar.rect.width; //ovde je neki problem, nece da uzme topBar width, stavi ga na 0
        MaxValue = GetComponentInParent<HealthSystem>().GetHealth();
        Value = MaxValue;
    }


    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //TargetWidth=_fullWidth * (80/100);
            //_fullWidth=_fullWidth * (80/100);
            Change(20);
            Debug.Log("Damage!");
            GetComponentInParent<IDamageable>().Heal(20);
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            //TargetWidth=_fullWidth * (120/100);
            //_fullWidth=_fullWidth * (120/100);
            Change(-20);
            Debug.Log("Heal!");
            GetComponentInParent<IDamageable>().Damage(20);
        }
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
