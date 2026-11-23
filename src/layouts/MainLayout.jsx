
import { Outlet, Link } from "react-router-dom";

function MainLayout() {
  return (
    <div className="app-layout">

      {/* Navbar */}
      <header className="navbar">
        <h2>EasyBooking</h2>

        <nav>
          <Link to="/">Home</Link>
          <Link to="/login">Login</Link>
          <Link to="/register">Register</Link>
        </nav>
      </header>

      {/* Main Content */}
      <main className="main-content">
        <Outlet />
      </main>

      {/* Footer */}
      <footer className="footer">
        <p>© 2026 EasyBooking. All rights reserved.</p>
      </footer>

    </div>
  );
}

export default MainLayout;
