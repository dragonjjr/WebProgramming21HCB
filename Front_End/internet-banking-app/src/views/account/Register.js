import React, { useState } from "react";
import { Button, Form, Input, Alert, Spin } from "antd";
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
import ReCAPTCHA from "react-google-recaptcha";
import { Link, useNavigate } from "react-router-dom";

function Register(props)
{
    const [form] = Form.useForm();

    const navigate = useNavigate();
    const [messageApp, setMessageApp] = useState("");

    const [buttonDisabled, setButtonDisabled] = useState(true);

    const [loading, setLoading] = useState(false);

    return (
        <div className="register_screen">
          <div style={{ verticalAlign: "center" }}>
            <Form name="register" className="register-form">
                <div>
                    <Form.Item name="fullname" rules={[{required: true, message: "Please input your Username!"}]}>
                        <Input prefix={<UserOutlined className="site-form-item-icon" />}
                        placeholder="FullName" size="large" autoComplete="off"></Input>
                    </Form.Item>
                    <Form.Item name="cmnd" rules={[{required: true, message: "Please input your Username!"}]}>
                        <Input prefix={<IdcardOutlined className="site-form-item-icon" />}
                        placeholder="Your Identity" size="large" autoComplete="off"></Input>
                    </Form.Item>
                    <Form.Item name="username" rules={[{required: true, message: "Please input your Username!"}]}>
                        <Input prefix={<UserOutlined className="site-form-item-icon" />}
                        placeholder="Username" size="large" autoComplete="off"></Input>
                    </Form.Item>
                    <Form.Item name="password" rules={[{required: true, message: "Please input your password!"}]}>
                        <Input.Password type="password" 
                        prefix={<LockOutlined className="site-form-item-icon" />}
                        placeholder="Password" size="large" autoComplete="off"
                        iconRender={(visible) =>
                            visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />
                          }
                        ></Input.Password>
                    </Form.Item>
                    <Form.Item name="email" rules={[{required: true, message: "Please input your email!"}]}>
                        <Input prefix={<MailOutlined className="site-form-item-icon" />}
                        placeholder="Email" size="large" autoComplete="off"></Input>
                    </Form.Item>
                    <Form.Item name="phone" rules={[{required: true, message: "Please input your phone!"}]}>
                        <Input prefix={<PhoneOutlined className="site-form-item-icon" />}
                        placeholder="Phone" size="large" autoComplete="off"></Input>
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
                        type="primary" danger
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
          </div>
        </div>
      );
}
export default Register;