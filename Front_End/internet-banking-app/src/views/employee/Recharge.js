import React, { useState, useEffect } from "react";
import {
  Button,
  Form,
  Input,
  Row,
  notification,
  Spin,
  InputNumber,
} from "antd";
import { instance, parseJwt } from "../../utils.js";

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
  const [userId] = useState(parseJwt(localStorage.App_AccessToken).userId);

  const [data, setData] = useState([]);

  // Loading...
  const [loading, setLoading] = useState(false);

  const [form] = Form.useForm();

  const appendData = async () => {
    const res = await instance.get(`Customer/GetUserBalance/${userId}`, {});
    if (res.data.status === 200) {
      setData(res.data.data.stk);
    }
  };

  useEffect(() => {
    appendData();
  }, []);

  const openNotification = (soDu) => {
    console.log(soDu);
    notification.open({
      duration: 0,
      description: (
        <div>
          <p>Recharge successfully!</p>
          <p>Account balance: {soDu}</p>
        </div>
      ),
    });
  };

  //Submit Form
  const onFinish = async (values) => {
    const res = await instance.post(`Employee/Recharge`, {
      bankID: 1,
      stK_Send: data,
      stK_Receive: values.AccountNumber,
      soTien: values.Amount,
      noiDung: "Employee recharge",
      paymentTypeID: 1,
      transactionTypeId: 1,
    });
    if (res.data.status === 200) {
      setLoading(false);
      openNotification(res.data.data);
    }
    setLoading(false);
    onReset();
  };

  //Reset Form
  const onReset = () => {
    form.resetFields();
  };

  return (
    <Row type="flex" justify="center" align="middle">
      <Form
        {...layout}
        form={form}
        name="control-hooks"
        onFinish={onFinish}
        size="large"
        style={{ width: 650, minWidth: 300 }}
      >
        <Form.Item
          name="AccountNumber"
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
          <Spin spinning={loading}>
            <Button
              type="primary"
              htmlType="submit"
              block
              onClick={() => {
                setLoading(true);
              }}
            >
              Submit
            </Button>
          </Spin>
        </Form.Item>
      </Form>
    </Row>
  );
}
export default Recharge;
