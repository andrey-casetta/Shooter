using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponWheel : MonoBehaviour
{
    private GameObject _wwPanel;
    void Start()
    {
        _wwPanel = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            Debug.Log(hit.collider.name);
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            _wwPanel.SetActive(true);
        }
        else
        {
            _wwPanel.SetActive(false);
        }
    }
}
