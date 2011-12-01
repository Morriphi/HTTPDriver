properties {
	$base_dir = Resolve-Path .\..
	$build_artifacts = (Get-ChildItem "$base_dir" -Recurse -Include *.* -Name | Select-String "bin")
	$test_assemblies = (Get-ChildItem "$base_dir" -Recurse -Include *Test.dll -Name | Select-String "bin")
}

Task Default -depends Test 

Task Clean {
	Write-Host "Cleaning Up" -for DarkGreen
	foreach($artifact in $build_artifacts) {
		Remove-Item $base_dir\$artifact
	}
}

Task Build -depends Clean {
	Write-Host "Building" -for DarkGreen
	Exec { msbuild $base_dir\HttpDriver.sln }
}

Task Test -depends Build {
	foreach($test_asm_name in $test_assemblies) {
		Write-Host "Running Test: " + $test_asm_name -for DarkGreen
		Exec { ..\lib\NUnit.2.5.10.11092\tools\nunit-console.exe /nodots $base_dir\$test_asm_name }
    }
}