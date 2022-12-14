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
  Modal,
  InputNumber,
} from "antd";
import { UserOutlined } from "@ant-design/icons";
import { instance, parseJwt } from "../../utils.js";
import ListRecipient from "./ListRecipient.js";
import { StoreContext } from "../../AppContext.js";
import { useNavigate } from "react-router-dom";

const formItemLayout = {
  labelCol: {
    span: 8,
  },
  wrapperCol: {
    span: 12,
  },
};

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
    console.log(values);
    setLoadingTranfer(true);
    const res = await instance.post(`External/SendMoney`, {
      sendPayAccount: values.send_STK,
      sendAccountName: values.send_STK,
      receiverPayAccount: values.receive_STK,
      typeFee: values.paymentFeeTypeID === 1 ? "sender" : "receiver",
      amountOwed: values.send_Money,
      bankReferenceId: "bank1",
      description: values.content,
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
      send_Money: 0,
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
            <InputNumber
              placeholder="Amount"
              formatter={(value) =>
                `${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ",")
              }
              parser={(value) => value.replace(/\$\s?|(,*)/g, "")}
              style={{ minWidth: 200, width: 350 }}
            />
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

  const [loadingVerifyOTP, setLoadingVerifyOTP] = useState(false);

  const onCheckOTP = async (otp) => {
    const res = await instance.post(
      `InternalTransfer/CheckOTPTransaction/false`,
      {
        transactionID: transaction[0],
        otp: otp.otp,
      }
    );
    if (res.data.status === 200) {
      setLoadingVerifyOTP(false);
      nextCurrent();
    }

    setTimeout(() => {
      setLoadingVerifyOTP(false);
    }, 6000);
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
            <Spin spinning={loadingVerifyOTP}>
              <Button
                type="primary"
                onClick={() => {
                  form.submit();
                  setLoadingVerifyOTP(true);
                }}
              >
                Submit
              </Button>
            </Spin>
          </Form.Item>
        </Card>
      </Form>
    </Row>
  );
};

const ResultTransaction = () => {
  const navigate = useNavigate();

  const { transaction } = useContext(StoreContext);

  const [result, setResult] = useState();

  const [formEdit] = Form.useForm();

  const [modelEditRecipient, setModelEditRecipient] = useState(false);

  const [bankReference, setBankReference] = useState();

  const [userId] = useState(parseJwt(localStorage.App_AccessToken).userId);

  const [recipientInfo, setRecipientInfor] = useState();

  const success = (title, content) => {
    Modal.success({
      title: title,
      content: content,
    });
  };

  const error = (title, content) => {
    Modal.error({
      title: title,
      content: content,
    });
  };

  useEffect(
    () => async () => {
      const resBankReference = await instance.get(
        `Customer/GetListBankReference`
      );
      if (resBankReference.data.status === 200) {
        setBankReference(
          resBankReference.data.data.map((item) => ({
            value: item.id,
            label: item.name,
          }))
        );
      }
    },
    []
  );
  const confirmAdd = async (userId, paramsAdd) => {
    const res = await instance.post(`Customer/Recipient/AddRecipient`, {
      stk: paramsAdd.stkEdit,
      name: paramsAdd.nameEdit,
      userID: userId,
      bankID: paramsAdd.bankEdit,
    });
    if (res.data.status === 200) {
      success("Add recipient", res.data.message);
    } else {
      error("Add recipient", res.data.message);
    }
  };

  const getData = async () => {
    const res = await instance.get(
      `InternalTransfer/GetInforTransaction?transactionId=${transaction[0]}`
    );
    if (res.data.status === 200) {
      setResult(res.data.data);
    }
  };

  useEffect(() => {
    getData();
  }, []);

  const onFillModalAdd = async () => {
    const res = await instance.get(
      `External/GetInforFromPartner?STK=${result.stkReceive}`
    );
    if (res.data.success === true) {
      setRecipientInfor(res.data.data);

      console.log(res.data.data);
      formEdit.setFieldsValue({
        stkEdit: result.stkReceive,
        nameEdit: res.data.data.name,
        bankEdit: 2,
      });
    }
  };

  return (
    <>
      <Modal
        title={
          <div style={{ textAlign: "center" }}>
            <h2>Add Recipient</h2>
          </div>
        }
        centered
        open={modelEditRecipient}
        forceRender
        onCancel={() => setModelEditRecipient(false)}
        footer={
          <div style={{ display: "flex", justifyContent: "center" }}>
            <Button
              type="primary"
              style={{ minWidth: 70, width: 80 }}
              onClick={() => {
                formEdit.submit();
              }}
            >
              Add
            </Button>
            <Button
              onClick={() => {
                setModelEditRecipient(false);
              }}
              style={{ minWidth: 70, width: 80 }}
            >
              Cancel
            </Button>
          </div>
        }
      >
        <Form
          form={formEdit}
          onFinish={(value) => {
            confirmAdd(userId, value);
          }}
        >
          <Form.Item
            {...formItemLayout}
            name="stkEdit"
            label="Account number"
            rules={[
              {
                required: true,
                message: "Please input account number",
              },
            ]}
          >
            <Input
              placeholder="Please input your name"
              name="stkEdit"
              style={{ minWidth: 200 }}
            />
          </Form.Item>
          <Form.Item {...formItemLayout} name="nameEdit" label="Nickname">
            <Input
              placeholder="Please inputs nick name"
              name="nameEdit"
              style={{ minWidth: 200 }}
            />
          </Form.Item>
          <Form.Item {...formItemLayout} name="bankEdit" label="Bank">
            <Select style={{ minWidth: 200 }} options={bankReference} />
          </Form.Item>
        </Form>
      </Modal>
      <Result
        status="success"
        title="Successfully Transaction"
        subTitle={
          result && (
            <div>
              <h1 style={{ padding: 0, margin: 0 }}>{result.money}</h1>
              <p></p>
              <div style={{ padding: 0, margin: 0 }}>
                <h3>{result.transDate}</h3>
              </div>

              <Row>
                <h4 style={{ padding: 0, margin: 0 }}>
                  Name: {result.transactionType}
                </h4>
              </Row>
              <Row>
                <h4 style={{ padding: 0, margin: 0 }}>
                  From: {result.stkSend}
                </h4>
              </Row>
              <Row>
                <h4 style={{ padding: 0, margin: 0 }}>
                  To: {result.stkReceive}
                </h4>
              </Row>
              <Row>
                <h4 style={{ padding: 0, margin: 0 }}>
                  Transaction ID: {transaction[0]}
                </h4>
              </Row>
              <Row>
                <h4 style={{ padding: 0, margin: 0 }}>
                  Content: {result.content}
                </h4>
              </Row>
            </div>
          )
        }
        extra={[
          <Button type="primary" key="home" onClick={() => navigate("/")}>
            Home
          </Button>,
          <Button
            key="transactionhistory"
            onClick={() => navigate("/transactionhistory")}
          >
            Transaction History
          </Button>,
          <Button
            key="saverecipient"
            onClick={() => {
              setModelEditRecipient(true);
              onFillModalAdd();
            }}
          >
            Save Recipient
          </Button>,
        ]}
      />
    </>
  );
};

const ExternalTranfer = () => {
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
      content: <ResultTransaction />,
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
        </Card>
      </Row>
    </>
  );
};
export default ExternalTranfer;
