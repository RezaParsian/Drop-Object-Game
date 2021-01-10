using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    public Image[] heart;
    int point;
    float dealy = 2;
    public void IncresePoint()
    {
        heart[point].color = Color.white;
        point++;
    }
}
