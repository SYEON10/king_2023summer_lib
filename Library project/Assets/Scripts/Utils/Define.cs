using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Monster,
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
        Lobby,
        Game,
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
