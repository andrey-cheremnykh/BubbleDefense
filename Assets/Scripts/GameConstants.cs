using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants
{
    public static readonly int[] PRICES_FOR_ARCHER = {10, 30, 70, 200, 250};
    public static readonly int[] PRICES_FOR_CANNON = {20, 60, 130, 280, 280};
    public static readonly int[] PRICES_FOR_MAGIC = {8, 22, 56, 160, 190};

    public static readonly int[] DAMAGE_FOR_ARCHER = { 10, 22, 60, 70, 60 };
    public static readonly int[] RADIUS_FOR_ARCHER = { 15, 18, 20, 25, 18 };

    public static readonly int[] DAMAGE_FOR_CANNON = { 15, 30, 65, 130, 160 };
    public static readonly int[] RADIUS_FOR_CANNON = { 18, 22, 27, 32, 27 };
    public static readonly int[] RADIUS_EXPLOSION_CANNON = { 8, 10, 12, 12, 16 };

    public static readonly int[] DAMAGE_FOR_MAGIC = { 10, 20, 40, 50, 25 };
    public static readonly int[] RADIUS_FOR_MAGIC = { 15, 18, 20, 25, 18 };
    public static readonly int[] SLOWNESS_FOR_MAGIC = { 40, 60, 75, 90, 60 };


}
