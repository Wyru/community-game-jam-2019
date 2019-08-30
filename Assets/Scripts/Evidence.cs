using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "Evidence", menuName = "CommunityGamJam2019/Evidence", order = 0)]
public class Evidence : ScriptableObject {
    public new string name;
    [TextArea]
    public string description;
    [TextArea]
    public string fakeDescription;
    public Sprite icon;
}