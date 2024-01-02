using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public List<Material> hairMaterials = new List<Material>();
    public List<GameObject> Shirts = new List<GameObject>();
    public List<GameObject> Pants = new List<GameObject>();
    public List<GameObject> Shoes = new List<GameObject>();
    public List<GameObject> SkinColor = new List<GameObject>();
    public List<GameObject> Hair = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        RandomizeClothes();
    }
    public void RandomizeClothes()
    {
        int randomShirt = Random.Range(0, Shirts.Count);
        for (int i = 0; i < Shirts.Count; i++)
        {
            if (i == randomShirt)
                Shirts[i].SetActive(true);
            else
                Shirts[i].SetActive(false);
        }

        int randomPants = Random.Range(0, Pants.Count);
        for (int i = 0; i < Pants.Count; i++)
        {
            if (i == randomPants)
                Pants[i].SetActive(true);
            else
                Pants[i].SetActive(false);
        }

        int randomShoes = Random.Range(0, Shoes.Count);
        for (int i = 0; i < Shoes.Count; i++)
        {
            if (i == randomShoes)
                Shoes[i].SetActive(true);
            else
                Shoes[i].SetActive(false);
        }
        int randomHair = Random.Range(0, Hair.Count);
        int currentHair = 0;
        Material currentMat;
        for (int i = 0; i < Hair.Count; i++)
        {
            if (i == randomShoes)
            {
                Hair[i].SetActive(true);
                currentHair = i;
                currentMat = Hair[i].GetComponent<Renderer>().material;
                currentMat.SetColor("_BaseColor", new Color32(((byte)Random.Range(0, 256)), ((byte)Random.Range(0, 256)), ((byte)Random.Range(0, 256)), 255));
                Hair[i].GetComponent<Renderer>().material = currentMat;
            }
            else
                Hair[i].SetActive(false);
        }

        // int randomMaterial = Random.Range(0, hairMaterials.Count);
        // Hair[currentHair].GetComponent<Renderer>().material = hairMaterials[randomMaterial];

    }

}

