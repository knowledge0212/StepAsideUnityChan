using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroy : MonoBehaviour
{	
	private GameObject unitychan;
	private GameObject mainCamera;
	private float difference;

	void Start(){
		this.unitychan = GameObject.Find ("unitychan");
		this.mainCamera = GameObject.Find ("Main Camera");
		this.difference = this.unitychan.transform.position.z - this.mainCamera.transform.position.z;
	}
		
	// Update is called once per frame
	void Update ()
	{
		//unityちゃんと自身の位置に一定以上の距離がある場合、オブジェクトを削除する
		if ((this.unitychan.transform.position.z - this.transform.position.z) > this.difference) {
			Destroy (this.gameObject);
		}
	}
}
