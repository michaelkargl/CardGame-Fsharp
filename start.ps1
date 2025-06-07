$ProjectDirectory = Join-Path $PSScriptRoot 'CardGame'

Write-Host -ForegroundColor Magenta 'Starting CardGame'
dotnet run --project $ProjectDirectory
