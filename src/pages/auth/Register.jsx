
import { useState } from "react";
import { Link } from "react-router-dom";
import { UserRound, Store, Users } from "lucide-react";

import AuthLayout from "../../components/auth/AuthLayout";
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

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setError("");

    if (
      !formData.name ||
      !formData.email ||
      !formData.password ||
      !formData.confirmPassword
    ) {
      setError("Please fill in all required fields.");
      return;
    }

    if (
      !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)
    ) {
      setError("Please enter a valid email.");
      return;
    }

    if (formData.password.length < 8) {
      setError("Password must be at least 8 characters.");
      return;
    }

    if (formData.password !== formData.confirmPassword) {
      setError("Passwords do not match.");
      return;
    }

    if (role === "business" && !formData.businessName.trim()) {
      setError("Please enter your business name.");
      return;
    }

    // Backend integration will be added later
    console.log({ ...formData, role });
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
            className={role === "customer" ? "role-card selected" : "role-card"}
            onClick={() => setRole("customer")}
          >
            <UserRound size={21} />
            <span>Customer</span>
          </button>

          <button
            type="button"
            className={role === "business" ? "role-card selected" : "role-card"}
            onClick={() => setRole("business")}
          >
            <Store size={21} />
            <span>Business</span>
          </button>

          <button
            type="button"
            className={role === "staff" ? "role-card selected" : "role-card"}
            onClick={() => setRole("staff")}
          >
            <Users size={21} />
            <span>Staff</span>
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

        {error && <p className="auth-error">{error}</p>}

        <button type="submit" className="auth-submit">
          Create Account
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
