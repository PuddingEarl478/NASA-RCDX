using UnityEngine;
using UnityEngine.SceneManagement;

// This class is mostly just to allow changing scenes from button callbacks.
public class SceneLoader : MonoBehaviour {

    public void LoadScene(string _scene) {
		SceneManager.LoadScene(_scene);
	}
}
