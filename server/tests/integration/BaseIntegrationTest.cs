using api;
using data_access;
using Microsoft.AspNetCore.Mvc.Testing;
using PgCtx;
using Xunit.Abstractions;

namespace tests.integration;

public class BaseIntegrationTest : WebApplicationFactory<Program>
{
    protected readonly PgCtxSetup<MyDbContext> _setup = new();
    protected readonly ITestOutputHelper _testOutputHelper;

    public BaseIntegrationTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        Environment.SetEnvironmentVariable("conn", _setup._postgres.GetConnectionString());
    }
}