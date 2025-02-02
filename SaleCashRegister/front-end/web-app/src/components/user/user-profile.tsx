"use client";

import {
  Card,
  Popover,
  Button,
  Space,
  Descriptions,
  Tooltip,
  theme,
} from "antd";
import {
  UserOutlined,
  LogoutOutlined,
  SettingOutlined,
  TeamOutlined,
  IdcardOutlined,
} from "@ant-design/icons";
import { useLocalization } from "@/localization/useLocalization";
import { useAuth } from "@/context/auth/AuthContext";
import { useRouter } from "next/navigation";

const UserProfile = () => {
  const { t } = useLocalization();
  const { logout, username, tenant } = useAuth();
  const router = useRouter();
  const { token } = theme.useToken();

  const handleLogout = () => {
    logout();
    router.push("/login");
  };

  const labelStyle = {
    width: token.controlHeightLG * 3,
    display: 'flex',
    alignItems: 'center',
    gap: token.paddingXS,
  };

  const content = (
    <Card
      style={{
        width: token.controlHeightLG * 7.5,
        border: "none",
      }}
      bodyStyle={{ padding: 0 }}
    >
      <div style={{ padding: `0 ${token.paddingMD}px` }}>
        <Descriptions
          size="small"
          column={1}
          contentStyle={{ display: 'flex', alignItems: 'center' }}
          labelStyle={labelStyle}
        >
          <Descriptions.Item
            label={
              <div style={labelStyle}>
                <TeamOutlined />
                {t("tenant")}:
              </div>
            }
          >
            {tenant}
          </Descriptions.Item>
          <Descriptions.Item
            label={
              <div style={labelStyle}>
                <IdcardOutlined />
                {t("userCode")}:
              </div>
            }
          >
            {username}
          </Descriptions.Item>
        </Descriptions>
      </div>
      <div style={{ padding: `${token.paddingXS}px ${token.paddingXS}px` }}>
        <Space
          align="end"
          style={{ width: "100%", justifyContent: "flex-end" }}
        >
          <Tooltip title={t("profile")}>
            <Button
              type="text"
              icon={<SettingOutlined />}
              onClick={() => router.push("/profile")}
              size="middle"
            />
          </Tooltip>
          <Tooltip title={t("logout")}>
            <Button
              type="text"
              icon={<LogoutOutlined />}
              onClick={handleLogout}
              size="middle"
              danger
            />
          </Tooltip>
        </Space>
      </div>
    </Card>
  );

  return (
    <Popover
      content={content}
      trigger="click"
      placement="bottomRight"
      arrow={false}
    >
      <Button
        icon={<UserOutlined />}
        type="text"
        style={{ cursor: "pointer" }}
      />
    </Popover>
  );
};

export default UserProfile;
