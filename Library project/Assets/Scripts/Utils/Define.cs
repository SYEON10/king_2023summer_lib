using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Define
{

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum MonsterType 
    { 
        Unknown, 
        Flying, 
        Walking 
    } 
    public enum AttackType 
    { 
        Unkown, 
        Close, 
        Far 
    } 
    public enum State 
    { 
        Die, 
        Moving, 
        Idle, 
        Skill, 
    } 

    public enum Scene
    {
        Main,
        Game,
        Boss,
        Ending,
        Unknown,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }
    public enum MouseEvent
    {
        Press,
        Click,

    }

    public enum CameraMode
    {
        QuaterView,
    }

    public enum CreatureState
    {
        Idle,
        Moving,
        Skill,
        Dead,
    }
}
