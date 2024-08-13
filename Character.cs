using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Character class takes in user input, uses those values to move player via CharacterController.
 * Passes CharacterController/Input values to Animator to animate character.
 */

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]

public class Character : MonoBehaviour
{
    //max speed
    public float m_speed;
    public bool IsCasting;

    private Animator _anim;
    private CharacterController _cc;
    private CastRod _castRod;

    private Vector3 _direction; //what direction to move player in
    private Vector3 _movement; //actual vector3 applied as movement (lerps towards direction)
    private Transform _target = null;

    private float _vertical; //gravity component, moves player down each frame to keep them grounded
    private byte _fish;
    private byte _money;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _cc = GetComponent<CharacterController>();
        _castRod = GetComponent<CastRod>();

        //Load fish count
        if (PlayerPrefs.HasKey("fish"))
            _fish = (byte)PlayerPrefs.GetInt("fish");

        //Load money count
        if (PlayerPrefs.HasKey("money"))
            _money = (byte)PlayerPrefs.GetInt("money");

        _vertical = -5.0f; //moves player down each frame
        _anim.SetInteger("speed", 1); //initialize speed multiplier
    }

    private void Update()
    {
        //set animator components here
        _anim.SetFloat("velocity", _cc.velocity.magnitude/m_speed);
        IsCasting = _anim.GetCurrentAnimatorStateInfo(0).IsName("Cast");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //this.speed is max speed, anim.speed is a multiplier (ie. 0 to stop movement, 1.5 for a sprint)
        _movement = Vector3.Lerp(_movement, _direction * m_speed * _anim.GetInteger("speed"), 0.25f);
        _movement.y = _vertical;

        //if a target exists, rotate towards it. Otherwise, rotate towards direction player is moving in
        if (_anim.GetBool("interact") || _anim.GetBool("door"))
        {
            Debug.Log("Facing Target");
            if (_anim.GetBool("door"))
            {
                transform.rotation = _target.rotation;
            }
            else {
                Vector3 targetDirection = new Vector3(_target.position.x, 0.0f, _target.position.z);
                Vector3 playerDirection = new Vector3(transform.position.x, 0.0f, transform.position.z);
                transform.rotation = Quaternion.LookRotation(targetDirection - playerDirection, Vector3.up);
            }
            
        }
        else {
            if ((new Vector3(_movement.x, 0.0f, _movement.z).magnitude > 0.01f))
                transform.rotation = Quaternion.LookRotation(new Vector3(_movement.x, 0.0f, _movement.z));
        }

        _cc.Move(_movement * Time.fixedDeltaTime);
    }

    public void Cast() {
        if (_castRod) {
            //lure will be placed at the foot of the player, and up 1.5 meters up
            _castRod.ReleaseLure(this.transform.position + (1.5f * Vector3.up), this.transform.forward.normalized);
        }
    }

    public Transform GetHand() {
        return _anim.GetBoneTransform(HumanBodyBones.LeftHand);
    }

    public Vector3 GetPosition() { return this.transform.position; }
    public Vector3 GetLurePosition() { return _castRod.GetLurePosition(); }
    public byte GetFish() { return _fish; }
    public byte GetMoney() { return _money; }

    public void SetDirection(Vector3 d) { this._direction = d; }
    public void SetInteraction(bool i, Transform interactable) { 
        this._anim.SetBool("interact", i);
        _target = interactable;
    }
    public void SetDoorEnter(Transform door) {
        this._anim.SetBool("door", true);
        _target = door;
    }
    public void SetPosition(Vector3 pos) {
        _cc.enabled = false;
        transform.position = pos;
        _cc.enabled = true;
    }
    public void SetRotation(Vector3 rotDirection) {
        if(rotDirection.magnitude > 0.01f)
            transform.rotation = Quaternion.LookRotation(rotDirection);
    }
    public void SetCast(bool c) {
        _anim.SetBool("cast", c);
    }
    public void SetReel(bool r)
    {
        _anim.SetBool("reel", r);
    }
    public void SetCatch(bool c)
    {
        _anim.SetBool("catch", c);
    }
    public void SetSpeed(int s) { _anim.SetInteger("speed", s); }

    public void ReelRod() {
        if (_castRod)
            _castRod.SetReelValues();
    }

    public void AddFish(string fish) { 
        //preemptively adding a string for fish name, since that's the fish that would get increased
        _fish++;

        //save fish count to memoery
        PlayerPrefs.SetInt("fish", _fish);
    }

    public void SellFish() {
        if (_fish > 0) {
            _fish--;
            _money += 5;

            PlayerPrefs.SetInt("fish", _fish);
            PlayerPrefs.SetInt("money", _money);
        }
    }

    public void BuyItem() {
        if (_money >= 50) {
            _money -= 50;
            PlayerPrefs.SetInt("money", _money);

            _castRod.BuyLure();
            _castRod.ChangeLure();
        }
    }

    public void AddMoney() {
        _money += 5;
        PlayerPrefs.SetInt("money", _money);
    }
}
