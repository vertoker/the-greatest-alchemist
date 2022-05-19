using UnityEngine;
using System.Collections.Generic;

namespace GameBehaviour
{
    public class GameInitializer : MonoBehaviour
    {
        //private static GameInitializer _instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            //_instance = FindObjectOfType<GameInitializer>();
            var inits = FindObjectsOfType<MonoInit>();
            foreach (var init in inits)
                init.Init();
        }
    }

}
