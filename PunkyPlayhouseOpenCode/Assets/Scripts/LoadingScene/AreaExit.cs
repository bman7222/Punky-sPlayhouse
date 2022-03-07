using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{

    public string areaToLoad,areaTransitionName;

    public float waitToLoad = 1f;

    private bool shouldLoadAfterFade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //if it should fade, decrease wait to load time by 1 every frame
        if (shouldLoadAfterFade)
        {
            waitToLoad -= Time.deltaTime;

            //if wait to load is <=0 stop should fade, and go to the next scene
            if (waitToLoad <= 0)
            {
                shouldLoadAfterFade = false;

                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    //on enter, if plater, load scene and change transition name
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            shouldLoadAfterFade = true;

            UIFade.Instance.fadetoBlack();

            PlayerController.Instance.areaTransitionName = areaTransitionName;
        }
    }

}
