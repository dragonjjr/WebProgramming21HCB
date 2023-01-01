import React, { useEffect, useState } from "react";
import {
  List,
  message,
  Button,
  Row,
  Modal,
  Form,
  Input,
  Popconfirm,
} from "antd";
import {
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

const ListRecipient = () => {
  const [data, setData] = useState([]);

  const [modelEditRecipient, setModelEditRecipient] = useState(false);

  const [modelAddRecipient, setModelAddRecipient] = useState(false);

  const [recpientEdit, setRecipientEdit] = useState();

  const [formEdit] = Form.useForm();

  const [formAdd] = Form.useForm();

  const appendData = async () => {
    const token = parseJwt(localStorage.App_AccessToken);
    const userId = token.userId;

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
    });
    if (res.data.status === 200) {
      success("Update recipient", res.data.message);
    } else {
      error("Update recipient", res.data.message);
    }

    appendData();
  };

  const onFillFormEdit = (item) => {
    formEdit.setFieldsValue({
      stkEdit: `${item.stk}`,
      nameEdit: `${item.name}`,
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
            console.log(value);
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
        </Form>
      </Modal>
      <div>
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
            Add Recipient
          </Button>
        </div>
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
              <List.Item key={item.id}>
                <List.Item.Meta
                  title={
                    <div>
                      <h2>{item.name}</h2>
                    </div>
                  }
                  description={<h3 style={{ marginTop: -5 }}>{item.stk}</h3>}
                />
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
              </List.Item>
            )}
          />
        </div>
      </div>
    </Row>
  );
};
export default ListRecipient;
