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

function App() {
  const [action, setAction] = useState(null);
  const [editingEmployee, setEditingEmployee] = useState(null);
  const [dataSource, setDataSource] = useState([]);

  useEffect(() => {
    loadTasks();
  }, []);

  const loadTasks = async function (e) {
    const res = await instance.get(`Administrator/GetEmployeeList`);
    setDataSource(res.data.data);
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

  const onAddEmployee = () => {
    // const randomNumber = parseInt(Math.random() * 1000);
    // const newStudent = {
    //   id: randomNumber,
    //   name: "Name " + randomNumber,
    //   email: randomNumber + "@gmail.com",
    //   address: "Address " + randomNumber,
    // };
    // setDataSource((pre) => {
    //   return [...pre, newStudent];
    // });
    setAction(ActionType.Add);
  };
  const onDeleteEmployee = (record) => {
    Modal.confirm({
      title: "Are you sure, you want to delete this employee record?",
      okText: "Yes",
      okType: "danger",
      onOk: () => {
        setDataSource((pre) => {
          return pre.filter((employee) => employee.id !== record.id);
        });
      },
    });
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
            setDataSource((pre) => {
              return pre.map((employee) => {
                if (employee.id === editingEmployee.id) {
                  return editingEmployee;
                } else {
                  return employee;
                }
              });
            });
            resetEditing();
          }}
        >
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

export default App;
