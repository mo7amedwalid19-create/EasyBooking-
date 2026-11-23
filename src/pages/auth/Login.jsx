
import { useState } from "react";
import { Link } from "react-router-dom";
import { Eye, EyeOff } from "lucide-react";

import AuthLayout from "../../components/auth/AuthLayout";
import "./Auth.css";

function Login() {
  const [showPassword, setShowPassword] = useState(false);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    setError("");

    if (!email || !password) {
      setError("Please fill in all fields.");
      return;
    }

    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
      setError("Please enter a valid email.");
      return;
    }

    // Backend integration will be added later
    console.log({ email, password });
  };

  return (
    <AuthLayout
      title="Welcome Back!"
      subtitle="Enter your details to access your account."
    >
      <form onSubmit={handleSubmit} className="auth-form">

        <div className="form-group">
          <label>Email Address</label>
          <input
            type="email"
            placeholder="name@example.com"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>

        <div className="form-group">
          <div className="password-label">
            <label>Password</label>
            <Link to="/forgot-password">
              Forgot Password?
            </Link>
          </div>

          <div className="password-input">
            <input
              type={showPassword ? "text" : "password"}
              placeholder="Enter your password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />

            <button
              type="button"
              onClick={() => setShowPassword(!showPassword)}
            >
              {showPassword ? <EyeOff size={19} /> : <Eye size={19} />}
            </button>
          </div>
        </div>

        {error && <p className="auth-error">{error}</p>}

        <button type="submit" className="auth-submit">
          Sign In
        </button>

        <p className="auth-switch">
          Don't have an account?
          <Link to="/register"> Create Account</Link>
        </p>

      </form>
    </AuthLayout>
  );
}

export default Login;
