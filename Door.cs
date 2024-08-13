using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * This class handles scene changes, as well as the aesthetic properties that come with it
 * This includes rotating the player towards the door, starting the enter/walk animation, initiating the fade out UI element and
 */
public class Door : MonoBehaviour
{

    public string toLevel;
    public static Vector3 a_spawn; //used to change spawn point between scenes
    public static Vector3 a_direction; //used to change spawn direction between scenes
    public Vector3 spawn; //used to change spawn point in Editor
    private RectTransform _fishFade;

    private void Start()
    {
        //store fish fade component, and set scale to be max of 5 (this keeps it from actually appearing on screen)
        _fishFade = GameObject.Find("FishFadeOut").GetComponent<RectTransform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            //player has entered the door

            //begins the animation, stops player from moving and rotates towards door
            Human.player.SetDoorEnter(this.transform);

            _fishFade.GetComponent<Animator>().SetBool("fadein", true);

            //start UI fade
            StartDoorFade();
        }
    }

    void OnSceneLoaded() { 
    
    }

    void StartDoorFade() {
        StartCoroutine("EnterDoor");
    }

    IEnumerator EnterDoor() {

        a_spawn = spawn;

        //TODO: make doors face the direction that player is expected to leave from; will no longer need to add the (-) here
        a_direction = GetComponentInParent<Transform>().forward;

        //wait for UI to fade all the way out
        for (float i = 0; i < 0.99f; i += Time.fixedDeltaTime) {
           
            yield return null;
        }

        //after
        SceneManager.LoadSceneAsync(toLevel, LoadSceneMode.Single);
    }
}
