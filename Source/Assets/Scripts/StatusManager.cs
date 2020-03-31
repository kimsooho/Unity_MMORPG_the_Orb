using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusManager : MonoBehaviour {

    public Player player;
    Controller controller;

    public Text level;
    public Text str;
    public Text agi;
    public Text con;
    public Text restPoint;

    int befStr;
    int befAgi;
    int befCon;
    int befPoint;
    int befHp;
    int befMaxHp;
    // Use this for initialization
    private void OnEnable()
    {
        level.text = player.Level.ToString();

        befStr = player.Str;
        befAgi = player.Agi;
        befCon = player.Con;
        befPoint = player.Point;
        befHp = player.Hp;
        befMaxHp = player.MaxHp;
        controller = player.GetComponent<Controller>();
    }

    void Update () {
        str.text = player.Str.ToString();
        agi.text = player.Agi.ToString();
        con.text = player.Con.ToString();
        
        restPoint.text = "남은 포인트 : " + player.Point;
        
	}

    public void OnClickStrMinus()
    {
        if (player.Str > befStr)
        {
            player.Str--;
            player.Point++;
        }
    }
    public void OnClickStrPlus()
    {
        if (player.Point > 0)
        {
            player.Str++;
            player.Point--;
        }
    }
    public void OnClickAgiMinus()
    {
        if (player.Agi > befStr)
        {
            player.Agi--;
            player.Point++;
            controller.speed -= 0.1f;
            controller.rSpeed -= 0.2f;
        }
    }
    public void OnClickAgiPlus()
    {
        if (player.Point > 0)
        {
            player.Agi++;
            player.Point--;
            controller.speed += 0.1f;
            controller.rSpeed += 0.2f;
        }
    }
    public void OnClickConMinus()
    {
        if (player.Con > befStr)
        {
            player.Con--;
            player.Point++;
            player.MaxHp -= 10;
            player.Hp -= 10;
        }
    }
    public void OnClickConPlus()
    {
        if (player.Point > 0)
        {
            player.Con++;
            player.Point--;
            player.MaxHp += 10;
            player.Hp += 10;
        }
    }

    public void OnClickCancel()
    {
        player.Str = befStr;
        player.Agi = befAgi;
        player.Con = befCon;
        player.Point = befPoint;
        player.MaxHp = befMaxHp;
        player.Hp = befHp;
        gameObject.SetActive(false);
    }
    public void OnClickConfirm()
    {
        gameObject.SetActive(false);
    }
}
