using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour {

    public Button product1;
    public Button product2;
    public Button product3;
    public Button product4;

    public Inventory iv;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickProduct1() // Hp포션
    {
        iv.Add("hp");
    }
    public void OnClickProduct2() // Mp포션
    {
        iv.Add("mp");
    }
    public void OnClickProduct3() // x
    {

    }
    public void OnClickProduct4() // x
    {

    }
}
