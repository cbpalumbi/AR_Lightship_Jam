using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : MonoBehaviour
{
    // Start is called before the first frame update
    private List<string> inPot = new List<string>();
    private string currRecipe = "";
    Dictionary<string, string[]> recipes = new Dictionary<string, string[]>() {
        {"sushi", new [] {"rice", "fish", "seaweed"}},
        {"risotto", new [] {"rice", "cheese", "peas"}},
        {"samosa", new [] {"flour", "potato", "peas"}}
    };

    string[] food = new string[] {"sushi", "risotto", "samosa"};

    public GameObject sushi;
    public GameObject risotto;
    public GameObject samosa;

    public ParticleSystem failParticles;
    public ParticleSystem successParticles;

    private int numIngredient = 0;

    
    void Start()
    {
        currRecipe = food[Random.Range(0, 3)];
        sushi.GetComponent<Renderer>().enabled = false;
        risotto.GetComponent<Renderer>().enabled = false;
        samosa.GetComponent<Renderer>().enabled = false;
        failParticles.Stop();
        successParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other) {
        // Get the tag of the object and destroy object
        string tagHit = other.gameObject.tag;
        Debug.Log(tagHit);
        Debug.Log("curr recipe " + currRecipe);
        Destroy(other.gameObject);
        
        // Check ingredient added is valid
        // Invalid if the tag isn't an ingredient in the recipe or if
        // there is already one of that ingredient in the pot
        for (int i = 0; i < numIngredient; ++i) {
            if (tagHit == inPot[i]) {
                StartCoroutine(FailOrder());
                return;
            }
        }
        bool inRecipe = false;
        for (int i = 0; i < 3; ++i) {
            if (tagHit == recipes[currRecipe][i]) {
                inRecipe = true;
                break;
            }
        }
        if (!inRecipe) {
            StartCoroutine(FailOrder());
            return;
        }
        // Add ingredient tag to the pot
        // If there are 3 items check then recipe must be complete
        inPot.Add(tagHit);
        numIngredient += 1;
        if (numIngredient == 3) {
            StartCoroutine(RecipeComplete());
            return;
        }
    }

    private void ChangeRecipe(){
        // Pick random new recipe
        // Reset items in pot and number of ingredients
        currRecipe = food[Random.Range(0, 3)];
        inPot.Clear();
        numIngredient = 0;
    }

    IEnumerator RecipeComplete() {
        switch (currRecipe) {
            case "sushi":
                Debug.Log("showing sushi");
                sushi.GetComponent<Renderer>().enabled = true;
                break;
            case "risotto":
                risotto.GetComponent<Renderer>().enabled = true;
                break;
            case "samosa":
                samosa.GetComponent<Renderer>().enabled = true;
                break;
            default:
                Debug.Log("None recipe complete");
                break;
        }
        successParticles.Play();

        // Wait 3 seconds before making food vanish
        yield return new WaitForSeconds(3);
        successParticles.Stop();
        switch (currRecipe) {
            case "sushi":
                Debug.Log("No longer showing sushi");
                sushi.GetComponent<Renderer>().enabled = false;
                break;
            case "risotto":
                risotto.GetComponent<Renderer>().enabled = false;
                break;
            case "samosa":
                samosa.GetComponent<Renderer>().enabled = false;
                break;
            default:
                Debug.Log("None recipe complete");
                break;
        }
        ChangeRecipe();
    }

    IEnumerator FailOrder() {
        Debug.Log("Order Failed");
        failParticles.Play();
        yield return new WaitForSeconds(2);
        ChangeRecipe();
        failParticles.Stop();
    }
}
