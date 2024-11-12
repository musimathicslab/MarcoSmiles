using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class KeyboardRotation : MonoBehaviour
{

    private MRUK MRUK;

    // Start is called before the first frame update
    void Start()
    {
        // Get the MRUK instance
        MRUK = GameObject.FindGameObjectWithTag("MRUK_GO").GetComponent<MRUK>();

        // Get the actual room
        MRUKRoom mrukRoom = MRUK.GetCurrentRoom();

        // Get all tables in the room
        List<MRUKAnchor> tableAnchors = mrukRoom.Anchors.Where(anchor => anchor.Label == MRUKAnchor.SceneLabels.TABLE).ToList();

        // Get all walls in the room
        List<MRUKAnchor> wallAnchors = mrukRoom.Anchors.Where(anchor => anchor.Label == MRUKAnchor.SceneLabels.WALL_FACE).ToList();

        // Find the closest table to this
        MRUKAnchor closestTable = tableAnchors.OrderBy(anchor => Vector3.Distance(anchor.transform.position, this.gameObject.transform.position)).First();

        // Rotate the keyboard to face away from closest wall
        var cardinalAxisIndex = 0;
        Vector3 directionAwayFromClosestWall = GetDirectionAwayFromClosestWall(closestTable, out cardinalAxisIndex, WallAnchors: wallAnchors);
        this.gameObject.transform.rotation = Quaternion.LookRotation(directionAwayFromClosestWall);

        // Rotate the keyboard to be parallel to the table
        this.gameObject.transform.rotation = Quaternion.Euler(-90, this.gameObject.transform.rotation.eulerAngles.y, this.gameObject.transform.rotation.eulerAngles.z);

        // Put the keyboard on the x and z of the table
        this.gameObject.transform.position = new Vector3(closestTable.transform.position.x, this.gameObject.transform.position.y, closestTable.transform.position.z);

        Debug.Log("I'm a fucking genius, suck my ba**s :P ");

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Got it in Meta.XR.MRUtilityKit, they had it private, fucking bastards
    private Vector3 GetDirectionAwayFromClosestWall(MRUKAnchor anchor, out int cardinalAxisIndex, List<int> excludedAxes = null, List<MRUKAnchor> WallAnchors = null)
    {
        float maxDist = float.PositiveInfinity;
        Vector3 result = anchor.transform.up;
        cardinalAxisIndex = 0;
        for (int i = 0; i < 4; i++)
        {
            if (excludedAxes != null && excludedAxes.Contains(i))
            {
                continue;
            }

            Vector3 vector = Quaternion.Euler(0f, 90f * (float)i, 0f) * -anchor.transform.up;
            foreach (MRUKAnchor wallAnchor in WallAnchors)
            {
                if (wallAnchor.Raycast(new Ray(anchor.transform.position, vector), maxDist, out var hitInfo))
                {
                    maxDist = hitInfo.distance;
                    cardinalAxisIndex = i;
                    result = -vector;
                }
            }
        }

        return result;
    }

}
