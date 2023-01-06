import React, { useEffect, useState } from "react";
import { List, Button, Row, Modal, Form, Input } from "antd";
import { DeleteTwoTone, PlusSquareTwoTone } from "@ant-design/icons";
import { instance, parseJwt } from "../../utils.js";

const formItemLayout = {
  labelCol: {
    span: 8,
  },
  wrapperCol: {
    span: 12,
  },
};

const DebtList = ({ isSelf, parentCallback, cantEdit }) => {
  const [stk, setSTK] = useState();

  const [data, setData] = useState([]);

  const [userId] = useState(parseJwt(localStorage.App_AccessToken).userId);

  const [modelCancelRecipient, setModelCancelRecipient] = useState(false);

  const [modelAddRecipient, setModelAddRecipient] = useState(false);

  const [recpientEdit, setRecipientEdit] = useState();

  const [itemSelected, setItemSelected] = useState();

  const [formDelete] = Form.useForm();

  const [formAdd] = Form.useForm();

  const getSTK = async () => {
    const resUserSTK = await instance.get(
      `Customer/GetUserBalance/${userId}`,
      {}
    );

    if (resUserSTK.data.status === 200) {
      setSTK(resUserSTK.data.data.stk);
    }
  };

  const appendData = async () => {
    const res = await instance.get(
      `DebtReminder/viewInfoDebtReminds/${stk}?isSelf=${isSelf}&status=0`,
      {}
    );
    if (res.data.status === 200) {
      setData(res.data.data);
    }
  };

  useEffect(() => {
    getSTK();
    if (stk !== undefined) {
      appendData();
    }
  }, [stk]);

  useEffect(() => {
    if (!modelAddRecipient) {
      formAdd.resetFields();
    }
    if (!modelCancelRecipient) {
      formDelete.resetFields();
    }
  });

  const confirmUpdate = async (id, paramsUpdate) => {
    const res = await instance.patch(`Customer/Recipient/${id}`, {
      stk: paramsUpdate.stkEdit,
      name: paramsUpdate.nameEdit,
    });
    if (res.data.status === 200) {
      success("Update recipient", res.data.message);
    } else {
      error("Update recipient", res.data.message);
    }

    appendData();
  };

  const confirmDelete = async (id) => {
    const res = await instance.patch(`DebtReminder/CancelDebtRemind/${id}`, {});
    if (res.data.status === 200) {
      success("Cancel debt", res.data.message);
    } else {
      error("Cancel debt", res.data.message);
    }

    appendData();
  };

  const CreateDebt = async (paramsAdd) => {
    const res = await instance.post(`DebtReminder/CreateDebtRemind/`, {
      stkSend: stk,
      stkReceive: paramsAdd.stkAdd,
      money: paramsAdd.AmountAdd,
      content: paramsAdd.contentAdd,
    });
    if (res.data.status === 200) {
      success("Create a debt", res.data.message);
    } else {
      error("Create a debt", res.data.message);
    }

    appendData();
  };

  const onFillFormCancel = (item) => {
    formDelete.setFieldsValue({
      stkCancel: item.stk,
      amountCancel: item.money,
      contentCancel: item.content,
    });
  };

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

  const sendData = (item) => {
    parentCallback(item);
  };

  return (
    <Row type="flex" justify="center" align="middle">
      <Modal
        title={
          <div style={{ textAlign: "center" }}>
            <h2>Add Debt</h2>
          </div>
        }
        centered
        open={modelAddRecipient}
        forceRender
        onCancel={() => setModelAddRecipient(false)}
        footer={
          <div style={{ display: "flex", justifyContent: "center" }}>
            <Button
              type="primary"
              style={{ minWidth: 70, width: "auto" }}
              onClick={() => formAdd.submit()}
            >
              Send debt
            </Button>
            <Button
              onClick={() => {
                setModelAddRecipient(false);
              }}
              style={{ minWidth: 70, width: 80 }}
            >
              Cancel
            </Button>
          </div>
        }
      >
        <Form
          form={formAdd}
          onFinish={(value) => {
            CreateDebt(value);
            formAdd.resetFields();
          }}
        >
          <Form.Item
            {...formItemLayout}
            name="stkAdd"
            label="To"
            rules={[
              {
                required: true,
                message: "Please input account number",
              },
            ]}
          >
            <Input
              placeholder="Please input account number debt"
              name="nameAdd"
              style={{ minWidth: 200 }}
            />
          </Form.Item>
          <Form.Item {...formItemLayout} name="AmountAdd" label="Amount">
            <Input
              placeholder="Please inputs amount"
              name="nameAdd"
              style={{ minWidth: 200 }}
            />
          </Form.Item>
          <Form.Item {...formItemLayout} name="contentAdd" label="Content">
            <Input
              placeholder="Please inputs content"
              name="nameAdd"
              style={{ minWidth: 200 }}
            />
          </Form.Item>
        </Form>
      </Modal>
      <Modal
        title={
          <div style={{ textAlign: "center" }}>
            <h2>Confirm Cancel Debt</h2>
          </div>
        }
        centered
        open={modelCancelRecipient}
        forceRender
        onCancel={() => setModelCancelRecipient(false)}
        footer={
          <div style={{ display: "flex", justifyContent: "center" }}>
            <Button
              type="primary"
              danger
              style={{ minWidth: 70, width: 80 }}
              onClick={() => {
                formDelete.submit();
                confirmDelete(recpientEdit);
              }}
            >
              Delete
            </Button>
            <Button
              onClick={() => {
                setModelCancelRecipient(false);
              }}
              style={{ minWidth: 70, width: 80 }}
            >
              Cancel
            </Button>
          </div>
        }
      >
        <Form form={formDelete}>
          <Form.Item
            {...formItemLayout}
            name="stkCancel"
            label="To"
            rules={[
              {
                required: true,
                message: "Please input account number",
              },
            ]}
          >
            <Input
              placeholder="Please input your name"
              style={{ minWidth: 200 }}
            />
          </Form.Item>
          <Form.Item {...formItemLayout} name="amountCancel" label="Amount">
            <Input
              placeholder="Please inputs nick name"
              style={{ minWidth: 200 }}
            />
          </Form.Item>
          <Form.Item {...formItemLayout} name="contentCancel" label="Content">
            <Input
              placeholder="Please inputs nick name"
              style={{ minWidth: 200 }}
            />
          </Form.Item>
        </Form>
      </Modal>
      <div>
        {cantEdit && (
          <div style={{ textAlign: "right", margin: 20 }}>
            <Button
              icon={<PlusSquareTwoTone size="large" twoToneColor="#52c41a" />}
              type="primary"
              ghost
              size="large"
              onClick={() => {
                setModelAddRecipient(true);
              }}
            >
              Add Debt
            </Button>
          </div>
        )}
        <div
          id="scrollableDiv"
          style={{
            minWidth: 400,
            minHeight: 400,
            height: 450,
            overflow: "auto",
            background: "#F8F8FF",
          }}
        >
          <List
            dataSource={data}
            bordered
            renderItem={(item) => (
              <List.Item
                key={item.id}
                onClick={() => {
                  setItemSelected(item.id);
                  if (parentCallback) {
                    sendData(item);
                  }
                }}
                style={(() =>
                  item.id === itemSelected
                    ? { background: "#32CD32" }
                    : { background: "#F8F8FF" })()}
              >
                {item.status === 1 ? (
                  <div
                    style={{
                      position: "sticky",
                      top: 0,
                      left: 0,
                      background: "blue",
                      borderRadius: 2,
                      width: 70,
                      textAlign: "center",
                    }}
                  >
                    <strong>Paid</strong>
                  </div>
                ) : (
                  <div
                    style={{
                      position: "sticky",
                      top: 0,
                      left: 0,
                      background: "red",
                      width: 70,
                      borderRadius: 2,
                      textAlign: "center",
                    }}
                  >
                    <strong>Unpaid</strong>
                  </div>
                )}
                <div>
                  {isSelf ? (
                    <div>
                      <strong>To: </strong>
                      {item.stk}
                    </div>
                  ) : (
                    <div>
                      <strong>From: </strong>
                      {item.stk}
                    </div>
                  )}
                  <div>
                    <strong>Content: </strong>
                    {item.content}
                  </div>
                  <div>
                    {" "}
                    <strong>Amount: </strong>
                    {item.money}
                  </div>
                </div>
                <div>
                  {cantEdit && (
                    <Button
                      icon={<DeleteTwoTone twoToneColor="#eb2f96" />}
                      type="ghost"
                      size="large"
                      onClick={() => {
                        setModelCancelRecipient(true);
                        onFillFormCancel(item);
                        setRecipientEdit(item.id);
                      }}
                    ></Button>
                  )}
                </div>
              </List.Item>
            )}
          />
        </div>
      </div>
    </Row>
  );
};
export default DebtList;
