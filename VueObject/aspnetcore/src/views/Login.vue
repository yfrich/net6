<template>
用户名：<input type="text" v-model="state.loginData.userName"/>
密码：<input type="password" v-model="state.loginData.password"/>
<input type="submit" value="登录" @click="loginSubmit"/>
<ul>
    <li v-for="p in state.processInfos" :key="p.id">
    {{p.id}}
    {{p.name}}
    {{p.workingset}}
    </li>
</ul>
</template>

<script>    
    import axios from 'axios';
    import {reactive,onMounted} from 'vue';
    export default{
        name:"Login",
        setup(){
            const state=reactive({loginData:{},processInfos:[]});
            const loginSubmit=async()=>{
                const payload=state.loginData;
                const resp=await axios.post('https://localhost:7014/Login/Login',payload);
                const data=resp.data;
                if (!data.ok) {
                    alert('登陆失败');
                }
                state.processInfos=data.processInfos;
            }
            return {state,loginSubmit};
        }
    }
</script>

