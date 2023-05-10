using UnityEngine;

public class ClickGround : MonoBehaviour
{
    [SerializeField] private ParticleSystem clickGround;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Enemy")) return;
                clickGround.transform.position = new Vector3(hit.point.x,0.4f,hit.point.z);
                clickGround.Play();
            }
           
        }
    }
}
