import React, { useState } from "react";
import { Button, Form, Input, Alert, Spin, notification, Row } from "antd";
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
import { instance } from "../../utils";
import { useEffect } from "react";

function Register(props) {
  //openNotificationWithIcon
  const [api, contextHolder] = notification.useNotification();

  const openNotificationWithIcon = (type) => {
    if (type === "Success") {
      api.success({
        message: "Register account",
        description: "You have successfully register account.",
      });
    } else {
      api.error({
        message: "Register account",
        description: "You have failed to register account !!!",
      });
    }
  };

  const [loading, setLoading] = useState(false);

  const loadData = async function (values) {
    try {
      const res = await instance.post(`Employee/RegisterAccount`, {
        userName: values.userName,
        password: values.password,
        infor: {
          name: values.fullname,
          cmnd: values.cmnd,
          email: values.email,
          phone: values.phone,
          isStaff: false,
        },
      });

      if (res.data.status === 200) {
        openNotificationWithIcon("Success");
        console.log("ASDS");
      } else {
        openNotificationWithIcon("Fail");
      }
      setLoading(false);
    } catch (err) {
      console.log(err);
      openNotificationWithIcon("Fail");
    }
  };

  return (
    <Row type="flex" justify="center" align="middle">
      {contextHolder}
      <Form
        name="register"
        className="register-form"
        style={{ width: 450 }}
        onFinish={(e) => loadData(e)}
      >
        <div>
          <Form.Item
            name="fullname"
            rules={[{ required: true, message: "Please input your Username!" }]}
          >
            <Input
              prefix={<UserOutlined className="site-form-item-icon" />}
              placeholder="FullName"
              size="large"
              autoComplete="off"
            ></Input>
          </Form.Item>
          <Form.Item
            name="cmnd"
            rules={[{ required: true, message: "Please input your Username!" }]}
          >
            <Input
              prefix={<IdcardOutlined className="site-form-item-icon" />}
              placeholder="Your Identity"
              size="large"
              autoComplete="off"
            ></Input>
          </Form.Item>
          <Form.Item
            name="userName"
            rules={[{ required: true, message: "Please input your Username!" }]}
          >
            <Input
              prefix={<UserOutlined className="site-form-item-icon" />}
              placeholder="Username"
              size="large"
              autoComplete="off"
            ></Input>
          </Form.Item>
          <Form.Item
            name="password"
            rules={[{ required: true, message: "Please input your password!" }]}
          >
            <Input.Password
              type="password"
              prefix={<LockOutlined className="site-form-item-icon" />}
              placeholder="Password"
              size="large"
              autoComplete="off"
              iconRender={(visible) =>
                visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />
              }
            ></Input.Password>
          </Form.Item>
          <Form.Item
            name="email"
            rules={[{ required: true, message: "Please input your email!" }]}
          >
            <Input
              prefix={<MailOutlined className="site-form-item-icon" />}
              placeholder="Email"
              size="large"
              autoComplete="off"
            ></Input>
          </Form.Item>
          <Form.Item
            name="phone"
            rules={[{ required: true, message: "Please input your phone!" }]}
          >
            <Input
              prefix={<PhoneOutlined className="site-form-item-icon" />}
              placeholder="Phone"
              size="large"
              autoComplete="off"
            ></Input>
          </Form.Item>
          <Form.Item>
            <Button
              type="primary"
              htmlType="submit"
              className="login-form-button"
              size="large"
              block
            >
              Register
            </Button>
          </Form.Item>
          <Form.Item>
            <Button
              type="primary"
              danger
              htmlType="button"
              className="login-form-button"
              size="large"
              block
            >
              Clear
            </Button>
          </Form.Item>
        </div>
      </Form>
    </Row>
  );
}
export default Register;
