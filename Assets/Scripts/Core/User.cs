using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace alexshko.fishingworld.Core
{
    public class User
    {
        public int Coins { get; set; }
        public int Emeralds { get; set; }

        public int CurrentLevel { get; set; }

        //the name of fish and how many times was caught
        public Dictionary<string, int> Fishes;

        //items bought:

        public User()
        {
            Coins = Emeralds = CurrentLevel = 0;
            Fishes = new Dictionary<string, int>();
        }

        public User(User u)
        {
            this.Coins = u.Coins;
            this.Emeralds = u.Emeralds;
            this.CurrentLevel = u.CurrentLevel;
        }
    }
}