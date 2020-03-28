using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float backgroundSize;
    public float SizeZone;
    public float paralaxSpeed;

    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rigthIndex;
    private float lastCameraX;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            layers[i] = transform.GetChild(i);
        leftIndex = 0;
        rigthIndex = layers.Length - 1;
    }

    private void ScrollLeft()
    {
        int lastRigth = rigthIndex;
        layers[rigthIndex].position = new Vector3((layers[leftIndex].position.x - backgroundSize), layers[leftIndex].position.y, layers[leftIndex].position.z);
        leftIndex = rigthIndex;
        rigthIndex--;
        if (rigthIndex < 0) rigthIndex = layers.Length - 1;
    }

    private void ScrollRigth()
    {
        int lastLeft = leftIndex;
        layers[leftIndex].position = new Vector3((layers[rigthIndex].position.x + backgroundSize), layers[leftIndex].position.y, layers[leftIndex].position.z);
        rigthIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length) leftIndex = 0;
    }

    void FixedUpdate()
    {
        float deltaX = cameraTransform.position.x - lastCameraX;
        transform.position += Vector3.right * (deltaX * paralaxSpeed);
        lastCameraX = cameraTransform.position.x;
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone * SizeZone)) ScrollLeft();
        if (cameraTransform.position.x > (layers[rigthIndex].transform.position.x - viewZone * SizeZone)) ScrollRigth();
    }
}
