using Microsoft.ML.Data;
using Microsoft.Extensions.ML;
using Microsoft.ML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Sentiment Prediction API",
        Description = "An Tanzu based Web API for predicting toxicity sentiment of text.This API encapsulates a model trained on a minimal dataset and hence provides results with lower degree of accuracy",
        TermsOfService = new Uri("https://thewidgetstore.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Development Contact",
            Url = new Uri("https://thewidgetstore.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Development License",
            Url = new Uri("https://thewidgetstore.com/license")
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowedOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddPredictionEnginePool<ModelInput, ModelOutput>()
    .FromUri(modelName:"SentimentAnalysisModel", uri:"https://raw.githubusercontent.com/PradeepLoganathan/SentimentAnalysisAPI/main/SentimentModel.zip", period: TimeSpan.FromMinutes(1));

// builder.Services.AddPredictionEnginePool<ModelInput, ModelOutput>().FromFile(modelName: "SentimentAnalysisModel", filePath:"SentimentModel.zip", watchForChanges: true);

var app = builder.Build();

app.UseCors("MyAllowedOrigins");
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


app.UseHttpsRedirection();

app.Map("/", () => Results.Redirect("/swagger"));

app.MapPost("/predict", async ([FromServices] PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool,[FromBody] ModelInput input) => { 
    var result = await Task.FromResult(predictionEnginePool.Predict(modelName: "SentimentAnalysisModel", input));
    var prediction = Convert.ToBoolean(result.Prediction) ? "Toxic" : "Non Toxic";
    return prediction;
})
.WithDescription("The sentiment prediction endpoint")
.WithName("PredictSentiment")
.Produces<string>()
.Produces( 404 )
.Accepts<ModelInput>("application/xml")
.Accepts<ModelInput>("application/json")
.WithOpenApi(generatedOperation =>
{
    // var parameter1 = generatedOperation.Parameters[0];
    // parameter1.Description = "The text";

    // var parameter2 = generatedOperation.Parameters[1];
    // parameter2.Description = "The label";

    generatedOperation.Summary = "This endpoint generates the prediction";
    generatedOperation.Description = "This endpoint uses the model built as part of the training and generates a sentiment prediction";


    return generatedOperation;
});

app.Run();