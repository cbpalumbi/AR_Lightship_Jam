using System.Collections.Generic;

using Niantic.ARDK.AR;
using Niantic.ARDK.AR.Anchors;
using Niantic.ARDK.AR.ARSessionEventArgs;
using Niantic.ARDK.AR.HitTest;
using Niantic.ARDK.External;
using Niantic.ARDK.Utilities;
using Niantic.ARDK.Utilities.Logging;

using UnityEngine;
using Niantic.ARDKExamples.Helpers;
using Niantic.ARDK.Extensions;

public class PlaceCauldron : MonoBehaviour
{
    //public GameObject grabManager;
    public Camera Camera;

    /// The types of hit test results to filter against when performing a hit test.
    [EnumFlagAttribute]
    public ARHitTestResultType HitTestType = ARHitTestResultType.ExistingPlane;

    /// The object we will place when we get a valid hit test result!
    public GameObject PlacementObjectPf;

    /// A list of placed game objects to be destroyed in the OnDestroy method.
    private GameObject _placedObject;

    /// Internal reference to the session, used to get the current frame to hit test against.
    private IARSession _session;

    // Start is called before the first frame update
    private void Start()
    {
      ARSessionFactory.SessionInitialized += OnAnyARSessionDidInitialize;
    }

    private void OnAnyARSessionDidInitialize(AnyARSessionInitializedArgs args)
    {
      _session = args.Session;
      _session.Deinitialized += OnSessionDeinitialized;
    }

    private void OnSessionDeinitialized(ARSessionDeinitializedArgs args)
    {
      Destroy(_placedObject);
    }

    private void OnDestroy()
    {
      ARSessionFactory.SessionInitialized -= OnAnyARSessionDidInitialize;

      _session = null;

      Destroy(_placedObject);
    }

    private void Update()
    {
      if (_session == null)
      {
        return;
      }

      if (PlatformAgnosticInput.touchCount <= 0)
      {
        return;
      }

      var touch = PlatformAgnosticInput.GetTouch(0);
      if (touch.phase == TouchPhase.Began)
      {
        TouchBegan(touch);
      }

    }

    private void TouchBegan(Touch touch)
    {
      //grabManager.GetComponent<grabObject>().pickObj();
      
      var currentFrame = _session.CurrentFrame;
      if (currentFrame == null)
      {
        return;
      }

      var results = currentFrame.HitTest
      (
        Camera.pixelWidth,
        Camera.pixelHeight,
        touch.position,
        HitTestType
      );

      // Get the closest result
      var result = results[0];

      var hitPosition = result.WorldTransform.ToPosition();

      _placedObject = Instantiate(PlacementObjectPf, hitPosition, Quaternion.identity);
      var anchor = result.Anchor;
      GetComponent<ARPlaneManager>().enabled = false;
      GetComponent<PlaceCauldron>().enabled = false;
    }
}