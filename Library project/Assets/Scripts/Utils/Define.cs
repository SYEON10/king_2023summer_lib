using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Define
{

    public enum Scene
    {
        Lobby,
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
