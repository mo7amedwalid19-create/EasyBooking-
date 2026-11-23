
import { useState } from "react";
import { Link } from "react-router-dom";
import { UserRound, Store } from "lucide-react";

import AuthLayout from "../../components/auth/AuthLayout";
import { register } from "../../services/authService";
import "./Auth.css";

function Register() {
  const [role, setRole] = useState("customer");

  const [formData, setFormData] = useState({
    name: "",
    email: "",
    phone: "",
    businessName: "",
    password: "",
    confirmPassword: "",
  });

  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  const [loading, setLoading] = useState(false);

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    setError("");
    setSuccess("");

    // Validate required fields
    if (
      !formData.name.trim() ||
      !formData.email.trim() ||
      !formData.password ||
      !formData.confirmPassword
    ) {
      setError("Please fill in all required fields.");
      return;
    }

    // Validate email
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email.trim())) {
      setError("Please enter a valid email.");
      return;
    }

    // Validate password
    if (formData.password.length < 8) {
      setError("Password must be at least 8 characters.");
      return;
    }

    // Confirm password
    if (formData.password !== formData.confirmPassword) {
      setError("Passwords do not match.");
      return;
    }

    // Validate business name
    if (role === "business" && !formData.businessName.trim()) {
      setError("Please enter your business name.");
      return;
    }

    // Convert frontend role to backend role
    const apiRole = role === "customer" ? "Customer" : "Provider";

    // Backend RegisterRequest fields
    const userData = {
      fullName: formData.name.trim(),
      email: formData.email.trim(),
      password: formData.password,
      role: apiRole,
    };

    try {
      setLoading(true);

      const result = await register(userData);

      setSuccess(result.message || "Account created successfully!");

      // Clear form after successful registration
      setFormData({
        name: "",
        email: "",
        phone: "",
        businessName: "",
        password: "",
        confirmPassword: "",
      });

    } catch (err) {
      setError(err.message || "Registration failed. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <AuthLayout
      title="Create Account"
      subtitle="Join EasyBooking and get started today."
    >
      <form onSubmit={handleSubmit} className="auth-form">

        <label className="section-label">
          I want to join as
        </label>

        <div className="role-options">

          <button
            type="button"
            className={
              role === "customer"
                ? "role-card selected"
                : "role-card"
            }
            onClick={() => {
              setRole("customer");
              setError("");
              setSuccess("");
            }}
          >
            <UserRound size={21} />
            <span>Customer</span>
          </button>

          <button
            type="button"
            className={
              role === "business"
                ? "role-card selected"
                : "role-card"
            }
            onClick={() => {
              setRole("business");
              setError("");
              setSuccess("");
            }}
          >
            <Store size={21} />
            <span>Business</span>
          </button>

        </div>

        {role === "business" && (
          <div className="form-group">
            <label>Business Name</label>
            <input
              name="businessName"
              placeholder="Enter business name"
              value={formData.businessName}
              onChange={handleChange}
            />
          </div>
        )}

        <div className="form-group">
          <label>Full Name</label>
          <input
            type="text"
            name="name"
            placeholder="Enter your full name"
            value={formData.name}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>Email Address</label>
          <input
            type="email"
            name="email"
            placeholder="name@example.com"
            value={formData.email}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>Phone Number</label>
          <input
            type="tel"
            name="phone"
            placeholder="Enter phone number"
            value={formData.phone}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>Password</label>
          <input
            type="password"
            name="password"
            placeholder="At least 8 characters"
            value={formData.password}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>Confirm Password</label>
          <input
            type="password"
            name="confirmPassword"
            placeholder="Confirm your password"
            value={formData.confirmPassword}
            onChange={handleChange}
          />
        </div>

        {error && (
          <p className="auth-error">
            {error}
          </p>
        )}

        {success && (
          <p className="auth-success">
            {success}
            <br />
            <Link to="/login">Go to Login</Link>
          </p>
        )}

        <button
          type="submit"
          className="auth-submit"
          disabled={loading}
        >
          {loading ? "Creating Account..." : "Create Account"}
        </button>

        <p className="auth-switch">
          Already have an account?
          <Link to="/login"> Sign In</Link>
        </p>

      </form>
    </AuthLayout>
  );
}

export default Register;
