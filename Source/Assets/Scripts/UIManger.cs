using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManger : MonoBehaviour {

    private SQLiteDB db = null;
    
    //버튼
    public Button btnGameStart;
    public Button btnOption;
    public Button btnQuit;
    public Button btnConfirm;
    public Button btnCancle;
    //슬라이더
    public Slider sliderBGM;
    public Slider sliderEff;
    //토글
    public Toggle togBGMOn;
    public Toggle togBGMOff;
    public Toggle togEffOn;
    public Toggle togEffOff;
    //인풋 필드
    public InputField inputName;
    //패널
    public GameObject panelOption;
    public GameObject panelCreate;
    //기타 변수
    bool befTogBGM;
    int befBGM;
    bool befTogEff;
    int befEff;
    private void Start()
    {
        db = new SQLiteDB();

        string dbfilename = "MiniRPG.sqlite";
        string dbpath = Application.dataPath + "/StreamingAssets" + "/" + dbfilename;

        db.Open(dbpath);
    }
    public void OnClickBtnGameStart()
    {
        string query = "SELECT COUNT(*) AS NUM FROM Player;";
        int num=0;
        SQLiteQuery qr;
        qr = new SQLiteQuery(db, query);
        if (qr.Step())
        {
            num = qr.GetInteger("NUM");
        }
        if (num == 0)
        {
            btnGameStart.enabled = false;
            btnOption.enabled = false;
            btnQuit.enabled = false;
            panelCreate.SetActive(true);
            qr.Release();
        }
        else
        {
            
            db.Close();
            GameObject.Find("GameDirector").GetComponent<AudioSource>().Stop();
            Application.LoadLevel("GameScene");
        }
        
    }

    public void OnClickBtnOption()
    {
        btnGameStart.enabled = false;
        btnOption.enabled = false;
        btnQuit.enabled = false;
        panelOption.SetActive(true);
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

        btnGameStart.enabled = true;
        btnOption.enabled = true;
        btnQuit.enabled = true;
        panelOption.SetActive(false);
    }
    public void OnClickBtnCancel()
    {
        //게임 감독 스크립트에 있게 될 static 변수 조작
        GameDirector.checkTogBGMOn = befTogBGM;
        GameDirector.BGM = befBGM;
        GameDirector.checkTogEffOn = befTogEff;
        GameDirector.eff = befEff;

        btnGameStart.enabled = true;
        btnOption.enabled = true;
        btnQuit.enabled = true;
        panelOption.SetActive(false);
    }
    public void OnClickBtnQuit()
    {
        Application.Quit();
    }
    public void OnClickBtnCreate()
    {
        if (inputName.text == "")
        {
            return;
        }
        else
        {
            //닉네임,레벨,최대HP,최대MP,최대EXP,HP,MP,EXP,X,Y,Z,STR,CON,AGI순서
            string query = "INSERT INTO Player VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);";
            SQLiteQuery qr = new SQLiteQuery(db, query);
            qr.Bind(inputName.text.ToString());
            qr.Bind(1);
            qr.Bind(100);
            qr.Bind(100);
            qr.Bind(100);
            qr.Bind(100);
            qr.Bind(100);
            qr.Bind(0);
            qr.Bind(-9);
            qr.Bind(2);
            qr.Bind(-334);
            qr.Bind(10);
            qr.Bind(10);
            qr.Bind(10);
            qr.Bind(0);
            qr.Step();
            qr.Release();
            btnGameStart.enabled = true;
            btnOption.enabled = true;
            btnQuit.enabled = true;
            panelCreate.SetActive(false);
        }
    }
}
