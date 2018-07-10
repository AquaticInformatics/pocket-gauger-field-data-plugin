# pocket-gauger-field-data-plugin

[![Build status](https://ci.appveyor.com/api/projects/status/0ndcntced4nowxsp/branch/master?svg=true)](https://ci.appveyor.com/project/SystemsAdministrator/pocket-gauger-field-data-plugin/branch/master)

An AQTS field data plugin supporting [Pocket Gauger](https://www.isodaq.co.uk/casestudies/view/cs/pocket-gauger) zip archives.

## Requirements

- Requires Visual Studio 2017 (Community Edition is fine)
- .NET 4.7 runtime

## Building the plugin

- Load the `src\PocketGaugerPlugin.sln` file in Visual Studio and build the `Release` configuration.
- The `src\PocketGauger\deploy\Release\PocketGauger.plugin` file can then be installed on your AQTS app server.

## Testing the plugin within Visual Studio

Use the included `PluginTester.exe` tool from the `Aquarius.FieldDataFramework` package to test your plugin logic on the sample files.

1. Open the EhsnPlugin project's **Properties** page
2. Select the **Debug** tab
3. Select **Start external program:** as the start action and browse to `"src\packages\Aquarius.FieldDataFramework.17.4.0\tools\PluginTester.exe`
4. Enter the **Command line arguments:** to launch your plugin

```
/Plugin=PocketGauger.dll /Json=AppendedResults.json /Data=..\..\..\..\data\GaugingSummaryWithThreeVisits.zip
```

The `/Plugin=` argument can be the filename of your plugin assembly, without any folder. The default working directory for a start action is the bin folder containing your plugin.

5. Set a breakpoint in the plugin's `ParseFile()` methods.
6. Select your plugin project in Solution Explorer and select **"Debug | Start new instance"**
7. Now you're debugging your plugin!

See the [PluginTester](https://github.com/AquaticInformatics/aquarius-field-data-framework/tree/master/src/PluginTester) documentation for more details.

## Installation of the plugin

Use the [FieldDataPluginTool](https://github.com/AquaticInformatics/aquarius-field-data-framework/tree/master/src/FieldDataPluginTool) to install the plugin on your AQTS app server.
