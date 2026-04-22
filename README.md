# Travel and Tourism Booking System

A modern ASP.NET Core MVC application for a Travel & Tourism booking system with professional authentication, user management, and booking capabilities using **User Secrets** for secure credential management.

**Version**: 1.0.0 | **Built with**: .NET 10, C# 14.0, Bootstrap 5 | **Last Updated**: December 2024

---

## 🚀 Quick Start (5 Minutes)

```bash
# 1. Clone Repository
git clone https://github.com/Shady-Mo/MVC-Project.git
cd MVCProject

# 2. Initialize User Secrets
dotnet user-secrets init

# 3. Set Credentials (Replace with your actual values)
dotnet user-secrets set "ConnectionStrings:mvccon" "data source=YOUR_SERVER; initial catalog=YOUR_DB; User ID=YOUR_USER; Password=YOUR_PASSWORD; trust server certificate=true;"
dotnet user-secrets set "Authentication:Google:ClientId" "YOUR_CLIENT_ID"
dotnet user-secrets set "Authentication:Google:ClientSecret" "YOUR_CLIENT_SECRET"
dotnet user-secrets set "EmailSettings:From" "your-email@gmail.com"
dotnet user-secrets set "EmailSettings:Username" "your-email@gmail.com"
dotnet user-secrets set "EmailSettings:Password" "YOUR_APP_PASSWORD"

# 4. Create Database
dotnet ef database update

# 5. Run Application
dotnet run
```

Visit: `https://localhost:7000`

---

## ✨ Features

### 🔐 Authentication & Authorization
- ✅ **Email/Password Login** - Secure user authentication with validation
- ✅ **User Registration** - Complete signup with duplicate checking
- ✅ **Google OAuth 2.0** - One-click Sign-In/Sign-Up
- ✅ **Password Recovery** - Email-based token reset
- ✅ **Account Lockout** - 5 failed attempts → 30 seconds lockout
- ✅ **Role-Based Access** - Customer and Admin roles
- ✅ **Remember Me** - Persistent login sessions

### 📧 Email & Communication
- SMTP Email Service with Gmail integration
- Password reset with secure tokens (24-hour expiry)
- Email verification and confirmations

### 👤 User Management
- Complete user profiles (name, email, phone, address)
- Secure password storage with PBKDF2 + salt
- External login integration (Google)
- User Secrets for credential management

