using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Attributes {
    Athletics, Evasion, Wits
};

[CreateAssetMenu(fileName= "New Character Attributes", menuName= "Attributes/Character")]
public class CharacterAttributes : ScriptableObject {
    
    [Header("Main Attributes")]
    [SerializeField] private int athletics;
    public int Athletics => athletics;
    [SerializeField] private int evasion;
    public int Evasion => evasion;
    [SerializeField] private int wits;
    public int Wits => wits;

    [Header("Movement Attributes")]
    [SerializeField] private float movementSpeed;
    public float MovementSpeed => movementSpeed;

    [SerializeField] private float rotationSpeed;
    public float RotationSpeed => rotationSpeed;
}
