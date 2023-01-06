import React from "react";
import UserBalance from "./UserBalance";
import {
  List,
  message,
  Button,
  Row,
  Modal,
  Form,
  Input,
  Card,
  Col,
} from "antd";
import { Link, useNavigate } from "react-router-dom";
import Menu from "../../components/Menu";

function Customer(props) {
  const navigate = useNavigate();
  return (
    <>
      <Menu></Menu>
      <Row
        type="flex"
        justify="center"
        align="middle"
        style={{ marginTop: 70 }}
      >
        <Card>
          <UserBalance />
          <Row style={{ marginTop: 20 }}>
            <Col span={11}>
              <Button
                block
                type="primary"
                onClick={() => navigate("/debtmanage")}
              >
                Debt Manage
              </Button>
            </Col>
            <Col span={2}></Col>
            <Col span={11}>
              <Button
                block
                type="primary"
                onClick={() => navigate("/recipientmanage")}
              >
                Recipient Manage
              </Button>
            </Col>
          </Row>
          <Row style={{ marginTop: 15 }}>
            <Col span={11}>
              <Button
                block
                type="primary"
                onClick={() => navigate("/InternalTranfer")}
              >
                Internal Tranfer
              </Button>
            </Col>
            <Col span={2}></Col>
            <Col span={11}>
              <Button
                block
                type="primary"
                onClick={() => navigate("/ExternalTranfer")}
              >
                External Tranfer
              </Button>
            </Col>
          </Row>
          <Row style={{ marginTop: 15 }}>
            <Col span={11}>
              <Button
                block
                type="primary"
                onClick={() => navigate("/TransactionHistory")}
              >
                Transaction History
              </Button>
            </Col>
            <Col span={2}></Col>
            <Col span={11}></Col>
          </Row>
        </Card>
      </Row>
    </>
  );
}

export default Customer;
