using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using NasaReader;

public class Receiver : MonoBehaviour {

	[Header("Rocket Objects")]
	[SerializeField] private Transform commandModule;
	[SerializeField] private Transform launchAbortSystem;
	[SerializeField] private Transform booster;

	[Header("Particles")]
	[SerializeField] private GameObject boosterThrust;
	[SerializeField] private GameObject lasThrust;

	[Header("CM Position Text")]
	[SerializeField] private TextMeshProUGUI cmLatitudeText;
	[SerializeField] private TextMeshProUGUI cmLongitudeText;
	[SerializeField] private TextMeshProUGUI cmAltitudeText;

	[Header("LAS Position Text")]
	[SerializeField] private TextMeshProUGUI lasLatitudeText;
	[SerializeField] private TextMeshProUGUI lasLongitudeText;
	[SerializeField] private TextMeshProUGUI lasAltitudeText;

	[Header("Booster Position Text")]
	[SerializeField] private TextMeshProUGUI boosterLatitudeText;
	[SerializeField] private TextMeshProUGUI boosterLongitudeText;
	[SerializeField] private TextMeshProUGUI boosterAltitudeText;

	[Header("Other")]
	[SerializeField] private TextMeshProUGUI packetCounter;
	[SerializeField] private float lerpSpeed = 0.75f;
	
	private UnitySync unitySync;
	private int packetsReceived = 0;

	private void Start() {
		unitySync = new UnitySync();
		unitySync.Start();
		boosterThrust.SetActive(false);
		lasThrust.SetActive(false);
		packetsReceived = 0;
	}

    private void Update() {

		unitySync.Update();
		Debug.Log(unitySync.Data.ToString());


		foreach (StreamData _data in unitySync.DataHistory) {

			packetsReceived++;
			packetCounter.text = "Packets Received: " + packetsReceived.ToString();

			if(_data.flag == EngineState.Booster) {
				boosterThrust.SetActive(true);
				lasThrust.SetActive(false);
			} else if(_data.flag == EngineState.LAS) {
				lasThrust.SetActive(true);
				boosterThrust.SetActive(false);
			} else {
				boosterThrust.SetActive(false);
				lasThrust.SetActive(false);
			}

			StartCoroutine(LerpValues(_data));

		}

		unitySync.DataHistory.Clear();

		//if(unitySync.Data.flag == EngineState.)


    }

	public static UnityEngine.Vector3 ConvertToVector(NasaReader.Vector3 _vector) {
		return new UnityEngine.Vector3((float)_vector.Lat, (float)_vector.Alt, (float)_vector.Lon);
	}

	public static UnityEngine.Quaternion ConvertToQuaternion(NasaReader.Quaternion _quaternion) {
		UnityEngine.Quaternion _quat = new UnityEngine.Quaternion((float)_quaternion.x, (float)_quaternion.y, (float)_quaternion.z, (float)_quaternion.w);
		return _quat;
	}

	private IEnumerator LerpValues(StreamData _data) {
		for (int i = 0; i < 60; i++) {
			commandModule.localPosition = UnityEngine.Vector3.Lerp(commandModule.position, ConvertToVector(_data.CMPos), Time.deltaTime);

			cmLatitudeText.text = "Latitude: " + Math.Round(_data.CMPos.Lat, 4) + " radians";
			cmLongitudeText.text = "Longitude: " + Math.Round(_data.CMPos.Lon, 4) + " radians";
			cmAltitudeText.text = "Altitude: " + Math.Round(_data.CMPos.Alt, 0) + " feet";

			commandModule.localRotation = UnityEngine.Quaternion.Lerp(commandModule.rotation, ConvertToQuaternion(_data.CMRot), Time.deltaTime);


			launchAbortSystem.localPosition = UnityEngine.Vector3.Lerp(launchAbortSystem.position, ConvertToVector(_data.LASPos), Time.deltaTime);

			lasLatitudeText.text = "Latitude: " + Math.Round(_data.CMPos.Lat, 4) + " radians";
			lasLongitudeText.text = "Longitude: " + Math.Round(_data.CMPos.Lon, 4) + " radians";
			lasAltitudeText.text = "Altitude: " + Math.Round(_data.CMPos.Alt, 0) + " feet";

			launchAbortSystem.localRotation = launchAbortSystem.rotation = UnityEngine.Quaternion.Lerp(launchAbortSystem.rotation, ConvertToQuaternion(_data.LASRot), Time.deltaTime);

			booster.localPosition = UnityEngine.Vector3.Lerp(booster.position, ConvertToVector(_data.BoosterPos), Time.deltaTime);

			boosterLatitudeText.text = "Latitude: " + Math.Round(_data.CMPos.Lat, 4) + " radians";
			boosterLongitudeText.text = "Longitude: " + Math.Round(_data.CMPos.Lon, 4) + " radians";
			boosterAltitudeText.text = "Altitude: " + Math.Round(_data.CMPos.Alt, 0) + " feet";

			booster.localRotation = UnityEngine.Quaternion.Lerp(booster.rotation, ConvertToQuaternion(_data.BoosterRot), Time.deltaTime);

			yield return new WaitForFixedUpdate();
		}

	}
}
