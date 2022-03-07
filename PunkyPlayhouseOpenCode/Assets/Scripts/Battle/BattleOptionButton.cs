using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleOptionButton : MonoBehaviour
{

    public Text explanationText;

    public string optionToActivate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if the game object is selected, and the game object is a battle option button, then set explanation text to the buttons option to activate
        if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<BattleOptionButton>() != null)
        {
            explanationText.text = EventSystem.current.currentSelectedGameObject.GetComponent<BattleOptionButton>().optionToActivate;

            //change the highlighted buttons color to red
            ColorBlock colorRed = EventSystem.current.currentSelectedGameObject.GetComponent<Button>().colors;

            colorRed.selectedColor = new Color(1f, 0f, 0f);

            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().colors = colorRed;

        }
    }
}
