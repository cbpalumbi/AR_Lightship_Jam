using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image risotto;
    public Image samosa;
    public Image sushi;
    public string currRecipe;
    // Start is called before the first frame update
    void Start()
    {
        currRecipe = "samosa";
    }

    // Update is called once per frame
    void Update()
    {
        if(currRecipe=="samosa"){
            samosa.gameObject.SetActive(true);
            risotto.gameObject.SetActive(false);
            sushi.gameObject.SetActive(false);
        }
        else if(currRecipe=="risotto"){
            samosa.gameObject.SetActive(false);
            risotto.gameObject.SetActive(true);
            sushi.gameObject.SetActive(false);
        }
        else if(currRecipe=="sushi"){
            samosa.gameObject.SetActive(false);
            risotto.gameObject.SetActive(false);
            sushi.gameObject.SetActive(true);
        }
    }
}
