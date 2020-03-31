using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NpcInteraction : MonoBehaviour {

    public Transform smith;
    public Transform man;
    public Transform youth;
    public GameObject panelShop;
    public GameObject panelDialog;
    public Text dialogText;
    bool interactionStart;
	// Use this for initialization
	void Start () {
        interactionStart = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (interactionStart == false)
            {
                if (Vector3.Distance(gameObject.transform.position, smith.position) <= 2)
                {
                    panelShop.SetActive(true);
                    GameObject.Find("Canvas").transform.Find("PanelInventory").gameObject.SetActive(true);
                    interactionStart = true;
                }
                else if (Vector3.Distance(gameObject.transform.position, man.position) <= 2)
                {
                    panelDialog.SetActive(true);
                    dialogText.text = "촌장\n마을을 부탁드립니다.";
                    interactionStart = true;
                }
                else if (Vector3.Distance(gameObject.transform.position, youth.position) <= 2)
                {
                    panelDialog.SetActive(true);
                    dialogText.text = "청년\n몬스터들 때문에 불안해요.";
                    interactionStart = true;
                }
            }
            else
            {
                panelShop.SetActive(false);
                GameObject.Find("Canvas").transform.Find("PanelInventory").gameObject.SetActive(false);
                panelDialog.SetActive(false);
                interactionStart = false;
            }
        }
	}
}
