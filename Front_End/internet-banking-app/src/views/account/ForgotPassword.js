import React, { useState } from "react";
import { Button, Form, Input, Alert, message, Spin } from "antd";
import { MailOutlined, LockOutlined, KeyOutlined } from "@ant-design/icons";
import { instance } from "../../utils";
import { Link, useNavigate } from "react-router-dom";
import "../../Assets/CSS/Account.css";

function ForgotPassword(props) {
  const [form] = Form.useForm();

  const navigate = useNavigate();

  const [isSendMail, setSendMail] = useState(false);

  const [idUser, setIdUser] = useState(null);

  const [isErrorSavePassword, setErrorSavePassword] = useState(false);

  const [messageApp, setMessageApp] = useState("");

  const [buttonDisabled, setButtonDisabled] = useState(true);

  const [messageApi, contextHolder] = message.useMessage();

  const [loading, setLoading] = useState(false);

  const success = () => {
    messageApi.open({
      type: "success",
      content: messageApp,
    });
  };

  const onSendMail = async function (values) {
    try {
      setLoading(true);
      // Push data to api
      const res = await instance.post("Account/ForgotPassword", {
        email: values.email,
      });

      //Send mail successfully
      if (res.data.status === 200) {
        setIdUser(res.data.data);
        setSendMail(true);
        setLoading(false);
      }
      //Send mail Failed
      else {
        setSendMail(false);
      }
    } catch (err) {
      console.log(err);
    }
  };

  const onSavePassword = async function (values) {
    try {
      setLoading(true);
      // Push data to api
      const res = await instance.patch(
        `Account/ForgotPassword/ResetPassword/${idUser}`,
        {
          otpCode: values.OTPCode,
          newPassword: values.NewPassword,
          confirmNewPassword: values.ConfirmNewPassword,
        }
      );

      setLoading(false);
      //Reset password successfully
      if (res.data.status === 200) {
        success();
        setTimeout(() => navigate("/"), 2000);
      }
      //Reset password Failed
      else {
        setErrorSavePassword(true);
      }
      setMessageApp(res.data.message);
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <div className="login_screen">
      {contextHolder}

      {
        // if idUser not exist then render form send mail
        !idUser ? (
          <Form
            size="large"
            name="normal_login"
            className="send-mail-form"
            onFinish={onSendMail}
            form={form}
            onFieldsChange={() =>
              setButtonDisabled(
                form.getFieldsError().some((field) => field.errors.length > 0)
              )
            }
          >
            {isSendMail === false ? (
              <div>
                <Spin spinning={loading}>
                  <Form.Item>
                    <div style={{ textAlign: "center", marginTop: -50 }}>
                      <h2>Reset Password</h2>
                    </div>
                    <div style={{ wordWrap: "breakWord" }}>
                      <p>
                        Enter your email address below and we'll send you an OTP
                        code to reset your password.
                      </p>
                    </div>
                  </Form.Item>
                  <Form.Item
                    name="email"
                    rules={[
                      {
                        type: "email",
                        message: "The input is not valid Email!",
                      },
                      {
                        required: true,
                        message: "Please input your Email!",
                      },
                    ]}
                  >
                    <Input
                      prefix={<MailOutlined className="site-form-item-icon" />}
                      placeholder="Username"
                      size="large"
                    />
                  </Form.Item>
                  <Form.Item>
                    <Button
                      type="primary"
                      htmlType="submit"
                      className="login-form-button"
                      size="large"
                      block
                      style={{ marginTop: "15px" }}
                      disabled={buttonDisabled}
                    >
                      Reset Password
                    </Button>
                  </Form.Item>
                  <Form.Item>
                    <div className="container-forgot-register">
                      <Link to="/login">Sign in</Link>
                      <Link to="/signup">Sign Up</Link>
                    </div>
                  </Form.Item>
                </Spin>
              </div>
            ) : (
              // if send mail success then render alert success
              <div>
                <Form.Item>
                  <div style={{ textAlign: "center", marginTop: -50 }}>
                    <h2>Reset Password</h2>
                  </div>
                </Form.Item>
                <Form.Item style={{ marginTop: "-30px" }}>
                  <Alert
                    message="Check your inbox for the next steps. If you don't receive an email, and it's not in your spam folder this could mean you signed up with a different address."
                    type="error"
                  />
                </Form.Item>
                <Form.Item>
                  <div className="container-forgot-register">
                    <Link to="/login">Login</Link>
                    <Link to="/signup">Sign Up</Link>
                  </div>
                </Form.Item>
              </div>
            )}
          </Form>
        ) : (
          // if idUser exist then render form reset password
          <Form
            name="form-reset-password"
            className="send-mail-form"
            onFinish={onSavePassword}
          >
            <div>
              <Spin spinning={loading}>
                <Form.Item>
                  <div style={{ textAlign: "center", marginTop: -50 }}>
                    <h2>Reset Password</h2>
                  </div>
                </Form.Item>
                {
                  // if error reset password then render alert message
                  isErrorSavePassword && (
                    <Form.Item>
                      <Alert message={messageApp} type="error" />
                    </Form.Item>
                  )
                }
                <Form.Item
                  name="OTPCode"
                  rules={[
                    {
                      required: true,
                      message: "Please input your otp code!",
                    },
                  ]}
                >
                  <Input
                    prefix={<KeyOutlined className="site-form-item-icon" />}
                    placeholder="OTP"
                    size="large"
                    autoComplete="off"
                  />
                </Form.Item>
                <Form.Item
                  name="NewPassword"
                  rules={[
                    {
                      required: true,
                      message: "Please input your new password!",
                    },
                  ]}
                >
                  <Input.Password
                    prefix={<LockOutlined className="site-form-item-icon" />}
                    placeholder="New password"
                    size="large"
                  />
                </Form.Item>
                <Form.Item
                  name="ConfirmNewPassword"
                  rules={[
                    {
                      required: true,
                      message: "Please input your confirm new password!",
                    },
                  ]}
                >
                  <Input.Password
                    prefix={<LockOutlined className="site-form-item-icon" />}
                    placeholder="Confirm new password"
                    size="large"
                  />
                </Form.Item>
                <Form.Item>
                  <Button
                    type="primary"
                    htmlType="submit"
                    className="login-form-button"
                    size="large"
                    block
                  >
                    Save Password
                  </Button>
                </Form.Item>
                <Form.Item>
                  <div className="container-forgot-register">
                    <Link to="/login">Sign in</Link>
                    <Link to="/signup">Register now!</Link>
                  </div>
                </Form.Item>
              </Spin>
            </div>
          </Form>
        )
      }
    </div>
  );
}

export default ForgotPassword;
