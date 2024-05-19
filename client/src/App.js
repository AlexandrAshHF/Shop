import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import AdminPage from "./pages/AdminPage";
import CatalogPage from "./pages/CatalogPage";
import CategoryPage from "./pages/CategoryPage";
import DefaultPage from "./pages/DefaultPage";
import ProductPage from "./pages/ProductPage";
import ProfilePage from "./pages/ProfilePage";
import AuthPage from "./pages/AuthPage";
import BasketPage from "./pages/BasketPage"
import FullOrderPage from "./pages/FullOrderPage";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route index element={<CategoryPage />} />
        <Route path="/admin/:adminKey/:page" element={<AdminPage />} />
        <Route path="/admin/:adminKey" element={<AdminPage />} />
        <Route path="/catalog/*" element={<CatalogPage />} />
        <Route path="/catalog/:categoryId/*" element={<CatalogPage />} />
        <Route path="/catalog/:categoryId/:typeId" element={<CatalogPage />} />
        <Route path="/category/*" element={<CategoryPage />} />
        <Route path="/category/:categoryId" element={<CategoryPage />} />
        <Route path="/product/:productId" element={<ProductPage />} />
        <Route path="/profile" element={<ProfilePage />} />
        <Route path="/account/:type" element={<AuthPage />} />
        <Route path="/basket" element={<BasketPage/>}/>
        <Route path="/order/:orderId" element={<FullOrderPage/>}/>
        <Route path="*" element={<CategoryPage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
