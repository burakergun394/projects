"use client";

import { Menu, Typography, theme } from "antd";
import { useRouter, usePathname } from "next/navigation";
import { useLocalization } from "@/localization/useLocalization";
import {
  DashboardOutlined,
  UserOutlined,
  TeamOutlined,
  ShoppingOutlined,
  SettingOutlined,
} from "@ant-design/icons";
import { useMemo } from "react";
import { useAuth } from "@/context/auth/AuthContext";

const { Title } = Typography;

interface SideMenuProps {
  collapsed: boolean;
}

const SideMenu = ({ collapsed }: SideMenuProps) => {
  const router = useRouter();
  const pathname = usePathname();
  const { t } = useLocalization();
  const { token } = theme.useToken();
  const { tenant } = useAuth();

  const menuItems = useMemo(
    () => [
      {
        key: "/",
        icon: <DashboardOutlined />,
        label: t("dashboard"),
      },
      {
        type: "group",
        label: collapsed ? null : t("management"),
        children: [
          {
            key: "/users",
            icon: <UserOutlined />,
            label: t("users"),
          },
          {
            key: "/roles",
            icon: <TeamOutlined />,
            label: t("roles"),
          },
          {
            key: "/products",
            icon: <ShoppingOutlined />,
            label: t("products"),
          },
        ],
      },
      {
        type: "group",
        label: collapsed ? null : t("settings"),
        children: [
          {
            key: "/settings/general",
            icon: <SettingOutlined />,
            label: t("generalSettings"),
          },
          {
            key: "/settings/system",
            icon: <SettingOutlined />,
            label: t("systemSettings"),
          },
        ],
      },
    ],
    [t, collapsed]
  );

  return (
    <div className="h-full flex flex-col">
      {/* Logo Area */}
      <div
        style={{
          padding: collapsed ? "24px 8px" : "24px 16px",
          borderBottom: `1px solid ${token.colorBorder}`,
          marginBottom: token.marginLG,
          overflow: "hidden",
          whiteSpace: "nowrap",
        }}
      >
        <Title
          level={4}
          style={{
            margin: 0,
            textAlign: "center",
            color: token.colorPrimary,
            textOverflow: "ellipsis",
            overflow: "hidden",
          }}
        >
          {collapsed ? tenant?.charAt(0) : tenant}
        </Title>
      </div>

      {/* Menu */}
      <Menu
        mode="inline"
        inlineCollapsed={collapsed}
        selectedKeys={[pathname]}
        onClick={({ key }) => router.push(key)}
        items={menuItems}
        style={{
          border: "none",
          flex: 1,
        }}
        styles={{
          item: {
            '&:hover': {
              backgroundColor: token.colorFillSecondary,
            },
            '&.ant-menu-item-selected': {
              backgroundColor: token.colorFillSecondary,
            },
          },
        }}
      />

      {/* Footer Area - Optional */}
      <div
        style={{
          padding: "16px",
          borderTop: `1px solid ${token.colorBorder}`,
          textAlign: "center",
          fontSize: "12px",
          color: token.colorTextSecondary,
        }}
      >
        {collapsed ? "v1" : "v1.0.0"}
      </div>
    </div>
  );
};

export default SideMenu;
