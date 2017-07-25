﻿#addin "nuget:http://repositorio.mstech.com.br/repository/nuget-prereleases/?package=Cake.MSTech&version=0.3.0-WebPackageForWeb0001"
#addin "nuget:https://www.nuget.org/api/v2?package=Newtonsoft.Json&version=9.0.1"
#tool "nuget:?package=GitVersion.CommandLine"
#load "./build/ms-parameters.cake"
#load "./build/ms-parameters.cake"
#load "./build/ms-helpers.cake"
#load "./build/ms-tasks.cake"
#load "./build/tasks.cake"






//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

BuildParameters parameters = new BuildParameters();

var buildSettings = BuildSettings.GetFromJsonFile(@"./build/buildsettings.json");


///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(context =>
{
    Information("Running tasks...");
    Information("Definindo os parâmetros...");
    buildSettings.SemVer = GitVersionHelper.GetVersion(context);
    Information("Building version {0}", buildSettings.SemVer);
    parameters.ServerRepository = MSTechParameters.FileServerRepository;
    parameters.ZipFileName = buildSettings.OutputDirectory +"/" + buildSettings.BuildName +"_" + buildSettings.SemVer +".zip";;
    parameters.IsLocalBuild = BuildSystem.IsLocalBuild;
    parameters.OutputArtifacts = parameters.ZipFileName;
    parameters.ServerRepositoryBuild = parameters.ServerRepository + "\\" + buildSettings.BuildName;
    parameters.IsPublishBuild = IsBuildTaggedOnJenkins() || IsReleaseBranchOnJenkins();
});

Teardown(context =>
{
    Information("Finished running tasks.");
});


//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
.IsDependentOn("Clean")
    .IsDependentOn("WebProject")    
    .IsDependentOn("Zip")
    .IsDependentOn("Publish");


//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);