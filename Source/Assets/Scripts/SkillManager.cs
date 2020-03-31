using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillManager : MonoBehaviour {

    public Player player;
    public GameObject bladePrefab;
    public GameObject shieldPrefab;
    public Text CoolQ;
    public Text CoolW;

    public Sprite[] sprites;

    GameObject blade;
    GameObject shield;
    GameObject target;
    
    bool buff;
    bool bladeEnable;
    bool shieldEnable;
    float bladeCool;
    float shieldCool;
    

	// Use this for initialization
	void Start () {
        buff = false;
        bladeEnable = true;
        shieldEnable = true;
        target = player.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (bladeEnable == true)
            CoolQ.text = "";
        else
        {
            CoolQ.text = (5 - (int)bladeCool).ToString();
        }
        if (shieldEnable == true)
            CoolW.text = "";
        else
        {
            CoolW.text = (15 - (int)shieldCool).ToString();
        }
        if (Input.GetKeyDown(KeyCode.Q)
            && bladeEnable == true)
        {
            Blade();
        }
        if (Input.GetKeyDown(KeyCode.W)
            &&buff==false
            &&shieldEnable==true)
        {
            Shield();
        }
        if (buff)
        {
            Vector3 v = player.transform.position;
            v.y += 3;
            shield.transform.position = v;
        }
        bladeCool += Time.deltaTime;
        shieldCool += Time.deltaTime;
        if (bladeCool >= 5)
            bladeEnable = true;
        if (shieldCool >= 15)
            shieldEnable = true;
	}

    void Blade()
    {
        if (player.Mp >= 50)
        {
            foreach (GameObject monster in MonsterManager.monsters)
            {
                if (Vector3.Distance(player.gameObject.transform.position, monster.transform.position) <= 5)
                {
                    target = monster;
                    break;
                }
                else
                {
                    target = player.gameObject;
                }
            }
            bladeEnable = false;
            bladeCool = 0;
            Vector3 v = target.transform.position;
            v.y += 3;
            blade = (GameObject)Instantiate(bladePrefab, v, Quaternion.identity);
            player.Mp -= 50;
        }
    }
    void Shield()
    {
        if (player.Mp >= 80)
        {
            shieldEnable = false;
            shieldCool = 0;
            Vector3 v = player.transform.position;
            v.y += 3;
            shield = (GameObject)Instantiate(shieldPrefab, v, Quaternion.identity);
            buff = true;
            player.IsInvincible = true;
            player.Mp -= 80;
            Invoke("finish", 5);
        }
    }
    void finish()
    {
        buff = false;
        player.IsInvincible = false;
        Destroy(shield);
    }
}
