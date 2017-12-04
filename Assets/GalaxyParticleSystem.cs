using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyParticleSystem : MonoBehaviour {
	
	public float starVelocityMultiplier = 3;
	
	private ParticleSystem starSystem;
	
    void Awake () {
		starSystem = GetComponent<ParticleSystem>();
		// SpeedUpField();
    }
	
	void SpeedUpField () {
		var main = starSystem.main;
        main.simulationSpeed += starVelocityMultiplier;
	}
	
	void SlowDownField () {
		
		var main = starSystem.main;
        main.simulationSpeed /= starVelocityMultiplier;
		
		Debug.Log("SLOOOOOOOOOOOOOWWWWWEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEER");
	}
	
	void OnEnable () {
		Messenger.AddListener("GameStart", SpeedUpField);
		Messenger.AddListener("GameOver", SlowDownField);
	}
	
	void OnDisable () {
		Messenger.RemoveListener("GameStart", SpeedUpField);
		Messenger.RemoveListener("GameOver", SlowDownField);
	}
}
