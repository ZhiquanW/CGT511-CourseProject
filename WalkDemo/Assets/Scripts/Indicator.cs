using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public Vector3 startPos;
    public Rigidbody indicator2;
    public List<Transform> transforms;
    private Rigidbody rigidbody;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.MovePosition(transforms[index++].position);
        indicator2.MovePosition(transforms[index].position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (index == transforms.Count)
            {
                GameManager.instance.SaveResults();
                GameManager.instance.StopAllCoroutines();
                gameObject.SetActive(false);

            }
            else
                rigidbody.MovePosition(transforms[index].position);
            if (index >= transforms.Count -1)
                indicator2.gameObject.SetActive(false);
            else           
                indicator2.MovePosition(transforms[index + 1].position);
            index++;
        }
    }
    public float CalculateDeviation(Vector3 pos)
    {
        float dis = 0;
        pos.y = 0.5f;
        if(index >= 2)
        {
            dis = Point2Line(pos, transforms[index - 2].position, transforms[index - 1].position);
        }
        else
        {
            dis = Point2Line(pos, startPos, transforms[index].position);
        }
        return dis;
    }
    public float Point2Line(Vector3 point, Vector3 linePoint1, Vector3 linePoint2)
    {
        float fProj = Vector3.Dot(point - linePoint1, (linePoint1 - linePoint2).normalized);
        return ((point - linePoint1).sqrMagnitude - fProj * fProj);
    }
    public void Reset()
    {
        index = 0;
        gameObject.SetActive(true);
        indicator2.gameObject.SetActive(true);
        rigidbody.MovePosition(transforms[index++].position);
        indicator2.MovePosition(transforms[index].position);
    }
}
