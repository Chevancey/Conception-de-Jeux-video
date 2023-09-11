using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Difficulty : ScriptableObject
{
    public List<float> blinkyIdleScatterChase = new List<float>(3);
    public List<float> PinkyIdleScatterChase = new List<float>(3);
    public List<float> InkyIdleScatterChase = new List<float>(3);
    public List<float> ClydeIdleScatterChase = new List<float>(3);
}
