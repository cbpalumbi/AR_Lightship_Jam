using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFood : MonoBehaviour
{
    List<string> in_pot;
    bool fail_order;
    public string current_recipe; //whatever it actuallly is 
    Dictionary<string, string[]> myDict  = new Dictionary<string, string[]>();
    public string[] recipes = {"sushi", "risotto", "samosa"};
    int numOfIngredients = 0;
    public GameObject sushi;
    public GameObject risotto;
    public GameObject samosa;
    int curr_index;


    void FoodAppear () {
        Debug.Log("heyy");
        if (current_recipe == "sushi") {
            sushi.SetActive(true);
        }
        else if (current_recipe == "risotto") {
            risotto.SetActive(true);
        }
        else if (current_recipe == "samosa"){
            samosa.SetActive(true);
        }
        
    }

    void next_order() {
        curr_index = (curr_index + 1) % 3;
        current_recipe = recipes[curr_index];
    }

    void Check_Recipe_Complete() {
        numOfIngredients = 0;
        foreach(string ingredients in in_pot) {
            for (int i = 0; i < 3;  i++) {
                if (ingredients == myDict[current_recipe][i]) {
                    numOfIngredients++;
                }
            }
        }
        if (numOfIngredients == 3) {
            FoodAppear();
            next_order();
        }
    }

    void Fail_order() {
      in_pot.Clear();
    }

    void OnTriggerEnter (Collider other) {
        
        string tag_hit = other.gameObject.tag;
        Debug.Log(tag_hit);
        Destroy(other.gameObject);
        
        
        // in_pot.Add(other.gameObject.tag);

        if ((tag_hit == myDict[current_recipe][0]) || (tag_hit == myDict[current_recipe][1]) || 
            (tag_hit == myDict[current_recipe][2])) {
                Debug.Log("in the if");
            foreach(string ingredients in in_pot) {
                if (tag_hit == ingredients) {
                    Fail_order();
                    //return;
                    fail_order = true;
                }  
            }
            if (!fail_order) {
                in_pot.Add(tag_hit);
                Debug.Log("added " + tag_hit + " to pot");
                Check_Recipe_Complete();
            }
        }
        else {
            Fail_order();
        }
       
    }

    // Start is called before the first frame update
    void Start()
    {
    //collided_ingredients = new List<GameObject>();
    in_pot = new List<string>();

    string[] sushi_ingredients = {"rice", "seaweed", "fish"};
    string[] risotto_ingredients = {"rice", "cheese", "pea"};
    string[] samosa_ingredients = {"potato", "pea", "flour"};

    myDict.Add("sushi", sushi_ingredients);
    myDict.Add("risotto", risotto_ingredients);
    myDict.Add("samosa", samosa_ingredients);

    fail_order = false;
    curr_index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        //CollissionChecker();
        
    }
}
