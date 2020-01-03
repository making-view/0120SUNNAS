using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField] private Canvas gazeUICanvas;
    [SerializeField] private float rayLength = 500f;
    [SerializeField] private float gazeDuration = 4f;
    private float timer = 0;

    private GameObject previousObject = null;
    private ExperienceCard previousCard = null;
    private ProgressBarButton previousProgressbarButton = null;

    private void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.green);

        // Checks if any object is being looked at and handle the result
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength))
        {
            if (hit.transform.gameObject != null)
            {
                HitObject(hit.transform.gameObject);
            }
            else
                HitObject(null);
        }
        else
            HitObject(null);

        // Updates progressbars if any were looked at this and/or last frame
        if (previousObject != null)
        {
            var currentProgressbarButton = previousObject.GetComponent<ProgressBarButton>();

            if (currentProgressbarButton != null)
            {
                timer += Time.deltaTime;
                currentProgressbarButton.SetProgress(Mathf.Clamp(timer / gazeDuration, 0.0f, 1.0f));

                if (currentProgressbarButton.isProgressBarFilled)
                {
                    currentProgressbarButton.Proceed();

                    previousObject = null;
                    timer = 0;
                }

                if (previousProgressbarButton != null && currentProgressbarButton != previousProgressbarButton)
                {
                    previousProgressbarButton.ResetProgress();
                }
                previousProgressbarButton = currentProgressbarButton;
            }
            else if (previousProgressbarButton != null)
            {
                previousProgressbarButton.ResetProgress();
                previousProgressbarButton = null;
            }
        }
        else if (previousProgressbarButton != null)
        {
            previousProgressbarButton.ResetProgress();
            previousProgressbarButton = null;
        }
    }

    /*
     * Update interal data structure to reflect what's currently being looked at
     */
    private void HitObject(GameObject go)
    {
        if (go == null)
        {
            previousObject = null;
            timer = 0;
        }
        else if (go != previousObject)
        {
            timer = 0;
            previousObject = go;

            TryActivateExperienceCard(go);
        }

        // Show reticle if looking in the direction of UI
        if (go != null && go.CompareTag("UI"))
            gazeUICanvas.gameObject.SetActive(true);
        else
            gazeUICanvas.gameObject.SetActive(false);
    }

    /*
     * If the GameObject is an Experience Card, activate it.
     * If another card was already active, deactivate it.
     */
    private void TryActivateExperienceCard(GameObject go)
    {
        if (go != null)
        {
            var experienceCard = go.GetComponentInChildren<ExperienceCard>();

            if (experienceCard != null)
            {
                experienceCard.Activate();

                if (previousCard != null && !previousCard.Equals(experienceCard))
                {
                    previousCard.Deactivate();
                }

                previousCard = experienceCard;
            }
        }
    }
}
