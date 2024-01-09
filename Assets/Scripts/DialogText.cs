using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "DialogText", menuName= "Dialogue/Text")]
public class DialogText : ScriptableObject {
    
    public enum Tag {
        SkillA, SkillB, SkillC
    };

    [SerializeField]
    private List<Tag> tags;
    public List<Tag> Tags => tags;

    [SerializeField]
    [TextArea] private string text;
    public string Text => text;

}
