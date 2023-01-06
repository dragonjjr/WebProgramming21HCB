import React, { useState, useEffect, useContext } from "react";
import {
  message,
  Button,
  Row,
  Form,
  Input,
  Card,
  Popover,
  Select,
  Steps,
  Spin,
  Result,
} from "antd";
import { UserOutlined } from "@ant-design/icons";
import { instance, parseJwt } from "../../utils.js";
import ListRecipient from "./ListRecipient.js";
import { StoreContext } from "../../AppContext.js";

const Tranfer = ({ nextCurrent }) => {
  const { transaction } = useContext(StoreContext);

  const callbackFunction = (childData) => {
    setInforRecipient(childData.stk);
  };
  const [inforBalance, setInforBalance] = useState([]);

  const [inforRecipient, setInforRecipient] = useState("");

  const [inforPaymentFeeType, setInforPaymentFeeType] = useState([]);

  const [inforPaymentFeeTypeID, setInforPaymentFeeTypeID] = useState();

  const [form] = Form.useForm();

  const [loadingTranfer, setLoadingTranfer] = useState(false);

  const [userId] = useState(parseJwt(localStorage.App_AccessToken).userId);

  const onTranfer = async (values) => {
    setLoadingTranfer(true);
    const res = await instance.post(`ExternalTransfer/ExternalTranfer`, {
      send_UserID: userId,
      send_STK: values.send_STK,
      send_Money: values.send_Money,
      receive_STK: values.receive_STK,
      content: values.content,
      paymentFeeTypeID: values.paymentFeeTypeID,
      transactionTypeID: 1,
      bankReferenceId: 1,
      isDebtRemind: false,
    });
    if (res.data.status === 200) {
      setLoadingTranfer(false);
      transaction[1](res.data.data);
      nextCurrent();
    }

    setTimeout(() => {
      setLoadingTranfer(false);
    }, 6000);
  };

  const appendData = async () => {
    const res = await instance.get(`Customer/GetUserBalance/${userId}`, {});
    if (res.data.status === 200) {
      setInforBalance(res.data.data);
    }
    const resPaymentFeeType = await instance.get(
      `InternalTransfer/GetPaymentFeeType`,
      {}
    );
    if (resPaymentFeeType.data.status === 200) {
      setInforPaymentFeeType(
        resPaymentFeeType.data.data.map((item) => ({
          value: item.id,
          label: item.name,
        }))
      );
      setInforPaymentFeeTypeID(resPaymentFeeType.data.data[0].id);
    }
  };

  useEffect(() => {
    appendData();
  }, []);

  useEffect(() => {
    form.setFieldsValue({
      send_STK: inforBalance.stk,
      paymentFeeTypeID: inforPaymentFeeTypeID,
      receive_STK: inforRecipient,
    });
  }, [inforBalance, inforPaymentFeeTypeID, inforRecipient]);

  return (
    <Row type="flex" justify="center" align="middle">
      <Form
        form={form}
        onFinish={(values) => {
          onTranfer(values);
        }}
      >
        <Card
          type="inner"
          title="Sender Information"
          style={{ marginBottom: 10 }}
        >
          <Form.Item noStyle>
            <h4>Payment Account</h4>
          </Form.Item>
          <Form.Item name="send_STK">
            <Select
              style={{
                width: 350,
              }}
              options={[
                {
                  value: inforBalance.stk,
                  label: inforBalance.stk,
                },
              ]}
            ></Select>
          </Form.Item>
          <Form.Item label="Available Balances">
            <Input value={inforBalance.soDu} />
          </Form.Item>
        </Card>
        <Card
          type="inner"
          title="Recipient Information"
          style={{ marginBottom: 10 }}
        >
          <Form.Item name="receive_STK">
            <Input
              placeholder="Enter/ Select the recipient"
              allowClear
              suffix={
                <Popover
                  content={
                    <ListRecipient
                      parentCallback={callbackFunction}
                      isSelect={true}
                    />
                  }
                  title="Choose a recipient"
                  trigger="click"
                  zIndex={10}
                  placement="right"
                >
                  <UserOutlined />
                </Popover>
              }
            />
          </Form.Item>
        </Card>
        <Card type="inner" title="Transaction Information">
          <Form.Item name="send_Money">
            <Input placeholder="Amount"></Input>
          </Form.Item>
          <Form.Item name="paymentFeeTypeID">
            <Select
              style={{
                width: 350,
              }}
              options={inforPaymentFeeType}
            ></Select>
          </Form.Item>
          <Form.Item name="content">
            <Input
              placeholder="Content"
              style={{
                width: 350,
              }}
            />
          </Form.Item>
        </Card>
        <Form.Item style={{ textAlign: "center", marginTop: 15 }}>
          <Spin spinning={loadingTranfer}>
            <Button
              type="primary"
              onClick={() => {
                form.submit();
              }}
            >
              Submit
            </Button>
          </Spin>
        </Form.Item>
      </Form>
    </Row>
  );
};

const VerifyOTP = ({ nextCurrent }) => {
  const { transaction } = useContext(StoreContext);

  const [form] = Form.useForm();

  const onCheckOTP = async (otp) => {
    const res = await instance.post(`ExternalTransfer/CheckOTPTransaction/`, {
      transactionID: transaction[0],
      otp: otp.otp,
    });
    if (res.data.status === 200) {
      nextCurrent();
    }
  };

  return (
    <Row type="flex" justify="center" align="middle">
      <Form form={form} onFinish={(otp) => onCheckOTP(otp)}>
        <Card type="inner" title="Verify OTP Code" style={{ marginBottom: 10 }}>
          <Form.Item noStyle>
            <h4>Please input OTP code to verify transaction</h4>
          </Form.Item>

          <Form.Item
            name="otp"
            rules={[
              {
                required: true,
                message: "Please input OTP code to verify transaction!",
              },
            ]}
          >
            <Input />
          </Form.Item>

          <Form.Item style={{ textAlign: "center", marginTop: 15 }}>
            <Button
              type="primary"
              onClick={() => {
                form.submit();
              }}
            >
              Submit
            </Button>
          </Form.Item>
        </Card>
      </Form>
    </Row>
  );
};

const ExternalTranfer = () => {
  const { transaction } = useContext(StoreContext);

  const [current, setCurrent] = useState(0);

  const next = () => {
    setCurrent(current + 1);
  };
  const steps = [
    {
      title: "Transfers",
      content: <Tranfer nextCurrent={next} />,
    },
    {
      title: "Verify OTP",
      content: <VerifyOTP nextCurrent={next} />,
    },
    {
      title: "Last",
      content: (
        <Result
          status="success"
          title="Successfully Transaction"
          subTitle={`Order number: ${transaction[0]} Cloud server configuration takes 1-5 minutes, please wait.`}
          extra={[
            <Button type="primary" key="console">
              Back Home
            </Button>,
            <Button key="buy">Buy Again</Button>,
          ]}
        />
      ),
    },
  ];

  const items = steps.map((item) => ({
    key: item.title,
    title: item.title,
  }));
  return (
    <>
      <Row type="flex" justify="center" align="middle">
        <Card title="External Tranfer">
          <Steps
            current={current}
            items={items}
            style={{ marginBottom: 20 }}
            size="small"
          />
          <div className="steps-content">{steps[current].content}</div>
          <div
            className="steps-action"
            style={{ marginTop: 15, textAlign: "center" }}
          >
            {current === steps.length - 1 && (
              <Button
                type="primary"
                onClick={() => message.success("Processing complete!")}
              >
                Done
              </Button>
            )}
          </div>
        </Card>
      </Row>
    </>
  );
};
export default ExternalTranfer;
