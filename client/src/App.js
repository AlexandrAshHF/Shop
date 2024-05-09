import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import AdminPage from "./pages/AdminPage";
import CatalogPage from "./pages/CatalogPage";
import CategoryPage from "./pages/CategoryPage";
import DefaultPage from "./pages/DefaultPage";
import ProductPage from "./pages/ProductPage";
import ProfilePage from "./pages/ProfilePage";
import AuthPage from "./pages/AuthPage"

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route index element={<DefaultPage />} />
        <Route path="/admin/:adminKey" element={<AdminPage />} />
        <Route path="/catalog/*" element={<CatalogPage />} />
        <Route path="/catalog/:categoryId/*" element={<CatalogPage />} />
        <Route path="/catalog/:categoryId/:typeId" element={<CatalogPage />} />
        <Route path="/category/*" element={<CategoryPage />} />
        <Route path="/category/:categoryId" element={<CategoryPage />} />
        <Route path="/product/:productId" element={<ProductPage />} />
        <Route path="/profile/:userId" element={<ProfilePage />} />
        <Route path="/account/:type" element={<AuthPage />} />
        <Route path="*" element={<DefaultPage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
