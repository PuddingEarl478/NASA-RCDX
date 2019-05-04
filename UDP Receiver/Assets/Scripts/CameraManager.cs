using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour {

	#region Variables

	[SerializeField] private Transform target;

	[SerializeField] private Image buttonImage;

	[SerializeField] private Sprite expandSprite;
	[SerializeField] private Sprite contractSprite;

	private Camera camera;
	private Rect viewport;

	private bool fullscreen = false;

	#endregion

	#region MonoBehaviour Callbacks

	private void Awake() {
		camera = GetComponent<Camera>();
	}

	private void Start() {
		viewport = camera.rect;
		buttonImage.sprite = expandSprite;
	}

	private void LateUpdate() {
		transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
		transform.LookAt(target);
	}

	#endregion

	#region Methods

	public void ToggleFullscreen() {
		if(!fullscreen) {
			camera.rect = new Rect(0f, 0f, 1f, 1f);
			camera.depth += 10;
			buttonImage.sprite = contractSprite;
			fullscreen = true;
		} else {
			camera.rect = viewport;
			camera.depth -= 10;
			fullscreen = false;
			buttonImage.sprite = expandSprite;
		}
	}

	#endregion

}
