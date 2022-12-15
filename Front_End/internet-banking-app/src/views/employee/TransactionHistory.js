import React, { useEffect, useState } from "react";
import { List, Row, Input, Spin } from "antd";
import { instance } from "../../utils.js";
import moment from "moment/moment.js";
const { Search } = Input;

function TransactionHistory() {
  const [query, setQuery] = useState("");

  const [loading, setLoading] = useState(false);

  const [data, setData] = useState([]);

  const loadMoreData = async (accountNumber) => {
    if (loading) {
      return;
    }
    setLoading(true);

    const res1 = await instance.get(
      `InternalTransfer/GetListTransactionByAcount`,
      {
        params: {
          accountNumber: accountNumber,
        },
      }
    );

    const res2 = await instance.get(
      `InternalTransfer/GetListTransactionByAcount`,
      {
        params: {
          accountNumber: accountNumber,
          typeTransaction: "1",
        },
      }
    );

    setData(
      [...res1.data.data, ...res2.data.data].sort((a, b) => {
        return new Date(b.transDate) - new Date(a.transDate);
      })
    );

    setLoading(false);
  };

  const onSearch = function (value) {
    setQuery(value);
    loadMoreData(value);
  };

  return (
    <Row type="flex" justify="center" align="middle">
      <div
        id="scrollableDiv"
        style={{
          width: 700,
          height: 550,
          overflow: "auto",
          background: "#F8F8FF",
        }}
      >
        <Spin spinning={loading}>
          <List
            dataSource={data}
            header={
              <Search
                placeholder="Account number"
                allowClear
                enterButton="Search"
                size="large"
                onSearch={onSearch}
              />
            }
            bordered
            renderItem={(item) => (
              <List.Item key={item.id}>
                <List.Item.Meta
                  title={
                    <h3>
                      {moment(item.transDate).format("HH:mm:ss DD-MM-YYYY")}
                    </h3>
                  }
                  description={
                    <h3>
                      Tranfer from {item.stkSend} to {item.stkReceive}
                      <p>{item.content}</p>
                    </h3>
                  }
                />
                <h1
                  style={(() => {
                    if (item.isDebtRemind === true) {
                      return {
                        minWidth: 100,
                        textAlign: "center",
                        color: "#304FFE",
                      };
                    } else {
                      if (item.stkSend === query) {
                        return {
                          minWidth: 100,
                          textAlign: "center",
                          color: "#FF1744",
                        };
                      } else {
                        return {
                          minWidth: 100,
                          textAlign: "center",
                          color: "#00C853",
                        };
                      }
                    }
                  })()}
                >
                  {item.stkSend === query ? -item.money : "+" + item.money}
                </h1>
              </List.Item>
            )}
          />
        </Spin>
      </div>
    </Row>
  );
}
export default TransactionHistory;
