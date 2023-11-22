using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tags
{
    None,
    Interactive,
    Pickable,
    Placeable,
}

public class CustomTags : MonoBehaviour
{
    [SerializeField]
    private List<Tags> tags = new List<Tags>();
	
    public bool HasTag(Tags tag)
    {
        return tags.Contains(tag);
    }
	
    public IEnumerable<Tags> GetTags()
    {
        return tags;
    }
	
    public void Rename(int index, Tags tagName)
    {
        tags[index] = tagName;
    }
	
    public Tags Get(int index)
    {
        return tags[index];
    }
	
    public int Count()
    {
        return tags.Count;
    }
}
