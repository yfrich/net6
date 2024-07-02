<template>
  <div>
    消息
    <input type="text" v-model="state.userMsg" v-on:keypress="txtMsgOnkeypress" />
  </div>
  <div>
    目标用户名:
    <input type="text" v-model="state.toUserName" v-on:keypress="txtMsgOnkeypress" />
    消息：<input type="text" v-model="state.privateMsg" v-on:keypress="txtPrivateMsgOnkeypress" />
  </div>
  <div>
    <div>
      用户名：<input type="text" v-model="state.username" /> 密码：<input
        type="password"
        v-model="state.password"
      />
      <button v-on:click="login">登录</button>
    </div>
  </div>
  <div>
    <input type="button" value="导入" v-on:click="importECDict" />
    <progress :value="state.improtedCount" :max="state.totalCount"></progress>
  </div>
  <div>
    公平信息：
    <ul>
      <li v-for="(msg, index) in state.messages" :key="index">
        {{ msg }}
      </li>
    </ul>
  </div>
  <div>
    私聊信息：
    <ul>
      <li v-for="(msg, index) in state.privatemessages" :key="index">
        {{ msg }}
      </li>
    </ul>
  </div>
</template>

<script>
import { reactive, onMounted } from 'vue';
import * as signalR from '@microsoft/signalr';
import axios from 'axios';
let connection;
let improtConnection;
export default {
  name: 'Login',
  setup() {
    //定义变量

    const state = reactive({
      userMsg: '',
      messages: [],
      privatemessages: [],
      username: '',
      password: '',
      token: '',
      toUserName: '',
      privateMsg: '',
      improtedCount: 0,
      totalCount: 0,
    });
    const login = async function () {
      let payload = {
        userName: state.username,
        password: state.password,
      };
      axios
        .post('https://localhost:7244/api/JWTVersion/Login', payload)
        .then(async (res) => {
          alert('登陆成功！！');
          const token = res.data;
          var options = { skipNegotiation: true, transport: signalR.HttpTransportType.WebSockets };
          options.accessTokenFactory = () => token;
          //建立连接
          connection = new signalR.HubConnectionBuilder()
            //跳过协议协商
            .withUrl('https://localhost:7244/Hubs/ChartRoomHub', options)
            .withAutomaticReconnect()
            .build();
          //启动
          await connection.start();
          //监听服务端发送过来的消息
          connection.on('ReceivePublicMessage', (rcvMsg) => {
            state.messages.push(rcvMsg);
          });
          //监听私聊消息
          connection.on('PrivateMsgRecevied', (fromUser, msg) => {
            state.privatemessages.push(fromUser + ':' + msg);
          });
          //建立连接
          improtConnection = new signalR.HubConnectionBuilder()
            //跳过协议协商
            .withUrl('https://localhost:7244/Hubs/ImportDictHub', options)
            .withAutomaticReconnect()
            .build();
          //启动
          await improtConnection.start();
          //监听服务端发送过来的消息
          improtConnection.on('ImportProgress', (totalCount, importCount) => {
            state.improtedCount = importCount;
            state.totalCount = totalCount;
          });
        })
        .catch((err) => {});
    };
    //公平发送
    const txtMsgOnkeypress = async function (e) {
      if (e.keyCode != 13) return;
      //客户端向服务器端发请求
      await connection.invoke('SendPublicMessage', state.userMsg);
      state.userMsg = '';
    };
    //私发
    const txtPrivateMsgOnkeypress = async function (e) {
      if (e.keyCode != 13) return;
      //客户端向服务器端发请求
      await connection.invoke('SendPrivateMessage', state.toUserName, state.privateMsg);
      state.privateMsg = '';
    };
    const importECDict = async function () {
      await improtConnection.invoke('ImportEcDict');
    };
    onMounted(async function () {
      /*
      var options={skipNegotiation:true,transport:signalR.HttpTransportType.WebSockets};
      options.accessTokenFactory=()=>state.token;
      //建立连接
      connection=new signalR.HubConnectionBuilder()
      // .withUrl("https://localhost:7244/Hubs/ChartRoomHub")
      //跳过协议协商
      .withUrl("https://localhost:7244/Hubs/ChartRoomHub",options)
      .withAutomaticReconnect().build();
      //启动
      await connection.start();
      //监听服务端发送过来的消息
      connection.on("ReceivePublicMessage",rcvMsg=>{
        state.messages.push(rcvMsg);
      });
      */
    });
    return { state, txtMsgOnkeypress, login, txtPrivateMsgOnkeypress, importECDict };
  },
};
</script>
