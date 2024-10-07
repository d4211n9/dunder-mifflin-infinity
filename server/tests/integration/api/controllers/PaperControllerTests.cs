using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using data_access.data_transfer_objects;
using data_access.models;
using tests.helper;
using Xunit.Abstractions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace tests.integration.api.controllers;

public class PaperControllerTests : BaseIntegrationTest
{
    public PaperControllerTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) {}
    
    
    [Fact]
    public async void GetPapers_StatusCodeIs200OkTest()
    {
        // Arrange
        var client = CreateClient();
        await CreateMockPaper(FakePapers.FakePaper1);
        var paperSearchDto = new PaperSearchDto
        {
            MaxPrice = int.MaxValue,
            MinPrice = 0,
            MinStock = 0,
            ShowDiscontinued = true,
            NameSearchQuery = "",
            PaginationDto = new PaginationDto
            {
                PageNumber = 1,
                PageSize = 5
            }
        };
        JsonContent jsonContent = JsonContent.Create(paperSearchDto);
        
        // Act
        var response = await client.PostAsync("/api/paper", jsonContent);
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
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

    private async Task CreateMockPaper(Paper mockPaper)
    {
        await _setup.DbContextInstance.Papers.AddAsync(mockPaper);
    }
}