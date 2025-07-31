# PowerShell script to create GitHub issues from CSV file
# You'll need a GitHub Personal Access Token with 'repo' scope

param(
    [Parameter(Mandatory=$true)]
    [string]$GitHubToken,
    [string]$CsvPath = "user_stories_github_issues.csv",
    [string]$Owner = "nfahrig-insight",
    [string]$Repo = "PictureReview"
)

# Function to create a GitHub issue
function Create-GitHubIssue {
    param(
        [string]$Title,
        [string]$Body,
        [string[]]$Labels,
        [string]$Token,
        [string]$Owner,
        [string]$Repo
    )
    
    $uri = "https://api.github.com/repos/$Owner/$Repo/issues"
    
    $headers = @{
        "Authorization" = "Bearer $Token"
        "Accept" = "application/vnd.github.v3+json"
        "User-Agent" = "PowerShell-Script"
    }
    
    $body = @{
        "title" = $Title
        "body" = $Body
        "labels" = $Labels
    } | ConvertTo-Json
    
    try {
        $response = Invoke-RestMethod -Uri $uri -Method Post -Headers $headers -Body $body -ContentType "application/json"
        Write-Host "✅ Created issue: $Title (Issue #$($response.number))" -ForegroundColor Green
        return $response
    }
    catch {
        Write-Host "❌ Failed to create issue: $Title" -ForegroundColor Red
        Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
        return $null
    }
}

# Main script
Write-Host "Creating GitHub issues from CSV file..." -ForegroundColor Cyan

# Check if CSV file exists
if (-not (Test-Path $CsvPath)) {
    Write-Host "❌ CSV file not found: $CsvPath" -ForegroundColor Red
    exit 1
}

# Read CSV file
try {
    $userStories = Import-Csv $CsvPath
    Write-Host "📄 Found $($userStories.Count) user stories in CSV file" -ForegroundColor Yellow
}
catch {
    Write-Host "❌ Failed to read CSV file: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Create issues
$createdCount = 0
$failedCount = 0

foreach ($story in $userStories) {
    $title = $story.title
    $body = $story.body
    $labels = @()
    
    # Parse labels (they might be comma-separated)
    if ($story.labels) {
        $labels = $story.labels -split ',' | ForEach-Object { $_.Trim() }
    }
    
    Write-Host "Creating issue: $title" -ForegroundColor White
    
    $result = Create-GitHubIssue -Title $title -Body $body -Labels $labels -Token $GitHubToken -Owner $Owner -Repo $Repo
    
    if ($result) {
        $createdCount++
    } else {
        $failedCount++
    }
    
    # Add a small delay to avoid rate limiting
    Start-Sleep -Milliseconds 500
}

Write-Host "`n📊 Summary:" -ForegroundColor Cyan
Write-Host "✅ Created: $createdCount issues" -ForegroundColor Green
Write-Host "❌ Failed: $failedCount issues" -ForegroundColor Red

if ($createdCount -gt 0) {
    Write-Host "`n🎉 Issues created successfully! Check your repository at:" -ForegroundColor Green
    Write-Host "https://github.com/$Owner/$Repo/issues" -ForegroundColor Blue
}
