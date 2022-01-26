/****
 * Created by: Andrew Nguyen
 * Created on 1/24/2022
 * Last Edited by: Andrew Nguyen
 * Last Edited on: 1/26/2022
 * 
 * Description: Spawns multiple cube prefabs into the scene. 
 ****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCubes : MonoBehaviour
{

    public GameObject cubePrefab; //new GameObject
    public float scalingFactor = 0.95f; //The amount each cube will shrink each frame
    public int numberOfCubes = 0; //Total number of cubes instantiated

    [HideInInspector]
    public List<GameObject> gameObjectList; //List for all cubes
        
        
    // Start is called before the first frame update
    void Start()
    {
        gameObjectList = new List<GameObject>(); //Instantiate the list
    }

    // Update is called once per frame
    void Update()
    {
        numberOfCubes++;
        GameObject gObj = Instantiate<GameObject>(cubePrefab); //Placeholder to instantiate first gameobject

        gObj.name = "Cube" + numberOfCubes; //Cube plus the number of the cube

        Color randomColor = new Color(Random.value, Random.value, Random.value); //Create a new random color
        gObj.GetComponent<Renderer>().material.color = randomColor; //Assigns that color to the game object

        gObj.transform.position = Random.insideUnitSphere; //random location inside a sphere radius of 1 out from 0,0,0

        gameObjectList.Add(gObj); //add to list

        List<GameObject> removeList = new List<GameObject>(); //List for removed objects.
        //Placing it and instantiating it in the same format as gameObjectList spams it in the console, with many lists with 1 each. Better to have 1 large one than many small. 

        foreach(GameObject goTemp in gameObjectList) { //Pull gameobjects from list. Create a gameobject goTemp, an item from the list. For each goTemp scale it will scale
            float scale = goTemp.transform.localScale.x; //Record the current scale of the object, at some point it will delete itself, if not, keep shrinking
            scale *= scalingFactor; //Multiply scale by scale factor above
            goTemp.transform.localScale = Vector3.one * scale; //Transforms scale

            if (scale <= 0.1f)
            {
                removeList.Add(goTemp);
            } //end boolean. Still need to remove it from the other list
        } //end foreach loop

        foreach(GameObject goTemp in removeList)
        {
            gameObjectList.Remove(goTemp); //For every object in removeList get rid of it from the original list
            Destroy(goTemp); //Delete the object 
        } //end foreach loop

        removeList.Clear(); //Empty the removeList if Destroy doesn't get rid of the object/items.
        Debug.Log(removeList.Count); //Check how many items are still in removeList
    } //end Update()
}
