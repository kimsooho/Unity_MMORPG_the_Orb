using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Controller))]

public class Player : MonoBehaviour {

    SQLiteDB db;

    public GameObject name;

    AudioSource audio;

    //기본 캐릭터 정보
    private int level;
    private int maxExp;
    private int maxHp;
    private int maxMp;
    private int exp;
    private int hp;
    private int mp;

    //캐릭터 스텟
    private int str; // 힘 : 공격력
    private int con; // 체력 : HP
    private int agi; // 민첩성 : 공격속도, 이동속도

    //스텟 포인트
    private int point;
    //종료 시 위치
    private double x; // 종료시 x좌표
    private double y; // 종료시 y좌표
    private double z;

    private bool isDead;
    private bool isInvincible;
    //프로퍼티
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }
    public int MaxExp
    {
        get
        {
            return maxExp;
        }
        set
        {
            maxExp = value;
        }
    }
    public int MaxHp
    {
        get
        {
            return maxHp;
        }
        set
        {
            maxHp = value;
        }
    }
    public int MaxMp
    {
        get
        {
            return maxMp;
        }
        set
        {
            maxMp = value;
        }
    }
    public int Exp
    {
        get
        {
            return exp;
        }
        set
        {
            exp = value;
        }
    }
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }
    public int Mp
    {
        get
        {
            return mp;
        }
        set
        {
            mp = value;
        }
    }
    public int Str
    {
        get
        {
            return str;
        }
        set
        {
            str = value;
        }
    }
    public int Con
    {
        get
        {
            return con;
        }
        set
        {
            con = value;
        }
    }
    public int Agi
    {
        get
        {
            return agi;
        }
        set
        {
            agi = value;
        }
    }
    public double X
    {
        get
        {
            return x;
        }
        set
        {
            x = value;
        }
    }
    public double Y
    {
        get
        {
            return y;
        }
        set
        {
            y = value;
        }
    }
    public double Z
    {
        get
        {
            return z;
        }
        set
        {
            z = value;
        }
    }
    public int Point
    {
        get
        {
            return point;
        }
        set
        {
            point = value;
        }
    }
    public bool IsInvincible
    {
        get
        {
            return isInvincible;
        }
        set
        {
            isInvincible = value;
        }
    }
	// Use this for initialization
	void Start () {
        //데이터베이스에서 불러와야함
        db = new SQLiteDB();
        audio = GetComponent<AudioSource>();
        string dbfilename = "MiniRPG.sqlite";
        string dbpath = Application.dataPath + "/StreamingAssets" + "/" + dbfilename;
        
        db.Open(dbpath);
        
        PlayerInit();
        isDead = false;
        isInvincible = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameDirector.checkTogEffOn)
            audio.volume = GameDirector.eff * 0.01f;
        else
            audio.volume = 0;
        name.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2.36f, gameObject.transform.position.z);
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        z = gameObject.transform.position.z;

        LevelUp();
        Dead();
	}

    void PlayerInit()
    {
        string query = "SELECT * FROM Player;";

        SQLiteQuery qr = new SQLiteQuery(db, query);

        if (qr.Step())
        {
            name.GetComponent<TextMesh>().text = qr.GetString("NAME");
            level = qr.GetInteger("LEVEL");
            maxHp = qr.GetInteger("MAX_HP");
            maxMp = qr.GetInteger("MAX_MP");
            maxExp = qr.GetInteger("MAX_EXP");
            hp = qr.GetInteger("HP");
            mp = qr.GetInteger("MP");
            exp = qr.GetInteger("EXP");
            x = qr.GetDouble("X");
            y = qr.GetDouble("Y");
            z = qr.GetDouble("Z");
            str = qr.GetInteger("STR");
            con = qr.GetInteger("CON");
            agi = qr.GetInteger("AGI");
            point = qr.GetInteger("POINT");
            gameObject.transform.position = new Vector3((float)x, (float)y, (float)z);
        }
        qr.Release();
    }

    void LevelUp()
    {
        if (level == 10)
            exp = maxExp;
        else
        {
            if (exp >= maxExp)
            {
                audio = GameObject.Find("LevelUp").GetComponent<AudioSource>();
                audio.Play();
                exp = exp % maxExp;
                level++;
                maxExp += 50;
                point += 5;
                maxHp += 20;
                hp = MaxHp;
                maxMp += 10;
                mp = MaxMp;
            }
        }
    }
    void Dead()
    {
        if (hp <= 0&&isDead==false)
        {
            hp = 0;
            isDead = true;
            audio = GameObject.Find("Dead").GetComponent<AudioSource>();
            audio.Play();
            Time.timeScale = 0;
            GameObject.Find("Canvas").transform.Find("PanelDead").gameObject.SetActive(true);
        }
    }
}
