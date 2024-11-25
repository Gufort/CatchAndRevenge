using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtils{
    public class Utils{

        public static Vector3 GetRandomDir(){
            return new UnityEngine.Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f)).normalized;
        }

    }
}
