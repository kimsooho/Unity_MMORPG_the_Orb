using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour {
    
    public GameObject player;

    Controller c;

    public float offsetX = 0f;
    public float offsetY = 5f;
    public float offsetZ = -10f;
    Vector3 cameraPosition;

    private void OnEnable()
    {
        c = new Controller();
    }

    void LateUpdate () {
        if (GameDirector.inBossZone == false)
        {
            cameraPosition.x = player.transform.position.x + offsetX;
            cameraPosition.y = player.transform.position.y + offsetY;
            cameraPosition.z = player.transform.position.z + offsetZ;

            transform.position = Vector3.Lerp(transform.position, cameraPosition, 2.5f * Time.deltaTime);
        }
        else
        {
            cameraPosition.x = player.transform.position.x + offsetX;
            cameraPosition.y = player.transform.position.y + offsetY+10;
            cameraPosition.z = player.transform.position.z + offsetZ-5;

            transform.position = Vector3.Lerp(transform.position, cameraPosition, 2.5f * Time.deltaTime);
        }
	}
}
