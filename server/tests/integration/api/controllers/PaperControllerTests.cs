using System.Text.Json;
using data_access.models;
using Xunit.Abstractions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace tests.integration.api.controllers;

public class PaperControllerTests : BaseIntegrationTest
{
    public PaperControllerTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) {}
    
    
    [Fact]
    public async void CreateProperty_EnsureActuallyCreatedInDbTest()
    {
        // Arrange
        var client = CreateClient();
        string propertyName = "Sturdy";
        int expectedIdOfCreatedProperty = 1;

        // Act
        var response = await client.PostAsync("/api/paper/property/create/" + propertyName, null)
            .Result.Content
            .ReadAsStringAsync();
        
        Property createdProperty = JsonSerializer
            .Deserialize<Property>(response, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new InvalidOperationException();
        
        // Assert
        var createdPropertyInDb = _setup.DbContextInstance.Properties.FirstOrDefault();
        
        Assert.NotNull(createdPropertyInDb);
        Assert.NotNull(createdProperty);
        Assert.Equivalent(createdPropertyInDb, createdProperty);
        Assert.Equivalent(propertyName, createdProperty.PropertyName);
        Assert.True(createdProperty.Id == expectedIdOfCreatedProperty);
        Assert.Empty(createdProperty.Papers);
    }
}