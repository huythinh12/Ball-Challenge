using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOfLand : MonoBehaviour
{
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;
        StartCoroutine(ChangeColor());

    }
    IEnumerator ChangeColor()
    {
        yield return null;
        float currentValue = 2;
        float total = 15;
        float valuePerTime = (currentValue / total);
        while (true)
        {
            yield return new WaitForFixedUpdate();
            material.color = new Color(material.color.r, material.color.g, material.color.b, 0f);


        }
    }
    // Update is called once per frame
    void Update()
    {

     

    }
}
