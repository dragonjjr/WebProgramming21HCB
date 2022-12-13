import React, { useState } from "react";
import {
  Button,
  Form,
  Input,
  Row,
  notification,
  Spin,
  InputNumber,
} from "antd";

const layout = {
  labelCol: {
    span: 8,
  },
  wrapperCol: {
    span: 16,
  },
};
const tailLayout = {
  wrapperCol: {
    offset: 8,
    span: 11,
  },
};

function Recharge() {
  const [form] = Form.useForm();

  //openNotificationWithIcon
  const [api, contextHolder] = notification.useNotification();

  const openNotificationWithIcon = (type) => {
    api[type]({
      message: "Notification Title",
      description:
        "This is the content of the notification. This is the content of the notification. This is the content of the notification.",
    });
  };

  // Loading...
  const [loading, setLoading] = useState(false);

  //Submit Form
  const onFinish = (values) => {
    console.log(values);
  };

  //Reset Form
  const onReset = () => {
    form.resetFields();
  };

  return (
    <Row type="flex" justify="center" align="middle">
      {contextHolder}
      <Spin spinning={loading} delay={500}>
        <Form
          {...layout}
          form={form}
          name="control-hooks"
          onFinish={onFinish}
          size="large"
          style={{ width: 650, minWidth: 300 }}
        >
          <Form.Item
            name="Username or Account number"
            label="Username or Account number"
            rules={[
              {
                required: true,
              },
            ]}
          >
            <Input style={{ maxWidth: 300 }} />
          </Form.Item>
          <Form.Item
            name="Amount"
            label="Amount"
            rules={[
              {
                required: true,
              },
            ]}
          >
            <InputNumber
              defaultValue={0}
              formatter={(value) =>
                `${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ",")
              }
              parser={(value) => value.replace(/\$\s?|(,*)/g, "")}
              style={{ minWidth: 200 }}
            />
          </Form.Item>

          <Form.Item {...tailLayout}>
            <Button
              type="primary"
              htmlType="submit"
              block
              onClick={() => {
                setLoading(true);
                openNotificationWithIcon("success");
              }}
            >
              Submit
            </Button>
          </Form.Item>
        </Form>
      </Spin>
    </Row>
  );
}
export default Recharge;
