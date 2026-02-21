using Hangfire;
using Hangfire.Dashboard;
using HangfireBasicAuthenticationFilter;
using Scalar.AspNetCore;
using Serilog;
using SurveyBasket;
using SurveyBasket.Middlewares;
using SurveyBasket.Services.DistributedCache;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);
builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
    app.MapScalarApiReference();
}

app.UseRequestLocalization(new RequestLocalizationOptions()
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US")
});

app.UseHttpDataLog();

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseCors("AllowAll");
//app.UseOutputCache();
//app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseExceptionHandler();//("error-handling-endpoint");

app.UseAuthorization();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = [ new HangfireCustomBasicAuthenticationFilter
    {
        User = app.Configuration.GetValue<string>("HangfireSettings:User"),
        Pass = app.Configuration.GetValue<string>("HangfireSettings:Pass")
    }],
    DashboardTitle = "Survay Basket Jops",
    //IsReadOnlyFunc = (DashboardContext context) => true  //make trigger and delete dashboard buttons hidden 
});

app.InvokeAutomatedHangfireJobs();

app.MapIdentityApi<ApplicationUser>();

app.MapControllers();

app.Run();
