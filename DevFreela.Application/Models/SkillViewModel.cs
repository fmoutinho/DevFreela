﻿using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class SkillViewModel
    {
        public SkillViewModel(string description)
        {
            Description = description;
        }

        public string Description { get; set; }

        public static SkillViewModel FromEntity(Skill skill)
        {
            return new SkillViewModel(skill.Description);
        }
    }
}
