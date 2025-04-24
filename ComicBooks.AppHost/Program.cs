var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
                      .WithDataVolume()
                      .WithPgAdmin();

var postgresdb = postgres.AddDatabase("comicbooksdb");
    
var apiService = builder.AddProject<Projects.ComicBooks_ApiService>("apiservice")
                        .WithReference(postgresdb)
                        .WaitFor(postgresdb);

// builder.AddProject<Projects.ComicBooks_Generator>("generator")
//     .WithReference(apiService)
//     .WaitFor(apiService)
//     .WithArgs(new string[] { $"hhh", "../ComicBooks.Angular/src/" });

builder.AddProject<Projects.ComicBooks_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddNpmApp("comicbooks", "../ComicBooks.Angular")
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithEnvironment("PORT", "4200")
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints();

builder.Build().Run();
