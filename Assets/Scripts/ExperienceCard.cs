using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceCard : MonoBehaviour
{
    [SerializeField] private Text experienceName;
    [SerializeField] private GameObject tableModel;
    [SerializeField] private float lineHeight;

    private Vector3 tableModelStartScale;
    private Color nameStartColor;
    private ParticleSystem highlightRings;
    private LineRenderer highlightLine;

    void Start()
    {
        tableModelStartScale = tableModel.transform.localScale;
        nameStartColor = experienceName.color;
        highlightRings = tableModel.GetComponentInChildren<ParticleSystem>(true);
        highlightLine = GetComponent<LineRenderer>();

        if (highlightLine != null && tableModel != null)
        {
            var buttonBottom = new Vector3(transform.position.x, transform.position.y - (lineHeight / 100), transform.position.z);

            highlightLine.SetPosition(0, buttonBottom);
            highlightLine.SetPosition(1, tableModel.transform.position);
        }
    }

    // Highlights the card and its table model
    public void Activate()
    {
        tableModel.transform.localScale = tableModelStartScale * 2;
        experienceName.color = new Color(82f / 255f, 141f / 255f, 144f / 255f);

        if (highlightRings != null)
        {
            highlightRings.gameObject.SetActive(true);
            highlightRings.Play();
        }

        if (highlightLine != null)
        {
            highlightLine.enabled = true;
        }
    }

    // Returns card and table model to default state
    public void Deactivate()
    {
        tableModel.transform.localScale = tableModelStartScale;
        experienceName.color = nameStartColor;

        if (highlightRings != null)
        {
            highlightRings.gameObject.SetActive(false);
            highlightRings.Stop();
        }

        if (highlightLine != null)
        {
            highlightLine.enabled = false;
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        ExperienceCard attr = obj as ExperienceCard;
        if ((object)attr == null)
            return false;

        return tableModel == attr.tableModel;
    }

    public override int GetHashCode()
    {
        var hashCode = 875532754;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<GameObject>.Default.GetHashCode(tableModel);
        return hashCode;
    }
}
