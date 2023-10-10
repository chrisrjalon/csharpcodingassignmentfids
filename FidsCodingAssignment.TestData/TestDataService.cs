using System.Reflection;
using System.Text.Json;
using FidsCodingAssignment.TestData.Models;

namespace FidsCodingAssignment.TestData;

public class TestDataService
{
    private const string ResourceName = "FidsCodingAssignment.TestData.Resources.rawData.json";
    public bool DataInitialized { get; private set; }

    public TestDataModel TestData { get; private set; } = null!;

    public TestDataService()
    {
        if (DataInitialized) 
            return;
        
        Initialize();
    }
    
    private void Initialize()
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        using var stream = assembly.GetManifestResourceStream(ResourceName);
        using var reader = new StreamReader(stream!);
        
        var rawText = reader.ReadToEnd();
        TestData = JsonSerializer.Deserialize<TestDataModel>(rawText)!;
        DataInitialized = true;
    }
}