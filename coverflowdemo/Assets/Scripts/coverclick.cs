using UnityEngine;
using System.Collections;

public class coverclick : MonoBehaviour {
	//this cover level Id
	public int levelId;
	
	//
	public Texture hover;
	public Texture down;
	public Texture normal;
	void Start () {
		renderer.material.mainTexture = normal;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseOver()
	{
	}
	void OnMouseEnter()
	{
		renderer.material.mainTexture = hover;
		//current cover animate to origin position
//		float deltax = transform.position.x - 0 ;
//		SendMessageUpwards("StartAnimation", deltax);
	}
	void OnMouseExit()
	{
		renderer.material.mainTexture = normal;
	}
	void OnMouseDown ()
	{
		renderer.material.mainTexture = down;
	}
	
	void OnMouseUpAsButton()
	{
		renderer.material.mainTexture = hover;
		//current cover animate to origin position
		float deltax = transform.position.x - 0 ;
		SendMessageUpwards("StartAnimation", deltax);
	}
	
}
