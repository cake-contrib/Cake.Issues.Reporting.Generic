#load nuget:?package=Cake.Recipe&version=3.0.1

//*************************************************************************************************
// Settings
//*************************************************************************************************

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.Issues.Reporting.Generic",
    repositoryOwner: "cake-contrib",
    repositoryName: "Cake.Issues.Reporting.Generic",
    appVeyorAccountName: "cakecontrib",
    shouldGenerateDocumentation: false);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[Cake.Issues]* -[Cake.Issues.Testing]* -[Cake.Issues.Reporting]* -[Cake.Issues.Reporting.Generic]LitJson.* -[Shouldly]* -[HtmlAgilityPack]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

//*************************************************************************************************
// Execution
//*************************************************************************************************

Build.RunDotNetCore();
