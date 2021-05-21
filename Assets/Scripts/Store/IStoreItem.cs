﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace alexshko.fishingworld.Store
{
    public interface IStoreItem
    {
        int CoinsPrice { get; }
        int EmeraldPrice { get; }
        bool isAlreadyBaught { get; }
        bool isCapableBuying { get; }

        bool Buy();
        void Equip();

    }
}
