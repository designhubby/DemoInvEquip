Param([String]$toMultiLine)
$newlineDelimited = $toMultiLine -replace ';', "%0D%0A"
echo 'newlineDelimited:'
echo $newlineDelimited
Write-Host "##vso[task.setvariable variable=INPUT_NotifyEmails;isoutput=true]$newlineDelimited"
