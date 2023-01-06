import React, { useState } from "react";
import {
  AppstoreOutlined,
  MailOutlined,
  SettingOutlined,
} from "@ant-design/icons";
import { Menu } from "antd";
import { Link, useNavigate } from "react-router-dom";

const menuItems = [
  {
    key: "Home",
    icon: (
      <div>
        <Link to="/">Home</Link>
      </div>
    ),
  },

  {
    key: "logout",

    icon: (
      <div>
        <Link to="/logout">Logout</Link>
      </div>
    ),
  },
];
const App = () => {
  const [current, setCurrent] = useState("home");
  const onClick = (e) => {
    setCurrent(e.key);
  };
  return (
    <Menu
      selectedKeys={current}
      onClick={onClick}
      mode="horizontal"
      style={{ justifyContent: "flex-end", background: "#20B2AA" }}
      items={menuItems}
    />
  );
};
export default App;
