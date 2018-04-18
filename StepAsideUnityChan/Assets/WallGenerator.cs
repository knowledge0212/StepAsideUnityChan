using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour {

	//wallPrefabを入れる
	public GameObject wallPrefab;

	// Use this for initialization
	void Start () {
		addWall ();
	}

	// Item作成を判定する壁を作成する
	private void addWall(){
		int wallStartPos = -240;
		int wallEndPos = 80;
		for(int i=wallStartPos; i < wallEndPos; i+=40){
			GameObject wall = Instantiate (wallPrefab) as GameObject;
			wallPrefab.transform.position = 
				new Vector3 (wall.transform.position.x,wall.transform.position.y,i);
		}
	}
}
