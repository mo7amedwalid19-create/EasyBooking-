
import { Routes, Route } from "react-router-dom";
import MainLayout from "../layouts/MainLayout";
import Login from "../pages/auth/Login";
import Register from "../pages/auth/Register";

function Home() {
  return <h1>Welcome to EasyBooking</h1>;
}


function CustomerDashboard() {
  return <h1>Customer Dashboard</h1>;
}

function ProviderDashboard() {
  return <h1>Provider Dashboard</h1>;
}

function AdminDashboard() {
  return <h1>Admin Dashboard</h1>;
}

function NotFound() {
  return <h1>404 - Page Not Found</h1>;
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

      <Route path="/customer/dashboard" element={<CustomerDashboard />} />
      <Route path="/provider/dashboard" element={<ProviderDashboard />} />
      <Route path="/admin/dashboard" element={<AdminDashboard />} />

      <Route path="*" element={<NotFound />} />

    </Routes>
  );
}

export default AppRoutes;
