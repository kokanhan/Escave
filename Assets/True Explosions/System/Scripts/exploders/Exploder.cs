using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Exploder : MonoBehaviour {

	public float explosionTime = 0;
	public float randomizeExplosionTime = 0;
	public float radius = 15;
	public float power = 1;
	public int probeCount = 150;
	public float explodeDuration = 0.5f;

	private bool underFire;
	private float curTime;

	protected bool exploded = false;

	public GameObject player;
	public Camera camera;
	public bool PlayerisSafe = false;

	private float explodedWaveCurTime;
	private float explodedWaveTime = 0.5f;

	public GameObject theEnd;
	public GameObject flame;
	private Vector3[] flamePos = new Vector3[]{new Vector3(0.068f, 1.56f, 0.09f), new Vector3(0.176f, 1.479f, 0.058f), new Vector3(0.031f, 0.941f, 0.025f)};

	public AudioSource audioSource;

	public GameObject safeAreaObject;
	public GameObject deadend;

	public virtual IEnumerator explode() {
		ExploderComponent[] components = GetComponents<ExploderComponent>();
		foreach (ExploderComponent component in components) {
			if (component.enabled) {
				component.onExplosionStarted(this);
			}
		}
		while (explodeDuration > Time.time - explosionTime) {
			disableCollider();
			for (int i = 0; i < probeCount; i++) {
				shootFromCurrentPosition();
			}
			enableCollider();
			yield return new WaitForFixedUpdate();
		}
	}

	protected virtual void shootFromCurrentPosition() {
		Vector3 probeDir = Random.onUnitSphere;
		Ray testRay = new Ray(transform.position, probeDir);
		shootRay(testRay, radius);
	}

	protected bool wasTrigger;
	public virtual void disableCollider() {
		if (GetComponent<Collider>()) {
			wasTrigger = GetComponent<Collider>().isTrigger;
			GetComponent<Collider>().isTrigger = true;
		}
	}

	public virtual void enableCollider() {
		if (GetComponent<Collider>()) {
			GetComponent<Collider>().isTrigger = wasTrigger;
		}
	}


	protected virtual void init() {
		power *= 500000;

		if (randomizeExplosionTime > 0.01f) {
			explosionTime += Random.Range(0.0f, randomizeExplosionTime);
		}
	}

	void Start() {
		init();
	}

	void FixedUpdate()
	{
		if(exploded && explodedWaveCurTime < explodedWaveTime)
		{
			//朝玩家面向的方向施加作用力 反方向推一把
			//player.GetComponent<Rigidbody>().AddForce(player.transform.forward * -1000f);
			//效果要朝一个固定方向 不管玩家看哪里
			if(PlayerisSafe == false)
            {
				player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -1f) * -1000f);
				deadend.SetActive(true);
				safeAreaObject.GetComponent<safeArea>().hurt = true;
				//Debug.Log("you are not safe");
			}
            else
            {
				player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -1f) * -10f);
				//Debug.Log("safe");
			}
			
			explodedWaveCurTime += Time.deltaTime;
		}

		if (curTime > explosionTime && !exploded)
		{
			exploded = true;
			underFire = false;
			audioSource.Play();
			camera.GetComponent<CameraShaker>().ShakeCamera(1.5f, 0.55f);

			foreach (var rock in GetComponent<explosion>().rocks)
			{
					rock.SetActive(false);
			}

			Destroy(GetComponent<explosion>().bomb);

			StartCoroutine("explode");
		}

		if(underFire)
		{
			if(curTime / explosionTime < 0.3f)
			{
				flame.transform.localPosition = Vector3.Lerp(flamePos[0], flamePos[1], curTime / explosionTime / 0.3f);
			}
			else
			{
				flame.transform.localPosition = Vector3.Lerp(flamePos[1], flamePos[2], (curTime - explosionTime * 0.3f) / explosionTime / 0.7f);
			}

			theEnd.GetComponent<Renderer>().material.SetFloat("_progress", curTime / explosionTime);

			curTime += Time.deltaTime;
		}
	}

	private void shootRay(Ray testRay, float estimatedRadius) {
		RaycastHit hit;
		if (Physics.Raycast(testRay, out hit, estimatedRadius)) {
			if (hit.rigidbody != null) {
				//可以显示下hit.rigidbody的名字是什么
				Debug.Log("shootRay rigidbody" + hit.rigidbody);

				hit.rigidbody.AddForceAtPosition(power * Time.deltaTime * testRay.direction / probeCount, hit.point);
				estimatedRadius /= 2;
			} else {
				Vector3 reflectVec = Random.onUnitSphere;
				if (Vector3.Dot(reflectVec, hit.normal) < 0) {
					reflectVec *= -1;
				}
				Ray emittedRay = new Ray(hit.point, reflectVec);
				shootRay(emittedRay, estimatedRadius - hit.distance);
			}
		}
	}

	public void startExplosionCountingDown()
	{
		underFire = true;
		flame.SetActive(true);
		safeAreaObject.SetActive(true);
	}
}
