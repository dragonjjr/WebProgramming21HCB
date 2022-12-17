import { Space, Table, Tag } from "antd";
import { useEffect, useState } from "react";
import { EditOutlined, DeleteOutlined } from "@ant-design/icons";
import { instance } from "../../utils.js";
import "../../Assets/CSS/Account.css";

const columns = [
  {
    title: "Account send",
    dataIndex: "stkSend",
    key: "stkSend",
  },
  {
    title: "Account receive",
    dataIndex: "stkReceive",
    key: "stkReceive",
  },
  {
    title: "Money",
    dataIndex: "money",
    key: "money",
  },
  {
    title: "Content",
    dataIndex: "content",
    key: "content",
  },
  {
    title: "Payment Fee Type",
    dataIndex: "paymentFeeType",
    key: "paymentFeeType",
  },
  {
    title: "Bank Reference",
    dataIndex: "bankReference",
    key: "bankReference",
  },
  {
    title: "Transaction date",
    dataIndex: "transDate",
    key: "transDate",
  },
];
function DashBoard() {
  const [dataSource, setDataSource] = useState([]);

  useEffect(() => {
    loadDatas();
  }, []);

  const loadDatas = async function (e) {
    const res = await instance.get(`Administrator/ViewTransaction`);
    if (res.data.status === 200) {
      const result = res.data.data.map((row) => ({
        key: row.id,
        stkSend: row.stkSend,
        stkReceive: row.stkReceive,
        money: row.money,
        content: row.content,
        paymentFeeType: row.paymentFeeType,
        bankReference: row.bankReference,
        transDate: row.transDate,
      }));

      setDataSource(result);
    }
  };

  return <Table columns={columns} dataSource={dataSource} />;
}

export default DashBoard;
