using System.Text.Json;
using api;
using data_access;
using data_access.data_transfer_objects;
using data_access.models;
using Microsoft.AspNetCore.Mvc.Testing;
using PgCtx;
using tests.helper;
using Xunit.Abstractions;
using Xunit.Sdk;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace tests.integration.api.controllers;

public class CustomerControllerTests : BaseIntegrationTest
{
    public CustomerControllerTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) {}
    
    [Theory]
    [InlineData("SearchQuery=Pandora", 1, 5, 1, 1)]
    [InlineData("", 1, 5, 5, 1)]
    [InlineData("", 2, 3, 2, 2)]
    public async void GetCustomers_CanFilterForCorrectCustomersTest(
        string searchQuery,
        int pageNumber,
        int pageSize,
        int expectedListCount,
        int expectedTotalPages)
    {
        // Arrange
        var client = CreateClient();
        await CreateMockCustomers();
        
        // Act 
        var response = await client.GetAsync("/api/" + 
                                            nameof(Customer) + 
                                             "?" + searchQuery + 
                                             "&PaginationDto.PageNumber=" + pageNumber + 
                                             "&PaginationDto.PageSize=" + pageSize)
            .Result.Content
            .ReadAsStringAsync();
        
        SelectionWithPaginationDto<Customer> selectionWithPaginationDto = JsonSerializer
            .Deserialize<SelectionWithPaginationDto<Customer>>(response, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new InvalidOperationException();
    
        // Assert
        Assert.NotNull(selectionWithPaginationDto);
        Assert.Equal(expectedListCount, selectionWithPaginationDto.Selection.Count());
        Assert.Equal(expectedTotalPages, selectionWithPaginationDto.TotalPages);
    }
    
    private async Task CreateMockCustomers()
    {
        IList<Customer> mockCustomers = new List<Customer>();
        mockCustomers.Add(FakeCustomers.FakeCustomer1);
        mockCustomers.Add(FakeCustomers.FakeCustomer2);
        mockCustomers.Add(FakeCustomers.FakeCustomer3);
        mockCustomers.Add(FakeCustomers.FakeCustomer4);
        mockCustomers.Add(FakeCustomers.FakeCustomer5); 
        
        await _setup.DbContextInstance.Customers.AddRangeAsync(mockCustomers);
        await _setup.DbContextInstance.SaveChangesAsync();
    }
}