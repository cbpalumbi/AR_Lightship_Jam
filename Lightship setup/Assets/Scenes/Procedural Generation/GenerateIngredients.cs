using System;
using System.Collections;
using System.Collections.Generic;
 
using UnityEngine;
 
using Random = UnityEngine.Random;
 
public class GardenChunk: MonoBehaviour
{
  public List<GameObject> _groundPrefabs;
  public List<GameObject> _wallPrefabs;
  public float _groundNormalTolerance = 0.01f;
  public float _wallNormalTolerance = 0.001f;
  public int _spawnMinVertexCount = 100;
  public int _despawnMaxVertexCount = 50;
  public float _growthDuration = 4.0f;
  private MeshFilter _filter;
  private GameObject _ingredient;

  // List of all the GameObjects placed in the world
  // private List<GameObject> _worldIngredients;
  // Dictionary that contains how many of each ingredient
  // there are in the world
  // Dictionary<string, int> _ingredientCount = new Dictionary<string, int>(); 
 
  private void Start()
  {
    _filter = GetComponent<MeshFilter>();
 
    // don't update the object if there's no mesh filter
    enabled = (bool)_filter;
  }
 
  private void Update()
  {
    int vertexCount = _filter.sharedMesh.vertexCount;
    if (vertexCount >= _spawnMinVertexCount && !(bool)_ingredient)
    {
      // plant a plant! (might not succeed)
      _ingredient = InstantiatePlant(_filter.sharedMesh);
      // addIngredient(_ingredient);
      
    }
    else if (vertexCount <= _despawnMaxVertexCount && (bool)_ingredient)
    {
      // pull a plant!
      StopCoroutine(GrowPlant());
      // removeIngredient(_ingredient);
      Destroy(_ingredient);
      _ingredient = null;
    }
  }
 
  private GameObject InstantiatePlant(Mesh mesh)
  {
    bool wall;
    Vector3 position;
    Vector3 normal;
    // if we find a suitable vertex, plop a plant at that location
    if (FindVertex(_filter.sharedMesh, out wall, out position, out normal))
    {
      GameObject prefab = wall
        ? _wallPrefabs[Random.Range(0, _wallPrefabs.Count)]
        : _groundPrefabs[Random.Range(0, _groundPrefabs.Count)];
 
      Quaternion rotation = wall
        ? Quaternion.LookRotation(normal, Vector3.up)
        : Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);
 
      // use local position/rotation because of the different coordinate system
      GameObject plant = Instantiate(prefab, transform, false);
      plant.transform.localPosition = position;
      plant.transform.localRotation = rotation;
      plant.transform.localScale = Vector3.zero;
      StartCoroutine(GrowPlant());
      return plant;
    }
 
    return null;
  }
 
  private bool FindVertex(Mesh mesh, out bool wall, out Vector3 position, out Vector3 normal)
  {
    int v = Random.Range(0, mesh.vertexCount);
    position = mesh.vertices[v];
    normal = mesh.normals[v];
    bool ground = normal.y > 1.0f - _groundNormalTolerance && _groundPrefabs.Count > 0;
    wall = Math.Abs(normal.y) < _wallNormalTolerance && _wallPrefabs.Count > 0;
    return wall || ground;
  }
 
  private IEnumerator GrowPlant()
  {
    yield return null;
 
    float progress = 0.0f;
    // end scale has Y inverted because of the transform on the mesh root
    Vector3 endScale = new Vector3(0.1f, 0.1f, 0.1f);
    while (progress < 1.0f && (bool)_ingredient)
    {
      progress = Math.Min(1.0f, progress + Time.deltaTime / _growthDuration);
      _ingredient.transform.localScale = Vector3.Lerp(Vector3.zero, endScale, progress);
      yield return null;
    }
  }

  /*
  private void removeIngredient(GameObject ingridient) {
    _worldIngredients.Remove(_ingredient);
    
  }

  private void addIngredient(GameObject ingredient) {
    _worldIngredients.Add(_ingredient);
  }
  */
}