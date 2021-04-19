using MiniJSON;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace alexshko.fishingworld.Core
{
    public class User
    {
        public int Coins;
        public int Emeralds;

        public int CurrentLevel;

        //the name of fish and how many times was caught
        public Dictionary<string, int> Fishes;

        //items bought:

        public User()
        {
            Coins = Emeralds = CurrentLevel = 0;
            Fishes = new Dictionary<string, int>();
            Fishes["Carppie"] = 2;
            Fishes["Locus"] = 3;
        }

        public User(User u)
        {
            this.Coins = u.Coins;
            this.Emeralds = u.Emeralds;
            this.CurrentLevel = u.CurrentLevel;
        }

        public string ToJson()
        {
            string fishesString = ",\"Fishes\":" + Json.Serialize(Fishes);
            string dataForJson = JsonUtility.ToJson(this);
            dataForJson = dataForJson.Substring(0, dataForJson.Length - 1) + fishesString + "}";
            return dataForJson;
        }
    }
}