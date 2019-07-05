﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSection : MonoBehaviour
{
    public Vector3 pos;
    public Vector3 vel;

    //To write RopeSection.zero
    public static readonly RopeSection zero = new RopeSection(Vector3.zero);

    public RopeSection(Vector3 pos)
    {
        this.pos = pos;

        this.vel = Vector3.zero;
    }
}
