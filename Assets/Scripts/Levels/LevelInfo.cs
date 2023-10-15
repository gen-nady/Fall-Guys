﻿using System;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "Levels", menuName = "CreateLevel", order = 0)]
    public class LevelInfo : ScriptableObject
    {
        public string Name;
        public Vector3 StartPosition;
        public int LevelIndex;
    }
}