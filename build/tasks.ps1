properties {
	$base_dir = Resolve-Path .\..
	$build_artifacts = (Get-ChildItem "$base_dir" -Recurse -Include *.* -Name | Select-String "bin")
	$test_assemblies = (Get-ChildItem "$base_dir" -Recurse -Include *Test.dll -Name | Select-String "bin")
}

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

Task Checkin {
	if($commit_comment -eq ""){
		Write-Host "Unable to check in: commit comment blank" -for Red
	} else {
		Write-Host "Commiting: $commit_comment" -for White
		Set-Location $base_dir
		git add -A
		git commit -m $commit_comment
	}
}