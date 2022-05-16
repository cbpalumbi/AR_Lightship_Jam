using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image risotto;
    public Image samosa;
    public Image sushi;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRecipeImage(string recipe) {
        Debug.Log("uimanager recipe "+ recipe);
        if(recipe=="samosa"){
            samosa.gameObject.SetActive(true);
            risotto.gameObject.SetActive(false);
            sushi.gameObject.SetActive(false);
        }
        else if(recipe=="risotto"){
            samosa.gameObject.SetActive(false);
            risotto.gameObject.SetActive(true);
            sushi.gameObject.SetActive(false);
        }
        else if(recipe=="sushi"){
            samosa.gameObject.SetActive(false);
            risotto.gameObject.SetActive(false);
            sushi.gameObject.SetActive(true);
        }
    }
}
