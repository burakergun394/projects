"use client";

import {
  Form,
  Input,
  Button,
  Card,
  message,
  Typography,
  Space,
  notification,
} from "antd";
import { useLocalization } from "@/localization/useLocalization";
import { useState } from "react";
import { useRouter } from "next/navigation";
import { useAuth } from "@/context/auth/AuthContext";
import TenantSelect from "@/components/tenant/tenant-select";
import { LockOutlined, UserOutlined, LoginOutlined } from "@ant-design/icons";

const { Title, Text } = Typography;

interface LoginForm {
  tenant: string;
  username: string;
  password: string;
}

const LoginPage = () => {
  const { t } = useLocalization();
  const router = useRouter();
  const { login } = useAuth();
  const [loading, setLoading] = useState(false);

  const onFinish = async (values: LoginForm) => {
    try {
      setLoading(true);
      await new Promise((resolve) => setTimeout(resolve, 1000));
      await login(values.username, values.tenant);
      router.push("/");
    } catch (error) {
      notification.error({
        message: t("loginError"),
        description: t("anErrorOccurred"),
      });
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="h-full w-full flex items-center justify-center">
      <Card className="w-full max-w-md shadow-lg rounded-lg">
        <Space direction="vertical" size={24} style={{ width: "100%" }}>
          <div className="text-center">
            <Title level={2} className="mb-2">
              {t("loginTitle")}
            </Title>
            <Text type="secondary">
              {t("welcomeBackPleaseLoginToYourAccount")}
            </Text>
          </div>

          <Form
            name="login"
            layout="vertical"
            onFinish={onFinish}
            requiredMark={false}
            size="large"
          >
            <Form.Item
              name="tenant"
              label={t("tenant")}
              rules={[{ required: true, message: t("tenantRequired") }]}
            >
              <TenantSelect />
            </Form.Item>

            <Form.Item
              name="username"
              label={t("username")}
              rules={[{ required: true, message: t("usernameRequired") }]}
            >
              <Input
                prefix={<UserOutlined />}
                placeholder={t("usernamePlaceholder")}
              />
            </Form.Item>

            <Form.Item
              name="password"
              label={t("password")}
              rules={[{ required: true, message: t("passwordRequired") }]}
            >
              <Input.Password
                prefix={<LockOutlined />}
                placeholder={t("passwordPlaceholder")}
              />
            </Form.Item>

            <Form.Item>
              <Button
                type="primary"
                htmlType="submit"
                loading={loading}
                block
                icon={<LoginOutlined />}
                className="h-12"
              >
                {t("login")}
              </Button>
            </Form.Item>
          </Form>
        </Space>
      </Card>
    </div>
  );
};

export default LoginPage;
