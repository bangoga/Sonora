using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlace : MonoBehaviour
{
    public GameObject visual;
    public GameObject objectToPlace;
    
    private ARRaycastManager rayManager;
    private ARSessionOrigin arOrigin;
    private GameObject current;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    // Start is called before the first frame update
    void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        objectToPlace.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        //objectToPlace.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        arOrigin.MakeContentAppearAt(objectToPlace.transform, placementPose.position, placementPose.rotation);
        objectToPlace.SetActive(true);
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(screenCenter, hits, TrackableType.Planes);
        placementPoseIsValid = hits.Count > 0;
        if(placementPoseIsValid)
        {
            placementPose = hits[0].pose;
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            visual.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
    }
}
