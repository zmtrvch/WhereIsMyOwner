using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {
	public AudioClip attackSound = null;
	AudioSource attackSource = null;
	public AudioClip deathSound = null;
	AudioSource deathSource = null;
	bool sound = true;
	public float speed = 1.0f;
	public Vector3 moveBy = Vector3.one;


	public enum Mode
	{
		go_to_a,
		go_to_b,
		attack,
		die
	}

	Rigidbody2D myBody = null;

	Vector3 pointA;
	Vector3 pointB;
	public Mode currentMode = Mode.go_to_b;


	void Start()
	{
		this.myBody = this.GetComponent<Rigidbody2D>();
		this.pointA = this.transform.position;
		attackSource = gameObject.AddComponent<AudioSource> ();
		attackSource.clip = attackSound;
		deathSource = gameObject.AddComponent<AudioSource> ();
		deathSource.clip = deathSound;
		moveBy.y = 0;
		moveBy.z = 0;
		this.pointB = pointA + moveBy;

	}



	void FixedUpdate()
	{
		setMode();

		run();

		StartCoroutine(die());

	}


	private void setMode()
	{
		Vector3 cat_pos = Cat.current.transform.position;
		Vector3 my_pos = this.transform.position;

		if (currentMode == Mode.die) return;
		else if (cat_pos.x > Mathf.Min(pointA.x, pointB.x)
			&& cat_pos.x < Mathf.Max(pointA.x, pointB.x))
		{
			currentMode = Mode.attack;
		}
		else if (currentMode == Mode.go_to_a)
		{
			if (isArrived(my_pos, pointA))
			{
				currentMode = Mode.go_to_b;
			}
		}
		else if (currentMode == Mode.go_to_b)
		{
			if (isArrived(my_pos, pointB))
			{
				currentMode = Mode.go_to_a;
			}
		}
		else currentMode = Mode.go_to_b;
	}


	private IEnumerator attack(Cat cat)
	{ 
		Animator animator = GetComponent<Animator>();

		playMusicOnAttack ();
		animator.SetBool("attack", true);
		cat.removeHealth(1);
		yield return new WaitForSeconds(0.2f);

		animator.SetBool("attack", false);       
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (currentMode != Mode.die)
		{
			Cat cat = collision.gameObject.GetComponent<Cat>();
			if (cat != null)
			{
				Vector3 cat_position = Cat.current.transform.position;
				Vector3 my_pos = this.transform.position;
				currentMode = Mode.attack;

				if (currentMode == Mode.attack && Mathf.Abs(cat_position.y - my_pos.y) < 1.0f)
				{
					
						StartCoroutine (attack (cat));

				}
				else if (currentMode == Mode.attack && Mathf.Abs(cat_position.y - my_pos.y) > 1.0f)
				{
					currentMode = Mode.die;
					playMusicOnDeath ();

					StartCoroutine(die());
				}
			}
		}
	}

	private void run()
	{

		//[-1, 1]
		float value = this.getDirection();
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		Animator animator = GetComponent<Animator>();

		if (value < 0)
		{
			sr.flipX = false;

		}
		else if (value > 0)
		{
			sr.flipX = true;
		}
		if (Mathf.Abs(value) > 0)
		{
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		}

		if (Mathf.Abs(value) > 0)
		{
			animator.SetBool("run", true);
		}
		else
		{
			animator.SetBool("run", false);
		}
	}


	private float getDirection()
	{
		Vector3 cat_position = Cat.current.transform.position;
		Vector3 my_pos = this.transform.position;

		if (currentMode == Mode.attack)
		{
		if (my_pos.x - cat_position.x < -1)
			{
				return 1;
			}
			else if (my_pos.x - cat_position.x > 1)
			{
				return -1;
			}
			else return 0;
		}

		else  if (currentMode == Mode.go_to_a)
		{
			return -1; 
		}
		else if (currentMode == Mode.go_to_b)
		{
			return 1; 
		}
		return 0; 
	}

	private IEnumerator die()
	{

		if (currentMode == Mode.die)
		{

			Animator animator = GetComponent<Animator>();
			animator.SetBool("die", true);

			if (myBody != null) Destroy(myBody);

			yield return new WaitForSeconds(1.5f);

			animator.SetBool("die", false);


			Destroy(this.gameObject);

		}
	}

	private bool isArrived(Vector3 pos, Vector3 target)
	{
		pos.z = 0;
		target.z = 0;
		return Vector3.Distance(pos, target) < 0.2f;
	}
	public void playMusicOnAttack() {
		if (sound)
			attackSource.Play ();
		else
			attackSource.Stop ();
	}
	public void playMusicOnDeath() {
		if (sound)
			deathSource.Play ();
		else
			deathSource.Stop ();
	}
	public void  setSoundOff(){
		sound = false;
	}
	public void setSoundOn(){
		sound = true;
	}
}
