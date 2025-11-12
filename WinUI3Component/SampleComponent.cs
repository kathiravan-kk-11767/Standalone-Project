namespace WinUI3Component;

/// <summary>
/// Sample WinUI3 component
/// </summary>
public class SampleComponent
{
    public string Name { get; set; } = "WinUI3 Component";

    public string GetComponentInfo()
    {
        return $"Component: {Name}";
    }
}
