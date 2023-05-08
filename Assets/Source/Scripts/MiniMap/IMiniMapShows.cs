using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IMiniMapShows
{
    public MiniMapID MiniMapId { get; }
    public Transform Transform { get; }
}