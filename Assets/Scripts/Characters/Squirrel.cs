using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : MonoBehaviour {
	public AudioClip attackSound = null;
	bool sound = true;
	AudioSource attackSource = null;
	public AudioClip deathSound = null;
	AudioSource deathSource = null;
	public float speed = 1.0f;
	public float radius = 5.0f;

	public Vector3 moveBy = Vector3.one;
	public float nutPeriod = 3.0f;

	public enum Mode
	{
		go_to_a,
		go_to_b,
		attack,
		nut_attack,
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
		this.timeBefore = this.nutPeriod;
		moveBy.y = 0;
		moveBy.z = 0;
		this.pointB = pointA + moveBy;
		attackSource = gameObject.AddComponent<AudioSource> ();
		attackSource.clip = attackSound;
		deathSource = gameObject.AddComponent<AudioSource> ();
		deathSource.clip = deathSound;
	}



	void FixedUpdate()
	{
		setMode();
		run();
		nutAttack();
		StartCoroutine(die());

	}

	private float timeBefore;
	private void nutAttack()
	{
		if (currentMode == Mode.nut_attack && timeBefore >= nutPeriod)
		{
			StartCoroutine(throwNut());
			timeBefore = 0;
		}
		else timeBefore += Time.deltaTime;
	}

	private IEnumerator throwNut()
	{

		Animator animator = GetComponent<Animator>();

		animator.SetBool("attack", true);
		launchNut(getDirection());

		yield return new WaitForSeconds(0.0f);

		animator.SetBool("attack", false);
	}

	public GameObject Nut;
	void launchNut(float direction)
	{
		playMusicOnAttack();
		if (direction != 0)
		{
			GameObject obj = GameObject.Instantiate(this.Nut);
			obj.transform.position = this.transform.position;
			obj.transform.position += new Vector3(0.0f, 1.0f, 0.0f);
			Nut nut = obj.GetComponent<Nut>();
			nut.launch(direction);
		}
	}

	private void setMode()
	{
		Vector3 cat_pos = Cat.current.transform.position;
		Vector3 my_pos = this.transform.position;

		if (currentMode == Mode.die) return;
		else if (Mathf.Abs(cat_pos.x - my_pos.x) < radius)
		{
			currentMode = Mode.nut_attack;

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

		animator.SetBool("attack", true);
		cat.removeHealth(1);
		yield return new WaitForSeconds(0.1f);

		animator.SetBool("attack", false);

	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (currentMode != Mode.die)
		{
			Cat cat = collision.gameObject.GetComponent<Cat>();
			if (cat != null)
			{
				Vector3 cat_pos = Cat.current.transform.position;
				Vector3 my_pos = this.transform.position;
				currentMode = Mode.attack;

				if (currentMode == Mode.attack && Mathf.Abs(cat_pos.y - my_pos.y) < 1.0f)
				{
					
						StartCoroutine (attack (cat));

				}
				else if (currentMode == Mode.attack && Mathf.Abs(cat_pos.y - my_pos.y) > 1.0f)
				{
					playMusicOnDeath ();
					currentMode = Mode.die;
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
		if (currentMode != Mode.nut_attack)
		{
			if (Mathf.Abs(value) > 0)
			{
				Vector2 vel = myBody.velocity;
				vel.x = value * speed;
				myBody.velocity = vel;
			}


			if (Mathf.Abs(value) > 0)
			{
				animator.SetBool("walk", true);
			}
			else
			{
				animator.SetBool("walk", false);
			}
		} else animator.SetBool("walk", false);
	}


	private float getDirection()
	{
		Vector3 cat_pos = Cat.current.transform.position;
		Vector3 my_pos = this.transform.position;

		if (currentMode == Mode.attack || currentMode == Mode.nut_attack)
		{
			
			if (my_pos.x - cat_pos.x < -1)
			{
				return 1;
			}
			else if (my_pos.x - cat_pos.x > 1)
			{
				return -1;
			}
			else return 0;
		}

		else if (currentMode == Mode.go_to_a)
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

			animator.SetBool("die",true);
			this.GetComponent<BoxCollider2D>().isTrigger = true;

			if (myBody != null) Destroy(myBody);

			yield return new WaitForSeconds(1.0f);

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
		Debug.Log ("playdeath");
		if (sound) {
			Debug.Log ("play");
			deathSource.Play ();
		} else {
			Debug.Log ("stop");
			deathSource.Stop ();
		}
	}
	public void  setSoundOff(){
		sound = false;
	}
	public void setSoundOn(){
		sound = true;
	}
}
