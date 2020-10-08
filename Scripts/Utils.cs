using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    public Vector3 GetMousePosition()
    {
        Vector3 dir = cam.ScreenToWorldPoint(Input.mousePosition);
        dir = new Vector3(dir.x, dir.y, 0);
        return dir;
    }

}
