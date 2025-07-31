# Instructions for Creating GitHub Issues from User Stories

## Option 1: Using the PowerShell Script (Recommended)

1. **Create a GitHub Personal Access Token:**
   - Go to GitHub.com → Settings → Developer settings → Personal access tokens → Tokens (classic)
   - Click "Generate new token (classic)"
   - Give it a name like "PhotoAnalyzer Issues"
   - Select the "repo" scope (full control of private repositories)
   - Copy the token (you won't see it again!)

2. **Run the PowerShell script:**
   ```powershell
   .\create-github-issues.ps1 -GitHubToken "your_token_here"
   ```

## Option 2: Manual Creation

You can manually create issues by going to:
https://github.com/nfahrig-insight/PictureReview/issues/new

Copy the title and body from each row in the CSV file.

## Option 3: Install GitHub CLI (Future Use)

Install GitHub CLI for easier automation:
```powershell
winget install GitHub.cli
```

Then you can use commands like:
```bash
gh issue create --title "Issue Title" --body "Issue Description" --label "backend"
```

## User Stories Summary

The CSV file contains 19 user stories covering:
- **Backend features:** OneDrive integration, photo scanning, similarity detection, blurriness detection
- **Frontend features:** Web interface, photo preview, metadata display
- **API features:** REST endpoints for photo management
- **UX features:** Daily limits, user actions (Keep/Archive/Delete)

## Labels Used:
- `backend` - Server-side functionality
- `frontend` - User interface components  
- `api` - REST API endpoints
- `UX` - User experience features
