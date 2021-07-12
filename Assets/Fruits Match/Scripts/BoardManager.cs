using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {
	public static BoardManager instance;
	public List<Sprite> characters = new List<Sprite>();
	public GameObject tile;
	public int xSize, ySize;
	private GameObject[,] tiles;

	Shuffle obj = new Shuffle();
	public Sprite fruits;

	public bool IsShifting { get; set; }

	void Start() {
		instance = GetComponent<BoardManager>();
		Shuffle.red = characters[0];
		obj.shufflemethod(characters);
		CheckList(characters);
		CreateBoard(0.8f, 0.8f);
	}
	public void CheckList(List<Sprite> arr)
	{
		int index = arr.IndexOf(fruits);
		Debug.Log(index);
		int i = 1;
		while (i != 3)
		{

			if (arr[index] == fruits)
			{
				obj.shufflemethod(arr);
			}

			index++;
			i++;
		}

	}
	private void CreateBoard (float xOffset, float yOffset) {
		tiles = new GameObject[xSize, ySize];

        float startX = transform.position.x;
		float startY = transform.position.y;

		int k = 0;

		for (int x = 0; x < xSize; x++) 
		{
			for (int y = 0; y < ySize; y++)
			{
				GameObject newTile = Instantiate(tile, new Vector3(startY + (yOffset * y),startX + (xOffset * x), 0), tile.transform.rotation);
				tiles[x, y] = newTile;
				newTile.transform.parent = transform;

				Sprite newSprite = characters[k];

				newTile.GetComponent<SpriteRenderer>().sprite = newSprite;
				k++;
			}
		}
    }

}
