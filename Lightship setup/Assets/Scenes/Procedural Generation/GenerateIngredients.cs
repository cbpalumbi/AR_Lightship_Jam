using System;
using System.Collections;
using System.Collections.Generic;
 
using UnityEngine;
 
using Random = UnityEngine.Random;
 
public class GenerateIngredients: MonoBehaviour
{
  public List<GameObject> _groundPrefabs;
  public List<GameObject> _wallPrefabs;
  public float _groundNormalTolerance = 0.01f;
  public float _wallNormalTolerance = 0.001f;
  public int _spawnMinVertexCount = 100;
  public int _despawnMaxVertexCount = 50;
  public float _growthDuration = 4.0f;
  public int _numIngredientsPerChunk = 1;
  private int _numIngredientsSpawned = 0;
  private MeshFilter _filter;
  private GameObject _ingredient;
 
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
      // Place a ingredient! (might not succeed)
      for (int i = 0; i < _numIngredientsPerChunk; ++i) {
        _ingredient = InstantiateIngredient(_filter.sharedMesh);
      }
    }
    else if (vertexCount <= _despawnMaxVertexCount && (bool)_ingredient)
    {
      // delete a ingredient!
      StopCoroutine(SpawnIngredient());
      Destroy(_ingredient);
      _ingredient = null;
      --_numIngredientsSpawned;
    }
  }
 
  private GameObject InstantiateIngredient(Mesh mesh)
  {
    bool wall;
    Vector3 position;
    Vector3 normal;
    // if we find a suitable vertex, plop a ingredient at that location
    if (FindVertex(_filter.sharedMesh, out wall, out position, out normal))
    {
      GameObject prefab = wall
        ? _wallPrefabs[Random.Range(0, _wallPrefabs.Count)]
        : _groundPrefabs[Random.Range(0, _groundPrefabs.Count)];
 
      Quaternion rotation = wall
        ? Quaternion.LookRotation(normal, Vector3.up)
        : Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);
 
      // use local position/rotation because of the different coordinate system
      GameObject ingredient = Instantiate(prefab, transform, false);
      Vector3 addVector = new Vector3(0.0f, -0.3f, 0.0f);
      ingredient.transform.localPosition = position + addVector;
      ingredient.transform.localRotation = rotation;
      ingredient.transform.localScale = Vector3.zero;
      StartCoroutine(SpawnIngredient());
      return ingredient;
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
 
  private IEnumerator SpawnIngredient()
  {
    yield return null;
 
    float progress = 0.0f;
    // end scale has Y inverted because of the transform on the mesh root
    Vector3 endScale = new Vector3(1.0f, -1.0f, 1.0f);
    while (progress < 1.0f && (bool)_ingredient)
    {
      progress = Math.Min(1.0f, progress + Time.deltaTime / _growthDuration);
      _ingredient.transform.localScale = Vector3.Lerp(Vector3.zero, endScale, progress);
      yield return null;
    }
  }
}