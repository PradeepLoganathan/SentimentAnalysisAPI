using Microsoft.ML.Data;
using Microsoft.Extensions.ML;
using Microsoft.ML;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddPredictionEnginePool<ModelInput, ModelOutput>()
//     .FromUri(modelName:"SentimentAnalysisModel", uri:"https://github.com/PradeepLoganathan/SentimentAnalysisTrainer/blob/f0a74409951fc565a696c79e8490559a6a7adaa8/MLModels/SentimentModel.zip", period: TimeSpan.FromMinutes(1));

builder.Services.AddPredictionEnginePool<ModelInput, ModelOutput>().FromFile(modelName: "SentimentAnalysisModel", filePath:"SentimentModel.zip", watchForChanges: true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Map("/", () => Results.Redirect("/swagger"));

app.MapPost("/predict", async ([FromServices] PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool,[FromBody] ModelInput input) => { 
    var result = await Task.FromResult(predictionEnginePool.Predict(modelName: "SentimentAnalysisModel", input));
    var prediction = Convert.ToBoolean(result.Prediction) ? "Toxic" : "Non Toxic";
    return prediction;
})
.WithDescription("The sentiment prediction endpoint")
.WithOpenApi();

app.Run();