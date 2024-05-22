import { Layout, Menu, MenuProps } from "antd";
import { useState } from "react";
import {
  DesktopOutlined,
  PieChartOutlined,
  UserOutlined,
  LoginOutlined
} from "@ant-design/icons";
import Link from "next/link";

const MySider = () => {
  const { Sider } = Layout;
  const [collapsed, setCollapsed] = useState(false);
  const [current, setCurrent] = useState<string>("page1");

  type MenuItem = Required<MenuProps>["items"][number];

  const getItem = (
    label: React.ReactNode,
    key: React.Key,
    icon?: React.ReactNode,
    href?: string,
    children?: MenuItem[]
  ): MenuItem => ({
    key,
    icon,
    children,
    label: href ? <Link href={href}>{label}</Link> : label,
  });

  const items: MenuItem[] = [
    getItem("Page 1", "page1", <PieChartOutlined />, "/page1"),
    getItem("Page 2", "page2", <DesktopOutlined />, "/page2"),
    getItem("Page 3", "page3", <UserOutlined />, undefined, [
      getItem("Page 3 - 1", "page3_1", "/page3/1"),
      getItem("Page 3 - 2", "page3_2", "/page3/2"),
      getItem("Page 3 - 3", "page3_3", "/page3/3"),
    ]),
    getItem("Login", "login", <LoginOutlined />, "/login"),
  ];

  const onClick: MenuProps["onClick"] = (e) => {
    setCurrent(e.key);
  };

  return (
    <Sider
      collapsible
      collapsed={collapsed}
      onCollapse={(value) => setCollapsed(value)}
    >
      <div className="demo-logo-vertical" />
      <Menu
        theme="dark"
        defaultSelectedKeys={[current]}
        mode="inline"
        items={items}
        onClick={onClick}
      ></Menu>
    </Sider>
  );
};

export default MySider;
