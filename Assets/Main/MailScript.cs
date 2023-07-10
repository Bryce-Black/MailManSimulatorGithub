using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailScript : MonoBehaviour
{
    public MailScriptableObject mailScript;
    private bool pointsGiven = false;

    private string MailName;
    private float MailMass;
    private float MailSpeed;
    private int MailPoints;
    MailBoxContoller mailBoxController;

    FirstPersonController firstPersonController;
    private Rigidbody rb;

    private void Start()
    {
        mailBoxController = GameObject.FindGameObjectWithTag("MailBoxController").GetComponent<MailBoxContoller>();
        MailName = mailScript.mailName;
        MailMass = mailScript.mailMass;
        MailSpeed = mailScript.mailSpeed;
        MailPoints = mailScript.mailPoints;
        firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        firstPersonController.ChangeMailInfo(MailName, MailSpeed);
        rb = GetComponent<Rigidbody>();
        Vector3 spinDirection = transform.up;
        rb.AddTorque(spinDirection * 10f);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Boundry")
        {
            mailBoxController.MailHasFailed();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Unlocked")
        {
            mailBoxController.MailHasBeenDelivered(MailPoints);
            Destroy(this.gameObject);
        }
        else
        {
            mailBoxController.MailHasFailed();
            Destroy(this.gameObject);
        }
    }

}
