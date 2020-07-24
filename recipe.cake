#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&version=2.0.0-alpha0319&prerelease

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.Issues.Reporting.Generic",
    repositoryOwner: "cake-contrib",
    repositoryName: "Cake.Issues.Reporting.Generic",
    appVeyorAccountName: "cakecontrib",
    shouldGenerateDocumentation: false,
    shouldRunCodecov: false,
    shouldRunGitVersion: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    buildMSBuildToolVersion: MSBuildToolVersion.VS2019,
    dupFinderExcludePattern: new string[] { BuildParameters.RootDirectoryPath + "/src/Cake.Issues.Reporting.Generic.Tests/*.cs", BuildParameters.RootDirectoryPath + "/src/Cake.Issues.Reporting.Generic/LitJson/*.cs" },
    testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[Cake.Issues]* -[Cake.Issues.Testing]* -[Cake.Issues.Reporting]* -[Cake.Issues.Reporting.Generic]LitJson.* -[Shouldly]* -[HtmlAgilityPack]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Task("Prepare-Build-Demo-Reports")
    .Does<BuildVersion>((context, buildVersion) =>
{
    var package =
        BuildParameters.Paths.Directories.NuGetPackages.CombineWithFilePath("Cake.Issues.Reporting.Generic." + buildVersion.SemVersion + ".nupkg");
    var addinDir = BuildParameters.Paths.Directories.TempBuild.Combine("Cake.Issues.Reporting.Generic");

    if (DirectoryExists(addinDir))
    {
        DeleteDirectory(
            addinDir,
            new DeleteDirectorySettings 
            {
                Recursive = true,
                Force = true
            });
    }

    Unzip(package, addinDir);
});

Build.Run();
