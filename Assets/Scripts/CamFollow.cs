using UnityEngine;
using Unity.Cinemachine;
using TMPro;
using UnityEngine.Rendering;

public class CameraMouseLook2D : MonoBehaviour
{
    public Transform player;
    public float mouseInfluence;
    public float maxMouseInfluence;

    private CinemachinePositionComposer positionComp;
    private Camera mainCam;

    private Vector3 currentOffset;
    public float damping;

    void Start()
    {
        positionComp = this.GetComponent<CinemachinePositionComposer>();
        mainCam = Camera.main;
    }

    void Update()
    {
        Vector3 mouseWorld = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        float offsetDir = Vector2.Distance(mouseWorld, player.position) / mouseInfluence;
        offsetDir = Mathf.Clamp(offsetDir,0,maxMouseInfluence);

        //positionComp.TargetOffset = new Vector3(0, offsetDir, positionComp.TargetOffset.z);
        // Target offset you want to reach
        Vector3 targetOffset = new Vector3(0, offsetDir, 0);

        // Smoothly move toward the target offset
        currentOffset = Vector3.Lerp(currentOffset, targetOffset, Time.deltaTime * damping);

        // Apply smoothed offset
        positionComp.TargetOffset = currentOffset;
    }
}