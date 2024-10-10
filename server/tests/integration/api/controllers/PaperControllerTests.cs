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
    public async void CreatePaper_StatusCodeIs200OkTest()
    {
        // Arrange
        var client = CreateClient();
        var createPaperDto = new CreatePaperDto
        {
            Name = FakePapers.FakePaper1.Name,
            Stock = FakePapers.FakePaper1.Stock,
            Price = FakePapers.FakePaper1.Price
        };
        var jsonContent = JsonContent.Create(createPaperDto);
        
        // Act
        var response = await client.PostAsync("/api/paper/create", jsonContent);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async void DiscontinuePaper_StatusCodeIs200OkTest()
    {
        // Arrange
        var client = CreateClient();
        await CreateMockPaper(FakePapers.FakePaper1);
        var discontinuePaperDto = new DiscontinuePaperDto
        {
            PaperId = FakePapers.FakePaper1.Id,
            Discontinue = !FakePapers.FakePaper1.Discontinued
        };
        var jsonContent = JsonContent.Create(discontinuePaperDto);

        // Act
        var response = await client.PutAsync("/api/paper/discontinue", jsonContent);
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async void ChangePaperStock_StatusCodeIs200OkTest()
    {
        // Arrange
        var client = CreateClient();
        await CreateMockPaper(FakePapers.FakePaper1);
        var changePaperStockDto = new ChangePaperStockDto
        {
            PaperId = FakePapers.FakePaper1.Id,
            ChangedStock = FakePapers.FakePaper1.Stock + 100
        };
        var jsonContent = JsonContent.Create(changePaperStockDto);

        // Act
        var response = await client.PutAsync("/api/paper/stock", jsonContent);
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
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

    private Task CreateMockPaper(Paper mockPaper)
    {
        _setup.DbContextInstance.Papers.Add(mockPaper);

        return Task.FromResult(_setup.DbContextInstance.SaveChanges());
    }
}