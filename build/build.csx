#load "nuget:Dotnet.Build, 0.9.3"
#load "nuget:dotnet-steps, 0.0.2"

[StepDescription("Creates the NuGet packages")]
AsyncStep dockerImage = async () =>
{
    await Docker.BuildAsync("bernhardrichter/opentelemetry-elasticsearch-logs", BuildContext.LatestTag, BuildContext.RepositoryFolder);
};


[DefaultStep]
[StepDescription("Deploys packages if we are on a tag commit in a secure environment.")]
AsyncStep deploy = async () =>
{
    await dockerImage();
    if (BuildEnvironment.IsSecure && BuildEnvironment.IsTagCommit)
    {
        await Docker.PushAsync("bernhardrichter/opentelemetry-elasticsearch-logs", BuildContext.LatestTag, BuildContext.BuildFolder);
    }

    await Artifacts.Deploy();
};

await StepRunner.Execute(Args);
return 0;