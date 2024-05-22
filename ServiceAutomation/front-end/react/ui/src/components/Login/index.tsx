import React from "react";
import { Button, Checkbox, Form, Input, Select } from "antd";
import { BaseResponse, LoginRequest, LoginResponse } from "@/common.types";
import axios from "axios";
import useIdentity from "@/hooks/useIdentity";

const Login = () => {
  const [form] = Form.useForm();
  const { loginAsync } = useIdentity();

  const onClickHandler = async () => {
    const validateFields = await form.validateFields();
    const request: LoginRequest = {
      tenantCode: validateFields.tenantCode,
      userName: validateFields.userName,
      password: validateFields.password,
    };
    console.log(process.env.API_URL);
    console.log(process.env.BURAK);

    const response: BaseResponse<LoginResponse> = await loginAsync(request);
    if (response.isSuccess) {
      console.log(response.data?.authToken);
    } else {
      console.log(response.message);
    }
  };

  return (
    <>
      <Form
        form={form}
        labelCol={{ span: 8 }}
        wrapperCol={{ span: 16 }}
        style={{ maxWidth: 600 }}
        initialValues={{ remember: true }}
        autoComplete="off"
      >
        <Form.Item
          label="TenantCode"
          name="tenantCode"
          rules={[{ required: true, message: "Please input your username!" }]}
        >
          <Select
            showSearch
            style={{ width: 200 }}
            placeholder="Search to Tenant"
            optionFilterProp="children"
            filterOption={(input, option) =>
              (option?.label ?? "").includes(input)
            }
            filterSort={(optionA, optionB) =>
              (optionA?.label ?? "")
                .toLowerCase()
                .localeCompare((optionB?.label ?? "").toLowerCase())
            }
            options={[
              {
                value: "SYSTEM",
                label: "SYSTEM",
              },
              {
                value: "AYTAS",
                label: "AYTAS",
              },
            ]}
          />
        </Form.Item>

        <Form.Item
          label="UserName"
          name="userName"
          rules={[{ required: true, message: "Please input your username!" }]}
        >
          <Input />
        </Form.Item>

        <Form.Item
          label="Password"
          name="password"
          rules={[{ required: true, message: "Please input your password!" }]}
        >
          <Input.Password />
        </Form.Item>

        <Form.Item
          name="remember"
          valuePropName="checked"
          wrapperCol={{ offset: 8, span: 16 }}
        >
          <Checkbox>Remember me</Checkbox>
        </Form.Item>

        <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
          <Button type="primary" htmlType="submit" onClick={onClickHandler}>
            Submit
          </Button>
        </Form.Item>
      </Form>
    </>
  );
};

export default Login;
