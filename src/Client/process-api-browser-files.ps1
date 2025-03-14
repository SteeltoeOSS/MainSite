Get-ChildItem -Filter docfx-api*.json -File -Name | ForEach-Object {
    Write-Host "Running ""docfx build $_""..."
    docfx build $_
}
