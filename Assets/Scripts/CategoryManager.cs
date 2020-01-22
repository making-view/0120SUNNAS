using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryManager : MonoBehaviour
{
    [SerializeField] Category[] categories;
    
    private int maxCategories = 4;
    private int maxVideos = 4;

    // Start is called before the first frame update
    void Start()
    {
        var videoManager = FindObjectOfType<VideoManager>();
        var path = videoManager.GetActivePath();

        var directoryInfo = new DirectoryInfo(path);
        var directories = directoryInfo.GetDirectories().ToList().Take(maxCategories);

        var catIndex = 0;
        foreach(var directory in directories)
        {
            var category = categories[catIndex];
            category.categoryNameText.text = directory.Name;

            var files = directory.GetFiles();
            var videoIndex = 0;
            foreach(var file in files)
            {
                if (file.Extension == ".mp4")
                {
                    var card = category.cards[videoIndex];
                    card.titleBarText.text = file.Name.Substring(0, file.Name.IndexOf(file.Extension));
                    card.attractionVideoFile = directory.Name + "/" + file.Name;
                    videoIndex++;
                }
            }

            for (int i = videoIndex; i < maxVideos; i++)
            {
                category.cards[i].transform.parent.parent.parent.gameObject.SetActive(false);
            }

            catIndex++;
        }

        for (int i = catIndex; i < maxCategories; i++)
        {
            categories[i].categoryNameText.transform.parent.parent.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
