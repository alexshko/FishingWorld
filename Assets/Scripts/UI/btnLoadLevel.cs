using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace alexshko.fishingworld.UI
{
    public class btnLoadLevel : MonoBehaviour
    {
        public static int FirstLevelIndex = 1;
        public int Level;

        public void LoadLevelFromButton()
        {
            StartCoroutine(AsyncLoadLevel());
        }

        private IEnumerator AsyncLoadLevel()
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(Level + FirstLevelIndex - 1);
            while (!ao.isDone)
            {
                yield return null;
            }
        }
    }
}
