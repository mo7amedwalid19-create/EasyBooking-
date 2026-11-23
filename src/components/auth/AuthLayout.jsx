
import "./AuthLayout.css";

function AuthLayout({ children, title, subtitle }) {
  return (
    <div className="auth-container">

      {/* Left Side - Form */}
      <div className="auth-form-side">

        <div className="auth-brand">
          <span className="brand-icon">E</span>
          <span>EasyBooking</span>
        </div>

        <div className="auth-content">
          <h1>{title}</h1>
          <p className="auth-subtitle">{subtitle}</p>

          {children}
        </div>

        <p className="auth-footer">
          © 2026 EasyBooking. All rights reserved.
        </p>

      </div>

      {/* Right Side - Illustration */}
      <div className="auth-visual-side">

        <div className="visual-content">
          <div className="visual-icon">
            <span>✦</span>
          </div>

          <h2>Make Time for What Matters</h2>

          <p>
            Discover services, manage appointments,
            and make your everyday booking easier.
          </p>

          <div className="visual-dots">
            <span className="active"></span>
            <span></span>
            <span></span>
          </div>
        </div>

        <div className="visual-decoration decoration-one"></div>
        <div className="visual-decoration decoration-two"></div>

      </div>

    </div>
  );
}

export default AuthLayout;
