using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSOutlineControl : MonoBehaviour
{
    public Outline Class_Outline;
    // Start is called before the first frame update
    void Start()
    {
        Class_Outline = this.GetComponent<Outline>();
    }

    public void ChangeWidth(float number)
    {
        Class_Outline.OutlineWidth = number;
    }

    public void ChangeColor(Color NewColor)
    {
        Class_Outline.OutlineColor = NewColor;
    }

}
