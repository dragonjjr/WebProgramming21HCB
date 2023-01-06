import React, { useEffect, useState } from "react";
import { List, Row, Spin, Card } from "antd";
import { instance, parseJwt } from "../../utils.js";
import moment from "moment/moment.js";

function Transaction() {
  const [userId] = useState(parseJwt(localStorage.App_AccessToken).userId);

  const [loading, setLoading] = useState(false);

  const [data, setData] = useState([]);

  const loadMoreData = async () => {
    if (loading) {
      return;
    }
    setLoading(true);

    try {
      const res = await instance.get(`Customer/GetUserBalance/${userId}`, {});
      if (res.data.status === 200) {
        const res1 = await instance.get(
          `InternalTransfer/GetListTransactionByAcount`,
          {
            params: {
              accountNumber: res.data.data.stk,
            },
          }
        );

        const res2 = await instance.get(
          `InternalTransfer/GetListTransactionByAcount`,
          {
            params: {
              accountNumber: res.data.data.stk,
              typeTransaction: "1",
            },
          }
        );

        setData(
          [...res1.data.data, ...res2.data.data].sort((a, b) => {
            return new Date(b.transDate) - new Date(a.transDate);
          })
        );
      }
    } catch {
      setLoading(false);
    }

    setLoading(false);
  };

  useEffect(() => {
    loadMoreData();
  }, []);

  return (
    <Row type="flex" justify="center" align="middle">
      <Card title="Transaction History">
        <div
          id="scrollableDiv"
          style={{
            width: 450,
            height: 550,
            overflow: "auto",
            background: "#F8F8FF",
          }}
        >
          <Spin spinning={loading}>
            <List
              dataSource={data}
              bordered
              renderItem={(item) => (
                <List.Item
                  key={item.id}
                  title={item.transactionType}
                  style={{ padding: 0 }}
                >
                  <List.Item.Meta
                    avatar={
                      item.transactionType === "Chuyển nội bộ" ? (
                        <div
                          style={{
                            padding: 2,
                            width: 150,
                            height: 20,
                            background: "cyan",
                            position: "absolute",
                            textAlign: "Left",
                          }}
                        >
                          {item.transactionType}
                        </div>
                      ) : (
                        <div
                          style={{
                            padding: 2,
                            width: 150,
                            height: 20,
                            background: "orange",
                            position: "absolute",
                            textAlign: "Left",
                          }}
                        >
                          {item.transactionType}
                        </div>
                      )
                    }
                    title={
                      <div style={{ paddingTop: 2 }}>
                        <h5>
                          {moment(item.transDate).format("HH:mm:ss DD-MM-YYYY")}
                        </h5>
                      </div>
                    }
                    description={
                      <div style={{ marginTop: -10 }}>
                        <h5>
                          Tranfer from {item.stkSend} to {item.stkReceive}
                          <p>{item.content}</p>
                        </h5>
                      </div>
                    }
                  />
                  <h4
                    style={(() => {
                      if (item.isDebtRemind === true) {
                        return {
                          minWidth: 100,
                          textAlign: "center",
                          color: "#304FFE",
                        };
                      } else {
                        if (item.stkSend === userId) {
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
                    {item.stkSend === userId ? -item.money : "+" + item.money}
                  </h4>
                </List.Item>
              )}
            />
          </Spin>
        </div>
      </Card>
    </Row>
  );
}
export default Transaction;
