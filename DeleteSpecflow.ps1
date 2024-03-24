# Define function to recursively delete files with the specified extension
function Remove-FilesRecursively {
    param (
        [string]$Path,
        [string]$Extension
    )

    # Get all files in the current directory with the specified extension
    $files = Get-ChildItem -Path $Path -File -Recurse -Filter "*$Extension"

    # Loop through each file and delete it
    foreach ($file in $files) {
        Write-Host "Deleting $($file.FullName)"
        Remove-Item -Path $file.FullName -Force
    }
}

# Call the function to remove .feature.cs files recursively in the current directory
Remove-FilesRecursively -Path "." -Extension ".feature.cs"