using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "New Character Attributes", menuName= "Attributes/Character")]
public class CharacterAttributes : ScriptableObject {
    
    [Header("Main Attributes")]
    [SerializeField] private int athletics;
    [SerializeField] private int evasion;
    [SerializeField] private int wits;

    [Header("Movement Attributes")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
}
