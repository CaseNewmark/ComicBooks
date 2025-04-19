using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NJsonSchema.CodeGeneration.TypeScript;
using NSwag;
using NSwag.CodeGeneration.TypeScript;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
var app = builder.Build();
app.Start();

if (args.Length != 2)
    throw new ArgumentException("Expecting 2 arguments: URL, outputPath");

var url = args[0];
var outputPath = Path.Combine(Directory.GetCurrentDirectory(), args[1]);

var logger = app.Services.GetService<ILogger<Program>>();
logger?.LogInformation($"Generating TypeScript client for {url} to {outputPath}...");

await GenerateClient(
    document: await OpenApiDocument.FromUrlAsync(url),
    outputPath: outputPath,
    generateCode: (OpenApiDocument document) =>
    {
        var settings = new TypeScriptClientGeneratorSettings();

        settings.ClassName = "ApiClientService";
        settings.Template = TypeScriptTemplate.Angular;
        settings.PromiseType = PromiseType.Promise;
        settings.HttpClass = HttpClass.HttpClient;
        settings.RxJsVersion = 7;
        settings.UseSingletonProvider = true;
        settings.InjectionTokenType = InjectionTokenType.InjectionToken;
        settings.TypeScriptGeneratorSettings.TypeStyle = TypeScriptTypeStyle.Interface;
        settings.TypeScriptGeneratorSettings.TypeScriptVersion = 5.5M;
        settings.TypeScriptGeneratorSettings.DateTimeType = TypeScriptDateTimeType.String;

        var generator = new TypeScriptClientGenerator(document, settings);
        var code = generator.GenerateFile();

        return code;
    },
    logger: logger
);


async static Task GenerateClient(OpenApiDocument document, string outputPath, Func<OpenApiDocument, string> generateCode, ILogger? logger = null)
{
    var code = generateCode(document);
    await System.IO.File.WriteAllTextAsync(outputPath, code);
}