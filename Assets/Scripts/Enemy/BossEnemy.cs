using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossStat
{
    public float farDamage;
    public float nearDamage;
    public float moveSpeed;
    public float defenceValue;
    public float maxHP;
}

public enum BossPhase
{
    One,
    Two,
    Three,
    Death
}

public class BossEnemy : Enemy
{
    Vector3 localSacle;

    public BossStat bossStat = new BossStat();
    public float currentHP;
    public float CurrentHP { get { return currentHP; }  set { currentHP = value; } }
    public Transform playerTransform;

    #region Setting
    protected override void Awake()
    {
        localSacle = transform.localScale;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        hitColor = sprite.color;
    }

    protected override void OnEnable()
    {
        if (bossStat != null)
        {
            isDeath = false;
            CurrentHP = bossStat.maxHP;            
        }
    }

    public void Start()
    {
        GameManager.Instance.UI_Controller.BossHP.Init(bossStat);
        GameManager.Instance.UI_Controller.BossHP.OnOffUI(true);
        playerTransform = GameManager.Instance.Controller.gameObject.transform;
        farAttackCoolTimer = farAttackCoolTime - 0.5f;
    }

    public override void Hit(float _damage)
    {
        float trueDamage = _damage - bossStat.defenceValue;
        if (trueDamage <= 0)
            trueDamage = 1;
        CurrentHP -= trueDamage;
        GameManager.Instance.UI_Controller.BossHP.UpdateCurHpBar(currentHP);
        StartCoroutine(HitCor());
        if (CurrentHP <= 0)
        {
            isDeath = true;
            anim.SetBool("Death", true);
            gameObject.tag = "Death";
            // Boss Ending Trigger 발생
            GameManager.Instance.UI_Controller.BossHP.OnOffUI(false);
        }
    }
    #endregion

    #region AI

    public BossPhase currentPhase = BossPhase.One;
    public float farAttackCoolTimer = 0f;
    public float farAttackCoolTime = 5f;

    public float nearAttackCoolTimer = 0f;
    public float nearAttackCoolTime = 1.5f;

    public float spawnEnemyCoolTimer = 0;
    public float spawnEnemyCoolTime = 6f;

    [SerializeField] bool isFarAttacking = false;
    [SerializeField] bool isNearAttacking = false;

    private void Update()
    {
        switch (currentPhase)
        {
            case BossPhase.One:
                PhaseOne();
                break;
            case BossPhase.Two:
                PhaseTwo();
                break;
            case BossPhase.Three:
                PhaseThree();
                break;
            case BossPhase.Death:
                break;
        }    
    }

    public void PhaseOne()
    {
        FarAttack();
        UpdateAnim();
        if (currentHP < 60)
        {
            currentPhase = BossPhase.Two;
            nearAttackCoolTimer = nearAttackCoolTime;
        }
    }

    public void PhaseTwo()
    {
        FarAttack();
        NearAttack();
        UpdateAnim();
        if (currentHP < 30)
        {
            currentPhase = BossPhase.Three;
            spawnEnemyCoolTimer = spawnEnemyCoolTime;
        }
    }

    public void PhaseThree()
    {
        FarAttack();
        NearAttack();
        SpawnEnemy();
        UpdateAnim();
    }

    [SerializeField] GameObject nearAttackObj;
    [SerializeField] float nearAttackRange = 3f;
    public void NearAttack()
    {
        if (isFarAttacking)
        {
            nearAttackCoolTimer += Time.deltaTime;
            return;
        }
        if (nearAttackCoolTimer > nearAttackCoolTime)
        {
            if(nearAttackRange >= Vector2.Distance(playerTransform.position, transform.position))
            {
                rigid.velocity = Vector2.zero;
                isNearAttacking = true;
                anim.SetTrigger("NearAttack");
                nearAttackCoolTimer = 0f;
            }
            else
            {
                if (playerTransform.position.x < transform.position.x) // 플레이어가 왼쪽에 있음
                    rigid.velocity = new Vector2(-1 * bossStat.moveSpeed, rigid.velocity.y);
                else
                    rigid.velocity = new Vector2(bossStat.moveSpeed, rigid.velocity.y);
            }
        }
        else
        {
            nearAttackCoolTimer += Time.deltaTime;
        }
    }
    public void ActiveNearAttack() { nearAttackObj.SetActive(true); }

