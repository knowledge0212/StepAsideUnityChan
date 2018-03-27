using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnityChanController : MonoBehaviour
{
	
	//アニメーションするためのコンポーネントを入れる
	private Animator myAnimator;
	//Unityちゃんを移動させるコンポーネントを入れる
	private Rigidbody myRigidbody;

	//前進するための力
	private float forwardForce = 800.0f;
	//左右に移動するための力
	private float turnForce = 500.0f;
	//左右の移動できる範囲
	private float movableRange = 3.4f;
	//ジャンプするための力
	private float upForce = 500.0f;
	//動きを減衰させる係数
	private float coefficient = 0.95f;

	//ゲーム終了時に表示するテキスト
	private GameObject stateText;
	//スコアを表示するテキスト
	private GameObject scoreText;
	//得点
	private int score;

	//ゲーム終了時の判定
	private bool isEnd = false;
	private bool isLButtonDown = false;
	private bool isRButtonDown = false;

	// Use this for initialization
	void Start ()
	{
		//Animatorコンポーネントを取得
		this.myAnimator = GetComponent<Animator> ();
	
		//走るアニメーションを開始
		this.myAnimator.SetFloat ("Speed", 1);

		//Rigidbodyコンポーネントを取得
		this.myRigidbody = GetComponent<Rigidbody> ();

		//シーンの中のstateTextオブジェクトを取得
		this.stateText = GameObject.Find ("GameResultText");
		//シーンの中のscoreTextオブジェクトを取得
		this.scoreText = GameObject.Find ("ScoreText");

	}

	void Update ()
	{

		//ゲーム終了ならUnityちゃんの動きを減衰する
		if (this.isEnd) {
			this.forwardForce *= this.coefficient;
			this.turnForce *= this.coefficient;
			this.upForce *= this.coefficient;
			this.myAnimator.speed *= this.coefficient;
		}

		//Unityちゃんに前方向の力を加える
		this.myRigidbody.AddForce (this.transform.forward * this.forwardForce);

		//Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる
		if ((Input.GetKey (KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x) {
			//左に移動
			this.myRigidbody.AddForce (-this.turnForce, 0, 0);
		} else if ((Input.GetKey (KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange) {
			//右に移動
			this.myRigidbody.AddForce (this.turnForce, 0, 0);
		}

		//Jumpステートの場合はJumpにfalseをセットする
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {
			this.myAnimator.SetBool ("Jump", false);
		}
		//Jumpしていない時にスペースが押されたらジャンプする
		if (Input.GetKeyDown (KeyCode.Space) && this.transform.position.y < 0.5f) {
			//ジャンプアニメを再生
			this.myAnimator.SetBool ("Jump", true);
			//Unityちゃんに上方向の力を加える
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}
	}

	//トリガーモードで他のオブジェクトと接触した場合の処理
	void OnTriggerEnter (Collider other)
	{

		//障害物に衝突した場合
		if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag") {
			this.isEnd = true;
			//stateTextにGAME OVERを表示
			this.stateText.GetComponent<Text> ().text = "GAME OVER"; 
		} else if (other.gameObject.tag == "GoalTag") {
			this.isEnd = true;
			//stateTextにCLEAR!!を表示
			this.stateText.GetComponent<Text> ().text = "CLEAR!!";
		} else if (other.gameObject.tag == "CoinTag") {
			//スコア加点対応
			this.score += 10;
			this.scoreText.GetComponent<Text> ().text = "Score " + this.score + "pt";
			//パーティクルを再生
			GetComponent<ParticleSystem> ().Play ();
			//接触したコインのオブジェクトを破棄
			Destroy (other.gameObject);
		}

	}

	//ジャンプボタンを押した場合の処理
	public void GetMyJumpButtonDown ()
	{
		if (this.transform.position.y < 0.5f) {
			this.myAnimator.SetBool ("Jump", true);
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}
	}

	//左ボタンを押し続けた場合の処理（追加）
	public void GetMyLeftButtonDown ()
	{
		this.isLButtonDown = true;
	}
	//左ボタンを離した場合の処理（追加）
	public void GetMyLeftButtonUp ()
	{
		this.isLButtonDown = false;
	}

	//右ボタンを押し続けた場合の処理（追加）
	public void GetMyRightButtonDown ()
	{
		this.isRButtonDown = true;
	}
	//右ボタンを離した場合の処理（追加）
	public void GetMyRightButtonUp ()
	{
		this.isRButtonDown = false;
	}
}