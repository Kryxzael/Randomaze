using UnityEngine;

/// <summary>
/// Allows a centerelized way to obtain sprites, sounds, etc. dynamicly from spritesets
/// </summary>
public class ResourceSet
{
    /// <summary>
    /// The name of the directory the resources in this resourceset are located
    /// </summary>
    public string TargetName { get; set; }

    /// <summary>
    /// The fallback resourceset that will be used if a given resource was not found in this set
    /// </summary>
    public ResourceSet Fallback { get; set; }

    /// <summary>
    /// Creates a new resource set
    /// </summary>
    /// <param name="targetName">The name of the directory the resources in this resourceset are located</param>
    /// <param name="fallbackSet">The fallback resourceset that will be used if a given resource was not found in this set</param>
    public ResourceSet(string targetName, ResourceSet fallbackSet)
    {
        Fallback = fallbackSet;
        TargetName = targetName;
    }
    
    /// <summary>
    /// Gets a resource of type T with a given name
    /// </summary>
    /// <typeparam name="T">Type of the resource</typeparam>
    /// <param name="resourceName">Name of the resource</param>
    /// <returns></returns>
    public T GetResource<T>(string resourceName) where T : Object
    {
        T match = Resources.Load<T>("Resourcesets/" + TargetName + "/" + resourceName);

        if (match == null)
        {
            if (Fallback == null)
            {
                throw new System.IO.FileNotFoundException("No resource was found with the name " + resourceName);
            }

            return Fallback.GetResource<T>(resourceName);
        }

        return match;
    }
}