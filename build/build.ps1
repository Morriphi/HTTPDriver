properties {
	$base_dir = Resolve-Path .\..
	$test_assemblies = (Get-ChildItem "$base_dir" -Recurse -Include *Test.dll -Name | Select-String "bin")
}

Task Default -depends Test

Task Build {
   Exec { msbuild $base_dir\HttpDriver.sln }
}

Task Test -depends Build {
	foreach($test_asm_name in $test_assemblies) {
		Write-Host "Running Tests: " + $test_asm_name -for DarkGreen
		Exec { ..\lib\NUnit.2.5.10.11092\tools\nunit-console.exe /nodots $base_dir\$test_asm_name }
    }
}