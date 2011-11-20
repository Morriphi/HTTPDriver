Task Default -depends Build

Task Build {
   Exec { msbuild "..\HttpDriver.sln" }
}