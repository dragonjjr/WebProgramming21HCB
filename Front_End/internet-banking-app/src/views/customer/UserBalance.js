import React, { useEffect, useState } from "react";
import { Row, Form, Input, Card } from "antd";
import { instance, parseJwt } from "../../utils.js";

const formItemLayout = {
  labelCol: {
    span: 12,
  },
  wrapperCol: {
    span: 12,
  },
};

const UserBalance = () => {
  const [data, setData] = useState([]);

  const [userId, setUserId] = useState(
    parseJwt(localStorage.App_AccessToken).userId
  );

  const appendData = async () => {
    const res = await instance.get(`Customer/GetUserBalance/${userId}`, {});
    if (res.data.status === 200) {
      setData(res.data.data);
    }
  };

  useEffect(() => {
    appendData();
  }, []);

  return (
    <Row type="flex" justify="center" align="middle">
      <Card title="User Balance" headStyle={{ textAlign: "center" }}>
        <Form>
          <Form.Item
            {...formItemLayout}
            label="Account number"
            labelAlign="left"
          >
            <strong>{data.stk}</strong>
          </Form.Item>
          <Form.Item {...formItemLayout} label="Balance" labelAlign="left">
            <Input.Password value={data.soDu} />
          </Form.Item>
        </Form>
      </Card>
    </Row>
  );
};
export default UserBalance;