    public void EndNearAttack() { isNearAttacking = false; nearAttackObj.SetActive(false); }

    public void FarAttack()
    {
        if (isNearAttacking)
        {
            farAttackCoolTimer += Time.deltaTime;
            return;
        }
        if (farAttackCoolTimer > farAttackCoolTime)
        {
            rigid.velocity = Vector2.zero;
            isFarAttacking = true;
            anim.SetTrigger("FarAttack");
            GameObject go1 = PoolManager.Instace.GetObjectPool("BossFarAttack");
            go1.transform.position = playerTransform.position + new Vector3(-2f,2.6f, 0f);
            go1.transform.position = new Vector2(go1.transform.position.x, 2.6f);
            go1.GetComponent<BossAttackTrigger>().Setting(this);
            GameObject go2 = PoolManager.Instace.GetObjectPool("BossFarAttack");
            go2.transform.position = playerTransform.position + new Vector3(2f, 2.6f, 0f);
            go2.transform.position = new Vector2(go2.transform.position.x, 2.6f);
            go2.GetComponent<BossAttackTrigger>().Setting(this);
            GameObject go3 = PoolManager.Instace.GetObjectPool("BossFarAttack");
            go3.transform.position = playerTransform.position + new Vector3(0, 2.6f, 0f);
            go3.transform.position = new Vector2(go3.transform.position.x, 2.6f);
            go3.GetComponent<BossAttackTrigger>().Setting(this);
            farAttackCoolTimer = 0f;
        }
        else
        {
            farAttackCoolTimer += Time.deltaTime;
        }
    }

    public void EndFarAttack() {  isFarAttacking = false;  }

    [SerializeField] Transform[] spawnPositions;
    List<GameObject> spawnEnemyList = new List<GameObject>();
    public void SpawnEnemy()
    {
        if (spawnEnemyCoolTimer > spawnEnemyCoolTime)
        {
            int randNum = Random.Range(0, 2);
            int randNumSpawnPosCnt = Random.Range(0, spawnPositions.Length);
            if (randNum == 0)
                spawnEnemyList.Add(PoolManager.Instace.BossEnemySpawn("DarkSwordEnemy", spawnPositions[randNumSpawnPosCnt].position));
            else
                spawnEnemyList.Add(PoolManager.Instace.BossEnemySpawn("WizardEnemy", spawnPositions[randNumSpawnPosCnt].position));
            spawnEnemyCoolTimer = 0f;
        }
        else
        {
            spawnEnemyCoolTimer += Time.deltaTime;
        }
    }

    public void UpdateAnim()
    {
        if (Mathf.Abs(rigid.velocity.x) < 0.1f)
            anim.SetBool("Idle", true);
        else
            anim.SetBool("Idle", false);
        Flip();
    }


    bool canFlip = true;
    float flipTime = 0.2f;
    public void Flip()
    {
        if (canFlip)
        {
            if (transform.localScale.x < 0 && playerTransform.position.x > transform.position.x)
                transform.localScale = new Vector3(localSacle.x, localSacle.y, localSacle.z);
            else if(transform.localScale.x > 0 && playerTransform.position.x < transform.position.x)
                transform.localScale = new Vector3(-1*localSacle.x, localSacle.y, localSacle.z);
            canFlip = false;
            StartCoroutine(FlipCor());
        }
    }

    public IEnumerator FlipCor()
    {
        yield return new WaitForSeconds(flipTime);
        canFlip = true;
    }

    public void Death()
    {
        int cnt = spawnEnemyList.Count;
        for (int i = cnt - 1; i >= 0; i--)
            Destroy(spawnEnemyList[i].gameObject);
        PoolManager.Instace.DeleteBossEnemyList();
        currentPhase = BossPhase.Death;
        GameManager.Instance.UI_Controller.Dialogue.StartDialogue(3);
    }
    #endregion
}
