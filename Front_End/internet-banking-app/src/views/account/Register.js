import React, { useState } from "react";
import { Button, Form, Input, notification, Spin, Row } from "antd";
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
const { TextArea } = Input;

function Register(props) {
  const [loading, setLoading] = useState(false);

  const [form] = Form.useForm();

  const openNotification = (stk) => {
    notification.open({
      duration: 0,
      message: "Register account",
      description: (
        <div>
          <p>Register successfully!</p>
          <p>Account number: {stk}</p>
        </div>
      ),
    });
  };

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
          address: values.address,
          isStaff: false,
        },
      });

      if (res.data.status === 200) {
        openNotification(res.data.data);
      }
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
        form={form}
      >
        <div>
          <Form.Item
            name="fullname"
            rules={[{ required: true, message: "Please input your FullName!" }]}
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
            rules={[{ required: true, message: "Please input your ID Card!" }]}
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
          <Form.Item
            name="address"
            rules={[{ required: true, message: "Please input your address!" }]}
          >
            <TextArea
              prefix={<PhoneOutlined className="site-form-item-icon" />}
              rows={4}
              placeholder="Address"
              maxLength={6}
            />
          </Form.Item>
          <Form.Item>
            <Spin spinning={loading}>
              <Button
                type="primary"
                htmlType="submit"
                className="login-form-button"
                size="large"
                block
                onClick={() => setLoading(true)}
              >
                Register
              </Button>
            </Spin>
          </Form.Item>
          <Form.Item>
            <Button
              type="primary"
              danger
              htmlType="button"
              className="login-form-button"
              size="large"
              block
              onClick={() => form.resetFields()}
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
