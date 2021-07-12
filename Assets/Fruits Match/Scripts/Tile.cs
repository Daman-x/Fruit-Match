using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{ 
	private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);

	private static Tile previousSelected = null;

	public AudioSource selected;
	private SpriteRenderer render;
	private bool isSelected = false;

	private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

	private bool matchFound = false;

	void Awake()
	{
		render = GetComponent<SpriteRenderer>();
		matchFound = false;
	}

	private void Select()
	{
		isSelected = true;
		render.color = selectedColor;
		previousSelected = gameObject.GetComponent<Tile>();
	}

	private void Deselect()
	{
		isSelected = false;
		render.color = Color.white;
		previousSelected = null;
	}

	void OnMouseDown()
	{
		
		if (render.sprite == null || BoardManager.instance.IsShifting)
		{
			return;
		}

		if (isSelected)
		{ 
			Deselect();
		}
		else
		{
			if (previousSelected == null)
			{ 
				Select();
				selected.Play();
			}
			else
			{
				if (GetAllAdjacentTiles().Contains(previousSelected.gameObject))
				{
					GameManager.touched = true;
					//matchFound = false;
					GameManager.correct = false;
					SwapSprite(previousSelected.render); 
					previousSelected.ClearAllMatches();
					previousSelected.Deselect();
					ClearAllMatches();
					
				}
				else
				{ 
					previousSelected.GetComponent<Tile>().Deselect();
					Select();
				}
			}

		}
	}

	public void SwapSprite(SpriteRenderer render2)
	{ 
		if (render.sprite == render2.sprite)
		{ 
			return;
		}

		Sprite tempSprite = render2.sprite; 
		render2.sprite = render.sprite; 
		render.sprite = tempSprite; 
									
	}

	public GameObject GetAdjacent(Vector2 castDir)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
		if (hit.collider != null)
		{
			return hit.collider.gameObject;
		}
		return null;
	}
	public List<GameObject> GetAllAdjacentTiles()
	{
		List<GameObject> adjacentTiles = new List<GameObject>();
		for (int i = 0; i < adjacentDirections.Length; i++)
		{
			adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
		}
		return adjacentTiles;
	}

	private List<GameObject> FindMatch(Vector2 castDir)
	{ 
		List<GameObject> matchingTiles = new List<GameObject>(); 
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir); 
		while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
		{ 
			matchingTiles.Add(hit.collider.gameObject);
			hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
		}
		return matchingTiles; 
	}

	private void ClearMatch(Vector2[] paths) 
	{
		List<GameObject> matchingTiles = new List<GameObject>(); 
		for (int i = 0; i < paths.Length; i++) 
		{
			matchingTiles.AddRange(FindMatch(paths[i]));
		}
		if (matchingTiles.Count >= 1) 
		{
			matchFound = true;
		}
	}

	public void ClearAllMatches()
	{
		if (render.sprite == null)
			return;


		ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
		ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
		//Debug.Log(matchFound);
		GameManager.correct = matchFound;
		if (matchFound)
			matchFound = false;
	}
}