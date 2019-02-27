﻿using System;
using UnityEngine;

namespace Config.Data
{
	[Serializable]
	public class SkillFrame
	{
		public int FrameIndex;
		public int AttackValue;
	}

	[CreateAssetMenu(menuName = "Custom/Design/Skill")]
	public class SkillDefinition : ScriptableObject
	{
		public string Name;
		public AnimationClip Animation;
		public SkillFrame[] Frames;
	}
}