$root = (split-path -parent $MyInvocation.MyCommand.Definition) + '\..'
$version = [System.Reflection.Assembly]::LoadFile("$root\ViewModels\bin\Release\ZimCode.ViewModels.dll").GetName().Version
$versionStr = "{0}.{1}.{2}" -f ($version.Major, $version.Minor, $version.Build)

Write-Host "Setting .nuspec version tag to $versionStr"

$content = (Get-Content $root\NuGet\ZimCode.ViewModels.nuspec) 
$content = $content -replace '\$version\$',$versionStr

$content | Out-File $root\NuGet\ZimCode.ViewModels.compiled.nuspec

& $root\NuGet\NuGet.exe pack $root\NuGet\ZimCode.ViewModels.compiled.nuspec

& $root\NuGet\NuGet.exe setapikey $env:nuget_key -Verbosity quiet
& $root\NuGet\NuGet.exe push *.nupkg