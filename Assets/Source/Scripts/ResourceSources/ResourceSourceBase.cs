using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceSourceBase : MonoBehaviour, IMiniMapShows
{
    public MiniMapID MiniMapId => MiniMapID.ResourceSource;
    public event Action<Transform> RemoveMiniMapIcon;
}