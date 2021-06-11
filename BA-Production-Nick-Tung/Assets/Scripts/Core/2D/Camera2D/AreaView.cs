using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaView : MonoBehaviour
{
    [SerializeField]
    List<Transform> objectsInArea = new List<Transform>();

    [SerializeField]
    CameraFollow cameraFollow = null;
    [SerializeField]
    CameraZoom cameraZoom = null;
    [SerializeField]
    float viewTime = 0;
    [SerializeField]
    float zoomFollowPercentage = 0 ;
    [SerializeField]
    float focusCenterPercentage = 0;
    bool isViewing = false;
    [SerializeField]
    float personalMaxZoom = 20;

    float cachedZoomFollowPercentage = 0;
    float cachedZoomValued = 0;
    float cachedFocusCenterPercentage = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void View()
    {
        if (isViewing) return;

        cameraFollow.Clear(true);
        cameraZoom.Clear(true);

        cachedFocusCenterPercentage = cameraFollow.GetFollowPercentage();
        cachedZoomFollowPercentage = cameraZoom.GetFollowPercentage();
        cachedZoomValued = cameraZoom.GetMaxZoom();

        cameraFollow.SetFollowPercentage(focusCenterPercentage);
        cameraZoom.SetFollowPercentage(zoomFollowPercentage);
        cameraZoom.SetMaxZoom(personalMaxZoom);

        foreach (var obj in objectsInArea)
        {
            cameraFollow.AddEncapsolateObject(obj);
            cameraZoom.AddEncapsolateObject(obj);
        }

        isViewing = true;
        Invoke("UnView", viewTime);
    }
    public void UnView()
    {
        if (isViewing == false) return;

        cameraFollow.Clear(false);
        cameraZoom.Clear(false);

        cameraFollow.SetFollowPercentage(cachedFocusCenterPercentage);
        cameraZoom.SetFollowPercentage(cachedZoomFollowPercentage);
        cameraZoom.SetMaxZoom(cachedZoomValued);

        isViewing = false;
    }
}
