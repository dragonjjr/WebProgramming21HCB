import React, { useState } from "react";
import { Button, Form, Input, Alert, Spin, Row } from "antd";
import {
  LockOutlined,
  UserOutlined,
  EyeInvisibleOutlined,
  EyeTwoTone,
  MailOutlined,
  PhoneOutlined,
  IdcardOutlined,
} from "@ant-design/icons";
import "../../Assets/CSS/Register.css";
import { instance, parseJwt } from "../../utils";
import { useEffect } from "react";

function ChangePassword(props) {
  const [loading, setLoading] = useState(false);

  const [message, setMessage] = useState("");

  const [failed, setFailed] = useState(false);

  const loadData = async function (values) {
    try {
      const token = parseJwt(localStorage.App_AccessToken);
      setLoading(true);

      const res = await instance.patch(
        `Account/ChangePassword/${token.userId}`,
        {
          currentPassword: values.currentPassword,
          newPassword: values.newPassword,
          confirmNewPassword: values.confirmNewPassword,
        }
      );

      if (res.data.status === 200) {
        setFailed(false);
      } else {
        setFailed(true);
      }
      setMessage(res.data.message);
      setLoading(false);
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <Row type="flex" justify="center" align="middle">
      <Form
        name="register"
        className="register-form"
        style={{ width: 450 }}
        onFinish={(e) => loadData(e)}
      >
        <Form.Item
          name="currentPassword"
          rules={[
            { required: true, message: "Please input your new password!" },
          ]}
        >
          <Input.Password
            type="password"
            prefix={<LockOutlined className="site-form-item-icon" />}
            placeholder="Current Password"
            size="large"
            autoComplete="off"
            iconRender={(visible) =>
              visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />
            }
          ></Input.Password>
        </Form.Item>

        <Form.Item
          name="confirmNewPassword"
          rules={[
            {
              required: true,
              message: "Please input your confirm new password!",
            },
          ]}
        >
          <Input.Password
            type="password"
            prefix={<LockOutlined className="site-form-item-icon" />}
            placeholder="Current Password"
            size="large"
            autoComplete="off"
            iconRender={(visible) =>
              visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />
            }
          ></Input.Password>
        </Form.Item>

        <Form.Item
          name="newPassword"
          rules={[
            { required: true, message: "Please input your current password!" },
          ]}
        >
          <Input.Password
            type="password"
            prefix={<LockOutlined className="site-form-item-icon" />}
            placeholder="New Password"
            size="large"
            autoComplete="off"
            iconRender={(visible) =>
              visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />
            }
          ></Input.Password>
        </Form.Item>

        {message && (
          <Form.Item>
            {failed ? (
              <Alert message={message} type="error" />
            ) : (
              <Alert message={message} type="success" />
            )}
          </Form.Item>
        )}

        <Form.Item>
          <Button
            type="primary"
            htmlType="submit"
            className="login-form-button"
            size="large"
            block
            loading={loading}
          >
            Submit
          </Button>
        </Form.Item>
      </Form>
    </Row>
  );
}
export default ChangePassword;
