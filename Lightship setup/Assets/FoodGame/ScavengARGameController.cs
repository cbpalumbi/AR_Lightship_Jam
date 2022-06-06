using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.ARDK.VirtualStudio.AR.Mock;

public class ScavengARGameController : MonoBehaviour
{
#if UNITY_EDITOR

  void Start() {
    GetComponent<MockMesh>().enabled = true;
  }

#endif
}
