using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControl : MonoBehaviour {
    public Player player;
    public GameObject HgToTown;
    public GameObject TownToHg;
    public GameObject HgToBoss;
    public GameObject BossToHg;
    public GameObject Player;   
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (this.gameObject.name == "PortalHgToTown")
            {
                Transform ParentTransform = other.transform;
                while (true)
                {
                    if (ParentTransform.parent == null)
                    {
                        break;
                    }
                    else
                    {
                        ParentTransform = ParentTransform.parent;
                    }
                }
                Vector3 dPosition = TownToHg.transform.position;
                dPosition.z -= 5;

                ParentTransform.position = dPosition;
            }
            else if (this.gameObject.name == "PortalTownToHg")
            {
                Transform ParentTransform = other.transform;
                while (true)
                {
                    if (ParentTransform.parent == null)
                    {
                        break;
                    }
                    else
                    {
                        ParentTransform = ParentTransform.parent;
                    }
                }
                Vector3 dPosition = HgToTown.transform.position;
                dPosition.z += 5;

                ParentTransform.position = dPosition;
            }
            else if (this.gameObject.name == "PortalHgToBoss")
            {
                if (player.Level == 10)
                {
                    GameDirector.inBossZone = true;
                    Transform ParentTransform = other.transform;
                    while (true)
                    {
                        if (ParentTransform.parent == null)
                        {
                            break;
                        }
                        else
                        {
                            ParentTransform = ParentTransform.parent;
                        }
                    }
                    Vector3 dPosition = BossToHg.transform.position;
                    dPosition.z += 5;

                    ParentTransform.position = dPosition;
                }
            }
            else if (this.gameObject.name == "PortalBossToHg")
            {
                GameDirector.inBossZone = false;
                Transform ParentTransform = other.transform;
                while (true)
                {
                    if (ParentTransform.parent == null)
                    {
                        break;
                    }
                    else
                    {
                        ParentTransform = ParentTransform.parent;
                    }
                }
                Vector3 dPosition = HgToBoss.transform.position;
                dPosition.z -= 5;

                ParentTransform.position = dPosition;
            }
        }
    }
}
