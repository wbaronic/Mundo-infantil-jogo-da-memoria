using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


/*
 * Developed by WESoft Soluções
 *  http://www.wesoft.com.br
 * 
 */

public class Tile : MonoBehaviour 
{
	public Texture2D img;
	private GameObject back;
    public AudioClip somAceto { get; set; }

    private bool _isRotating = false;

    public bool IsRotating
    {
        get
        {
            return _isRotating;
        }

    }
	
	// Use this for initialization
	void Start () 
	{
		
	}
    

    public void Reveal()
    {
        _isRotating = true;
        PlayAudio();
        InvokeRepeating("rotateReveal", 0, 0.01f);
    }

    void rotateReveal()
	{
		if (transform.rotation.y < 1) 
		{
			transform.Rotate (0, 4, 0);  //you can change axis to rotate and speed
		} 
		else 
		{
            _isRotating = false;
            CancelInvoke("rotateReveal");            
        }

	}
	
    public void Hide()
    {
        _isRotating = true;
        InvokeRepeating("rotateHide", 0, 0.01f);
    }


	void rotateHide()
	{
		if (transform.rotation.y > 0) 
		{
			transform.Rotate (0, -4, 0);
		}
		else 
		{
            _isRotating = false;
            CancelInvoke("rotateHide");			
		}
	}

    
   
    public void PlayAudio()
    {
        GetComponent<AudioSource>().Play();
        
    }

	public void displayImage()
	{
        
		back = transform.Find("Back").gameObject;
        
		back.GetComponent<Renderer>().material.mainTexture = img;

		
	}

    
    


	public void Destroy()
	{
		if (gameObject!=null)
			Destroy (gameObject);
	}
}
