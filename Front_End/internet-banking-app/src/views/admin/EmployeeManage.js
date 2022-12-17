//import "./EmployeeManage.css";
import { Button, Table, Modal, Input } from "antd";
import { useEffect, useState } from "react";
import { EditOutlined, DeleteOutlined } from "@ant-design/icons";
import { instance } from "../../utils.js";

const { TextArea } = Input;
const ActionType = {
  Add: "Add",
  Edit: "Edit",
  Delete: "Delete",
};

function EmployeeManage() {
  const [action, setAction] = useState(null);
  const [editingEmployee, setEditingEmployee] = useState(null);
  const [dataSource, setDataSource] = useState([]);

  useEffect(() => {
    loadTasks();
  }, []);

  const loadTasks = async function (e) {
    const res = await instance.get(`Administrator/GetEmployeeList`);
    if (res.data.status === 200) {
      const result = res.data.data.map((row) => ({
        key: row.id,
        name: row.name,
        cmnd: row.cmnd,
        email: row.email,
        bankKind: row.bankKind,
        phone: row.phone,
        soDu: row.soDu,
        stk: row.stk,
        address: row.address,
      }));

      setDataSource(result);
    }
  };

  const columns = [
    {
      key: "1",
      title: "Name",
      dataIndex: "name",
    },
    {
      key: "2",
      title: "ID Card",
      dataIndex: "cmnd",
    },
    {
      key: "3",
      title: "Email",
      dataIndex: "email",
    },
    {
      key: "4",
      title: "Phone",
      dataIndex: "phone",
    },
    {
      key: "5",
      title: "Address",
      dataIndex: "address",
    },
    {
      key: "6",
      title: "Actions",
      render: (record) => {
        return (
          <>
            <EditOutlined
              onClick={() => {
                onEditEmployee(record);
              }}
            />
            <DeleteOutlined
              onClick={() => {
                onDeleteEmployee(record);
              }}
              style={{ color: "red", marginLeft: 12 }}
            />
          </>
        );
      },
    },
  ];
  const executeAction = async () => {
    console.log(action);
    // Add
    if (action === ActionType.Add) {
      const res = await instance.post(`/Administrator/AddNewEmployee`, {
        userName: editingEmployee.userName,
        password: editingEmployee.password,
        infor: {
          name: editingEmployee.name,
          cmnd: editingEmployee.cmnd,
          address: editingEmployee.address,
          email: editingEmployee.email,
          phone: editingEmployee.phone,
        },
      });

      if (res.data.status === 200) {
        console.log(res.data);
        alert("OK");
        loadTasks();
      } else {
        alert("Fail");
      }
    }
    // Update
    else if (action === ActionType.Edit) {
      const res = await instance.patch(
        `/Administrator/UpdateEmployeeInfo/${editingEmployee.key}`,
        {
          name: editingEmployee.name,
          cmnd: editingEmployee.cmnd,
          address: editingEmployee.address,
          email: editingEmployee.email,
          phone: editingEmployee.phone,
        }
      );

      if (res.data.status === 200) {
        alert("OK");
        loadTasks();
      } else {
        alert("Fail");
      }
    }

    // Delete
    else if (action === ActionType.Delete) {
      console.log(editingEmployee);
      const res = await instance.delete(
        `/Administrator/DeleteEmployee/${editingEmployee.key}`
      );

      if (res.data.status === 200) {
        alert("OK");
        loadTasks();
      } else {
        alert("Fail");
      }
    }

    resetEditing();
  };
  const onAddEmployee = () => {
    setAction(ActionType.Add);
  };
  const onDeleteEmployee = (record) => {
    // setAction(ActionType.Delete);
    // setEditingEmployee({ ...record });
    // Modal.confirm({
    //   title: "Are you sure, you want to delete this employee record?",
    //   open: { action } === ActionType.Delete,
    //   okText: "Yes",
    //   okType: "danger",
    //   onCancel: () => {
    //     resetEditing();
    //   },
    //   onOk: () => {
    //     executeAction();
    //   },
    // });
  };
  const onEditEmployee = (record) => {
    setAction(ActionType.Edit);
    setEditingEmployee({ ...record });
  };
  const resetEditing = () => {
    setAction(null);
    setEditingEmployee(null);
  };
  return (
    <div className="App">
      <header className="App-header">
        <Button onClick={onAddEmployee}>Add a new Employee</Button>
        <Table columns={columns} dataSource={dataSource}></Table>
        <Modal
          title={action === ActionType.Edit ? "Edit Employee" : "Add Employee"}
          open={action === ActionType.Edit || action === ActionType.Add}
          okText={action === ActionType.Edit ? "Save" : "Add"}
          onCancel={() => {
            resetEditing();
          }}
          onOk={() => {
            // setDataSource((pre) => {
            //   return pre.map((employee) => {
            //     if (employee.id === editingEmployee.id) {
            //       return editingEmployee;
            //     } else {
            //       return employee;
            //     }
            //   });
            // });
            executeAction();
          }}
        >
          {action === ActionType.Add && (
            <div>
              <label htmlFor="txtUserName">UserName</label>
              <Input
                id="txtUserName"
                placeholder="UserName"
                value={editingEmployee?.userName}
                onChange={(e) => {
                  setEditingEmployee((pre) => {
                    return { ...pre, userName: e.target.value };
                  });
                }}
              />
              <br />
              <br />
              <label htmlFor="txtPassword">Password</label>
              <Input
                id="txtPassword"
                placeholder="Password"
                value={editingEmployee?.password}
                onChange={(e) => {
                  setEditingEmployee((pre) => {
                    return { ...pre, password: e.target.value };
                  });
                }}
              />
              <br />
              <br />
            </div>
          )}
          <label htmlFor="txtName">Name</label>
          <Input
            id="txtName"
            placeholder="Name"
            value={editingEmployee?.name}
            onChange={(e) => {
              setEditingEmployee((pre) => {
                return { ...pre, name: e.target.value };
              });
            }}
          />
          <br />
          <br />
          <label htmlFor="txtCmnd">ID Card</label>
          <Input
            id="txtCmnd"
            placeholder="ID Card"
            value={editingEmployee?.cmnd}
            rules={[
              {
                required: true,
                message: "Please input your username!",
              },
            ]}
            onChange={(e) => {
              setEditingEmployee((pre) => {
                return { ...pre, cmnd: e.target.value };
              });
            }}
          />{" "}
          <br />
          <br />
          <label htmlFor="txtEmail">Email</label>
          <Input
            id="txtEmail"
            placeholder="Email"
            value={editingEmployee?.email}
            onChange={(e) => {
              setEditingEmployee((pre) => {
                return { ...pre, email: e.target.value };
              });
            }}
          />{" "}
          <br />
          <br />
          <label htmlFor="txtPhone">Phone</label>
          <Input
            id="txtPhone"
            placeholder="Phone"
            value={editingEmployee?.phone}
            onChange={(e) => {
              setEditingEmployee((pre) => {
                return { ...pre, phone: e.target.value };
              });
            }}
          />{" "}
          <br />
          <br />
          <label htmlFor="txtAddress">Address</label>
          <TextArea
            id="txtAddress"
            placeholder="Address"
            value={editingEmployee?.address}
            onChange={(e) => {
              setEditingEmployee((pre) => {
                return { ...pre, address: e.target.value };
              });
            }}
          />
        </Modal>
      </header>
    </div>
  );
}

export default EmployeeManage;
