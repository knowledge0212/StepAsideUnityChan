﻿using System.Collections;
using UnityEngine;

public class MyCameraController : MonoBehaviour {

	//Unityちゃんのオブジェクト
	private GameObject unitychan;
	//Unityちゃんとカメラの距離
	private float difference;

	// Use this for initialization
	void Start () {

		//Unityちゃんのオブジェクトを取得
		this.unitychan = GameObject.Find("unitychan");
		//Unityちゃんとカメラの位置（z位置）の差を求める
		this.difference = unitychan.transform.position.z - this.transform.position.z;
		
	}
	
	// Update is called once per frame
	void Update () {
		//Unityちゃんの位置に合わせてカメラの位置を移動
		this.transform.position = new Vector3(0,transform.position.y,this.unitychan.transform.position.z-this.difference);
	}
}
