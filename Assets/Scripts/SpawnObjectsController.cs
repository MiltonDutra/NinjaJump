using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectsController : MonoBehaviour {

    //public GameObject connectedBody;
    public GameObject coin;
    public float maxDistanceX, minDistanceX, maxDistanceY, minDistanceY;
    public static SpawnObjectsController spawnObjectsController;
    public GameObject[] poolObstacles;
    private Vector3 currentPosition;
    public IEnumerator IpoolObstacles;
	void Start () {
        if (spawnObjectsController == null)
        {
            spawnObjectsController = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        IpoolObstacles = poolObstacles.GetEnumerator();
        currentPosition = ConnectedBody().transform.position;
        InstantiateConnectedBodyInSpace();
    }
    public void InstantiateConnectedBodyInSpace()
    {
        Vector3 positionNewConnectedBody = new Vector3();
        float distance = 0;
        do
        {
            float postionX = Random.Range(minDistanceX, maxDistanceX);
            float positionY = Random.Range(minDistanceY, maxDistanceY);
            positionNewConnectedBody = new Vector3(currentPosition.x + postionX, positionY, currentPosition.z);
            distance = Vector2.Distance(currentPosition, positionNewConnectedBody);
        }
        while (distance >= 6.8f);
        //Coin(positionNewConnectedBody);
        GameObject objeto = ConnectedBody();
        objeto.transform.position = positionNewConnectedBody;
        currentPosition = objeto.transform.position;
        objeto.SetActive(true);
    }
    private void Coin(Vector3 newPosition)
    {
        float positionX = (-currentPosition.x + newPosition.x) / 2 + currentPosition.x;
        float positionY = (-currentPosition.y + newPosition.y) / 2 + currentPosition.y;
        Instantiate(coin, new Vector3(positionX, positionY, newPosition.z), coin.transform.rotation);
    }
    private GameObject ConnectedBody()
    {
        if (IpoolObstacles.MoveNext())
        {
            return IpoolObstacles.Current as GameObject;
        }
        else
        {
            IpoolObstacles.Reset();
            return ConnectedBody();
        }
    }
    public void DesactiveObstacle(GameObject objeto, float time)
    {
        StartCoroutine(TimeDesactive(objeto, time));
    }
    IEnumerator TimeDesactive(GameObject objeto, float time)
    {
        yield return new WaitForSeconds(time);
        objeto.SetActive(false);

    }
}
