using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    public GameObject spawnPoint;
    public int numberOfCubes;
    private float radius = 2f;

    public RectTransform horizontalLayoutGroup;
    private int count = 0;
    private int index = 0;
    public configObject configObject;
    private Dictionary<GameObject, int> cubeCount;

    List<TypeDetails> typeList;
    List<GameObject> listUI;

    public AudioSource selectedSound;
    public AudioSource destroySound;

    public GameOver gameOver;
    public LevelComplete levelComplete;
    public void Start()
    {
        cubeCount = new Dictionary<GameObject, int>();
        typeList = new List<TypeDetails>();
        listUI = new List<GameObject>();
        SpawnCubes();
    }
    public void Update()
    {
        if (numberOfCubes == 0)
        {
            gameObject.SetActive(false);
            levelComplete.complete();
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                TypeDetails typeDetails = hitObject.GetComponent<TypeDetails>();
                if (typeDetails == null)
                {
                    return;
                }
                if (typeDetails.checkClick == false)
                {
                    selectedSound.Play();
                    numberOfCubes--;
                    typeList.Add(typeDetails);
                    typeDetails.checkClick = true;
                    Destroy(hitObject);
                    GameObject UIObject = new GameObject();
                    Image img = UIObject.AddComponent<Image>();
                    TypeDetails td = UIObject.AddComponent<TypeDetails>();
                    td.Id = typeDetails.Id;
                    td.checkClick = true;
                    img.sprite = typeDetails.sprite;
                    DisplaySelectedObject(UIObject);
                    Destroy(UIObject);

                }
                CheckList(typeList, listUI);
            }
        }
    }
    private void SpawnCubes()
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            Vector3 position = spawnPoint.transform.position + UnityEngine.Random.insideUnitSphere * radius;

            GameObject obj = Instantiate(configObject.typeDetails[index].prefab, position, Quaternion.identity);
            obj.transform.parent = spawnPoint.transform;
            count++;

            if (count == 3)
            {
                cubeCount.Add(configObject.typeDetails[index].prefab, 3);
                index++;
                count = 0;
            }
        }
        index = 0;
    }
    public void CheckList(List<TypeDetails> list, List<GameObject> ListUI)
    {
        Dictionary<int, int> count = new Dictionary<int, int>();
        List<int> idsToDelete = new List<int>();

        for (int i = 0; i < list.Count; i++)
        {
            int id = list[i].Id;
            if (count.ContainsKey(id))
            {
                count[id]++;
            }
            else
            {
                count.Add(id, 1);
            }
            if (count[id] >= 3)
            {
                idsToDelete.Add(id);
            }
            if (idsToDelete.Count > 0)
            {
                foreach (int idToDelete in idsToDelete)
                {
                    for (int j = list.Count - 1; j >= 0; j--)
                    {
                        if (list[j].Id == idToDelete)
                        {
                            selectedSound.Stop();
                            destroySound.Play();
                            Destroy(ListUI[j].gameObject);
                            list.RemoveAt(j);
                            listUI.RemoveAt(j);
                        }
                    }
                }
                idsToDelete.Clear();
            }
        }
        if (list.Count > 7)
        {
            selectedSound.Stop();
            gameOver.gameOver();
            gameObject.SetActive(false);
            return;
        }
    }
    private void DisplaySelectedObject(GameObject objToDisplay)
    {
        GameObject clone = Instantiate(objToDisplay);
        listUI.Add(clone);
        var list = horizontalLayoutGroup.gameObject.GetComponentsInChildren<TypeDetails>().ToList().FindAll(_ => _.Id == objToDisplay.GetComponent<TypeDetails>().Id);
        var index = list.Count > 0 ? list.Max(_ => _.transform.GetSiblingIndex()) : horizontalLayoutGroup.childCount;
        clone.transform.SetParent(horizontalLayoutGroup);

        clone.transform.SetSiblingIndex(index);

        clone.transform.localPosition = Vector3.zero;
        clone.transform.localRotation = Quaternion.identity;
    }
}