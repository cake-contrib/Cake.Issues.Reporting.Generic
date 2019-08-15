#load nuget:?package=Cake.Recipe&version=1.0.0

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
    shouldRunCodecov: false);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    dupFinderExcludePattern: new string[] { BuildParameters.RootDirectoryPath + "/src/Cake.Issues.Reporting.Generic.Tests/*.cs" },
    testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[Cake.Issues]* -[Cake.Issues.Testing]* -[Cake.Issues.Reporting]* -[Shouldly]* -[Newtonsoft.Json]* -[HtmlAgilityPack]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Build.Run();
