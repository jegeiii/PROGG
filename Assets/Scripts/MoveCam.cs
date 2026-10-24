using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Transform mainCam;
    public Vector3 inpVec;

    void OnTriggerEnter2D (Collider2D col)
    {
        Vector3 currentPos = mainCam.position;
        currentPos += inpVec;
        mainCam.position = currentPos;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
