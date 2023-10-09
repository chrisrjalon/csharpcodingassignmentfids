using System.Reflection;
using System.Text.Json;
using FidsCodingAssignment.TestData.Models;

namespace FidsCodingAssignment.TestData;

public class TestDataService
{
    public bool DataInitialized { get; private set; }
    
    public TestDataModel GetTestData()
    {
        const string resourceName = "FidsCodingAssignment.TestData.Resources.rawData.json";
        
        var assembly = Assembly.GetExecutingAssembly();
        
        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream!);
        
        var rawText = reader.ReadToEnd();
        var testData = JsonSerializer.Deserialize<TestDataModel>(rawText);
        DataInitialized = true;
        
        return testData!;
    }
}