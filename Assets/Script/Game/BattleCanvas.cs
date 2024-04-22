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

    //������ �����ֳ��� �ο�� �ŷ� Ȯ��
    [SerializeField]
    BattleUnit Player,
            Enemy;

    [SerializeField, Tooltip("���� �̹���")]
    Image Enemyimg;

    [SerializeField]
    Image EnemyHPBar;

    [SerializeField]
    TMP_Text HpText;

    [SerializeField]
    GameObject _damageTextPrefab;

    [SerializeField]
    Queue<DamageText> damageTexts = new Queue<DamageText>();

    //�� ����
    bool isTurnActive;

    [SerializeField]
    Transform DamageTextParent;


    //���߿� ���� �ڷ�ƾ���� ����� �� ���� �� ����.
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

        // ������ �ؽ�Ʈ�� DamageTextParent�� �ڽ����� ����
        damageText.transform.SetParent(DamageTextParent);

        // ������ �ؽ�Ʈ�� RectTransform ������Ʈ ��������
        RectTransform rectTransform = damageText.GetComponent<RectTransform>();

        // ������ �ؽ�Ʈ�� ũ�� ���� (��: �ʺ� 100, ���� 50)
        rectTransform.sizeDelta = new Vector2(100, 50);

        // ��Ŀ ���� (��: ��� �߾�)
        rectTransform.anchorMin = new Vector2(0.5f, 1); // ���� �߾�
        rectTransform.anchorMax = new Vector2(0.5f, 1); // ���� �߾�
        rectTransform.pivot = new Vector2(0.5f, 1); // ���� �߾�

        // ���� ������ �ؽ�Ʈ�� ������ ���� ���ο� ������ �ؽ�Ʈ�� ��ġ ����
        rectTransform.anchoredPosition = new Vector2(0, -damageTexts.Count * (rectTransform.sizeDelta.y + 5)); // 5�� ���� ������

        damageTexts.Enqueue(damageText);
        Debug.Log(damageText + " " + text);
    }



    /// <summary>
    /// �� ��ٸ��� �뵵
    /// </summary>
    /// <param name="Active"></param>
    public void TurnActive(bool Active)
    {
        isTurnActive = Active;
    }

    /// <summary>
    /// �÷��̾ �� ����
    /// </summary>
    public void PlayerAttackchoice()
    {
        if(HpBarCoroutine != null)
        {
            return;
        }
        
        int EnemyHP = Player.Attack(Enemy);

        SetText("���Ϳ��� ������ 5�� �������ϴ�.");

        if (HpBarCoroutine == null)
            HpBarCoroutine = StartCoroutine(ChangeHPBar(EnemyHP, Enemy.MaxHp));
    }

    /// <summary>
    /// �� ���ʿ��� ���� ����
    /// </summary>
    void EnemyAttackchoice()
    {
        if(EnemyTurnCorou != null)
        {
            return;
        }
        int CurrentHp = Player.Hp;
        int PlayerHP = Enemy.Attack(Player);

        SetText("�� ����!");

        //Debug.Log(CurrentHp + " / " + PlayerHP);
        EnemyTurnCorou = StartCoroutine(HitPlayer(CurrentHp, PlayerHP));
        
    }
    /// <summary>
    /// ���� �ǰݽ�
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
        

        while (timer > 0) // timer�� 0���� Ŭ ������ �ݺ�
        {
            timer -= Time.deltaTime; // Ÿ�̸� ����
            EnemyHPBar.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, (1 - timer) / 1); // Ÿ�̸ӿ� ���� fillAmount ����
            yield return null; // �� ������ ��ٸ�
        }

        EnemyHPBar.fillAmount = targetFillAmount;

        

        HpBarCoroutine = null;
        TurnActive(true);
    }

    /// <summary>
    /// �÷��̾ �¾�����
    /// </summary>
    /// <param name="CurrentHP">�±��� ü��</param>
    /// <param name="Hp">���� �� ü��</param>
    /// <returns></returns>
    IEnumerator HitPlayer(int CurrentHP,int Hp)
    {
        int endHp = Hp; // ������ HP

        while (CurrentHP > endHp)
        {
            CurrentHP -= 1;
            HpText.text = $"{CurrentHP} / {Player.MaxHp}";
            yield return new WaitForSeconds(0.05f); // 1�ʿ� �� ���� �����ϵ��� ����
        }

        //���߿� �ڵ� �ٲܼ� ����. �׽�Ʈ�뵵�� ����
        yield return new WaitForSeconds(0.5f);
        ClearText();
        // ���Ұ� ������ �ڷ�ƾ ����
        EnemyTurnCorou = null;
    
    }


    #region �� �ǰݽ� �ڷ�ƾ
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

        Enemyimg.transform.localPosition = originPos; // �̹��� ����ġ
    }

    /// <summary>
    /// �̹��� ������
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

    //ü�� ���ݷ� �ӵ�
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
