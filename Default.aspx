<%@ Page Title="Home" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ElectricityBillProject.Default" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <title>JBVNL Utility Portal</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        /* Reset & base */
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: #f5f7fa;
            margin: 0;
            padding: 0;
            color: #333;
        }

        a {
            color: inherit;
            text-decoration: none;
        }

        a:hover {
            text-decoration: underline;
        }

        /* Header */
        .site-header {
            background: linear-gradient(90deg, #0072ff, #00c6ff);
            padding: 15px 30px;
            position: sticky;
            top: 0;
            z-index: 999;
            box-shadow: 0 4px 10px rgba(0, 114, 255, 0.3);
            animation: slideDownFade 1s ease forwards;
        }

        .header-content {
            max-width: 1200px;
            margin: 0 auto;
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 20px;
        }

        .logo {
            height: 40px;
            animation: rotateLogo 4s linear infinite;
        }

        .site-title {
            font-size: 1.8rem;
            font-weight: 800;
            color: white;
            letter-spacing: 1.2px;
            user-select: none;
            flex-grow: 1;
        }

        .nav-links a {
            font-weight: 600;
            margin-left: 18px;
            color: white;
            padding: 8px 14px;
            border-radius: 8px;
            transition: background-color 0.3s ease;
            user-select: none;
        }

        .nav-links a:hover {
            background-color: rgba(255 255 255 / 0.3);
        }

        /* Main layout */
        .main-wrapper {
            display: flex;
            justify-content: center;
            gap: 40px;
            max-width: 1200px;
            margin: 40px auto 60px;
            padding: 0 15px;
            align-items: flex-start;
        }

        .main-content {
            flex: 3;
            min-width: 320px;
        }

        /* Banner */
        .banner {
            background: linear-gradient(135deg, #0072ff, #00c6ff);
            color: white;
            padding: 60px 30px;
            text-align: center;
            border-radius: 12px;
            box-shadow: 0 8px 30px rgba(0, 114, 255, 0.4);
            margin-bottom: 40px;
            user-select: none;
        }

        .banner h2 {
            font-size: 2.8rem;
            font-weight: 700;
            margin-bottom: 12px;
            letter-spacing: 1.5px;
            text-transform: uppercase;
        }

        .banner p {
            font-size: 1.3rem;
            max-width: 600px;
            margin: 0 auto;
            line-height: 1.5;
        }

        /* Carousel */
        .carousel-container {
            width: 100%;
            max-width: 900px;
            margin: 0 auto 40px;
            overflow: hidden;
            border-radius: 12px;
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.15);
        }

        .carousel {
            display: flex;
            width: 300%;
            animation: slideCarousel 15s infinite;
        }

        .carousel-image {
            width: 100%;
            object-fit: cover;
            height: 300px;
            flex-shrink: 0;
            user-select: none;
        }

        @keyframes slideCarousel {
            0%, 33% {
                transform: translateX(0%);
            }
            34%, 66% {
                transform: translateX(-100%);
            }
            67%, 100% {
                transform: translateX(-200%);
            }
        }

        /* Two-column cards */
        .two-col {
            display: flex;
            justify-content: center;
            gap: 30px;
            flex-wrap: wrap;
        }

        .card {
            background: white;
            border-radius: 14px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1);
            padding: 25px 35px;
            width: 320px;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            text-align: center;
            user-select: none;
        }

        .card:hover {
            transform: translateY(-8px);
            box-shadow: 0 20px 40px rgba(0, 114, 255, 0.35);
        }

        .card h3 {
            margin-bottom: 20px;
            color: #0072ff;
            font-size: 1.8rem;
            font-weight: 700;
        }

        .action-link {
            display: block;
            color: #0072ff;
            font-weight: 600;
            margin-bottom: 12px;
            text-decoration: none;
            font-size: 1.1rem;
            transition: color 0.3s ease;
        }

        .action-link:hover {
            color: #004a99;
            text-decoration: underline;
        }

        /* Sidebar Enhanced */
        .enhanced-sidebar {
            background: linear-gradient(135deg, #e0f7ff, #ffffff);
            border-radius: 16px;
            padding: 30px;
            max-width: 320px;
            margin: 40px 0 0 0;
            box-shadow:
                0 0 10px rgba(0, 114, 255, 0.2),
                0 8px 20px rgba(0, 0, 0, 0.08);
            animation: fadeSlideIn 1.2s ease forwards;
            position: sticky;
            top: 25px;
            align-self: flex-start;
        }

        .sidebar-card {
            background: white;
            border-radius: 14px;
            padding: 20px 25px;
            margin-bottom: 25px;
            box-shadow: 0 5px 15px rgba(0, 114, 255, 0.1);
            transition: box-shadow 0.3s ease;
        }

        .sidebar-card:hover {
            box-shadow: 0 10px 30px rgba(0, 114, 255, 0.25);
        }

        .enhanced-sidebar h3 {
            font-weight: 700;
            font-size: 1.5rem;
            color: #0072ff;
            margin-bottom: 15px;
            background: linear-gradient(90deg, #0072ff, #00c6ff);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }

        .energy-tips {
            list-style: none;
            padding: 0;
            margin: 0;
        }

        .energy-tips li {
            font-size: 1rem;
            color: #004a99;
            margin-bottom: 12px;
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .energy-tips li .icon {
            display: inline-block;
            font-size: 1.3rem;
            animation: pulse 2.5s infinite;
        }

        @keyframes pulse {
            0%, 100% { transform: scale(1); opacity: 1; }
            50% { transform: scale(1.2); opacity: 0.7; }
        }

        .sidebar-img {
            width: 100%;
            border-radius: 12px;
            box-shadow: 0 8px 20px rgba(0, 114, 255, 0.15);
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            cursor: pointer;
        }

        .sidebar-img:hover {
            transform: scale(1.05);
            box-shadow: 0 12px 28px rgba(0, 114, 255, 0.3);
        }

        .mini-article-card p,
        .contact-card p {
            font-size: 1rem;
            color: #333;
            line-height: 1.5;
        }

        .contact-card a {
            color: #0072ff;
            text-decoration: none;
            font-weight: 600;
            transition: color 0.3s ease;
        }

        .contact-card a:hover {
            color: #004a99;
            text-decoration: underline;
        }

        /* Animations */
        @keyframes fadeSlideIn {
            0% {
                opacity: 0;
                transform: translateX(-40px);
            }
            100% {
                opacity: 1;
                transform: translateX(0);
            }
        }

        @keyframes slideDownFade {
            0% {
                opacity: 0;
                transform: translateY(-30px);
            }
            100% {
                opacity: 1;
                transform: translateY(0);
            }
        }

        @keyframes rotateLogo {
            from {
                transform: rotate(0deg);
            }
            to {
                transform: rotate(360deg);
            }
        }

        /* Fade-in helper */
        .fade-in {
            opacity: 0;
            animation-fill-mode: forwards;
            animation-timing-function: ease;
            animation-name: fadeInUp;
            animation-duration: 1.2s;
        }

        @keyframes fadeInUp {
            0% {
                opacity: 0;
                transform: translateY(30px);
            }
            100% {
                opacity: 1;
                transform: translateY(0);
            }
        }

        /* Responsive */
        @media (max-width: 900px) {
            .main-wrapper {
                flex-direction: column;
                gap: 30px;
                margin: 30px 15px 60px;
            }
            .main-content,
            .enhanced-sidebar {
                max-width: 100%;
                margin: 0 auto;
                position: static;
            }
        }
        .site-footer {
    background: linear-gradient(90deg, #0072ff, #00c6ff);
    color: #fff;
    padding: 20px 30px;
    text-align: center;
    font-size: 1rem;
    font-weight: 500;
    border-radius: 0 0 12px 12px;
    box-shadow: 0 -6px 15px rgba(0, 114, 255, 0.4);
    user-select: none;
    margin-top: 60px;
}

.site-footer a {
    color: #ffd966;
    font-weight: 600;
    text-decoration: none;
    transition: color 0.3s ease;
}

.site-footer a:hover,
.site-footer a:focus {
    color: #fff;
    text-decoration: underline;
}

.footer-content {
    max-width: 1200px;
    margin: 0 auto;
}

@media (max-width: 600px) {
    .site-footer {
        font-size: 0.9rem;
        padding: 15px 20px;
    }
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Animated Header -->
        <header class="site-header">
            <div class="header-content">
                <img src="Styles/jbvnl_logo.jpg" alt="Logo" class="logo" />
                <h1 class="site-title">JBVNL Utility Portal</h1>
                <nav class="nav-links">
                    <asp:HyperLink runat="server" NavigateUrl="UserLogin.aspx" Text="User Login" />
                    <asp:HyperLink runat="server" NavigateUrl="UserRegistration.aspx" Text="Register" />
                    <asp:HyperLink runat="server" NavigateUrl="AdminLogin.aspx" Text="Admin" />
                </nav>
            </div>
        </header>

        <div class="main-wrapper">
            <!-- Main Content Section -->
            <section class="main-content">
                <!-- Banner Section -->
                <div class="banner fade-in">
                    <h2>Utility Bill Payment & Services</h2>
                    <p>Fast, simple and secure. Pay bills, view history, and manage connections.</p>
                </div>

                <!-- Image Carousel -->
                <div class="carousel-container fade-in">
                    <div class="carousel" id="carousel">
                        <img src="https://images.unsplash.com/photo-1504384308090-c894fdcc538d?auto=format&fit=crop&w=1200&q=80" class="carousel-image" alt="Electricity Image 1" />
                        <img src="https://images.unsplash.com/photo-1509475826633-fed577a2c71b?auto=format&fit=crop&w=1200&q=80" class="carousel-image" alt="Electricity Image 2" />
                        <img src="https://images.unsplash.com/photo-1501594907352-04cda38ebc29?auto=format&fit=crop&w=1200&q=80" class="carousel-image" alt="Electricity Image 3" />
                    </div>
                </div>

                <!-- Action Cards -->
                <div class="two-col fade-in">
                    <div class="card">
                        <h3>Quick Actions</h3>
                        <asp:HyperLink CssClass="action-link" NavigateUrl="UserLogin.aspx" Text="User Login" runat="server" />
                        <asp:HyperLink CssClass="action-link" NavigateUrl="UserRegistration.aspx" Text="New User? Register" runat="server" />
                    </div>

                    <div class="card">
                        <h3>Admin</h3>
                        <asp:HyperLink CssClass="action-link" NavigateUrl="AdminLogin.aspx" Text="Admin Login" runat="server" />
                    </div>
                </div>
            </section>

            <!-- Sidebar Section -->
            <aside class="sidebar enhanced-sidebar fade-in">
                <section class="sidebar-card energy-tips-card">
                    <h3>🌿 Save Energy Tips</h3>
                    <ul class="energy-tips">
                        <li><span class="icon">💡</span> Turn off lights when not in use.</li>
                        <li><span class="icon">💡</span> Use LED bulbs for efficiency.</li>
                        <li><span class="icon">🔌</span> Unplug devices when not charging.</li>
                        <li><span class="icon">☀️</span> Use natural light during the day.</li>
                    </ul>
                </section>

                <section class="sidebar-card image-card">
                    <h3>📸 Energy Awareness</h3>
                    <img src="https://images.unsplash.com/photo-1581091870622-1e7b9c5c9f3e?auto=format&fit=crop&w=400&q=80" alt="Energy Awareness" class="sidebar-img" />
                </section>

                <section class="sidebar-card mini-article-card">
                    <h3>📚 Did You Know?</h3>
                    <p>
                        India’s per capita electricity consumption is growing rapidly. Small changes in daily habits can lead to big savings and a greener planet.
                    </p>
                </section>

                <section class="sidebar-card contact-card">
                    <h3>📞 Contact Us</h3>
                    <p>
                        JBVNL Support<br />
                        📧 <a href="mailto:support@jbvnl.in">support@jbvnl.in</a><br />
                        ☎️ <a href="tel:18001234567">1800-123-4567</a><br />
                        🕒 Mon–Sat: 9am–6pm
                    </p>
                </section>
            </aside>
        </div>
        <footer class="site-footer fade-in">
    <div class="footer-content">
        <p>© 2025 JBVNL. Design by <a href="#" target="_blank" rel="noopener">Sidrah Innovations</a> &nbsp;|&nbsp; Developed by <a href="#" target="_blank" rel="noopener">Shami Gaffar</a></p>
    </div>
</footer>

    </form>

    <script>
        
    </script>
</body>
</html>
