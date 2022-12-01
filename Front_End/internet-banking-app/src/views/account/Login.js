import React, { useState } from "react";
import { Button, Form, Input, Alert, Spin } from "antd";
import {
  LockOutlined,
  UserOutlined,
  EyeInvisibleOutlined,
  EyeTwoTone,
} from "@ant-design/icons";
import "../../Assets/CSS/Account.css";
import { instance } from "../../utils";
import ReCAPTCHA from "react-google-recaptcha";
import { Link, useNavigate } from "react-router-dom";

function Login(props) {
  const recaptchaRef = React.createRef();

  const navigate = useNavigate();

  const [isVerify, setVerify] = useState(false);

  const [isLoginFailed, setLoginFailed] = useState(false);

  const [loading, setLoading] = useState(false);

  const onLogin = async function (values) {
    try {
      setLoading(true);
      // Push data to api
      const res = await instance.post("Account/Login", {
        username: values.username,
        password: values.password,
      });

      //Login Success
      if (res.data.status === 200) {
        localStorage.setItem("App_AccessToken", res.data.data.accessToken);
        setLoginFailed(false);
        //Role
        navigate("/");
      }

      //Login Failed
      else {
        setLoginFailed(true);
      }
      setLoading(false);
    } catch (err) {
      console.log(err);
    }
  };

  // check reCaptcha
  const onVerify = function (value) {
    if (value) {
      setVerify(true);
    } else {
      setVerify(false);
    }
  };

  return (
    <div className="login_screen">
      <div style={{ verticalAlign: "center" }}>
        <Form
          name="normal_login"
          className="login-form"
          onFinish={onLogin}
          autoComplete="off"
        >
          <div>
            <Spin spinning={loading}>
              <Form.Item
                name="username"
                rules={[
                  {
                    required: true,
                    message: "Please input your Username!",
                  },
                ]}
              >
                <Input
                  prefix={<UserOutlined className="site-form-item-icon" />}
                  placeholder="Username"
                  size="large"
                  autoComplete="off"
                />
              </Form.Item>
              <Form.Item
                name="password"
                rules={[
                  {
                    required: true,
                    message: "Please input your Password!",
                  },
                ]}
              >
                <Input.Password
                  autoComplete="off"
                  prefix={<LockOutlined className="site-form-item-icon" />}
                  type="password"
                  placeholder="Password"
                  size="large"
                  iconRender={(visible) =>
                    visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />
                  }
                />
              </Form.Item>
              <Form.Item>
                <ReCAPTCHA
                  ref={recaptchaRef}
                  sitekey="6Lfs-zwjAAAAAD7xGa8xIO35h0hIFysb6j9_KUKi"
                  onChange={onVerify}
                />
              </Form.Item>
              <Form.Item>
                <Button
                  type="primary"
                  htmlType="submit"
                  className="login-form-button"
                  size="large"
                  block
                  disabled={!isVerify}
                >
                  Log in
                </Button>
              </Form.Item>
              <Form.Item>
                <div className="container-forgot-register">
                  <Link to="/forgotpassword">Forgot password</Link>
                  <Link to="/signup">Sign Up</Link>
                </div>
              </Form.Item>
              {isLoginFailed && (
                <Form.Item>
                  <Alert message="Login failed!" type="error" />
                </Form.Item>
              )}
            </Spin>
          </div>
        </Form>
      </div>
    </div>
  );
}

export default Login;
