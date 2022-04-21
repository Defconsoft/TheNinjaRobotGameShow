using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayHolder : MonoBehaviour
{   

	public static ReplayHolder Instance;

    public bool hasPlayed;
    // Start is called before the first frame update
	void Awake(){

		if(Instance == null){
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			DestroyImmediate(gameObject);
		}
	}
}
