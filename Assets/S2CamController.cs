using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2CamController : MonoBehaviour
{
    private bool isCamZoom = false;
    [SerializeField] private Animator Cam;
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isCamZoom)
        {
            Cam.CrossFade(CamZoom, 0, 0);

        }
    }

    private static readonly int CamZoom = Animator.StringToHash("CameraStartZoomOut");
    private static readonly int CamNor = Animator.StringToHash("CamNormal");



}
