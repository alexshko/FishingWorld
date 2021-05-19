﻿using MiniJSON;
using System.Collections.Generic;
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
        //name of rods that were bought. each one has a boolean that says if its currently beeing used.
        public Dictionary<string, bool> RodsBought;
        public string CurrentRod { get
            {
                foreach (var keval in RodsBought)
                {
                    if (keval.Value)
                    {
                        return keval.Key;
                    }
                }
                return null;
            } 
        }
        public void UnSetAllRods()
        {
            foreach (var key in RodsBought.Keys)
            {
                RodsBought[key] = false;
            }
        }

        public User()
        {
            Coins = Emeralds  = 0;
            CurrentLevel = 1;
            Fishes = new Dictionary<string, int>();
            //Fishes["Carppie"] = 2;
            //Fishes["Locus"] = 3;

            RodsBought = new Dictionary<string, bool>();
            RodsBought["SimpleRod"] = true;
        }

        public User(User u)
        {
            this.Coins = u.Coins;
            this.Emeralds = u.Emeralds;
            this.CurrentLevel = u.CurrentLevel;
            this.RodsBought = new Dictionary<string, bool>(u.RodsBought);
        }

        public string ToJson()
        {
            string fishesString = ",\"Fishes\":" + Json.Serialize(Fishes);
            string RodsString = ",\"Rods\":" + Json.Serialize(RodsBought);
            string dataForJson = JsonUtility.ToJson(this);
            dataForJson = dataForJson.Substring(0, dataForJson.Length - 1) + fishesString + RodsString + "}";
            return dataForJson;
        }

        public static User FromJson(string jsonString)
        {
            return (JsonUtility.FromJson<User>(jsonString));
        }
    }
}