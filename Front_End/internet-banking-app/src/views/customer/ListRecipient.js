import React, { useEffect, useState } from "react";
import {
  List,
  Button,
  Row,
  Modal,
  Form,
  Input,
  Popconfirm,
  Select,
  Avatar,
} from "antd";
import {
  UserOutlined,
  DeleteTwoTone,
  EditTwoTone,
  PlusSquareTwoTone,
} from "@ant-design/icons";
import { instance, parseJwt } from "../../utils.js";

const formItemLayout = {
  labelCol: {
    span: 8,
  },
  wrapperCol: {
    span: 12,
  },
};

const ListRecipient = (props) => {
  const [data, setData] = useState([]);

  const [bankReference, setBankReference] = useState();

  const [modelEditRecipient, setModelEditRecipient] = useState(false);

  const [modelAddRecipient, setModelAddRecipient] = useState(false);

  const [recpientEdit, setRecipientEdit] = useState();

  const [itemSelected, setItemSelected] = useState();

  const [formEdit] = Form.useForm();

  const [formAdd] = Form.useForm();

  const [userId, setUserId] = useState(
    parseJwt(localStorage.App_AccessToken).userId
  );

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

  const appendData = async () => {
    const res = await instance.get(`Customer/${userId}`, {});
    if (res.data.status === 200) {
      setData(res.data.data);
    }
  };

  useEffect(() => {
    appendData();
  }, []);

  useEffect(() => {
    if (!modelAddRecipient) {
      formAdd.resetFields();
    }
    if (!modelEditRecipient) {
      formEdit.resetFields();
    }
  });

  const confirmDelete = async (id) => {
    const res = await instance.delete(`Customer/Recipient/${id}`, {});
    if (res.data.status === 200) {
      success("Delete recipient", res.data.message);
    } else {
      error("Delete recipient", res.data.message);
    }

    appendData();
  };

  const confirmUpdate = async (id, paramsUpdate) => {
    const res = await instance.patch(`Customer/Recipient/${id}`, {
      stk: paramsUpdate.stkEdit,
      name: paramsUpdate.nameEdit,
      bankID: paramsUpdate.bankEdit,
    });
    if (res.data.status === 200) {
      success("Update recipient", res.data.message);
    } else {
      error("Update recipient", res.data.message);
    }

    appendData();
  };

  const confirmAdd = async (userId, paramsAdd) => {
    const res = await instance.post(`Customer/Recipient/AddRecipient`, {
      stk: paramsAdd.stkAdd,
      name: paramsAdd.nameAdd,
      userID: userId,
      bankID: paramsAdd.bankAdd,
    });
    if (res.data.status === 200) {
      success("Add recipient", res.data.message);
    } else {
      error("Add recipient", res.data.message);
    }

    appendData();
  };

  const onFillFormEdit = (item) => {
    formEdit.setFieldsValue({
      stkEdit: `${item.stk}`,
      nameEdit: `${item.name}`,
      bankEdit: item.bank.id,
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
    if (props.parentCallback) {
      props.parentCallback(item);
    }
  };

  return (
    <Row type="flex" justify="center" align="middle">
      <Modal
        title={
          <div style={{ textAlign: "center" }}>
            <h2>Add Recipient</h2>
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
              style={{ minWidth: 70, width: 80 }}
              onClick={() => formAdd.submit()}
            >
              Add
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
            confirmAdd(userId, value);
            formAdd.resetFields();
          }}
        >
          <Form.Item
            {...formItemLayout}
            name="stkAdd"
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
              name="nameAdd"
              style={{ minWidth: 200 }}
            />
          </Form.Item>
          <Form.Item {...formItemLayout} name="nameAdd" label="Nickname">
            <Input
              placeholder="Please inputs nick name"
              name="nameAdd"
              style={{ minWidth: 200 }}
            />
          </Form.Item>
          <Form.Item {...formItemLayout} name="bankAdd" label="Bank">
            <Select style={{ minWidth: 200 }} options={bankReference} />
          </Form.Item>
        </Form>
      </Modal>

      <Modal
        title={
          <div style={{ textAlign: "center" }}>
            <h2>Edit Recipient</h2>
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
              Update
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
            confirmUpdate(recpientEdit, value);
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
      <div>
        {props.isSelect === false && (
          <div style={{ textAlign: "right", marginBottom: 20 }}>
            <Button
              icon={<PlusSquareTwoTone size="large" twoToneColor="#52c41a" />}
              type="primary"
              ghost
              size="large"
              onClick={() => {
                setModelAddRecipient(true);
              }}
            >
              Add Recipient
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
            itemLayout="horizontal"
            renderItem={(item) => (
              <List.Item
                key={item.id}
                onClick={() => {
                  setItemSelected(item.id);
                  sendData(item);
                  //console.log(item.id);
                }}
                style={(() =>
                  item.id === itemSelected
                    ? { background: "#32CD32" }
                    : { background: "#F8F8FF" })()}
              >
                <List.Item.Meta
                  avatar={
                    <Avatar size={"large"} style={{ marginTop: 5 }}>
                      <UserOutlined />
                    </Avatar>
                  }
                  title={
                    <div style={{ marginTop: -22, display: "flow" }}>
                      <div>{item.name}</div>
                      <div>{item.stk}</div>
                    </div>
                  }
                  description={item.bank.name}
                />
                {props.isSelect === false && (
                  <div>
                    <Button
                      icon={<EditTwoTone />}
                      type="ghost"
                      size="large"
                      onClick={() => {
                        setModelEditRecipient(true);
                        onFillFormEdit(item);
                        setRecipientEdit(item.id);
                      }}
                    ></Button>
                    <Popconfirm
                      title={`Are you sure you want to remove  ${item.name} ?`}
                      onConfirm={() => confirmDelete(item.id)}
                    >
                      <Button
                        icon={<DeleteTwoTone twoToneColor="#eb2f96" />}
                        type="ghost"
                        size="large"
                      ></Button>
                    </Popconfirm>
                  </div>
                )}
              </List.Item>
            )}
          />
        </div>
      </div>
    </Row>
  );
};
export default ListRecipient;
