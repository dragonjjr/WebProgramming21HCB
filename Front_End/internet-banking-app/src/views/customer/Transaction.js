import React, { useEffect, useState } from "react";
import { List, Row, Spin } from "antd";
import { instance, parseJwt } from "../../utils.js";
import moment from "moment/moment.js";

function Transaction() {
  const [userId, setUserId] = useState();

  const [loading, setLoading] = useState(false);

  const [data, setData] = useState([]);

  useEffect(() => {
    setUserId(parseJwt(localStorage.App_AccessToken).userId);
  }, []);

  const loadMoreData = async (accountNumber) => {
    if (loading) {
      return;
    }
    setLoading(true);

    try {
      const res1 = await instance.get(
        `InternalTransfer/GetListTransactionByAcount`,
        {
          params: {
            accountNumber: userId,
          },
        }
      );

      const res2 = await instance.get(
        `InternalTransfer/GetListTransactionByAcount`,
        {
          params: {
            accountNumber: userId,
            typeTransaction: "1",
          },
        }
      );

      setData(
        [...res1.data.data, ...res2.data.data].sort((a, b) => {
          return new Date(b.transDate) - new Date(a.transDate);
        })
      );
    } catch {
      setLoading(false);
    }

    setLoading(false);
  };

  useEffect(() => {
    loadMoreData();
  }, [userId]);

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
                </h1>
              </List.Item>
            )}
          />
        </Spin>
      </div>
    </Row>
  );
}
export default Transaction;
