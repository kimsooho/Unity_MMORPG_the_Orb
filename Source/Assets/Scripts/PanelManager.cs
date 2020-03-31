using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelManager : MonoBehaviour {

    SQLiteDB db;
    
    public GameObject panelInventory;
    public GameObject panelStatus;
    public GameObject panelSkill;
    public GameObject panelQuest;
    public GameObject panelPause;
    public GameObject panelOption;
    public GameObject panelDead;
    public GameObject panelMessage;
    public Player player;
    public Inventory inventory;
    //슬라이더
    public Slider sliderBGM;
    public Slider sliderEff;
    //토글
    public Toggle togBGMOn;
    public Toggle togBGMOff;
    public Toggle togEffOn;
    public Toggle togEffOff;
    bool openInventory;
    bool openStatus;
    bool openSkill;
    bool openQuest;
    bool openPause;
    bool befTogBGM;
    int befBGM;
    bool befTogEff;
    int befEff;
    // Use this for initialization
    void Start () {
        openInventory = false;
        openStatus = false;
        openSkill = false;
        openQuest = false;
        openPause = false;
        db = new SQLiteDB();

        string dbfilename = "MiniRPG.sqlite";
        string dbpath = Application.dataPath + "/StreamingAssets" + "/" + dbfilename;

        db.Open(dbpath);
    }
	
	// Update is called once per frame
	void Update () {
        OpenPanel();
        
	}

    void OpenPanel()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (openInventory)
            {
                panelInventory.SetActive(false);
                openInventory = false;
            }
            else
            {
                panelInventory.SetActive(true);
                openInventory = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (openStatus)
            {
                panelStatus.SetActive(false);
                openStatus = false;
            }
            else
            {
                panelStatus.SetActive(true);
                openStatus = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            if (openSkill)
            {
                panelSkill.SetActive(false);
                openSkill = false;
            }
            else
            {
                panelSkill.SetActive(true);
                openSkill = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            if (openQuest)
            {
                panelQuest.SetActive(false);
                openQuest = false;
            }
            else
            {
                panelQuest.SetActive(true);
                openQuest = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!openPause)
            {
                Time.timeScale = 0;
                panelPause.SetActive(true);
            }
        }
    }

    public void OnClickBtnResume()
    {
        Time.timeScale = 1;
        panelPause.SetActive(false);
    }
    public void OnClickBtnOption()
    {
        panelOption.SetActive(true);
        panelPause.SetActive(false);
        if (GameDirector.checkTogBGMOn)
        {
            togBGMOn.isOn = true;
            befTogBGM = true;
        }
        else
        {
            togBGMOn.isOn = false;
            befTogBGM = false;
        }
        if (GameDirector.checkTogEffOn)
        {
            togEffOn.isOn = true;
            befTogEff = true;
        }
        else
        {
            togEffOn.isOn = false;
            befTogEff = false;
        }
        sliderBGM.value = GameDirector.BGM;
        befBGM = GameDirector.BGM;
        sliderEff.value = GameDirector.eff;
        befEff = GameDirector.eff;
    }
    public void OnClickBtnConfirm()
    {
        //게임 감독 스크립트에 있게 될 static 변수 조작
        GameDirector.checkTogBGMOn = togBGMOn.isOn;
        GameDirector.BGM = (int)sliderBGM.value;
        GameDirector.checkTogEffOn = togEffOn.isOn;
        GameDirector.eff = (int)sliderEff.value;

        panelPause.SetActive(true);
        panelOption.SetActive(false);
    }
    public void OnClickBtnCancel()
    {
        //게임 감독 스크립트에 있게 될 static 변수 조작
        GameDirector.checkTogBGMOn = befTogBGM;
        GameDirector.BGM = befBGM;
        GameDirector.checkTogEffOn = befTogEff;
        GameDirector.eff = befEff;

        panelPause.SetActive(true);
        panelOption.SetActive(false);
    }
    public void OnClickBtnQuit()
    {
        if (GameDirector.inBossZone == false)
        {
            //레벨,최대HP,최대MP,최대EXP,HP,MP,EXP,X,Y,Z,STR,CON,AGI순서
            string query = "UPDATE Player SET LEVEL=?,MAX_HP=?,MAX_MP=?,MAX_EXP=?,HP=?,MP=?,EXP=?,X=?,Y=?,Z=?,STR=?,CON=?,AGI=?,POINT=? WHERE NAME=?;";
            SQLiteQuery qr = new SQLiteQuery(db, query);
            qr.Bind(player.Level);
            qr.Bind(player.MaxHp);
            qr.Bind(player.MaxMp);
            qr.Bind(player.MaxExp);
            qr.Bind(player.Hp);
            qr.Bind(player.Mp);
            qr.Bind(player.Exp);
            qr.Bind(player.X);
            qr.Bind(player.Y);
            qr.Bind(player.Z);
            qr.Bind(player.Str);
            qr.Bind(player.Con);
            qr.Bind(player.Agi);
            qr.Bind(player.Point);
            qr.Bind(player.name.GetComponent<TextMesh>().text);
            qr.Step();
            qr.Release();
            if (Inventory.items.Count > 0)
            {
                query = "UPDATE Inventory Set TYPE=?,EA=? WHERE SLOT=?;";
                qr = new SQLiteQuery(db, query);
                qr.Bind(Inventory.items[0].Type);
                qr.Bind(Inventory.items[0].Ea);
                qr.Bind("SLOT1");
                qr.Step();
                qr.Release();
            }
            if (Inventory.items.Count > 1)
            {
                query = "UPDATE Inventory Set TYPE=?,EA=? WHERE SLOT=?;";
                qr = new SQLiteQuery(db, query);
                qr.Bind(Inventory.items[1].Type);
                qr.Bind(Inventory.items[1].Ea);
                qr.Bind("SLOT2");
                qr.Step();
                qr.Release();
            }

            db.Close();

            GameObject.Find("GameDirector").GetComponent<AudioSource>().Stop();

            Application.LoadLevel("MainScene");
        }
        else
        {
            panelMessage.SetActive(true);
        }
    }
    public void OnClickReborn()
    {
        Vector3 rebornPos = new Vector3(-8.8f, 0, -333.7f);
        player.gameObject.transform.position = rebornPos;
        player.Hp = player.MaxHp;
        player.Mp = player.MaxMp;
        
        Time.timeScale = 1f;
        GameDirector.inBossZone = false;
        panelDead.SetActive(false);
    }
    public void OnClickBack()
    {
        panelMessage.SetActive(false);
    }
}
