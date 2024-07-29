using AStar.Api.HealthChecks;
using AStar.Web.Constants;
using AStar.FilesApi.Client.SDK.FilesApi;
using AStar.ImagesApi.Client.SDK.ImagesApi;
using Microsoft.AspNetCore.Components;

namespace AStar.Web.Components.Pages;

public partial class Home
{
    [Inject]
    private FilesApiClient FilesApiClient { get; set; } = default!;

    [Inject]
    private ImagesApiClient ImagesApiClient { get; set; } = default!;

    private const string NotChecked = "Not yet checked...";
    private const string CheckingText = "Checking...please wait...";
    private static readonly string WarningClass = "alert alert-warning";
    private readonly string healthCheckFailure = "alert alert-danger";
    private readonly string healthCheckSuccess = "alert alert-success";
    private readonly string healthCheckWarning = WarningClass;
    private string ImagesApiHealthCheckClass = WarningClass;
    private string FilesApiHealthCheckClass = WarningClass;
    private string DatabaseHealthCheckClass = WarningClass;
    private string FilesApiHealthStatus = NotChecked;
    private string ImagesApiHealthStatus = NotChecked;
    private string DatabaseHealthStatus = NotChecked;

    protected override Task OnInitializedAsync()
    {
        _ = base.OnInitializedAsync();
        return CheckApiStatuses();
    }

    private async Task CheckApiStatuses()
    {
        SetStatusCheckingDetails();

        var filesApiStatus = await FilesApiClient.GetHealthAsync();
        var imagesApiStatus = await ImagesApiClient.GetHealthAsync();
        FilesApiHealthCheckClass = SetHealthCheckClass(filesApiStatus);
        ImagesApiHealthCheckClass = SetHealthCheckClass(imagesApiStatus);
        FilesApiHealthStatus = filesApiStatus.Status;
        ImagesApiHealthStatus = imagesApiStatus.Status;
        var random = new Random();
        if(random.Next(1,10) > 5)
        {
            DatabaseHealthCheckClass = healthCheckSuccess;
            DatabaseHealthStatus = ApiCheckConstants.HealthyStatus;
        }
        else
        {
            DatabaseHealthCheckClass = healthCheckFailure;
            DatabaseHealthStatus = "Something bad happened...";
        }
    }

    private string SetHealthCheckClass(HealthStatusResponse healthStatusResponse) 
        => healthStatusResponse.Status == ApiCheckConstants.HealthyStatus 
                                          ? healthCheckSuccess 
                                          : healthCheckFailure;

    private void SetStatusCheckingDetails()
    {
        FilesApiHealthCheckClass = healthCheckWarning;
        ImagesApiHealthCheckClass = healthCheckWarning;
        FilesApiHealthStatus = CheckingText;
        ImagesApiHealthStatus = CheckingText;
    }
}