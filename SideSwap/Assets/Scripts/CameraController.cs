using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls movement of camera, follows player and smooths when screenwrapping

public class CameraController : MonoBehaviour {

    public GameObject player;
    public PlayerController pc;

    private Vector3 offset; //difference between camera position and player position
    private float time = 0f;

	void Start () {
        offset = transform.position - player.transform.position;

        if ((pc == null) && (GetComponent<PlayerController>() != null))
        {
            pc = GetComponent<PlayerController>();
        }
	}

    // Smooth camera follow for player, reduces "jerkiness" when screenwrapping. Mathf.Lerp used so only x c0-0rds are affected.
    void LateUpdate()
    {
        if (pc != null && pc.is_player_screenwrapping) //player has just screenwrapped
        //lerp smooths the camera movement, slower speed so its never jerky
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x, time * 4f), transform.position.y, transform.position.z);
            time = +Time.deltaTime;
            StartCoroutine(endCameraSmoothing());
        }
        else
        //faster lerp for normal gameplay, camera follows player
        {
            time = +Time.deltaTime;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x, time * 8f), transform.position.y, transform.position.z);
        }
    }

    IEnumerator endCameraSmoothing()
    {
        yield return new WaitForSeconds(0.5f); //allows time for the player to finish screenwrapping
        pc.is_player_screenwrapping = false; //reverts back to normal follow speed
    }
}