### 🎨 UI/UX
- Professional branding (Orange #fd7e14, Teal #20c997)
- Responsive design (360px to 1400px+)
- WCAG accessibility compliance
- Bootstrap 5 framework
- Font Awesome 6 icons
- Smooth animations and transitions

---

## 🛠️ Technology Stack

| Layer | Technologies |
|-------|--------------|
| **Frontend** | Bootstrap 5, jQuery, Font Awesome 6 |
| **Backend** | ASP.NET Core (.NET 10), C# 14.0 |
| **Database** | SQL Server, Entity Framework Core |
| **Auth** | ASP.NET Core Identity, Google OAuth 2.0 |
| **Email** | SMTP (Gmail) |
| **Mapping** | Mapster 10.0.7 |
| **Secrets** | User Secrets (Local), Environment Variables (Production) |

---

## 📋 Prerequisites

- **.NET SDK 10.0+** - [Download](https://dotnet.microsoft.com/download)
- **Visual Studio 2022+** or VS Code
- **SQL Server** - LocalDB or Express
- **Gmail Account** - For email service
- **Google OAuth Credentials** - For Sign-In

---

## 🔑 User Secrets Setup

### Why User Secrets?

✅ **Secure** - Credentials stored locally, not in files  
✅ **Protected** - Never committed to Git  
✅ **Per-Developer** - Each developer has own secrets  
✅ **Best Practice** - Recommended by Microsoft  

### Initialize (First Time Only)

```bash
cd MVCProject
dotnet user-secrets init
```

### Set All Required Secrets

#### Database Connection
```bash
dotnet user-secrets set "ConnectionStrings:mvccon" "data source=YOUR_SERVER; initial catalog=YOUR_DB; User ID=YOUR_USER; Password=YOUR_PASSWORD; trust server certificate=true;"
```

**Examples:**
```
LocalDB: data source=(LocalDB)\mssqllocaldb; initial catalog=TravelMonsterDb; Integrated Security=true;
Remote: data source=yourserver.com; initial catalog=YourDb; User ID=user; Password=pwd; trust server certificate=true;
```

#### Google OAuth
```bash
dotnet user-secrets set "Authentication:Google:ClientId" "YOUR_CLIENT_ID"
dotnet user-secrets set "Authentication:Google:ClientSecret" "YOUR_CLIENT_SECRET"
```

#### Gmail SMTP
```bash
dotnet user-secrets set "EmailSettings:From" "your-email@gmail.com"
dotnet user-secrets set "EmailSettings:Username" "your-email@gmail.com"
dotnet user-secrets set "EmailSettings:Password" "YOUR_APP_PASSWORD"
```

### Verify All Secrets Are Set

```bash
dotnet user-secrets list
```

### Storage Location

- **Windows**: `%APPDATA%\Microsoft\UserSecrets\<id>\secrets.json`
- **Linux**: `~/.microsoft/usersecrets/<id>/secrets.json`
- **macOS**: `~/.microsoft/usersecrets/<id>/secrets.json`

### Common Commands

| Command | Purpose |
|---------|---------|
| `dotnet user-secrets init` | Initialize secrets storage |
| `dotnet user-secrets list` | Show all secrets (values hidden) |
| `dotnet user-secrets set "key" "value"` | Set a secret |
| `dotnet user-secrets remove "key"` | Delete a secret |
| `dotnet user-secrets clear` | Delete all secrets |

---

## 📚 Getting Your Credentials

### 🗄️ Database Connection String

**SQL Server LocalDB:**
```
data source=(LocalDB)\mssqllocaldb; initial catalog=TravelMonsterDb; Integrated Security=true;
```

**Remote SQL Server:**
```
data source=YOUR_SERVER; initial catalog=YOUR_DATABASE; User ID=YOUR_USER; Password=YOUR_PASSWORD; trust server certificate=true;
```

### 🔐 Google OAuth Credentials

1. Go to [Google Cloud Console](https://console.cloud.google.com/)
2. Create new project
3. Enable **Google+ API**
4. Create OAuth 2.0 **Web Application** credentials
5. Add authorized redirect URIs:
   - `https://localhost:7000/signin-google`
   - `https://yourdomain.com/signin-google`
6. Copy **Client ID** and **Client Secret**

### 📧 Gmail App Password

1. Go to [myaccount.google.com](https://myaccount.google.com/security)
2. Enable **2-Factor Authentication**
3. Go to **App passwords**
4. Select **Mail** and **Windows Computer**
5. Copy the **16-character app password**
6. Use as `EmailSettings:Password`

---

## 📁 Project Structure

```
MVCProject/
├── Controllers/
│   ├── AccountController.cs          # Authentication logic
│   └── HomeController.cs
├── Models/
│   ├── AppUser.cs                    # Identity model
│   ├── Booking.cs                    # Booking model
│   └── ...
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml              # Login page
│   │   ├── Register.cshtml           # Registration
│   │   ├── ExternalLoginConfirmation.cshtml
│   │   ├── VerifyEmail.cshtml        # Email verification
│   │   ├── EmailSent.cshtml          # Confirmation
│   │   └── ForgetPassword.cshtml     # Password reset
│   └── Shared/
│       ├── _Layout.cshtml
│       └── _LoginLayout.cshtml
├── Services/
│   └── EmailService/
│       ├── IEmailService.cs
│       └── EmailService.cs
├── ViewModels/
│   └── AccountViewModels/
│       ├── LoginViewModel.cs
│       ├── RegisterViewModel.cs
│       └── ...
├── Data/
│   └── AppDbContext.cs
├── appsettings.json                  # App configuration
├── appsettings.example.json          # Example template
├── Program.cs                        # DI & Configuration
└── README.md                         # This file
```

---

## 🔄 Authentication Flows

### 1️⃣ Standard Email/Password Login

```
User enters email & password
    ↓
Validate input
    ├─ Invalid → Show error
    ↓
Check if user exists
    ├─ Not found → Show error
    ↓
Verify password
    ├─ Wrong → Check lockout (5 attempts = 30 sec lock)
    ↓
Sign in & create session
    ↓
Redirect to Home
```

### 2️⃣ Google OAuth Sign-In

```
Click "Sign in with Google"
    ↓
Redirect to Google OAuth
    ↓
User authenticates with Google
    ↓
Return with authorization code
    ├─ New user → ExternalLoginConfirmation → Complete profile → Create account
    ├─ Existing user → Sign in immediately
    └─ Error → Redirect to Login
```

### 3️⃣ Password Recovery

```
Click "Forgot Password"
    ↓
Enter email (VerifyEmail)
    ↓
Check if email exists
    ├─ Not found → Show error
    ↓
Generate reset token (24-hour expiry)
    ↓
Send email with reset link
    ↓
Show confirmation (EmailSent)
    ↓
User opens email & clicks link
    ↓
Enter new password (ForgetPassword)
    ↓
Validate & update password
    ↓
Success → Redirect to Login
```

---

## 📝 API Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/Account/Login` | Login page |
| POST | `/Account/Login` | Process login |
| GET | `/Account/Register` | Registration page |
| POST | `/Account/Register` | Process registration |
| POST | `/Account/ExternalLogin` | Initiate Google OAuth |
| GET | `/Account/ExternalLoginCallback` | Handle OAuth callback |
| POST | `/Account/ExternalLoginConfirmation` | Complete Google profile |
| GET | `/Account/VerifyEmail` | Email verification page |
| POST | `/Account/VerifyEmail` | Send password reset email |
| GET | `/Account/EmailSent` | Confirmation message |
| GET | `/Account/ForgetPassword` | Password reset page |
| POST | `/Account/ForgetPassword` | Process password reset |
| POST | `/Account/Logout` | Logout user |

---

## 🚀 Database Setup

### Apply Migrations

```bash
# Using CLI
dotnet ef database update

# Or in Visual Studio Package Manager Console
Update-Database
```

### Database Schema (Key Tables)

**AppUser** (ASP.NET Identity Extended)
- Id, UserName, Email
- PhoneNumber, FullName, Address
- PasswordHash, SecurityStamp
- LockoutEnabled, AccessFailedCount
- ... (other Identity fields)

---

## 🚀 Deployment

### Azure App Service

#### 1. Create Resources

```bash
az login
az group create --name travel-monster --location eastus
az appservice plan create --name travel-monster-plan --resource-group travel-monster --sku B1
az webapp create --name travel-monster-app --resource-group travel-monster --plan travel-monster-plan
```

#### 2. Configure Application Settings

**Do NOT use User Secrets in production!** Use environment variables instead:

```bash
az webapp config appsettings set \
  --name travel-monster-app \
  --resource-group travel-monster \
  --settings \
  ConnectionStrings__mvccon="your_production_connection_string" \
  Authentication__Google__ClientId="your_prod_client_id" \
  Authentication__Google__ClientSecret="your_prod_client_secret" \
  EmailSettings__Username="your-email@gmail.com" \
  EmailSettings__Password="your_app_password"
```

#### 3. Deploy from GitHub

```bash
az webapp deployment github-actions add \
  --repo-url https://github.com/Shady-Mo/MVC-Project \
  --branch main \
  --resource-group travel-monster \
  --name travel-monster-app
```

### Environment Variables Pattern

**In Production, use this pattern for secrets:**

```
ConnectionStrings__mvccon = [your_connection_string]
Authentication__Google__ClientId = [your_client_id]
Authentication__Google__ClientSecret = [your_client_secret]
EmailSettings__Username = [your_email]
EmailSettings__Password = [your_app_password]
```

---

## 🔐 Security Best Practices

### ✅ Implemented in Project

- ✅ Passwords hashed with **PBKDF2 + salt**
- ✅ Account lockout after **5 failed attempts** (30 seconds)
- ✅ **User Secrets** for local development
- ✅ **HTTPS** enforced in production
- ✅ **CSRF** protection on all forms
- ✅ **SQL injection** prevention (EF Core parameterized queries)
- ✅ **XSS** protection (Razor HTML encoding)
- ✅ **Secure tokens** for password reset (24-hour expiry, one-time use)
- ✅ **Google OAuth** server-side validation
- ✅ **HSTS** headers configured

### 🔒 Credential Management Strategy

| Environment | Method | Use Case |
|-------------|--------|----------|
| **Local Development** | User Secrets | Safe, per-developer |
| **Server/CI-CD** | Environment Variables | Simple, secure |
| **Production Cloud** | Azure Key Vault | Enterprise-grade |

### ❌ Never Do This

```csharp
// ❌ WRONG - Hardcoded credentials
var password = "mySecretPassword123";

// ✅ RIGHT - Use configuration
var password = configuration["EmailSettings:Password"];
```

### 🔐 Before Production Deployment

- [ ] All secrets in environment variables (not files)
- [ ] HTTPS enforced and certificates valid
- [ ] HSTS headers configured
- [ ] CORS policy restrictive
- [ ] Password requirements enforced
- [ ] Account lockout configured
- [ ] All forms have CSRF protection
- [ ] Database connection secure
- [ ] Email credentials in environment
- [ ] No secrets in source code
- [ ] No debug mode in production
- [ ] Security headers configured

---

## 🧪 Testing

### Test Account
Create via registration page or database

### Test Google Sign-In
Use your personal Google account

### Test Password Reset
1. Click "Forgot Password"
2. Enter email address
3. Check email for reset link
4. Follow link and set new password

---

## 🐛 Troubleshooting

### Email Not Sending
✓ Verify Gmail app password (16 characters, not regular password)  
✓ Check 2-Factor Authentication enabled on Gmail  
✓ Verify SMTP settings: smtp.gmail.com:587  
✓ Check firewall allows port 587  
✓ Review application logs for errors  

### Google Sign-In Not Working
✓ Verify Client ID and Secret are correct  
✓ Check redirect URIs exactly match Google Console  
✓ Ensure Google+ API is enabled  
✓ Check browser console for CORS/auth errors  

### Database Connection Failed
✓ Verify connection string format  
✓ Check SQL Server is running  
✓ Verify database exists  
✓ Check user permissions on database  

### User Secrets Not Working
✓ Run `dotnet user-secrets list` to verify all secrets set  
✓ Ensure running from MVCProject directory  
✓ Restart application after setting secrets  
✓ Check secrets file exists in OS storage location  

---

## 💡 Common Tasks

### Add New Secret
```bash
dotnet user-secrets set "Section:Key" "value"
```

### List All Secrets (No Values)
```bash
dotnet user-secrets list
```

### Remove Secret
```bash
dotnet user-secrets remove "Section:Key"
```

### Clear All Secrets
```bash
dotnet user-secrets clear
```

### Search Specific Key
```bash
dotnet user-secrets list | findstr "KeyName"
```

### Delete All and Start Over
```bash
dotnet user-secrets clear
dotnet user-secrets init
# Re-add all secrets...
```

---

## 📋 Setup Verification Checklist

- [ ] .NET 10 SDK installed
- [ ] Visual Studio 2022+ or VS Code
- [ ] SQL Server running
- [ ] Repository cloned
- [ ] User Secrets initialized
- [ ] All 7 secrets set correctly
- [ ] Database migrations applied
- [ ] Application runs without errors
- [ ] Login page accessible
- [ ] Can register new user
- [ ] Email sending works
- [ ] Google Sign-In configured
- [ ] Password reset email works

---

## 🤝 Contributing

1. Fork repository
2. Create feature branch (`git checkout -b feature/amazing-feature`)
3. Commit changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open Pull Request

**Important**: Never commit credentials or secrets to GitHub!

---

## 📄 License

MIT License - see [LICENSE](LICENSE) for details

---

## 👨‍💻 Author

**Shady Mohamed**  
- GitHub: [@Shady-Mo](https://github.com/Shady-Mo)
- Email: shady.mohamed7899@gmail.com

---

## 🙏 Acknowledgments

- ASP.NET Core Identity team
- Google OAuth documentation
- Bootstrap 5 framework
- Font Awesome icons
- Entity Framework Core
- Microsoft .NET team

---

## 📞 Support

- 📖 Check this README
- 🐛 Report bugs on GitHub Issues
- 💬 Include error messages and steps to reproduce
- ❓ Check existing issues before creating new ones

---

## ✅ Project Status

| Component | Status | Notes |
|-----------|--------|-------|
| Authentication | ✅ Complete | Email + Google OAuth |
| Email Service | ✅ Complete | Gmail SMTP |
| Password Recovery | ✅ Complete | Token-based reset |
| User Secrets | ✅ Complete | Secure local credentials |
| Database | ✅ Complete | SQL Server + EF Core |
| UI/UX | ✅ Complete | Responsive design |
| Security | ✅ Complete | Best practices |
| Documentation | ✅ Complete | Comprehensive |

---

**⭐ If you find this project helpful, please star it on GitHub!**

**Ready to start?** Follow the Quick Start at the top! 🚀
