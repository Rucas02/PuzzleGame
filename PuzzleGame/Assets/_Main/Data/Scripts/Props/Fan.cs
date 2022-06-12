using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Prop
{
    private Animator _anim;
    
    [SerializeField] private bool _state;
    private bool prevState;
    
    
    private float _currentForce;
    [SerializeField] private float _force = 200f;
    
    public AudioSource turnOnSound;
    public AudioSource turnOffSound;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("IsOn", _state);

        if (_state && prevState != _state)
            TurnOn();
        if (!_state && prevState != _state)
            TurnOff();


    }

    private void TurnOn()
    {
        prevState = _state;
        turnOnSound.pitch = 1;
        turnOnSound?.Play();
    }

    private void TurnOff()
    {
        prevState = _state;
        turnOffSound.pitch = Random.Range(1.1f, 1.2f);
        turnOffSound?.Play();
    }

    public void SetState(bool isOn) => _state = isOn;
    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        if (_state)
        {
            Player _player = other.GetComponent<Collider>().GetComponentInParent<Player>();
            if (_player != null)
            {
                if (_player.Data.ID == "Player_Pyramid")
                    _currentForce = _force * 2.5f;
                else if (_player.Data.ID == "Player_Sphere")
                    _currentForce = _force;
                else
                    _currentForce = 0f;
            }
            else
            {
                _currentForce = _force;
            }
                
                other.GetComponent<Collider>().GetComponentInParent<Rigidbody>().AddForce(transform.up * _currentForce * Time.deltaTime, ForceMode.Force);
        }
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (_state)
        {
            Player player = other.GetComponent<Collider>().GetComponentInParent<Player>();
            if (player)
                player.SetIsInteracting(true);
        }
    }
    
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        Player player = other.GetComponent<Collider>().GetComponentInParent<Player>();
        if (player)
            player.SetIsInteracting(false);
    }
}
