using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlanetType { Gas, Stone };
public class Planet : MonoBehaviour
{
    [SerializeField] PlanetType m_type;

    public PlanetType Type { get => m_type; set => m_type = value; }
}
