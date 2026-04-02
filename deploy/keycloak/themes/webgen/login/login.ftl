<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <title>${realm.displayName!realm.name}</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    
    <link rel="stylesheet" href="${url.resourcesPath}/css/styles.css" />
</head>
<body>
    <div class="container">
        <div class="card">
            
            <!-- Logo -->
            <div class="logo">
                <img src="${url.resourcesPath}/img/logo.png" alt="WebGen Logo" />
            </div>

            <!-- Title -->
            <h1 class="title">Welcome back</h1>
            <p class="subtitle">Sign in to continue to WebGen</p>

            <!-- Error -->
            <#if message?has_content>
                <div class="error">${message.summary}</div>
            </#if>

            <!-- Login Form -->
            <form id="kc-form-login" action="${url.loginAction}" method="post">

                <div class="input-group">
                    <label for="username">Username or Email</label>
                    <input 
                        id="username"
                        name="username"
                        type="text"
                        value="${login.username!}"
                        placeholder="Enter your username"
                        required
                        autofocus
                    />
                </div>

                <div class="input-group">
                    <label for="password">Password</label>
                    <input 
                        id="password"
                        name="password"
                        type="password"
                        placeholder="••••••••"
                        required
                    />
                </div>

                <div class="actions">
                    <button type="submit" class="btn-primary">Sign In</button>
                </div>

                <div class="footer-links">
                    <#if realm.resetPasswordAllowed>
                        <a href="${url.loginResetCredentialsUrl}">Forgot password?</a>
                    </#if>
                </div>

            </form>
        </div>
    </div>
</body>
</html>