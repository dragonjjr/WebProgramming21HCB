import React from "react";
import { Tabs, Row, Card } from "antd";
import DebtList from "./DebtList";

const App = () => (
  <Row type="flex" justify="center" align="middle">
    <Card title="Debt Manage">
      <Tabs
        defaultActiveKey="1"
        items={[
          {
            label: `Remind other people`,
            key: "1",
            children: <DebtList isSelf={true} cantEdit={true}></DebtList>,
          },
          {
            label: `Your debt`,
            key: "2",
            children: <DebtList isSelf={false} cantEdit={false} />,
          },
        ]}
      />
    </Card>
  </Row>
);
export default App;
