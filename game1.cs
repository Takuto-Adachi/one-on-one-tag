using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game1 : MonoBehaviour
{
    public GameObject player;
    public Vector3 pos;
    public Vector3 rot;
    private float jumpPower=800;
    public bool isGround;
    public bool isTouch = false;
    public bool isOver = false;
    private float limit = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb=player.GetComponent<Rigidbody>(); //playerがRigidbodyを取得
        if(Input.GetKey(KeyCode.Z)) //playerの回転
        {
            rot.y -= 3f; //playerが反時計回りに回転
        }
        if(Input.GetKey(KeyCode.C)) //playerの回転
        {
            rot.y += 3f; //playerが時計回りに回転
        }
        if(Input.GetKey(KeyCode.S)) //playerの前進
        {
            player.transform.position += transform.forward*0.2f; //playerが向いてる方向に前進
        }
        if(isGround == true)
        {
            if(Input.GetKeyDown(KeyCode.X)) //playerのジャンプ
            {
                isGround = false; //接地判定をきる
                rb.AddForce(new Vector3(0,jumpPower,0));
                //Y方向にjumpPowerを与える
            }
        }
        if(isTouch == true) //playerの捕まる判定
        {
            isTouch = false; //接触判定をきる
            player.GetComponent<Rigidbody>().isKinematic=true;
            player.transform.position = new Vector3(20,105,20); //負けゾーンに転送
            player.GetComponent<Rigidbody>().isKinematic=false;
        }
        if(limit <= 10)
        //limitは体力ゲージのような意味合いであり、10秒間だけ加速できる
        {
            if(Input.GetKey(KeyCode.A)) //playerの加速
            {
                player.transform.position += transform.forward*0.2f;
                //playerが向いてる方向に前進
                limit += Time.fixedDeltaTime;
                //加速ボタンを押している間limitが10までは増え続ける、limitは押している時間
            }
        }
        if(!Input.GetKey(KeyCode.A))
        {
            if(limit >= 0)
            //体力ゲージの回復、加速ボタンを押した秒数だけ全回復に時間がかかる
            {
                limit -= Time.fixedDeltaTime;
                //加速ボタンを押していない時limitが0になるまで減り続ける
            }
        }
        /*if(isOver == true) //フィールド外に出た場合の負け判定
        {
            player.GetComponent<Rigidbody>().isKinematic=true;
            player.transform.position = new Vector3(20,105,20); //負けゾーンに転送
            player.GetComponent<Rigidbody>().isKinematic=false;
        }*/
        player.transform.localEulerAngles = new Vector3(rot.x, rot.y, rot.z);
        //playerの向きを取得
    }

    void OnCollisionEnter(Collision other)
    //接地判定の定義
    {
        if(other.gameObject.tag == "Ground")
        //Groundのtagを持つgameObjectが接触すると反応する
        {
            isGround = true; //接地判定がオン
        }
        /*if(other.gameObject.tag == "over")
        //overのtagを持つgameObjectが接触すると反応する
        {
            isOver = true; //場外判定がオン
        }*/
    }
    void OnTriggerEnter(Collider other)
    //勝敗判定の定義
    {
        if(other.gameObject.tag == "player2")
        //player2のtagを持つgameObjectが侵入すると反応する
        {
            isTouch = true; //接触判定がオン
        }
    }
}
