var target = Argument("target", "Default");

Task("Restore-NuGet-Packages")
    .Does(() =>
{
    NuGetRestore("../Source/SimpleDataGenerator.sln");
});

Task("Clean")
    .Does(() =>
{
    CleanDirectories("../Source/**/bin");
    CleanDirectories("../Source/**/obj");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    MSBuild("../Source/SimpleDataGenerator.sln", settings => settings.SetConfiguration("Release"));
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    NUnit("../Source/SimpleDataGenerator.Tests.Core/bin/Release/SimpleDataGenerator.Tests.Core.dll", new NUnitSettings {
        ToolPath = "../Source/packages/NUnit.ConsoleRunner.3.2.1/tools/nunit3-console.exe"
    });
	
});

Task("Default")
	.IsDependentOn("Run-Unit-Tests")
    .Does(() =>
{
    Information("SimpleDataGenerator building finished.");
});

RunTarget(target);