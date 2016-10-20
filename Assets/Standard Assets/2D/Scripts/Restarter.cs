using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    public class Restarter : MonoBehaviour
    {
        GameObject spawnPoint;
        public void Start()
        {
            spawnPoint = GameObject.Find("SpawnPoint");
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                //SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
                GameObject.FindObjectOfType<PlatformerCharacter2D>().transform.position = spawnPoint.transform.position;
            }
        }
    }
}
