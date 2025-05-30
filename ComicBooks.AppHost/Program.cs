var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
                      .WithDataVolume()
                      .WithPgAdmin();

var postgresdb = postgres.AddDatabase("comicbooksdb");
    
var apiService = builder.AddProject<Projects.ComicBooks_ApiService>("apiservice")
                        .WithReference(postgresdb)
                        .WaitFor(postgresdb);

builder.AddProject<Projects.ComicBooks_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
