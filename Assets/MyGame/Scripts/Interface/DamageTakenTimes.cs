using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DamageTakenTimes 
{
    void TakeDamage(int damage, Vector2 force, GameObject instigattor);
}
//Khai báo 1 interface dùng cho cả người chơi va enemy
