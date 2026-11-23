
import { Routes, Route, Navigate } from "react-router-dom";

import MainLayout from "../layouts/MainLayout";
import Login from "../pages/auth/Login";
import Register from "../pages/auth/Register";

// Home Page
function Home() {
  return <h1>Welcome to EasyBooking</h1>;
}

// Dashboards
function CustomerDashboard() {
  return <h1>Customer Dashboard</h1>;
}

function ProviderDashboard() {
  return <h1>Provider Dashboard</h1>;
}

function AdminDashboard() {
  return <h1>Admin Dashboard</h1>;
}

// Not Found
function NotFound() {
  return <h1>404 - Page Not Found</h1>;
}

// Get dashboard according to user role
function getDashboardPath(role) {
  switch (role) {
    case "Customer":
      return "/customer/dashboard";

    case "Provider":
      return "/provider/dashboard";

    case "Admin":
      return "/admin/dashboard";

    default:
      return "/login";
  }
}

// Protected Route
function ProtectedRoute({ allowedRoles, children }) {
  const token = localStorage.getItem("token");
  const role = localStorage.getItem("role");

  // User is not logged in
  if (!token) {
    return <Navigate to="/login" replace />;
  }

  // User role is not allowed
  if (!allowedRoles.includes(role)) {
    return <Navigate to={getDashboardPath(role)} replace />;
  }

  return children;
}

function AppRoutes() {
  return (
    <Routes>

      {/* Authentication Pages */}
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />

      {/* Main Website */}
      <Route element={<MainLayout />}>
        <Route path="/" element={<Home />} />
      </Route>

      {/* Customer Dashboard */}
      <Route
        path="/customer/dashboard"
        element={
          <ProtectedRoute allowedRoles={["Customer"]}>
            <CustomerDashboard />
          </ProtectedRoute>
        }
      />

      {/* Provider Dashboard */}
      <Route
        path="/provider/dashboard"
        element={
          <ProtectedRoute allowedRoles={["Provider"]}>
            <ProviderDashboard />
          </ProtectedRoute>
        }
      />

      {/* Admin Dashboard */}
      <Route
        path="/admin/dashboard"
        element={
          <ProtectedRoute allowedRoles={["Admin"]}>
            <AdminDashboard />
          </ProtectedRoute>
        }
      />

      {/* Not Found */}
      <Route path="*" element={<NotFound />} />

    </Routes>
  );
}

export default AppRoutes;
