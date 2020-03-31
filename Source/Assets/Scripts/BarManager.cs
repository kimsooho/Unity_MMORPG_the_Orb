using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour {

    public Player Player;

    public Image HpBar;
    public Image MpBar;
    public Image ExpBar;
    public Text hp;
    public Text mp;
    public Text exp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HpBar.fillAmount = (float)Player.Hp/Player.MaxHp;
        MpBar.fillAmount = (float)Player.Mp / Player.MaxMp ;
        ExpBar.fillAmount = (float)Player.Exp/Player.MaxExp;
        hp.text = Player.Hp.ToString()+" / "+Player.MaxHp;
        mp.text = Player.Mp.ToString()+" / "+Player.MaxMp;
        exp.text = Player.Exp.ToString()+" / "+Player.MaxExp;
	}
}
