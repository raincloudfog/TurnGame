using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;



public class BattleCanvas : MonoBehaviour
{
    [SerializeField]
    Button[] Playerbutton;
    [Space(20)]

    //당장은 두유닛끼리 싸우는 거로 확인
    [SerializeField]
    BattleUnit Player,
            Enemy;

    [SerializeField, Tooltip("적의 이미지")]
    Image Enemyimg;

    [SerializeField]
    Image EnemyHPBar;

    [SerializeField]
    TMP_Text HpText;

    [SerializeField]
    GameObject _damageTextPrefab;

    [SerializeField]
    Queue<DamageText> damageTexts = new Queue<DamageText>();

    //턴 진행
    bool isTurnActive;

    [SerializeField]
    Transform DamageTextParent;


    //나중에 적은 코루틴으로 사용할 수 있을 것 같음.
    [SerializeField]
    Coroutine HpBarCoroutine;
    Coroutine EnemyTurnCorou;

    private void Awake()
    {
        Playerbutton[0].onClick.AddListener(PlayerAttackchoice);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = new BattleUnit();
        Enemy = new BattleUnit();
        HpText.text = $"{Player.Hp} / {Player.MaxHp}";
    }

    public void Update()
    {
        if (!isTurnActive || HpBarCoroutine != null)
        {
            return;
        }

        EnemyAttackchoice();

        isTurnActive = false;
    }

    void SetText(string text)
    {
        GameObject damageTextobj = ObjectPool.Instance.GetItem(PoolName.DamageText, _damageTextPrefab);
        DamageText damageText = damageTextobj.GetComponent<DamageText>();
        damageText.SetText(text);

        // 데미지 텍스트를 DamageTextParent의 자식으로 설정
        damageText.transform.SetParent(DamageTextParent);

        // 데미지 텍스트의 RectTransform 컴포넌트 가져오기
        RectTransform rectTransform = damageText.GetComponent<RectTransform>();

        // 데미지 텍스트의 크기 설정 (예: 너비 100, 높이 50)
        rectTransform.sizeDelta = new Vector2(100, 50);

        // 앵커 설정 (예: 상단 중앙)
        rectTransform.anchorMin = new Vector2(0.5f, 1); // 위쪽 중앙
        rectTransform.anchorMax = new Vector2(0.5f, 1); // 위쪽 중앙
        rectTransform.pivot = new Vector2(0.5f, 1); // 위쪽 중앙

        // 이전 데미지 텍스트의 개수에 따라 새로운 데미지 텍스트의 위치 조정
        rectTransform.anchoredPosition = new Vector2(0, -damageTexts.Count * (rectTransform.sizeDelta.y + 5)); // 5는 간격 조절용

        damageTexts.Enqueue(damageText);
        Debug.Log(damageText + " " + text);
    }



    /// <summary>
    /// 턴 기다리는 용도
    /// </summary>
    /// <param name="Active"></param>
    public void TurnActive(bool Active)
    {
        isTurnActive = Active;
    }

    /// <summary>
    /// 플레이어가 적 공격
    /// </summary>
    public void PlayerAttackchoice()
    {
        if(HpBarCoroutine != null)
        {
            return;
        }
        
        int EnemyHP = Player.Attack(Enemy);

        SetText("몬스터에게 데미지 5를 입혔습니다.");

        if (HpBarCoroutine == null)
            HpBarCoroutine = StartCoroutine(ChangeHPBar(EnemyHP, Enemy.MaxHp));
    }

    /// <summary>
    /// 적 차례에서 적이 공격
    /// </summary>
    void EnemyAttackchoice()
    {
        if(EnemyTurnCorou != null)
        {
            return;
        }
        int CurrentHp = Player.Hp;
        int PlayerHP = Enemy.Attack(Player);

        SetText("적 공격!");

        //Debug.Log(CurrentHp + " / " + PlayerHP);
        EnemyTurnCorou = StartCoroutine(HitPlayer(CurrentHp, PlayerHP));
        
    }
    /// <summary>
    /// 몬스터 피격시
    /// </summary>
    /// <param name="currentHP"></param>
    /// <param name="maxHP"></param>
    /// <returns></returns>
    IEnumerator ChangeHPBar(int currentHP, int maxHP)
    {        
        if (currentHP < 0)
        {
            currentHP = 0;
        }

        float targetFillAmount = (float)currentHP / (float)maxHP;

        float timer = 1;

        float startFillAmount = EnemyHPBar.fillAmount;

        StartCoroutine(HitMotionEnemy());
        

        while (timer > 0) // timer가 0보다 클 때까지 반복
        {
            timer -= Time.deltaTime; // 타이머 감소
            EnemyHPBar.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, (1 - timer) / 1); // 타이머에 따라 fillAmount 갱신
            yield return null; // 한 프레임 기다림
        }

        EnemyHPBar.fillAmount = targetFillAmount;

        

        HpBarCoroutine = null;
        TurnActive(true);
    }

    /// <summary>
    /// 플레이어가 맞았을시
    /// </summary>
    /// <param name="CurrentHP">맞기전 체력</param>
    /// <param name="Hp">맞은 후 체력</param>
    /// <returns></returns>
    IEnumerator HitPlayer(int CurrentHP,int Hp)
    {
        int endHp = Hp; // 감소한 HP

        while (CurrentHP > endHp)
        {
            CurrentHP -= 1;
            HpText.text = $"{CurrentHP} / {Player.MaxHp}";
            yield return new WaitForSeconds(0.05f); // 1초에 한 번씩 감소하도록 설정
        }

        //나중에 코드 바꿀수 있음. 테스트용도로 만듦
        yield return new WaitForSeconds(0.5f);
        ClearText();
        // 감소가 끝나면 코루틴 종료
        EnemyTurnCorou = null;
    
    }


    #region 적 피격시 코루틴
    IEnumerator HitMotionEnemy()
    {
        StartCoroutine(BlinkImage());
        StartCoroutine(ShakeImage());

        yield return new WaitForSeconds(0.5f);

        Enemyimg.enabled = true;                
    }


    IEnumerator ShakeImage()
    {
        int count = 5;
        float magnitude = 0.1f;

        Vector3 originPos = Enemyimg.transform.localPosition;
        for (int i = 0; i < count; i++)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;

            Enemyimg.transform.localPosition = originPos + new Vector3(x, 0, 0);
            yield return null;
               
        }

        Enemyimg.transform.localPosition = originPos; // 이미지 정위치
    }

    /// <summary>
    /// 이미지 깜박임
    /// </summary>
    /// <returns></returns>
    IEnumerator BlinkImage()
    {
        float duration = 0.2f;
        while(duration > 0)
        {
            Enemyimg.enabled = !Enemyimg.enabled;
            duration -= Time.deltaTime;
            yield return null;
        }

        Enemyimg.enabled = true;
    }

    #endregion

    void ClearText()
    {
        while (damageTexts.Count > 0)
        {
            DamageText text = damageTexts.Dequeue();
            Debug.Log("ClearText() " + text);
            text.gameObject.SetActive(false);
            text.transform.SetParent(null);
            ObjectPool.Instance.SetItem(PoolName.DamageText, text.gameObject);
        }
    }
}

[Serializable]
public class BattleUnit 
{

    //체력 공격력 속도
    public int MaxHp;
    public int Hp;
    public int Damage;
    public int Speed;

    public BattleUnit()
    {
        MaxHp = 100;
        Hp = MaxHp;
        Damage = 5;
        Speed = 5;
    }

    public int Attack(BattleUnit enemy)
    {
        enemy.Hp -= Damage;
        return enemy.Hp;
    }

}
