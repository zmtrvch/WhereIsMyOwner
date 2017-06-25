using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Cat : MonoBehaviour {


	public static Cat current;
	public float speed = 3; //скорость кота
	Rigidbody2D myBody = null;

	public int MaxHealth = 2;
	public int health = 1;

	Animator myController = null;
	bool isGrounded = false;
	bool JumpActive = false;
	float JumpTime = 0f;
	public float MaxJumpTime = 2f;
	public float JumpSpeed = 2f;

	public float WaitTime = 2f;
	bool sound = true; //звук включен




	void Awake()
	{
		current = this;
	}


	// Use this for initialization
	void Start()
	{

		myBody = this.GetComponent<Rigidbody2D>();
		myController = this.GetComponent<Animator>();
		LevelController.current.setStartPosition(transform.position);
	}
	void FixedUpdate () {
		float value = Input.GetAxis ("Horizontal");

		Animator animator = GetComponent<Animator> ();

		if(Mathf.Abs(value) > 0) {
			animator.SetBool ("run", true);
		} else {
			animator.SetBool ("run", false);
		}

		if (Mathf.Abs (value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		}

		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if (value < 0)
		{
			sr.flipX = true;
		}
		else if (value > 0)
		{
			sr.flipX = false;
		}
		Vector3 from = transform.position + Vector3.up * 0.3f;
		Vector3 to = transform.position + Vector3.down * 1.6f;
		int layer_id = 1 << LayerMask.NameToLayer("Ground");
		//Перевіряємо чи проходить лінія через Collider з шаром Ground
		RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
		if (hit)
		{
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}

		//Намалювати лінію (для розробника)
		Debug.DrawLine(from, to, Color.red);

		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			this.JumpActive = true;
		}
		if (this.JumpActive)
		{
			//Якщо кнопку ще тримають
			if (Input.GetButton("Jump"))
			{
				this.JumpTime += Time.deltaTime;
				if (this.JumpTime < this.MaxJumpTime)
				{
					Vector2 vel = myBody.velocity;
					vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
					myBody.velocity = vel;
				}
			}
			else
			{
				this.JumpActive = false;
				this.JumpTime = 0;
			}
		}

		if (this.isGrounded)
		{
			animator.SetBool("jump", false);
		}
		else
		{
			animator.SetBool("jump", true);
		}

	}
		public void addHealth(int number)
		{
			this.health += number;
			if (this.health > MaxHealth)
				this.health = MaxHealth;
			this.onHealthChange();
		}

		public void removeHealth(int number)
		{
			this.health -= number;
			if (this.health < 0)
				this.health = 0;
			this.onHealthChange();
		}

		void onHealthChange()
		{
			if (this.health == 1)
			{
				this.transform.localScale = Vector3.one;
			}
			else if (this.health == 2)
			{
				this.transform.localScale = Vector3.one * 2;
			}
			else if (this.health == 0)
			{
			Die ();
			}
		}
		
		public void Die(){
			StartCoroutine (dieAnimation(0.5f));


		}
		public IEnumerator dieAnimation (float time){
			Animator animator = GetComponent<Animator>();
			animator.SetBool("die", true);
			yield return new WaitForSeconds (time);
			animator.SetBool("die", false);
			LevelController.current.onCatDeath(this);

		}
		//включить, віключить звук
		public void  setSoundOff(){
			sound = false;
		}
		public void setSoundOn(){
			sound = true;
		}

	}