"use client";

import { Layout, theme, Space } from "antd";
import { useAuth } from "@/context/auth/AuthContext";
import SideMenu from "./side-menu";
import ThemeSwitcher from "../theme/theme-switcher";
import LanguageSwitcher from "../localization/language-switcher";
import { usePathname } from "next/navigation";
import { useState } from "react";
import UserProfile from "../user/user-profile";

const { Content, Sider, Header, Footer } = Layout;

const MainLayout = ({ children }: { children: React.ReactNode }) => {
  const { isAuth } = useAuth();
  const pathname = usePathname();
  const [collapsed, setCollapsed] = useState(false);
  const {
    token: { colorBgContainer },
  } = theme.useToken();

  if (!isAuth) {
    return children;
  }

  const isLoginPage = pathname === "/login";

  return (
    <Layout style={{ height: "100vh" }}>
      {!isLoginPage && (
        <Sider
          collapsible
          collapsed={collapsed}
          onCollapse={(value) => setCollapsed(value)}
          style={{ height: "100vh", position: "fixed", left: 0 }}
        >
          <SideMenu collapsed={collapsed} />
        </Sider>
      )}
      <Layout style={{ marginLeft: isLoginPage ? 0 : (collapsed ? 80 : 200) }}>
        <Header
          style={{
            padding: "0 24px",
            background: colorBgContainer,
            display: "flex",
            alignItems: "center",
            justifyContent: "flex-end",
            position: "sticky",
            top: 0,
            zIndex: 1,
            height: "64px",
          }}
        >
          <Space>
            <LanguageSwitcher />
            <ThemeSwitcher />
            <UserProfile />
          </Space>
        </Header>
        <Content
          style={{
            padding: isLoginPage ? 0 : 24,
            background: colorBgContainer,
            height: "calc(100vh - 64px - 50px)", // viewport - header - footer
            overflow: "auto",
          }}
        >
          {children}
        </Content>
        <Footer
          style={{
            textAlign: "center",
            background: colorBgContainer,
            height: "50px",
            padding: "12px",
          }}
        >
          @2025 - All rights reserved
        </Footer>
      </Layout>
    </Layout>
  );
};

export default MainLayout;
