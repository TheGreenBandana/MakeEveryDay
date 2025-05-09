﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockEditor
{
    internal class Block
    {
        // Fields
        private string name;
        private int width;
        private Color color;
        private int healthMod;
        private int eduMod;
        private int happyMod;
        private int wealthMod;
        private CustomRange healthRange;
        private CustomRange eduRange;
        private CustomRange happyRange;
        private CustomRange wealthRange;
        private CustomRange ageRange;
        private int numSpawns;
        private int[] dependencies;
        private string deathMessage;

        // Properties
        public string Name { get => name; set => name = value; }
        public int Width { get => width; set => width = value; }
        public Color Color { get => color; set => color = value; }
        public int HealthMod { get => healthMod; set => healthMod = value; }
        public int EduMod { get => eduMod; set => eduMod = value; }
        public int HappyMod { get => happyMod; set => happyMod = value; }
        public int WealthMod { get => wealthMod; set => wealthMod = value; }
        public CustomRange HealthRange { get => healthRange; set => healthRange = value; }
        public CustomRange EduRange { get => eduRange; set => eduRange = value; }
        public CustomRange HappyRange { get => happyRange; set => happyRange = value; }
        public CustomRange WealthRange { get => wealthRange; set => wealthRange = value; }
        public CustomRange AgeRange { get => ageRange; set => ageRange = value; }
        public int NumSpawns { get => numSpawns; set => numSpawns = value; }
        public int[] Dependencies { get => dependencies; set => dependencies = value; }
        public string DeathMessage { get => deathMessage; set => deathMessage = value; }

        /// <summary>
        /// Creates a block with given values.
        /// </summary>
        public Block(string name, int width, Color color, int healthMod, int eduMod, int happyMod, int wealthMod,
            CustomRange healthRange, CustomRange eduRange, CustomRange happyRange, CustomRange wealthRange, CustomRange ageRange, int numSpawns, int[] dependencies, string deathMessage)
        {
            this.name = name;
            this.width = width;
            this.color = color;
            this.healthMod = healthMod;
            this.happyMod = happyMod;
            this.eduMod = eduMod;
            this.wealthMod = wealthMod;
            this.healthRange = healthRange;
            this.eduRange = eduRange;
            this.happyRange = happyRange;
            this.wealthRange = wealthRange;
            this.ageRange = ageRange;
            this.numSpawns = numSpawns;
            this.dependencies = dependencies;
            this.deathMessage = deathMessage.Length > 0 ? deathMessage : $"\"{name}\" killed you.";
        }

        /// <summary>
        /// Turns the block into something storable.
        /// </summary>
        /// <returns>The string of the block.</returns>
        public override string ToString()
        {
            string dependencyString = "";
            for (int i = 0; i < dependencies.Length; i++)
            {
                dependencyString += dependencies[i];
                if (i != dependencies.Length - 1)
                    dependencyString += ',';
            }
            if (dependencyString == "")
                dependencyString = "-1";
            return $"{name}|{width}|{color.ToArgb()}|{healthMod}|{happyMod}|{eduMod}|{wealthMod}|" +
                $"{healthRange.ToString()}|{eduRange.ToString()}|{happyRange.ToString()}|{wealthRange.ToString()}|" +
                $"{ageRange.ToString()}|{numSpawns.ToString()}|{dependencyString}|{deathMessage}";
        }
    }
}
