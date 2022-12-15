import React from "react";
import { BarChartOutlined, UserOutlined } from "@ant-design/icons";
import { Breadcrumb, Layout, Menu } from "antd";
import EmployeeManage from "./EmployeeManage";

// Layout
const { Header, Content, Sider } = Layout;
const headerTitle = ["Admin"].map((key) => ({
  key,
  label: `${key}`,
}));
const menuItems = [
  { key: "1", icon: BarChartOutlined, name: "Dashboard" },
  { key: "2", icon: UserOutlined, name: "Employee" },
].map((item, index) => {
  const key = String(index + 1);
  return {
    key: `${key}`,
    icon: React.createElement(item.icon),
    label: `${item.name}`,
  };
});

// UI
function Admin() {
  return (
    <Layout>
      <Header className="header">
        <div className="logo" />
        <Menu
          theme="dark"
          mode="horizontal"
          defaultSelectedKeys={["Admin"]}
          items={headerTitle}
        />
      </Header>
      <Layout>
        <Sider width={200} className="site-layout-background">
          <Menu
            mode="inline"
            defaultSelectedKeys={["1"]}
            defaultOpenKeys={["sub1"]}
            style={{
              height: "100%",
              borderRight: 0,
            }}
            items={menuItems}
          />
        </Sider>
        <Layout
          style={{
            padding: "0 24px 24px",
          }}
        >
          <Breadcrumb
            style={{
              margin: "16px 0",
            }}
          >
            <Breadcrumb.Item>Home</Breadcrumb.Item>
            <Breadcrumb.Item>List</Breadcrumb.Item>
            <Breadcrumb.Item>App</Breadcrumb.Item>
          </Breadcrumb>
          <Content
            className="site-layout-background"
            style={{
              padding: 24,
              margin: 0,
              minHeight: 280,
            }}
          >
            <EmployeeManage />
          </Content>
        </Layout>
      </Layout>
    </Layout>
  );
}

export default Admin;
