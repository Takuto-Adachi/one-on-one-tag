using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2ver2 : MonoBehaviour
{
    // 移動・視点関連
    public GameObject player;
    public Vector3 pos;
    public Vector3 rot;
    private float rotspeed = 3;
    public Transform player2Camera; // 子オブジェクトの参照
    
    // 跳躍関連
    private float jumpPower=800;
    private bool isGround = true;
    
    // 勝敗関連
    private bool isTouch = false;
    public GameObject loseplayer2; 
    public GameObject winplayer1; 
    private bool isOver = false;
    
    // 加速関連のパラメータ
    private float limit = 0f;
    float maxlimit = 5f; // 制限時間
    float recoveryspeed = 1f; // 回復速度（秒間の回復量）
    private bool isAccelerating = false; // 加速判定
    private bool isOutstock = false; // 体力切れ判定
    
    void Start()
    {
        player2Camera = transform.Find("Player2Camera");
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb=player.GetComponent<Rigidbody>(); //playerがRigidbodyを取得

        //視点移動関連
        //playerの左右回転
        if(Input.GetKey(KeyCode.LeftArrow)) 
        {
            rot.y -= rotspeed; //playerが反時計回りに回転
        }

        if(Input.GetKey(KeyCode.RightArrow)) 
        {
            rot.y += rotspeed; //playerが時計回りに回転
        }

        //playerのカメラを上下回転
        if(Input.GetKey(KeyCode.UpArrow)) 
        {
            //playerのカメラが下に回転
            rot.x -= rotspeed;
            rot.x = Mathf.Max(rot.x, -60);  //首の可動域
        }

        if(Input.GetKey(KeyCode.DownArrow)) 
        {
            //playerのカメラが上に回転
            rot.x += rotspeed;
            rot.x = Mathf.Min(rot.x, 50);  //首の可動域
        }
        //視点移動の適用を適用
        player.transform.localEulerAngles = new Vector3(0, rot.y, 0);  //身体の向き（playerの向き）
        player2Camera.localEulerAngles = new Vector3(rot.x, 0, 0);  //首の向き（cameraの向き）

        //アバター移動関連（playerの前進）
        if(Input.GetKey(KeyCode.Slash)) 
        {
            player.transform.position += transform.forward*0.2f; //playerが向いてる方向に前進
        }

        // 跳躍関連
        if(isGround == true) //地面に立っている時
        {
            if(Input.GetKeyDown(KeyCode.RightShift)) //playerのジャンプ
            {
                isGround = false; //接地判定をきる
                rb.AddForce(new Vector3(0,jumpPower,0));  //Y方向にjumpPowerを与える
            }
        }

        // 勝敗が決まった時の処理
        if(isTouch == true) //playerの捕まる判定
        {
            isTouch = false; //接触判定をきる
            loseplayer2.SetActive(true); // 負け演出
            winplayer1.SetActive(true); // 勝ち演出

            //初期の負け判定
            // player.GetComponent<Rigidbody>().isKinematic=true;
            // player.transform.position = new Vector3(20,105,20); //負けゾーンに転送
            // player.GetComponent<Rigidbody>().isKinematic=false;
        }

        // 加速関連
        if (Input.GetKey(KeyCode.Backslash) && limit < maxlimit && isOutstock == false) //playerの加速
        //limitは体力ゲージのような意味合いであり、任意の秒数だけ加速できる
        {
            // 加速判定＆処理
            isAccelerating = true;
            player.transform.position += transform.forward * 0.2f;
            // 制限時間
            limit += Time.deltaTime;
            limit = Mathf.Min(limit, maxlimit);
            //体力切れ判定（これによってしっかりと制限、体力を使い果たすと回復まで時間がかかる）
            if(limit == maxlimit)
            {
                isOutstock = true;
            }
        }
        else
        {
            isAccelerating = false;
        }

        if (!isAccelerating)
        //体力ゲージの回復、加速ボタンを押した秒数だけ全回復に時間がかかる（回復スピードの変更が可能）
        {
            // ゲージ回復
            limit -= recoveryspeed * Time.deltaTime;
            limit = Mathf.Max(limit, 0f);  //0を下回らない
            if(limit == 0)
            {
                isOutstock = false;
            }
        }

        // 追加ルール
        /*if(isOver == true) //フィールド外に出た場合の負け判定
        {
            player.GetComponent<Rigidbody>().isKinematic=true;
            player.transform.position = new Vector3(20,105,20); //負けゾーンに転送
            player.GetComponent<Rigidbody>().isKinematic=false;
        }*/
    }

    //接地判定の定義
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        //Groundのtagを持つgameObjectが接触すると反応する
        {
            isGround = true; //接地判定がオン
        }

        // 場外判定
        /*if(other.gameObject.tag == "over")
        //overのtagを持つgameObjectが接触すると反応する
        {
            isOver = true; //場外判定がオン
        }*/
    }

    //勝敗判定の定義
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "player1")
        //player2のtagを持つgameObjectが侵入すると反応する
        {
            isTouch = true; //接触判定がオン
        }
    }
}
