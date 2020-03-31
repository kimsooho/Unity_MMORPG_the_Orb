using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public GameObject mobPrefab;
    GameObject monster;

    Vector3 createPos;

    float createTime;
    float time;

    bool isCreate;

	// Use this for initialization
	void Start () {
        createPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 3);

        monster = null;

        createTime = 10;
        isCreate = false;
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= createTime
            &&isCreate==false)
        {
            monster = (GameObject)Instantiate(mobPrefab,createPos, Quaternion.identity);
            monster.transform.parent = gameObject.transform;
            MonsterManager.monsters.Add(monster);
            isCreate = true;
        }
        if(monster==null
            && isCreate == true)
        {
            isCreate = false;
            time = 0;
        }
	}
}
